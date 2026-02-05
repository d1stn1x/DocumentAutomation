namespace DocumentAutomation;

partial class TemplateManagementForm
{
    private System.ComponentModel.IContainer components = null;
    private ListView listViewTemplates;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Button btnClose;
    private Panel panelButtons;
    private ColumnHeader columnName;
    private ColumnHeader columnCategory;
    private ColumnHeader columnDescription;
    private ColumnHeader columnCreated;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.listViewTemplates = new ListView();
        this.columnName = new ColumnHeader();
        this.columnCategory = new ColumnHeader();
        this.columnDescription = new ColumnHeader();
        this.columnCreated = new ColumnHeader();
        this.panelButtons = new Panel();
        this.btnAdd = new Button();
        this.btnEdit = new Button();
        this.btnDelete = new Button();
        this.btnClose = new Button();
        
        this.panelButtons.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // listViewTemplates
        // 
        this.listViewTemplates.Columns.AddRange(new ColumnHeader[] {
            this.columnName,
            this.columnCategory,
            this.columnDescription,
            this.columnCreated});
        this.listViewTemplates.Dock = DockStyle.Fill;
        this.listViewTemplates.FullRowSelect = true;
        this.listViewTemplates.GridLines = true;
        this.listViewTemplates.Location = new Point(0, 0);
        this.listViewTemplates.MultiSelect = false;
        this.listViewTemplates.Name = "listViewTemplates";
        this.listViewTemplates.Size = new Size(900, 520);
        this.listViewTemplates.TabIndex = 0;
        this.listViewTemplates.UseCompatibleStateImageBehavior = false;
        this.listViewTemplates.View = View.Details;
        this.listViewTemplates.DoubleClick += new EventHandler(this.listViewTemplates_DoubleClick);
        
        // 
        // columnName
        // 
        this.columnName.Text = "Название";
        this.columnName.Width = 200;
        
        // 
        // columnCategory
        // 
        this.columnCategory.Text = "Категория";
        this.columnCategory.Width = 150;
        
        // 
        // columnDescription
        // 
        this.columnDescription.Text = "Описание";
        this.columnDescription.Width = 350;
        
        // 
        // columnCreated
        // 
        this.columnCreated.Text = "Создан";
        this.columnCreated.Width = 150;
        
        // 
        // panelButtons
        // 
        this.panelButtons.Controls.Add(this.btnClose);
        this.panelButtons.Controls.Add(this.btnDelete);
        this.panelButtons.Controls.Add(this.btnEdit);
        this.panelButtons.Controls.Add(this.btnAdd);
        this.panelButtons.Dock = DockStyle.Bottom;
        this.panelButtons.Location = new Point(0, 520);
        this.panelButtons.Name = "panelButtons";
        this.panelButtons.Padding = new Padding(10);
        this.panelButtons.Size = new Size(900, 60);
        this.panelButtons.TabIndex = 1;
        
        // 
        // btnAdd
        // 
        this.btnAdd.Location = new Point(10, 15);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new Size(120, 35);
        this.btnAdd.TabIndex = 0;
        this.btnAdd.Text = "Добавить";
        this.btnAdd.UseVisualStyleBackColor = true;
        this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
        
        // 
        // btnEdit
        // 
        this.btnEdit.Location = new Point(140, 15);
        this.btnEdit.Name = "btnEdit";
        this.btnEdit.Size = new Size(120, 35);
        this.btnEdit.TabIndex = 1;
        this.btnEdit.Text = "Редактировать";
        this.btnEdit.UseVisualStyleBackColor = true;
        this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
        
        // 
        // btnDelete
        // 
        this.btnDelete.Location = new Point(270, 15);
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new Size(120, 35);
        this.btnDelete.TabIndex = 2;
        this.btnDelete.Text = "Удалить";
        this.btnDelete.UseVisualStyleBackColor = true;
        this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
        
        // 
        // btnClose
        // 
        this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.btnClose.Location = new Point(770, 15);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new Size(120, 35);
        this.btnClose.TabIndex = 3;
        this.btnClose.Text = "Закрыть";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new EventHandler(this.btnClose_Click);
        
        // 
        // TemplateManagementForm
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(900, 580);
        this.Controls.Add(this.listViewTemplates);
        this.Controls.Add(this.panelButtons);
        this.Name = "TemplateManagementForm";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Управление шаблонами";
        this.Load += new EventHandler(this.TemplateManagementForm_Load);
        
        this.panelButtons.ResumeLayout(false);
        this.ResumeLayout(false);
    }
}
