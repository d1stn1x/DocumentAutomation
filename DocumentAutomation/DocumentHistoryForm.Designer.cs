namespace DocumentAutomation;

partial class DocumentHistoryForm
{
    private System.ComponentModel.IContainer components = null;
    private ListView listViewHistory;
    private Button btnView;
    private Button btnExport;
    private Button btnClose;
    private Panel panelButtons;
    private Label labelTotal;
    private ColumnHeader columnName;
    private ColumnHeader columnTemplate;
    private ColumnHeader columnDate;

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
        this.listViewHistory = new ListView();
        this.columnName = new ColumnHeader();
        this.columnTemplate = new ColumnHeader();
        this.columnDate = new ColumnHeader();
        this.panelButtons = new Panel();
        this.labelTotal = new Label();
        this.btnView = new Button();
        this.btnExport = new Button();
        this.btnClose = new Button();
        
        this.panelButtons.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // listViewHistory
        // 
        this.listViewHistory.Columns.AddRange(new ColumnHeader[] {
            this.columnName,
            this.columnTemplate,
            this.columnDate});
        this.listViewHistory.Dock = DockStyle.Fill;
        this.listViewHistory.FullRowSelect = true;
        this.listViewHistory.GridLines = true;
        this.listViewHistory.Location = new Point(0, 0);
        this.listViewHistory.MultiSelect = false;
        this.listViewHistory.Name = "listViewHistory";
        this.listViewHistory.Size = new Size(800, 520);
        this.listViewHistory.TabIndex = 0;
        this.listViewHistory.UseCompatibleStateImageBehavior = false;
        this.listViewHistory.View = View.Details;
        this.listViewHistory.DoubleClick += new EventHandler(this.listViewHistory_DoubleClick);
        
        // 
        // columnName
        // 
        this.columnName.Text = "Название документа";
        this.columnName.Width = 350;
        
        // 
        // columnTemplate
        // 
        this.columnTemplate.Text = "Шаблон";
        this.columnTemplate.Width = 200;
        
        // 
        // columnDate
        // 
        this.columnDate.Text = "Дата создания";
        this.columnDate.Width = 200;
        
        // 
        // panelButtons
        // 
        this.panelButtons.Controls.Add(this.btnClose);
        this.panelButtons.Controls.Add(this.btnExport);
        this.panelButtons.Controls.Add(this.btnView);
        this.panelButtons.Controls.Add(this.labelTotal);
        this.panelButtons.Dock = DockStyle.Bottom;
        this.panelButtons.Location = new Point(0, 520);
        this.panelButtons.Name = "panelButtons";
        this.panelButtons.Padding = new Padding(10);
        this.panelButtons.Size = new Size(800, 60);
        this.panelButtons.TabIndex = 1;
        
        // 
        // labelTotal
        // 
        this.labelTotal.AutoSize = true;
        this.labelTotal.Location = new Point(13, 25);
        this.labelTotal.Name = "labelTotal";
        this.labelTotal.Size = new Size(150, 15);
        this.labelTotal.TabIndex = 0;
        this.labelTotal.Text = "Всего документов: 0";
        
        // 
        // btnView
        // 
        this.btnView.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.btnView.Location = new Point(410, 15);
        this.btnView.Name = "btnView";
        this.btnView.Size = new Size(120, 35);
        this.btnView.TabIndex = 1;
        this.btnView.Text = "Просмотр";
        this.btnView.UseVisualStyleBackColor = true;
        this.btnView.Click += new EventHandler(this.btnView_Click);
        
        // 
        // btnExport
        // 
        this.btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.btnExport.Location = new Point(540, 15);
        this.btnExport.Name = "btnExport";
        this.btnExport.Size = new Size(120, 35);
        this.btnExport.TabIndex = 2;
        this.btnExport.Text = "Экспорт";
        this.btnExport.UseVisualStyleBackColor = true;
        this.btnExport.Click += new EventHandler(this.btnExport_Click);
        
        // 
        // btnClose
        // 
        this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.btnClose.Location = new Point(670, 15);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new Size(120, 35);
        this.btnClose.TabIndex = 3;
        this.btnClose.Text = "Закрыть";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new EventHandler(this.btnClose_Click);
        
        // 
        // DocumentHistoryForm
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(800, 580);
        this.Controls.Add(this.listViewHistory);
        this.Controls.Add(this.panelButtons);
        this.Name = "DocumentHistoryForm";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "История созданных документов";
        this.Load += new EventHandler(this.DocumentHistoryForm_Load);
        
        this.panelButtons.ResumeLayout(false);
        this.panelButtons.PerformLayout();
        this.ResumeLayout(false);
    }
}
