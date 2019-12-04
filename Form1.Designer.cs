namespace SICSim
{
    partial class SicSimForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.codeInput = new System.Windows.Forms.TextBox();
            this.regText = new System.Windows.Forms.TextBox();
            this.introText1 = new System.Windows.Forms.Label();
            this.introText2 = new System.Windows.Forms.Label();
            this.memView = new System.Windows.Forms.DataGridView();
            this.memAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MemLabl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memValues = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.instrBtn = new System.Windows.Forms.Button();
            this.fileBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.memView)).BeginInit();
            this.SuspendLayout();
            // 
            // codeInput
            // 
            this.codeInput.AcceptsReturn = true;
            this.codeInput.AcceptsTab = true;
            this.codeInput.Location = new System.Drawing.Point(344, 89);
            this.codeInput.Multiline = true;
            this.codeInput.Name = "codeInput";
            this.codeInput.Size = new System.Drawing.Size(520, 620);
            this.codeInput.TabIndex = 0;
            this.codeInput.UseWaitCursor = true;
            // 
            // regText
            // 
            this.regText.Location = new System.Drawing.Point(13, 9);
            this.regText.Multiline = true;
            this.regText.Name = "regText";
            this.regText.ReadOnly = true;
            this.regText.Size = new System.Drawing.Size(322, 700);
            this.regText.TabIndex = 2;
            this.regText.Text = "Registers:";
            this.regText.UseWaitCursor = true;
            // 
            // introText1
            // 
            this.introText1.AutoSize = true;
            this.introText1.Location = new System.Drawing.Point(341, 9);
            this.introText1.Name = "introText1";
            this.introText1.Size = new System.Drawing.Size(454, 17);
            this.introText1.TabIndex = 5;
            this.introText1.Text = "Welcome to SICSim SIC Assembly Simulator! Enter you SIC code below.";
            this.introText1.UseWaitCursor = true;
            // 
            // introText2
            // 
            this.introText2.AutoSize = true;
            this.introText2.Location = new System.Drawing.Point(341, 26);
            this.introText2.Name = "introText2";
            this.introText2.Size = new System.Drawing.Size(362, 17);
            this.introText2.TabIndex = 6;
            this.introText2.Text = "You can also view register values and memory contents.";
            this.introText2.UseWaitCursor = true;
            // 
            // memView
            // 
            this.memView.AllowUserToAddRows = false;
            this.memView.AllowUserToDeleteRows = false;
            this.memView.AllowUserToResizeColumns = false;
            this.memView.AllowUserToResizeRows = false;
            this.memView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.memView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.memView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.memAddr,
            this.MemLabl,
            this.memValues});
            this.memView.Location = new System.Drawing.Point(870, 9);
            this.memView.Name = "memView";
            this.memView.ReadOnly = true;
            this.memView.RowHeadersWidth = 51;
            this.memView.RowTemplate.Height = 24;
            this.memView.RowTemplate.ReadOnly = true;
            this.memView.Size = new System.Drawing.Size(466, 700);
            this.memView.TabIndex = 7;
            this.memView.UseWaitCursor = true;
            // 
            // memAddr
            // 
            this.memAddr.HeaderText = "Address";
            this.memAddr.MinimumWidth = 6;
            this.memAddr.Name = "memAddr";
            this.memAddr.ReadOnly = true;
            this.memAddr.Width = 89;
            // 
            // MemLabl
            // 
            this.MemLabl.HeaderText = "Labels";
            this.MemLabl.MinimumWidth = 6;
            this.MemLabl.Name = "MemLabl";
            this.MemLabl.ReadOnly = true;
            this.MemLabl.Width = 79;
            // 
            // memValues
            // 
            this.memValues.HeaderText = "Values";
            this.memValues.MinimumWidth = 6;
            this.memValues.Name = "memValues";
            this.memValues.ReadOnly = true;
            this.memValues.Width = 80;
            // 
            // instrBtn
            // 
            this.instrBtn.Location = new System.Drawing.Point(729, 51);
            this.instrBtn.Name = "instrBtn";
            this.instrBtn.Size = new System.Drawing.Size(135, 32);
            this.instrBtn.TabIndex = 12;
            this.instrBtn.Text = "Run Instructions";
            this.instrBtn.UseVisualStyleBackColor = true;
            this.instrBtn.UseWaitCursor = true;
            this.instrBtn.Click += new System.EventHandler(this.instrBtn_Click);
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(344, 51);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(95, 32);
            this.fileBtn.TabIndex = 13;
            this.fileBtn.Text = "Import File";
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.UseWaitCursor = true;
            this.fileBtn.Click += new System.EventHandler(this.fileBtn_Click);
            // 
            // SicSimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.fileBtn);
            this.Controls.Add(this.instrBtn);
            this.Controls.Add(this.memView);
            this.Controls.Add(this.introText2);
            this.Controls.Add(this.introText1);
            this.Controls.Add(this.regText);
            this.Controls.Add(this.codeInput);
            this.Name = "SicSimForm";
            this.Text = "SICSim";
            this.UseWaitCursor = true;
            ((System.ComponentModel.ISupportInitialize)(this.memView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox codeInput;
        private System.Windows.Forms.TextBox regText;
        private System.Windows.Forms.Label introText1;
        private System.Windows.Forms.Label introText2;
        private System.Windows.Forms.DataGridView memView;
        private System.Windows.Forms.DataGridViewTextBoxColumn memAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemLabl;
        private System.Windows.Forms.DataGridViewTextBoxColumn memValues;
        private System.Windows.Forms.Button instrBtn;
        private System.Windows.Forms.Button fileBtn;
    }
}

