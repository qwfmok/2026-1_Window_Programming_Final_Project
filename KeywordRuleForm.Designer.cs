using System.Drawing;
using System.Windows.Forms;

namespace PCActivityTimeline
{
    partial class KeywordRuleForm
    {
        private TableLayoutPanel mainPanel;
        private DataGridView gridRules;
        private Label lblHelp;
        private Label lblKeyword;
        private Label lblCategory;
        private TextBox txtKeyword;
        private ComboBox cmbCategory;
        private FlowLayoutPanel editorButtons;
        private FlowLayoutPanel bottomButtons;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblHelp = new System.Windows.Forms.Label();
            this.gridRules = new System.Windows.Forms.DataGridView();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.editorButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.bottomButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRules)).BeginInit();
            this.editorButtons.SuspendLayout();
            this.bottomButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 2;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.Controls.Add(this.lblHelp, 0, 0);
            this.mainPanel.Controls.Add(this.gridRules, 0, 1);
            this.mainPanel.Controls.Add(this.lblKeyword, 0, 2);
            this.mainPanel.Controls.Add(this.txtKeyword, 1, 2);
            this.mainPanel.Controls.Add(this.lblCategory, 0, 3);
            this.mainPanel.Controls.Add(this.cmbCategory, 1, 3);
            this.mainPanel.Controls.Add(this.editorButtons, 1, 4);
            this.mainPanel.Controls.Add(this.bottomButtons, 0, 5);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(14);
            this.mainPanel.RowCount = 6;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.mainPanel.Size = new System.Drawing.Size(640, 480);
            this.mainPanel.TabIndex = 0;
            // 
            // lblHelp
            // 
            this.mainPanel.SetColumnSpan(this.lblHelp, 2);
            this.lblHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblHelp.Location = new System.Drawing.Point(17, 14);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(606, 52);
            this.lblHelp.TabIndex = 0;
            this.lblHelp.Text = "창 제목이나 프로그램명에 특정 키워드가 포함되면 지정한 분류로 자동 기록됩니다.\r\n예: youtube -> 휴식, github -> 과제, lms" +
    " -> 수업/시험";
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridRules
            // 
            this.gridRules.AllowUserToAddRows = false;
            this.gridRules.AllowUserToDeleteRows = false;
            this.gridRules.AllowUserToResizeColumns = false;
            this.gridRules.AllowUserToResizeRows = false;
            this.gridRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridRules.BackgroundColor = System.Drawing.Color.White;
            this.gridRules.ColumnHeadersHeight = 34;
            this.gridRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.mainPanel.SetColumnSpan(this.gridRules, 2);
            this.gridRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRules.Location = new System.Drawing.Point(14, 74);
            this.gridRules.Margin = new System.Windows.Forms.Padding(0, 8, 0, 10);
            this.gridRules.MultiSelect = false;
            this.gridRules.Name = "gridRules";
            this.gridRules.ReadOnly = true;
            this.gridRules.RowHeadersVisible = false;
            this.gridRules.RowHeadersWidth = 62;
            this.gridRules.RowTemplate.Height = 24;
            this.gridRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRules.Size = new System.Drawing.Size(612, 218);
            this.gridRules.TabIndex = 1;
            this.gridRules.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridRules_CellClick);
            // 
            // lblKeyword
            // 
            this.lblKeyword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKeyword.Location = new System.Drawing.Point(17, 302);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(76, 36);
            this.lblKeyword.TabIndex = 2;
            this.lblKeyword.Text = "키워드";
            this.lblKeyword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtKeyword
            // 
            this.txtKeyword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKeyword.Location = new System.Drawing.Point(99, 305);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(524, 31);
            this.txtKeyword.TabIndex = 3;
            // 
            // lblCategory
            // 
            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategory.Location = new System.Drawing.Point(17, 338);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(76, 36);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "분류";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCategory.Items.AddRange(new object[] {
            "과제",
            "자료검색",
            "수업/시험",
            "문서작성",
            "휴식",
            "기타",
            "미분류"});
            this.cmbCategory.Location = new System.Drawing.Point(99, 341);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(524, 33);
            this.cmbCategory.TabIndex = 5;
            this.cmbCategory.Text = "미분류";
            // 
            // editorButtons
            // 
            this.editorButtons.Controls.Add(this.btnAdd);
            this.editorButtons.Controls.Add(this.btnUpdate);
            this.editorButtons.Controls.Add(this.btnDelete);
            this.editorButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorButtons.Location = new System.Drawing.Point(99, 377);
            this.editorButtons.Name = "editorButtons";
            this.editorButtons.Size = new System.Drawing.Size(524, 38);
            this.editorButtons.TabIndex = 6;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(4, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 32);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "추가";
            this.btnAdd.Click += new System.EventHandler(this.AddRule);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(104, 4);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(92, 32);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "수정";
            this.btnUpdate.Click += new System.EventHandler(this.UpdateRule);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(204, 4);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 32);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "삭제";
            this.btnDelete.Click += new System.EventHandler(this.DeleteRule);
            // 
            // bottomButtons
            // 
            this.mainPanel.SetColumnSpan(this.bottomButtons, 2);
            this.bottomButtons.Controls.Add(this.btnCancel);
            this.bottomButtons.Controls.Add(this.btnSave);
            this.bottomButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.bottomButtons.Location = new System.Drawing.Point(17, 421);
            this.bottomButtons.Name = "bottomButtons";
            this.bottomButtons.Size = new System.Drawing.Size(606, 42);
            this.bottomButtons.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(502, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.CancelAndClose);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(394, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 32);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.SaveAndClose);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "키워드";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "분류";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // KeywordRuleForm
            // 
            this.AcceptButton = this.btnAdd;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.mainPanel);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.MinimumSize = new System.Drawing.Size(560, 420);
            this.Name = "KeywordRuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "키워드 분류 규칙";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRules)).EndInit();
            this.editorButtons.ResumeLayout(false);
            this.bottomButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}
