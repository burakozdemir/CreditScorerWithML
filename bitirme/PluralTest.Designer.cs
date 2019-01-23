namespace bitirme
{
    partial class PluralTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluralTest));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectTestFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.testResult = new System.Windows.Forms.DataGridView();
            this.detailsButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.classificationPanel = new System.Windows.Forms.Panel();
            this.colTrainingLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewImageColumn();
            this.Predicted = new System.Windows.Forms.DataGridViewImageColumn();
            this.Clusters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testResult)).BeginInit();
            this.classificationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(591, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(169, 97);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(759, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectTestFileToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.helpToolStripMenuItem.Text = "File";
            // 
            // selectTestFileToolStripMenuItem
            // 
            this.selectTestFileToolStripMenuItem.Name = "selectTestFileToolStripMenuItem";
            this.selectTestFileToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.selectTestFileToolStripMenuItem.Text = "Select Test File";
            this.selectTestFileToolStripMenuItem.Click += new System.EventHandler(this.readyTest);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // testResult
            // 
            this.testResult.AllowUserToAddRows = false;
            this.testResult.AllowUserToDeleteRows = false;
            this.testResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.testResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.testResult.BackgroundColor = System.Drawing.SystemColors.Window;
            this.testResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.testResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.testResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.testResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTrainingLabel,
            this.data,
            this.Result,
            this.Predicted,
            this.Clusters});
            this.testResult.Location = new System.Drawing.Point(0, 0);
            this.testResult.Name = "testResult";
            this.testResult.ReadOnly = true;
            this.testResult.RowHeadersVisible = false;
            this.testResult.Size = new System.Drawing.Size(757, 452);
            this.testResult.TabIndex = 22;
            // 
            // detailsButton
            // 
            this.detailsButton.Location = new System.Drawing.Point(171, 38);
            this.detailsButton.Name = "detailsButton";
            this.detailsButton.Size = new System.Drawing.Size(114, 45);
            this.detailsButton.TabIndex = 4;
            this.detailsButton.Text = "Details";
            this.detailsButton.UseVisualStyleBackColor = true;
            this.detailsButton.Click += new System.EventHandler(this.visualization_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(27, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 45);
            this.button2.TabIndex = 5;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.startTest);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // classificationPanel
            // 
            this.classificationPanel.Controls.Add(this.testResult);
            this.classificationPanel.Location = new System.Drawing.Point(0, 103);
            this.classificationPanel.Name = "classificationPanel";
            this.classificationPanel.Size = new System.Drawing.Size(760, 455);
            this.classificationPanel.TabIndex = 24;
            // 
            // colTrainingLabel
            // 
            this.colTrainingLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTrainingLabel.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTrainingLabel.FillWeight = 10F;
            this.colTrainingLabel.HeaderText = "Index";
            this.colTrainingLabel.Name = "colTrainingLabel";
            this.colTrainingLabel.ReadOnly = true;
            // 
            // data
            // 
            this.data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.data.HeaderText = "Datas";
            this.data.Name = "data";
            this.data.ReadOnly = true;
            // 
            // Result
            // 
            this.Result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            this.Result.DefaultCellStyle = dataGridViewCellStyle3;
            this.Result.FillWeight = 10F;
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            // 
            // Predicted
            // 
            this.Predicted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Predicted.FillWeight = 10F;
            this.Predicted.HeaderText = "Predicted";
            this.Predicted.Name = "Predicted";
            this.Predicted.ReadOnly = true;
            // 
            // Clusters
            // 
            this.Clusters.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Clusters.FillWeight = 15F;
            this.Clusters.HeaderText = "Clusters";
            this.Clusters.Name = "Clusters";
            this.Clusters.ReadOnly = true;
            // 
            // PluralTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 558);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.detailsButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.classificationPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "PluralTest";
            this.Text = "PluralTest";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testResult)).EndInit();
            this.classificationPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.DataGridView testResult;
        private System.Windows.Forms.Button detailsButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem selectTestFileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel classificationPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrainingLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.DataGridViewImageColumn Result;
        private System.Windows.Forms.DataGridViewImageColumn Predicted;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clusters;
    }
}