namespace DocumentAutomation;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.menuStrip = new MenuStrip();
        this.fileToolStripMenuItem = new ToolStripMenuItem();
        this.exitToolStripMenuItem = new ToolStripMenuItem();
        this.templatesToolStripMenuItem = new ToolStripMenuItem();
        this.manageTemplatesToolStripMenuItem = new ToolStripMenuItem();
        this.manageCategoriesToolStripMenuItem = new ToolStripMenuItem();
        this.viewHistoryToolStripMenuItem = new ToolStripMenuItem();
        this.helpToolStripMenuItem = new ToolStripMenuItem();
        this.aboutToolStripMenuItem = new ToolStripMenuItem();
        this.splitContainer = new SplitContainer();
        this.groupBoxTemplates = new GroupBox();
        this.listBoxTemplates = new ListBox();
        this.btnRefreshTemplates = new Button();
        this.groupBoxDocument = new GroupBox();
        this.txtOutput = new TextBox();
        this.panelControls = new Panel();
        this.btnGenerateDocument = new Button();
        this.btnSaveDocument = new Button();
        this.groupBoxVariables = new GroupBox();
        this.tableLayoutVariables = new TableLayoutPanel();
        
        this.menuStrip.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
        this.splitContainer.Panel1.SuspendLayout();
        this.splitContainer.Panel2.SuspendLayout();
        this.splitContainer.SuspendLayout();
        this.groupBoxTemplates.SuspendLayout();
        this.groupBoxDocument.SuspendLayout();
        this.panelControls.SuspendLayout();
        this.groupBoxVariables.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // menuStrip
        // 
        this.menuStrip.Items.AddRange(new ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.templatesToolStripMenuItem,
            this.helpToolStripMenuItem});
        this.menuStrip.Location = new Point(0, 0);
        this.menuStrip.Name = "menuStrip";
        this.menuStrip.Size = new Size(1000, 24);
        this.menuStrip.TabIndex = 0;
        this.menuStrip.Text = "menuStrip1";
        
        // 
        // fileToolStripMenuItem
        // 
        this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.exitToolStripMenuItem});
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        this.fileToolStripMenuItem.Size = new Size(48, 20);
        this.fileToolStripMenuItem.Text = "Файл";
        
        // 
        // exitToolStripMenuItem
        // 
        this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        this.exitToolStripMenuItem.Size = new Size(108, 22);
        this.exitToolStripMenuItem.Text = "Выход";
        this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
        
        // 
        // templatesToolStripMenuItem
        // 
        this.templatesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.manageTemplatesToolStripMenuItem,
            this.manageCategoriesToolStripMenuItem,
            this.viewHistoryToolStripMenuItem});
        this.templatesToolStripMenuItem.Name = "templatesToolStripMenuItem";
        this.templatesToolStripMenuItem.Size = new Size(70, 20);
        this.templatesToolStripMenuItem.Text = "Шаблоны";
        
        // 
        // manageTemplatesToolStripMenuItem
        // 
        this.manageTemplatesToolStripMenuItem.Name = "manageTemplatesToolStripMenuItem";
        this.manageTemplatesToolStripMenuItem.Size = new Size(220, 22);
        this.manageTemplatesToolStripMenuItem.Text = "Управление шаблонами";
        this.manageTemplatesToolStripMenuItem.Click += new EventHandler(this.manageTemplatesToolStripMenuItem_Click);
        
        // 
        // manageCategoriesToolStripMenuItem
        // 
        this.manageCategoriesToolStripMenuItem.Name = "manageCategoriesToolStripMenuItem";
        this.manageCategoriesToolStripMenuItem.Size = new Size(220, 22);
        this.manageCategoriesToolStripMenuItem.Text = "Управление категориями";
        this.manageCategoriesToolStripMenuItem.Click += new EventHandler(this.manageCategoriesToolStripMenuItem_Click);
        
        // 
        // viewHistoryToolStripMenuItem
        // 
        this.viewHistoryToolStripMenuItem.Name = "viewHistoryToolStripMenuItem";
        this.viewHistoryToolStripMenuItem.Size = new Size(220, 22);
        this.viewHistoryToolStripMenuItem.Text = "История документов";
        this.viewHistoryToolStripMenuItem.Click += new EventHandler(this.viewHistoryToolStripMenuItem_Click);
        
        // 
        // helpToolStripMenuItem
        // 
        this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.aboutToolStripMenuItem});
        this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        this.helpToolStripMenuItem.Size = new Size(68, 20);
        this.helpToolStripMenuItem.Text = "Справка";
        
        // 
        // aboutToolStripMenuItem
        // 
        this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        this.aboutToolStripMenuItem.Size = new Size(149, 22);
        this.aboutToolStripMenuItem.Text = "О программе";
        this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
        
        // 
        // splitContainer
        // 
        this.splitContainer.Dock = DockStyle.Fill;
        this.splitContainer.Location = new Point(0, 24);
        this.splitContainer.Name = "splitContainer";
        this.splitContainer.Orientation = Orientation.Horizontal;
        
        // 
        // splitContainer.Panel1
        // 
        this.splitContainer.Panel1.Controls.Add(this.groupBoxTemplates);
        this.splitContainer.Panel1.Controls.Add(this.groupBoxVariables);
        
        // 
        // splitContainer.Panel2
        // 
        this.splitContainer.Panel2.Controls.Add(this.groupBoxDocument);
        this.splitContainer.Panel2.Controls.Add(this.panelControls);
        
        this.splitContainer.Size = new Size(1000, 576);
        this.splitContainer.SplitterDistance = 250;
        this.splitContainer.TabIndex = 1;
        
        // 
        // groupBoxTemplates
        // 
        this.groupBoxTemplates.Controls.Add(this.listBoxTemplates);
        this.groupBoxTemplates.Controls.Add(this.btnRefreshTemplates);
        this.groupBoxTemplates.Dock = DockStyle.Left;
        this.groupBoxTemplates.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.groupBoxTemplates.ForeColor = Color.FromArgb(64, 64, 64);
        this.groupBoxTemplates.Location = new Point(0, 0);
        this.groupBoxTemplates.Name = "groupBoxTemplates";
        this.groupBoxTemplates.Padding = new Padding(3, 3, 3, 8);
        this.groupBoxTemplates.Size = new Size(300, 250);
        this.groupBoxTemplates.TabIndex = 0;
        this.groupBoxTemplates.TabStop = false;
        this.groupBoxTemplates.Text = "Шаблоны документов";
        
        // 
        // listBoxTemplates
        // 
        this.listBoxTemplates.BackColor = Color.FromArgb(250, 250, 250);
        this.listBoxTemplates.BorderStyle = BorderStyle.FixedSingle;
        this.listBoxTemplates.Dock = DockStyle.Fill;
        this.listBoxTemplates.Font = new Font("Segoe UI", 9F);
        this.listBoxTemplates.FormattingEnabled = true;
        this.listBoxTemplates.ItemHeight = 15;
        this.listBoxTemplates.Location = new Point(3, 19);
        this.listBoxTemplates.Name = "listBoxTemplates";
        this.listBoxTemplates.Size = new Size(294, 198);
        this.listBoxTemplates.TabIndex = 0;
        this.listBoxTemplates.SelectedIndexChanged += new EventHandler(this.listBoxTemplates_SelectedIndexChanged);
        
        // 
        // btnRefreshTemplates
        // 
        this.btnRefreshTemplates.BackColor = Color.FromArgb(230, 230, 250);
        this.btnRefreshTemplates.Dock = DockStyle.Bottom;
        this.btnRefreshTemplates.FlatStyle = FlatStyle.Flat;
        this.btnRefreshTemplates.Font = new Font("Segoe UI", 9F);
        this.btnRefreshTemplates.Location = new Point(3, 217);
        this.btnRefreshTemplates.Name = "btnRefreshTemplates";
        this.btnRefreshTemplates.Size = new Size(294, 30);
        this.btnRefreshTemplates.TabIndex = 1;
        this.btnRefreshTemplates.Text = "Обновить список";
        this.btnRefreshTemplates.UseVisualStyleBackColor = false;
        this.btnRefreshTemplates.Click += new EventHandler(this.btnRefreshTemplates_Click);
        
        // 
        // groupBoxVariables
        // 
        this.groupBoxVariables.Controls.Add(this.tableLayoutVariables);
        this.groupBoxVariables.Dock = DockStyle.Fill;
        this.groupBoxVariables.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.groupBoxVariables.ForeColor = Color.FromArgb(64, 64, 64);
        this.groupBoxVariables.Location = new Point(300, 0);
        this.groupBoxVariables.Name = "groupBoxVariables";
        this.groupBoxVariables.Padding = new Padding(10);
        this.groupBoxVariables.Size = new Size(700, 250);
        this.groupBoxVariables.TabIndex = 1;
        this.groupBoxVariables.TabStop = false;
        this.groupBoxVariables.Text = "Переменные шаблона";
        
        // 
        // tableLayoutVariables
        // 
        this.tableLayoutVariables.AutoScroll = true;
        this.tableLayoutVariables.ColumnCount = 2;
        this.tableLayoutVariables.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        this.tableLayoutVariables.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
        this.tableLayoutVariables.Dock = DockStyle.Fill;
        this.tableLayoutVariables.Location = new Point(10, 26);
        this.tableLayoutVariables.Name = "tableLayoutVariables";
        this.tableLayoutVariables.RowCount = 1;
        this.tableLayoutVariables.RowStyles.Add(new RowStyle());
        this.tableLayoutVariables.Size = new Size(680, 214);
        this.tableLayoutVariables.TabIndex = 0;
        
        // 
        // groupBoxDocument
        // 
        this.groupBoxDocument.Controls.Add(this.txtOutput);
        this.groupBoxDocument.Dock = DockStyle.Fill;
        this.groupBoxDocument.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.groupBoxDocument.ForeColor = Color.FromArgb(64, 64, 64);
        this.groupBoxDocument.Location = new Point(0, 0);
        this.groupBoxDocument.Name = "groupBoxDocument";
        this.groupBoxDocument.Padding = new Padding(3, 3, 3, 3);
        this.groupBoxDocument.Size = new Size(1000, 272);
        this.groupBoxDocument.TabIndex = 0;
        this.groupBoxDocument.TabStop = false;
        this.groupBoxDocument.Text = "Сгенерированный документ";
        
        // 
        // txtOutput
        // 
        this.txtOutput.BackColor = Color.FromArgb(255, 255, 250);
        this.txtOutput.BorderStyle = BorderStyle.FixedSingle;
        this.txtOutput.Dock = DockStyle.Fill;
        this.txtOutput.Font = new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtOutput.Location = new Point(3, 19);
        this.txtOutput.Multiline = true;
        this.txtOutput.Name = "txtOutput";
        this.txtOutput.ScrollBars = ScrollBars.Both;
        this.txtOutput.Size = new Size(994, 250);
        this.txtOutput.TabIndex = 0;
        this.txtOutput.WordWrap = false;
        
        // 
        // panelControls
        // 
        this.panelControls.BackColor = Color.FromArgb(240, 240, 240);
        this.panelControls.Controls.Add(this.btnSaveDocument);
        this.panelControls.Controls.Add(this.btnGenerateDocument);
        this.panelControls.Dock = DockStyle.Bottom;
        this.panelControls.Location = new Point(0, 272);
        this.panelControls.Name = "panelControls";
        this.panelControls.Padding = new Padding(10);
        this.panelControls.Size = new Size(1000, 50);
        this.panelControls.TabIndex = 1;
        
        // 
        // btnGenerateDocument
        // 
        this.btnGenerateDocument.BackColor = Color.FromArgb(100, 200, 100);
        this.btnGenerateDocument.Dock = DockStyle.Left;
        this.btnGenerateDocument.FlatStyle = FlatStyle.Flat;
        this.btnGenerateDocument.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.btnGenerateDocument.ForeColor = Color.White;
        this.btnGenerateDocument.Location = new Point(10, 10);
        this.btnGenerateDocument.Name = "btnGenerateDocument";
        this.btnGenerateDocument.Size = new Size(200, 30);
        this.btnGenerateDocument.TabIndex = 0;
        this.btnGenerateDocument.Text = "Сгенерировать документ";
        this.btnGenerateDocument.UseVisualStyleBackColor = false;
        this.btnGenerateDocument.Click += new EventHandler(this.btnGenerateDocument_Click);
        
        // 
        // btnSaveDocument
        // 
        this.btnSaveDocument.BackColor = Color.FromArgb(100, 149, 237);
        this.btnSaveDocument.Dock = DockStyle.Left;
        this.btnSaveDocument.Enabled = false;
        this.btnSaveDocument.FlatStyle = FlatStyle.Flat;
        this.btnSaveDocument.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.btnSaveDocument.ForeColor = Color.White;
        this.btnSaveDocument.Location = new Point(210, 10);
        this.btnSaveDocument.Name = "btnSaveDocument";
        this.btnSaveDocument.Size = new Size(200, 30);
        this.btnSaveDocument.TabIndex = 1;
        this.btnSaveDocument.Text = "Сохранить документ";
        this.btnSaveDocument.UseVisualStyleBackColor = false;
        this.btnSaveDocument.Click += new EventHandler(this.btnSaveDocument_Click);
        
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1000, 600);
        this.Controls.Add(this.splitContainer);
        this.Controls.Add(this.menuStrip);
        this.MainMenuStrip = this.menuStrip;
        this.Name = "MainForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Автоматизация создания документов";
        this.Load += new EventHandler(this.MainForm_Load);
        
        this.menuStrip.ResumeLayout(false);
        this.menuStrip.PerformLayout();
        this.splitContainer.Panel1.ResumeLayout(false);
        this.splitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
        this.splitContainer.ResumeLayout(false);
        this.groupBoxTemplates.ResumeLayout(false);
        this.groupBoxDocument.ResumeLayout(false);
        this.groupBoxDocument.PerformLayout();
        this.panelControls.ResumeLayout(false);
        this.groupBoxVariables.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem templatesToolStripMenuItem;
    private ToolStripMenuItem manageTemplatesToolStripMenuItem;
    private ToolStripMenuItem manageCategoriesToolStripMenuItem;
    private ToolStripMenuItem viewHistoryToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private SplitContainer splitContainer;
    private GroupBox groupBoxTemplates;
    private ListBox listBoxTemplates;
    private Button btnRefreshTemplates;
    private GroupBox groupBoxVariables;
    private TableLayoutPanel tableLayoutVariables;
    private GroupBox groupBoxDocument;
    private TextBox txtOutput;
    private Panel panelControls;
    private Button btnGenerateDocument;
    private Button btnSaveDocument;
}
