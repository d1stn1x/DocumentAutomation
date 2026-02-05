namespace DocumentAutomation;

partial class TemplateEditForm
{
    private System.ComponentModel.IContainer components = null;
    private Label labelName;
    private TextBox txtName;
    private Label labelDescription;
    private TextBox txtDescription;
    private Label labelCategory;
    private ComboBox comboBoxCategory;
    private Label labelContent;
    private TextBox txtContent;
    private Button btnSave;
    private Button btnCancel;
    private Button btnInsertVariable;
    private Panel panelButtons;

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
        this.labelName = new Label();
        this.txtName = new TextBox();
        this.labelDescription = new Label();
        this.txtDescription = new TextBox();
        this.labelCategory = new Label();
        this.comboBoxCategory = new ComboBox();
        this.labelContent = new Label();
        this.txtContent = new TextBox();
        this.panelButtons = new Panel();
        this.btnInsertVariable = new Button();
        this.btnCancel = new Button();
        this.btnSave = new Button();
        
        this.panelButtons.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // labelName
        // 
        this.labelName.AutoSize = true;
        this.labelName.Location = new Point(12, 15);
        this.labelName.Name = "labelName";
        this.labelName.Size = new Size(100, 15);
        this.labelName.TabIndex = 0;
        this.labelName.Text = "Название:";
        
        // 
        // txtName
        // 
        this.txtName.Location = new Point(12, 33);
        this.txtName.Name = "txtName";
        this.txtName.Size = new Size(560, 23);
        this.txtName.TabIndex = 1;
        
        // 
        // labelDescription
        // 
        this.labelDescription.AutoSize = true;
        this.labelDescription.Location = new Point(12, 65);
        this.labelDescription.Name = "labelDescription";
        this.labelDescription.Size = new Size(100, 15);
        this.labelDescription.TabIndex = 2;
        this.labelDescription.Text = "Описание:";
        
        // 
        // txtDescription
        // 
        this.txtDescription.Location = new Point(12, 83);
        this.txtDescription.Multiline = true;
        this.txtDescription.Name = "txtDescription";
        this.txtDescription.Size = new Size(560, 60);
        this.txtDescription.TabIndex = 3;
        
        // 
        // labelCategory
        // 
        this.labelCategory.AutoSize = true;
        this.labelCategory.Location = new Point(12, 155);
        this.labelCategory.Name = "labelCategory";
        this.labelCategory.Size = new Size(100, 15);
        this.labelCategory.TabIndex = 4;
        this.labelCategory.Text = "Категория:";
        
        // 
        // comboBoxCategory
        // 
        this.comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        this.comboBoxCategory.FormattingEnabled = true;
        this.comboBoxCategory.Location = new Point(12, 173);
        this.comboBoxCategory.Name = "comboBoxCategory";
        this.comboBoxCategory.Size = new Size(280, 23);
        this.comboBoxCategory.TabIndex = 5;
        
        // 
        // labelContent
        // 
        this.labelContent.AutoSize = true;
        this.labelContent.Location = new Point(12, 205);
        this.labelContent.Name = "labelContent";
        this.labelContent.Size = new Size(200, 15);
        this.labelContent.TabIndex = 6;
        this.labelContent.Text = "Содержимое (используйте {{ИМЯ_ПЕРЕМЕННОЙ}}):";
        
        // 
        // txtContent
        // 
        this.txtContent.AcceptsReturn = true;
        this.txtContent.AcceptsTab = true;
        this.txtContent.Font = new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtContent.Location = new Point(12, 223);
        this.txtContent.Multiline = true;
        this.txtContent.Name = "txtContent";
        this.txtContent.ScrollBars = ScrollBars.Both;
        this.txtContent.Size = new Size(560, 280);
        this.txtContent.TabIndex = 7;
        this.txtContent.WordWrap = false;
        
        // 
        // panelButtons
        // 
        this.panelButtons.Controls.Add(this.btnCancel);
        this.panelButtons.Controls.Add(this.btnSave);
        this.panelButtons.Controls.Add(this.btnInsertVariable);
        this.panelButtons.Dock = DockStyle.Bottom;
        this.panelButtons.Location = new Point(0, 515);
        this.panelButtons.Name = "panelButtons";
        this.panelButtons.Padding = new Padding(10);
        this.panelButtons.Size = new Size(584, 60);
        this.panelButtons.TabIndex = 8;
        
        // 
        // btnInsertVariable
        // 
        this.btnInsertVariable.Location = new Point(10, 15);
        this.btnInsertVariable.Name = "btnInsertVariable";
        this.btnInsertVariable.Size = new Size(150, 35);
        this.btnInsertVariable.TabIndex = 0;
        this.btnInsertVariable.Text = "Вставить переменную";
        this.btnInsertVariable.UseVisualStyleBackColor = true;
        this.btnInsertVariable.Click += new EventHandler(this.btnInsertVariable_Click);
        
        // 
        // btnSave
        // 
        this.btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.btnSave.Location = new Point(334, 15);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new Size(120, 35);
        this.btnSave.TabIndex = 1;
        this.btnSave.Text = "Сохранить";
        this.btnSave.UseVisualStyleBackColor = true;
        this.btnSave.Click += new EventHandler(this.btnSave_Click);
        
        // 
        // btnCancel
        // 
        this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.btnCancel.Location = new Point(460, 15);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new Size(120, 35);
        this.btnCancel.TabIndex = 2;
        this.btnCancel.Text = "Отмена";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        
        // 
        // TemplateEditForm
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(584, 575);
        this.Controls.Add(this.panelButtons);
        this.Controls.Add(this.txtContent);
        this.Controls.Add(this.labelContent);
        this.Controls.Add(this.comboBoxCategory);
        this.Controls.Add(this.labelCategory);
        this.Controls.Add(this.txtDescription);
        this.Controls.Add(this.labelDescription);
        this.Controls.Add(this.txtName);
        this.Controls.Add(this.labelName);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "TemplateEditForm";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Шаблон";
        this.Load += new EventHandler(this.TemplateEditForm_Load);
        
        this.panelButtons.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
