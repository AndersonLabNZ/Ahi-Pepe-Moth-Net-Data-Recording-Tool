namespace MothNet
{
    partial class InputData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputData));
            this.tableLayoutPanelMainMoths = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.listViewMoths = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVoucher = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBoxSpecies = new System.Windows.Forms.ComboBox();
            this.textBoxVoucher = new System.Windows.Forms.TextBox();
            this.numericUpDownCount = new System.Windows.Forms.NumericUpDown();
            this.radioButtonRodents = new System.Windows.Forms.RadioButton();
            this.radioButtonMoths = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanelMainMoths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMainMoths
            // 
            this.tableLayoutPanelMainMoths.ColumnCount = 8;
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanelMainMoths.Controls.Add(this.buttonRemove, 6, 2);
            this.tableLayoutPanelMainMoths.Controls.Add(this.textBoxTag, 2, 2);
            this.tableLayoutPanelMainMoths.Controls.Add(this.buttonSave, 7, 2);
            this.tableLayoutPanelMainMoths.Controls.Add(this.listViewMoths, 0, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.buttonAdd, 4, 2);
            this.tableLayoutPanelMainMoths.Controls.Add(this.comboBoxSpecies, 0, 2);
            this.tableLayoutPanelMainMoths.Controls.Add(this.textBoxVoucher, 1, 2);
            this.tableLayoutPanelMainMoths.Controls.Add(this.numericUpDownCount, 3, 2);
            this.tableLayoutPanelMainMoths.Controls.Add(this.radioButtonRodents, 7, 0);
            this.tableLayoutPanelMainMoths.Controls.Add(this.radioButtonMoths, 6, 0);
            this.tableLayoutPanelMainMoths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMainMoths.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMainMoths.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelMainMoths.Name = "tableLayoutPanelMainMoths";
            this.tableLayoutPanelMainMoths.RowCount = 3;
            this.tableLayoutPanelMainMoths.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelMainMoths.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMainMoths.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelMainMoths.Size = new System.Drawing.Size(580, 293);
            this.tableLayoutPanelMainMoths.TabIndex = 3;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRemove.Location = new System.Drawing.Point(433, 270);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(69, 20);
            this.buttonRemove.TabIndex = 8;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.RemoveItemClicked);
            // 
            // textBoxTag
            // 
            this.textBoxTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTag.Location = new System.Drawing.Point(223, 270);
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(64, 20);
            this.textBoxTag.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(508, 270);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(69, 20);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.SaveButtonClicked);
            // 
            // listViewMoths
            // 
            this.listViewMoths.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderVoucher,
            this.columnHeaderTag,
            this.columnHeaderCount});
            this.tableLayoutPanelMainMoths.SetColumnSpan(this.listViewMoths, 8);
            this.listViewMoths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMoths.GridLines = true;
            this.listViewMoths.Location = new System.Drawing.Point(3, 29);
            this.listViewMoths.Name = "listViewMoths";
            this.listViewMoths.Size = new System.Drawing.Size(574, 235);
            this.listViewMoths.TabIndex = 2;
            this.listViewMoths.UseCompatibleStateImageBehavior = false;
            this.listViewMoths.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Species";
            this.columnHeaderName.Width = 180;
            // 
            // columnHeaderVoucher
            // 
            this.columnHeaderVoucher.Text = "Voucher Number";
            this.columnHeaderVoucher.Width = 120;
            // 
            // columnHeaderTag
            // 
            this.columnHeaderTag.Text = "Tag Name";
            this.columnHeaderTag.Width = 120;
            // 
            // columnHeaderCount
            // 
            this.columnHeaderCount.Text = "Count";
            this.columnHeaderCount.Width = 100;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAdd.Location = new System.Drawing.Point(363, 270);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(64, 20);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.AddButtonClicked);
            // 
            // comboBoxSpecies
            // 
            this.comboBoxSpecies.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSpecies.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSpecies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSpecies.FormattingEnabled = true;
            this.comboBoxSpecies.Location = new System.Drawing.Point(3, 270);
            this.comboBoxSpecies.Name = "comboBoxSpecies";
            this.comboBoxSpecies.Size = new System.Drawing.Size(144, 21);
            this.comboBoxSpecies.TabIndex = 3;
            // 
            // textBoxVoucher
            // 
            this.textBoxVoucher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxVoucher.Location = new System.Drawing.Point(153, 270);
            this.textBoxVoucher.Name = "textBoxVoucher";
            this.textBoxVoucher.Size = new System.Drawing.Size(64, 20);
            this.textBoxVoucher.TabIndex = 4;
            // 
            // numericUpDownCount
            // 
            this.numericUpDownCount.Location = new System.Drawing.Point(293, 270);
            this.numericUpDownCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCount.Name = "numericUpDownCount";
            this.numericUpDownCount.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownCount.TabIndex = 6;
            this.numericUpDownCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButtonRodents
            // 
            this.radioButtonRodents.AutoSize = true;
            this.radioButtonRodents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonRodents.Location = new System.Drawing.Point(508, 3);
            this.radioButtonRodents.Name = "radioButtonRodents";
            this.radioButtonRodents.Size = new System.Drawing.Size(69, 20);
            this.radioButtonRodents.TabIndex = 1;
            this.radioButtonRodents.Text = "Rodents";
            this.radioButtonRodents.UseVisualStyleBackColor = true;
            this.radioButtonRodents.CheckedChanged += new System.EventHandler(this.CheckBoxCheckChange);
            // 
            // radioButtonMoths
            // 
            this.radioButtonMoths.AutoSize = true;
            this.radioButtonMoths.Checked = true;
            this.radioButtonMoths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonMoths.Location = new System.Drawing.Point(433, 3);
            this.radioButtonMoths.Name = "radioButtonMoths";
            this.radioButtonMoths.Size = new System.Drawing.Size(69, 20);
            this.radioButtonMoths.TabIndex = 0;
            this.radioButtonMoths.TabStop = true;
            this.radioButtonMoths.Text = "Moths";
            this.radioButtonMoths.UseVisualStyleBackColor = true;
            this.radioButtonMoths.CheckedChanged += new System.EventHandler(this.CheckBoxCheckChange);
            // 
            // InputData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 293);
            this.Controls.Add(this.tableLayoutPanelMainMoths);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(596, 332);
            this.Name = "InputData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Input Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
            this.tableLayoutPanelMainMoths.ResumeLayout(false);
            this.tableLayoutPanelMainMoths.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMainMoths;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.TextBox textBoxTag;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderVoucher;
        private System.Windows.Forms.ColumnHeader columnHeaderTag;
        private System.Windows.Forms.ColumnHeader columnHeaderCount;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ComboBox comboBoxSpecies;
        private System.Windows.Forms.TextBox textBoxVoucher;
        private System.Windows.Forms.NumericUpDown numericUpDownCount;
        private System.Windows.Forms.RadioButton radioButtonRodents;
        private System.Windows.Forms.RadioButton radioButtonMoths;
        public System.Windows.Forms.ListView listViewMoths;
    }
}