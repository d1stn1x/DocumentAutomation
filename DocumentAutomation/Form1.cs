using DocumentAutomation.Services;
using DocumentAutomation.Models;
using Microsoft.Extensions.Configuration;

namespace DocumentAutomation;

public partial class MainForm : Form
{
    private Dictionary<string, string> templateVariables = new Dictionary<string, string>();
    private string currentTemplate = string.Empty;
    private DatabaseService? _databaseService;
    private List<DocumentTemplate> _templates = new List<DocumentTemplate>();
    private DocumentTemplate? _selectedTemplate;

    public MainForm()
    {
        InitializeComponent();
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        try
        {
            // Load configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show(
                    "Строка подключения к базе данных не найдена в appsettings.json",
                    "Ошибка конфигурации",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _databaseService = new DatabaseService(connectionString);

            // Initialize database
            await _databaseService.InitializeDatabaseAsync();
            
            // Seed sample templates if database is empty
            await _databaseService.SeedSampleTemplatesAsync();

            // Load templates from database
            await LoadTemplatesAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Ошибка при инициализации базы данных:\n{ex.Message}\n\nПроверьте настройки подключения в appsettings.json",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private async Task LoadTemplatesAsync()
    {
        if (_databaseService == null) return;

        try
        {
            listBoxTemplates.Items.Clear();
            _templates = await _databaseService.GetAllTemplatesAsync();

            foreach (var template in _templates)
            {
                listBoxTemplates.Items.Add(template.Name);
            }

            if (listBoxTemplates.Items.Count > 0)
            {
                listBoxTemplates.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки шаблонов: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void listBoxTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listBoxTemplates.SelectedItem != null && listBoxTemplates.SelectedIndex >= 0)
        {
            _selectedTemplate = _templates[listBoxTemplates.SelectedIndex];
            currentTemplate = _selectedTemplate.Content;
            ExtractVariablesFromTemplate(currentTemplate);
            txtOutput.Clear();
            btnSaveDocument.Enabled = false;
        }
    }

    private void ExtractVariablesFromTemplate(string template)
    {
        templateVariables.Clear();
        tableLayoutVariables.Controls.Clear();
        tableLayoutVariables.RowStyles.Clear();
        tableLayoutVariables.RowCount = 0;

        // Find all variables in format {{VARIABLE_NAME}}
        var matches = System.Text.RegularExpressions.Regex.Matches(template, @"\{\{([^}]+)\}\}");
        var uniqueVariables = new HashSet<string>();

        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            uniqueVariables.Add(match.Groups[1].Value);
        }

        int row = 0;
        foreach (var variable in uniqueVariables.OrderBy(v => v))
        {
            templateVariables[variable] = string.Empty;

            // Create label
            Label label = new Label
            {
                Text = variable + ":",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Padding = new Padding(5)
            };

            // Create textbox
            TextBox textBox = new TextBox
            {
                Name = "txt_" + variable,
                Dock = DockStyle.Fill,
                Tag = variable
            };

            // Add default values for date
            if (variable.ToUpper().Contains("DATE") || variable.ToUpper().Contains("ДАТА"))
            {
                textBox.Text = DateTime.Now.ToString("dd.MM.yyyy");
            }

            tableLayoutVariables.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutVariables.Controls.Add(label, 0, row);
            tableLayoutVariables.Controls.Add(textBox, 1, row);
            
            row++;
            tableLayoutVariables.RowCount = row;
        }
    }

    private async void btnGenerateDocument_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(currentTemplate))
        {
            MessageBox.Show("Пожалуйста, выберите шаблон.", "Предупреждение", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Collect values from input fields
        string generatedDocument = currentTemplate;
        
        foreach (Control control in tableLayoutVariables.Controls)
        {
            if (control is TextBox textBox && textBox.Tag != null)
            {
                string variable = textBox.Tag.ToString()!;
                string value = textBox.Text;
                generatedDocument = generatedDocument.Replace("{{" + variable + "}}", value);
            }
        }

        txtOutput.Text = generatedDocument;
        btnSaveDocument.Enabled = true;

        // Save to database
        if (_databaseService != null && _selectedTemplate != null)
        {
            try
            {
                var document = new GeneratedDocument
                {
                    DocumentName = $"{_selectedTemplate.Name}_{DateTime.Now:yyyyMMdd_HHmmss}",
                    Content = generatedDocument,
                    TemplateId = _selectedTemplate.Id
                };

                await _databaseService.SaveGeneratedDocumentAsync(document);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения документа в БД: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void btnSaveDocument_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtOutput.Text))
        {
            MessageBox.Show("Нет документа для сохранения.", "Предупреждение", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        SaveFileDialog saveDialog = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            DefaultExt = "txt",
            FileName = "Документ_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt"
        };

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllText(saveDialog.FileName, txtOutput.Text);
            MessageBox.Show("Документ успешно сохранен!", "Успех", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private async void btnRefreshTemplates_Click(object sender, EventArgs e)
    {
        await LoadTemplatesAsync();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void manageTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (_databaseService == null)
        {
            MessageBox.Show("База данных не инициализирована", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var templateManagementForm = new TemplateManagementForm(_databaseService);
        templateManagementForm.ShowDialog();
        
        // Reload templates after management form closes
        _ = LoadTemplatesAsync();
    }

    private void manageCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (_databaseService == null)
        {
            MessageBox.Show("База данных не инициализирована", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var categoryManagementForm = new CategoryManagementForm(_databaseService);
        categoryManagementForm.ShowDialog();
    }

    private async void viewHistoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (_databaseService == null)
        {
            MessageBox.Show("База данных не инициализирована", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var historyForm = new DocumentHistoryForm(_databaseService);
        historyForm.ShowDialog();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show(
            "Система автоматизации создания документов\n\n" +
            "Версия 3.0 (Enhanced UI & Database)\n\n" +
            "Возможности:\n" +
            "• Управление шаблонами документов\n" +
            "• Организация по категориям\n" +
            "• История документов\n" +
            "• MS SQL Server база данных\n\n" +
            "Шаблоны и документы хранятся в базе данных.",
            "О программе",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}
