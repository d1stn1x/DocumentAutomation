namespace DocumentAutomation;

partial class CategoryManagementForm
{
    private System.ComponentModel.IContainer components = null;
    private ListView listViewCategories;
    private Button btnAdd;
    private Button btnDelete;
    private Button btnClose;
    private Panel panelButtons;
    private ColumnHeader columnName;
    private ColumnHeader columnDescription;
    private ColumnHeader columnColor;
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
        this.listViewCategories = new ListView();
        this.columnName = new ColumnHeader();
        this.columnDescription = new ColumnHeader();
        this.columnColor = new ColumnHeader();
        this.columnCreated = new ColumnHeader();
        this.panelButtons = new Panel();
        this.btnAdd = new Button();
        this.btnDelete = new Button();
        this.btnClose = new Button();
        
        this.panelButtons.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // listViewCategories
        // 
        this.listViewCategories.Columns.AddRange(new ColumnHeader[] {
            this.columnName,
            this.columnDescription,
            this.columnColor,
            this.columnCreated});
        this.listViewCategories.Dock = DockStyle.Fill;
        this.listViewCategories.FullRowSelect = true;
        this.listViewCategories.GridLines = true;
        this.listViewCategories.Location = new Point(0, 0);
        this.listViewCategories.MultiSelect = false;
        this.listViewCategories.Name = "listViewCategories";
        this.listViewCategories.Size = new Size(700, 420);
        this.listViewCategories.TabIndex = 0;
        this.listViewCategories.UseCompatibleStateImageBehavior = false;
        this.listViewCategories.View = View.Details;
        
        // 
        // columnName
        // 
        this.columnName.Text = "Название";
        this.columnName.Width = 150;
        
        // 
        // columnDescription
        // 
        this.columnDescription.Text = "Описание";
        this.columnDescription.Width = 300;
        
        // 
        // columnColor
        // 
        this.columnColor.Text = "Цвет";
        this.columnColor.Width = 100;
        
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
        this.panelButtons.Controls.Add(this.btnAdd);
        this.panelButtons.Dock = DockStyle.Bottom;
        this.panelButtons.Location = new Point(0, 420);
        this.panelButtons.Name = "panelButtons";
        this.panelButtons.Padding = new Padding(10);
        this.panelButtons.Size = new Size(700, 60);
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
        // btnDelete
        // 
        this.btnDelete.Location = new Point(140, 15);
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new Size(120, 35);
        this.btnDelete.TabIndex = 1;
        this.btnDelete.Text = "Удалить";
        this.btnDelete.UseVisualStyleBackColor = true;
        this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
        
        // 
        // btnClose
        // 
        this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.btnClose.Location = new Point(570, 15);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new Size(120, 35);
        this.btnClose.TabIndex = 2;
        this.btnClose.Text = "Закрыть";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new EventHandler(this.btnClose_Click);
        
        // 
        // CategoryManagementForm
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(700, 480);
        this.Controls.Add(this.listViewCategories);
        this.Controls.Add(this.panelButtons);
        this.Name = "CategoryManagementForm";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Управление категориями";
        this.Load += new EventHandler(this.CategoryManagementForm_Load);
        
        this.panelButtons.ResumeLayout(false);
        this.ResumeLayout(false);
    }
}
