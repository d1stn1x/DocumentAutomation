using DocumentAutomation.Models;
using DocumentAutomation.Services;

namespace DocumentAutomation;

public partial class TemplateEditForm : Form
{
    private readonly DatabaseService _databaseService;
    private readonly DocumentTemplate? _template;
    private readonly List<Category> _categories;

    public TemplateEditForm(DatabaseService databaseService, DocumentTemplate? template, List<Category> categories)
    {
        _databaseService = databaseService;
        _template = template;
        _categories = categories;
        InitializeComponent();
    }

    private void TemplateEditForm_Load(object sender, EventArgs e)
    {
        // Load categories into combo box
        comboBoxCategory.Items.Add("Без категории");
        comboBoxCategory.SelectedIndex = 0;
        
        foreach (var category in _categories)
        {
            comboBoxCategory.Items.Add(category.Name);
        }

        if (_template != null)
        {
            // Edit mode
            this.Text = "Редактирование шаблона";
            txtName.Text = _template.Name;
            txtDescription.Text = _template.Description ?? "";
            txtContent.Text = _template.Content;
            
            if (_template.CategoryId.HasValue)
            {
                var category = _categories.FirstOrDefault(c => c.Id == _template.CategoryId.Value);
                if (category != null)
                {
                    var index = comboBoxCategory.Items.IndexOf(category.Name);
                    if (index >= 0)
                    {
                        comboBoxCategory.SelectedIndex = index;
                    }
                }
            }
        }
        else
        {
            // Add mode
            this.Text = "Новый шаблон";
        }
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("Введите название шаблона", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtName.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(txtContent.Text))
        {
            MessageBox.Show("Введите содержимое шаблона", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtContent.Focus();
            return;
        }

        try
        {
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            int? categoryId = null;
            if (comboBoxCategory.SelectedIndex > 0)
            {
                var selectedCategoryName = comboBoxCategory.SelectedItem?.ToString();
                if (selectedCategoryName != null)
                {
                    var category = _categories.FirstOrDefault(c => c.Name == selectedCategoryName);
                    categoryId = category?.Id;
                }
            }

            if (_template != null)
            {
                // Update existing template
                _template.Name = txtName.Text.Trim();
                _template.Description = txtDescription.Text.Trim();
                _template.Content = txtContent.Text;
                _template.CategoryId = categoryId;
                
                await _databaseService.UpdateTemplateAsync(_template);
                MessageBox.Show("Шаблон успешно обновлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Create new template
                var newTemplate = new DocumentTemplate
                {
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Content = txtContent.Text,
                    CategoryId = categoryId
                };
                
                await _databaseService.AddTemplateAsync(newTemplate);
                MessageBox.Show("Шаблон успешно создан!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения шаблона: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void btnInsertVariable_Click(object sender, EventArgs e)
    {
        // Create a custom input dialog
        using var inputForm = new Form
        {
            Text = "Вставка переменной",
            Width = 400,
            Height = 150,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false
        };

        var label = new Label 
        { 
            Left = 10, 
            Top = 20, 
            Text = "Введите имя переменной (например: CLIENT_NAME):",
            Width = 360
        };
        var textBox = new TextBox { Left = 10, Top = 45, Width = 360 };
        var btnOk = new Button 
        { 
            Text = "ОК", 
            Left = 210, 
            Width = 80, 
            Top = 75, 
            DialogResult = DialogResult.OK 
        };
        var btnCancel = new Button 
        { 
            Text = "Отмена", 
            Left = 300, 
            Width = 80, 
            Top = 75, 
            DialogResult = DialogResult.Cancel 
        };

        btnOk.Click += (s, ea) => { inputForm.Close(); };
        inputForm.Controls.Add(label);
        inputForm.Controls.Add(textBox);
        inputForm.Controls.Add(btnOk);
        inputForm.Controls.Add(btnCancel);
        inputForm.AcceptButton = btnOk;
        inputForm.CancelButton = btnCancel;

        if (inputForm.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(textBox.Text))
        {
            string variableText = $"{{{{{textBox.Text.Trim().ToUpper()}}}}}";
            
            // Insert at cursor position or append
            int selectionStart = txtContent.SelectionStart;
            txtContent.Text = txtContent.Text.Insert(selectionStart, variableText);
            txtContent.SelectionStart = selectionStart + variableText.Length;
            txtContent.Focus();
        }
    }
}
