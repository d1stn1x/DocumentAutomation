using DocumentAutomation.Models;
using DocumentAutomation.Services;

namespace DocumentAutomation;

public partial class TemplateManagementForm : Form
{
    private readonly DatabaseService _databaseService;
    private List<DocumentTemplate> _templates = new List<DocumentTemplate>();
    private List<Category> _categories = new List<Category>();

    public TemplateManagementForm(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        InitializeComponent();
    }

    private async void TemplateManagementForm_Load(object sender, EventArgs e)
    {
        await LoadCategoriesAsync();
        await LoadTemplatesAsync();
    }

    private async Task LoadCategoriesAsync()
    {
        try
        {
            _categories = await _databaseService.GetAllCategoriesAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadTemplatesAsync()
    {
        try
        {
            _templates = await _databaseService.GetAllTemplatesAsync();
            UpdateTemplatesListView();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки шаблонов: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateTemplatesListView()
    {
        listViewTemplates.Items.Clear();
        
        foreach (var template in _templates)
        {
            var item = new ListViewItem(template.Name);
            item.SubItems.Add(template.Category?.Name ?? "Без категории");
            item.SubItems.Add(template.Description ?? "");
            item.SubItems.Add(template.CreatedDate.ToLocalTime().ToString("dd.MM.yyyy HH:mm"));
            item.Tag = template;
            listViewTemplates.Items.Add(item);
        }
    }

    private async void btnAdd_Click(object sender, EventArgs e)
    {
        var editForm = new TemplateEditForm(_databaseService, null, _categories);
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            await LoadTemplatesAsync();
        }
    }

    private async void btnEdit_Click(object sender, EventArgs e)
    {
        if (listViewTemplates.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите шаблон для редактирования", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedTemplate = listViewTemplates.SelectedItems[0].Tag as DocumentTemplate;
        var editForm = new TemplateEditForm(_databaseService, selectedTemplate, _categories);
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            await LoadTemplatesAsync();
        }
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        if (listViewTemplates.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите шаблон для удаления", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedTemplate = listViewTemplates.SelectedItems[0].Tag as DocumentTemplate;
        var result = MessageBox.Show(
            $"Вы уверены, что хотите удалить шаблон '{selectedTemplate!.Name}'?",
            "Подтверждение удаления",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            try
            {
                await _databaseService.DeleteTemplateAsync(selectedTemplate.Id);
                await LoadTemplatesAsync();
                MessageBox.Show("Шаблон успешно удален!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления шаблона: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void listViewTemplates_DoubleClick(object sender, EventArgs e)
    {
        btnEdit_Click(sender, e);
    }
}
