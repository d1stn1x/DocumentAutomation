using DocumentAutomation.Services;
using DocumentAutomation.Models;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentAutomation;

public partial class Form1 : Form
{
    private Dictionary<string, string> _templateVariables = new Dictionary<string, string>();
    private string _currentTemplate = string.Empty;
    private DatabaseService? _databaseService;
    private List<DocumentTemplate> _templates = new List<DocumentTemplate>();
    private DocumentTemplate? _selectedTemplate;
    private readonly ToolTip _toolTip = new ToolTip();

    // Компоненты интерфейса
    private GroupBox _grpTemplates;
    private ComboBox _cmbTemplates;
    private Button _btnRefreshTemplates;

    private GroupBox _grpInput;
    private TableLayoutPanel _tableLayoutVariables;
    private Panel _panelScroll;

    private GroupBox _grpOutput;
    private Button _btnGenerateDocument;
    private Button _btnSaveDocument;
    private TextBox _txtOutput;
    private Label _lblStatus;

    // Dictionary for converting technical variable names to user-friendly Russian labels
    private readonly Dictionary<string, string> _variableLabels = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        // Contract/Agreement variables
        ["CONTRACT_NUMBER"] = "Номер договора",
        ["CONTRACT_SUBJECT"] = "Предмет договора",
        ["COMPANY_NAME"] = "Название компании",
        ["CLIENT_NAME"] = "Имя клиента",
        ["DEADLINE"] = "Срок выполнения",
        ["PRICE"] = "Цена",
        ["DATE"] = "Дата",
        ["ДАТА"] = "Дата",

        // Invoice variables
        ["INVOICE_NUMBER"] = "Номер счета",
        ["INN"] = "ИНН",
        ["COMPANY_INN"] = "ИНН компании",
        ["SERVICE_NAME"] = "Наименование услуги/товара",
        ["QUANTITY"] = "Количество",
        ["UNIT_PRICE"] = "Цена за единицу",
        ["TOTAL_PRICE"] = "Общая сумма",

        // Letter variables
        ["SENDER_NAME"] = "Имя отправителя",
        ["SENDER_ADDRESS"] = "Адрес отправителя",
        ["SENDER_POSITION"] = "Должность отправителя",
        ["RECIPIENT_NAME"] = "Имя получателя",
        ["LETTER_BODY"] = "Текст письма"
    };

    public Form1()
    {
        InitializeComponent();
        this.Load += Form1_Load;
        SetupCustomUI();
    }

    private void SetupCustomUI()
    {
        this.Text = "Автоматизация создания документов";
        this.Size = new Size(900, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.MinimumSize = new Size(800, 600);

        // Создаем MenuStrip
        var menuStrip = new MenuStrip();

        var fileMenu = new ToolStripMenuItem("Файл");
        var exitItem = new ToolStripMenuItem("Выход");
        exitItem.Click += (s, e) => Application.Exit();
        fileMenu.DropDownItems.Add(exitItem);

        var templatesMenu = new ToolStripMenuItem("Шаблоны");
        var manageTemplatesItem = new ToolStripMenuItem("Управление шаблонами");
        manageTemplatesItem.Click += ManageTemplatesToolStripMenuItem_Click;
        var manageCategoriesItem = new ToolStripMenuItem("Управление категориями");
        manageCategoriesItem.Click += ManageCategoriesToolStripMenuItem_Click;
        templatesMenu.DropDownItems.AddRange(new ToolStripItem[] { manageTemplatesItem, manageCategoriesItem });

        var historyMenu = new ToolStripMenuItem("История");
        var viewHistoryItem = new ToolStripMenuItem("Просмотр истории");
        viewHistoryItem.Click += ViewHistoryToolStripMenuItem_Click;
        historyMenu.DropDownItems.Add(viewHistoryItem);

        var helpMenu = new ToolStripMenuItem("Справка");
        var aboutItem = new ToolStripMenuItem("О программе");
        aboutItem.Click += AboutToolStripMenuItem_Click;
        helpMenu.DropDownItems.Add(aboutItem);

        menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, templatesMenu, historyMenu, helpMenu });

        // ========== ОБЛАСТЬ 1: Шаблоны ==========
        _grpTemplates = new GroupBox
        {
            Text = "Шаблоны документов",
            Location = new Point(10, 30),
            Size = new Size(860, 70),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };

        _cmbTemplates = new ComboBox
        {
            Location = new Point(10, 25),
            Size = new Size(700, 25),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        _cmbTemplates.SelectedIndexChanged += CmbTemplates_SelectedIndexChanged;

        _btnRefreshTemplates = new Button
        {
            Text = "Обновить список",
            Location = new Point(720, 23),
            Size = new Size(120, 25),
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            BackColor = Color.LightBlue,
            FlatStyle = FlatStyle.Flat,
            UseVisualStyleBackColor = false
        };
        _btnRefreshTemplates.Click += BtnRefreshTemplates_Click;

        _grpTemplates.Controls.AddRange(new Control[] { _cmbTemplates, _btnRefreshTemplates });

        // ========== ОБЛАСТЬ 2: Ввод данных ==========
        _grpInput = new GroupBox
        {
            Text = "Заполните данные",
            Location = new Point(10, 110),
            Size = new Size(860, 300),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };

        _panelScroll = new Panel
        {
            Location = new Point(10, 20),
            Size = new Size(840, 270),
            AutoScroll = true,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            BorderStyle = BorderStyle.FixedSingle
        };

        _tableLayoutVariables = new TableLayoutPanel
        {
            Location = new Point(0, 0),
            Width = 820,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ColumnCount = 2,
            Padding = new Padding(5),
            BackColor = Color.WhiteSmoke
        };

        _tableLayoutVariables.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        _tableLayoutVariables.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

        _panelScroll.Controls.Add(_tableLayoutVariables);
        _grpInput.Controls.Add(_panelScroll);

        // ========== ОБЛАСТЬ 3: Генерация и результат ==========
        _grpOutput = new GroupBox
        {
            Text = "Сгенерированный документ",
            Location = new Point(10, 420),
            Size = new Size(860, 220),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
        };

        _btnGenerateDocument = new Button
        {
            Text = "Сгенерировать документ",
            Location = new Point(10, 20),
            Size = new Size(160, 30),
            BackColor = Color.LightGreen,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            UseVisualStyleBackColor = false
        };
        _btnGenerateDocument.Click += BtnGenerateDocument_Click;

        _btnSaveDocument = new Button
        {
            Text = "Сохранить документ",
            Location = new Point(180, 20),
            Size = new Size(160, 30),
            BackColor = Color.LightBlue,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            UseVisualStyleBackColor = false,
            Enabled = false
        };
        _btnSaveDocument.Click += BtnSaveDocument_Click;

        _lblStatus = new Label
        {
            Text = "Выберите шаблон для начала работы",
            Location = new Point(10, 60),
            Width = 840,
            Height = 30,
            TextAlign = ContentAlignment.MiddleLeft,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.LightYellow,
            Font = new Font("Segoe UI", 9F, FontStyle.Italic)
        };

        _txtOutput = new TextBox
        {
            Location = new Point(10, 95),
            Width = 840,
            Height = 110,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            ReadOnly = true,
            BackColor = Color.White,
            Font = new Font("Consolas", 10F, FontStyle.Regular)
        };

        _grpOutput.Controls.AddRange(new Control[] {
            _btnGenerateDocument, _btnSaveDocument, _lblStatus, _txtOutput
        });

        // Добавляем все на форму
        this.Controls.Add(menuStrip);
        this.Controls.AddRange(new Control[] {
            _grpTemplates, _grpInput, _grpOutput
        });

        this.MainMenuStrip = menuStrip;
    }

    private string GetFriendlyLabel(string variableName)
    {
        if (_variableLabels.TryGetValue(variableName, out string? friendlyLabel))
        {
            return friendlyLabel;
        }

        return variableName.Replace("_", " ").ToLower();
    }

    private async void Form1_Load(object sender, EventArgs e)
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

            _lblStatus.Text = "Готов к работе. Выберите шаблон.";
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
            _cmbTemplates.Items.Clear();
            _templates = await _databaseService.GetAllTemplatesAsync();

            foreach (var template in _templates)
            {
                _cmbTemplates.Items.Add(template.Name);
            }

            if (_cmbTemplates.Items.Count > 0)
            {
                _cmbTemplates.SelectedIndex = 0;
            }
            else
            {
                _lblStatus.Text = "Нет доступных шаблонов. Создайте новый шаблон.";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки шаблонов: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CmbTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_cmbTemplates.SelectedItem != null && _cmbTemplates.SelectedIndex >= 0 && _cmbTemplates.SelectedIndex < _templates.Count)
        {
            _selectedTemplate = _templates[_cmbTemplates.SelectedIndex];
            _currentTemplate = _selectedTemplate.Content;
            ExtractVariablesFromTemplate(_currentTemplate);
            _txtOutput.Clear();
            _btnSaveDocument.Enabled = false;

            _grpInput.Text = $"Заполните данные для шаблона: {_selectedTemplate.Name}";
            _lblStatus.Text = $"Выбран шаблон: {_selectedTemplate.Name}. Заполните поля и нажмите 'Сгенерировать'.";
        }
    }

    private void ExtractVariablesFromTemplate(string template)
    {
        _templateVariables.Clear();
        _tableLayoutVariables.Controls.Clear();
        _tableLayoutVariables.RowStyles.Clear();
        _tableLayoutVariables.RowCount = 0;

        // Find all variables in format {{VARIABLE_NAME}}
        var matches = Regex.Matches(template, @"\{\{([^}]+)\}\}");
        var uniqueVariables = new HashSet<string>();

        foreach (Match match in matches)
        {
            uniqueVariables.Add(match.Groups[1].Value);
        }

        if (uniqueVariables.Count == 0)
        {
            Label lblNoVars = new Label
            {
                Text = "В этом шаблоне нет переменных для заполнения",
                AutoSize = true,
                Padding = new Padding(10),
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 10F, FontStyle.Italic)
            };
            _tableLayoutVariables.Controls.Add(lblNoVars, 0, 0);
            _tableLayoutVariables.SetColumnSpan(lblNoVars, 2);
            return;
        }

        int row = 0;
        foreach (var variable in uniqueVariables.OrderBy(v => v))
        {
            _templateVariables[variable] = string.Empty;

            string friendlyLabel = GetFriendlyLabel(variable);

            Label label = new Label
            {
                Text = friendlyLabel + ":",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Padding = new Padding(5, 8, 5, 5),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _toolTip.SetToolTip(label, $"Переменная: {{{{{variable}}}}}");

            TextBox textBox = new TextBox
            {
                Name = "txt_" + variable,
                Dock = DockStyle.Fill,
                Tag = variable,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Margin = new Padding(5, 5, 10, 5)
            };

            _toolTip.SetToolTip(textBox, $"Переменная: {{{{{variable}}}}}");

            if (variable.ToUpper().Contains("DATE") || variable.ToUpper().Contains("ДАТА"))
            {
                textBox.Text = DateTime.Now.ToString("dd.MM.yyyy");
            }

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = $"Введите {friendlyLabel.ToLower()}";
                textBox.ForeColor = Color.Gray;

                textBox.Enter += (s, eArgs) => {
                    if (textBox.ForeColor == Color.Gray)
                    {
                        textBox.Text = "";
                        textBox.ForeColor = Color.Black;
                    }
                };

                textBox.Leave += (s, eArgs) => {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        textBox.Text = $"Введите {friendlyLabel.ToLower()}";
                        textBox.ForeColor = Color.Gray;
                    }
                };
            }

            _tableLayoutVariables.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _tableLayoutVariables.Controls.Add(label, 0, row);
            _tableLayoutVariables.Controls.Add(textBox, 1, row);

            row++;
            _tableLayoutVariables.RowCount = row;
        }

        _tableLayoutVariables.Height = (row * 35) + 10;
    }

    private async void BtnGenerateDocument_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_currentTemplate))
        {
            MessageBox.Show("Пожалуйста, выберите шаблон.", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string generatedDocument = _currentTemplate;

        foreach (Control control in _tableLayoutVariables.Controls)
        {
            if (control is TextBox textBox && textBox.Tag != null)
            {
                string variable = textBox.Tag.ToString()!;
                string value = textBox.Text;

                if (value.StartsWith("Введите ") && textBox.ForeColor == Color.Gray)
                {
                    value = "";
                }

                generatedDocument = generatedDocument.Replace("{{" + variable + "}}", value);
            }
        }

        _txtOutput.Text = generatedDocument;
        _btnSaveDocument.Enabled = true;
        _lblStatus.Text = "Документ сгенерирован. Нажмите 'Сохранить' для сохранения в файл.";

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
                _lblStatus.Text = "Документ сгенерирован и сохранен в истории.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения документа в БД: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _lblStatus.Text = "Документ сгенерирован, но не сохранен в БД.";
            }
        }
    }

    private void BtnSaveDocument_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_txtOutput.Text))
        {
            MessageBox.Show("Нет документа для сохранения.", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        SaveFileDialog saveDialog = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt|RTF files (*.rtf)|*.rtf|All files (*.*)|*.*",
            DefaultExt = "txt",
            FileName = "Документ_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt"
        };

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                File.WriteAllText(saveDialog.FileName, _txtOutput.Text);
                MessageBox.Show($"Документ успешно сохранен:\n{saveDialog.FileName}", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _lblStatus.Text = $"Документ сохранен: {Path.GetFileName(saveDialog.FileName)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private async void BtnRefreshTemplates_Click(object sender, EventArgs e)
    {
        await LoadTemplatesAsync();
        _lblStatus.Text = "Список шаблонов обновлен";
    }

    private void ManageTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (_databaseService == null)
        {
            MessageBox.Show("База данных не инициализирована", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        MessageBox.Show("Функция управления шаблонами будет доступна в следующей версии",
            "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

        _ = LoadTemplatesAsync();
    }

    private void ManageCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (_databaseService == null)
        {
            MessageBox.Show("База данных не инициализирована", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        MessageBox.Show("Функция управления категориями будет доступна в следующей версии",
            "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async void ViewHistoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (_databaseService == null)
        {
            MessageBox.Show("База данных не инициализирована", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        MessageBox.Show("Функция просмотра истории будет доступна в следующей версии",
            "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show(
            "Система автоматизации создания документов\n\n" +
            "Версия 3.0 (Enhanced UI & Database)\n\n" +
            "Возможности:\n" +
            "• Управление шаблонами документов\n" +
            "• Организация по категориям\n" +
            "• История документов\n" +
            "• MS SQL Server база данных\n\n" +
            "Интерфейс с автоматическим появлением полей\n" +
            "Шаблоны и документы хранятся в базе данных.\n\n" +
            ".NET 9 Edition",
            "О программе",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}