using DocumentAutomation.Models;
using DocumentAutomation.Services;

namespace DocumentAutomation;

public partial class DocumentHistoryForm : Form
{
    private readonly DatabaseService _databaseService;
    private List<GeneratedDocument> _documents = new List<GeneratedDocument>();

    public DocumentHistoryForm(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        InitializeComponent();
    }

    private async void DocumentHistoryForm_Load(object sender, EventArgs e)
    {
        await LoadDocumentsAsync();
    }

    private async Task LoadDocumentsAsync()
    {
        try
        {
            _documents = await _databaseService.GetGeneratedDocumentsAsync();
            UpdateDocumentsListView();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки истории: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateDocumentsListView()
    {
        listViewHistory.Items.Clear();
        
        foreach (var doc in _documents)
        {
            var item = new ListViewItem(doc.DocumentName);
            item.SubItems.Add(doc.Template?.Name ?? "Неизвестно");
            item.SubItems.Add(doc.GeneratedDate.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss"));
            item.Tag = doc;
            listViewHistory.Items.Add(item);
        }
        
        labelTotal.Text = $"Всего документов: {_documents.Count}";
    }

    private void btnView_Click(object sender, EventArgs e)
    {
        if (listViewHistory.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите документ для просмотра", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedDocument = listViewHistory.SelectedItems[0].Tag as GeneratedDocument;
        
        using var viewForm = new Form
        {
            Text = $"Просмотр: {selectedDocument!.DocumentName}",
            Width = 800,
            Height = 600,
            StartPosition = FormStartPosition.CenterParent
        };

        var textBox = new TextBox
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            ScrollBars = ScrollBars.Both,
            Font = new Font("Courier New", 9.75F),
            Text = selectedDocument.Content,
            ReadOnly = true,
            WordWrap = true
        };

        viewForm.Controls.Add(textBox);
        viewForm.ShowDialog();
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        if (listViewHistory.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите документ для экспорта", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedDocument = listViewHistory.SelectedItems[0].Tag as GeneratedDocument;
        
        var saveDialog = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            DefaultExt = "txt",
            FileName = selectedDocument!.DocumentName
        };

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                File.WriteAllText(saveDialog.FileName, selectedDocument.Content);
                MessageBox.Show("Документ успешно экспортирован!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void listViewHistory_DoubleClick(object sender, EventArgs e)
    {
        btnView_Click(sender, e);
    }
}
