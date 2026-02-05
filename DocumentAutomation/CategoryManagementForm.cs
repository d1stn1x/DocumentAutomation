using DocumentAutomation.Models;
using DocumentAutomation.Services;

namespace DocumentAutomation;

public partial class CategoryManagementForm : Form
{
    private readonly DatabaseService _databaseService;
    private List<Category> _categories = new List<Category>();

    public CategoryManagementForm(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        InitializeComponent();
    }

    private async void CategoryManagementForm_Load(object sender, EventArgs e)
    {
        await LoadCategoriesAsync();
    }

    private async Task LoadCategoriesAsync()
    {
        try
        {
            _categories = await _databaseService.GetAllCategoriesAsync();
            UpdateCategoriesListView();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateCategoriesListView()
    {
        listViewCategories.Items.Clear();
        
        foreach (var category in _categories)
        {
            var item = new ListViewItem(category.Name);
            item.SubItems.Add(category.Description ?? "");
            item.SubItems.Add(category.Color ?? "");
            item.SubItems.Add(category.CreatedDate.ToLocalTime().ToString("dd.MM.yyyy HH:mm"));
            item.Tag = category;
            
            if (!string.IsNullOrEmpty(category.Color))
            {
                try
                {
                    item.BackColor = ColorTranslator.FromHtml(category.Color);
                }
                catch { }
            }
            
            listViewCategories.Items.Add(item);
        }
    }

    private async void btnAdd_Click(object sender, EventArgs e)
    {
        using var inputForm = new Form
        {
            Text = "Новая категория",
            Width = 450,
            Height = 250,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false
        };

        var labelName = new Label { Left = 10, Top = 20, Text = "Название:" };
        var txtName = new TextBox { Left = 10, Top = 40, Width = 410 };
        var labelDesc = new Label { Left = 10, Top = 70, Text = "Описание:" };
        var txtDesc = new TextBox { Left = 10, Top = 90, Width = 410, Multiline = true, Height = 60 };
        var labelColor = new Label { Left = 10, Top = 160, Text = "Цвет (например: #FFE6E6):" };
        var txtColor = new TextBox { Left = 10, Top = 180, Width = 200 };
        
        var btnOk = new Button { Text = "ОК", Left = 250, Width = 80, Top = 175, DialogResult = DialogResult.OK };
        var btnCancel = new Button { Text = "Отмена", Left = 340, Width = 80, Top = 175, DialogResult = DialogResult.Cancel };

        btnOk.Click += (s, ea) => { inputForm.Close(); };
        inputForm.Controls.Add(labelName);
        inputForm.Controls.Add(txtName);
        inputForm.Controls.Add(labelDesc);
        inputForm.Controls.Add(txtDesc);
        inputForm.Controls.Add(labelColor);
        inputForm.Controls.Add(txtColor);
        inputForm.Controls.Add(btnOk);
        inputForm.Controls.Add(btnCancel);
        inputForm.AcceptButton = btnOk;
        inputForm.CancelButton = btnCancel;

        if (inputForm.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txtName.Text))
        {
            try
            {
                var newCategory = new Category
                {
                    Name = txtName.Text.Trim(),
                    Description = txtDesc.Text.Trim(),
                    Color = txtColor.Text.Trim()
                };
                
                await _databaseService.AddCategoryAsync(newCategory);
                await LoadCategoriesAsync();
                MessageBox.Show("Категория успешно добавлена!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления категории: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        if (listViewCategories.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите категорию для удаления", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedCategory = listViewCategories.SelectedItems[0].Tag as Category;
        var result = MessageBox.Show(
            $"Вы уверены, что хотите удалить категорию '{selectedCategory!.Name}'?",
            "Подтверждение удаления",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            try
            {
                await _databaseService.DeleteCategoryAsync(selectedCategory.Id);
                await LoadCategoriesAsync();
                MessageBox.Show("Категория успешно удалена!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления категории: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
