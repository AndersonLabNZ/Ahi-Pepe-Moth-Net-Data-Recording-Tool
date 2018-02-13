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
            this.buttonReturnEdit = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radioButtonRodents = new System.Windows.Forms.RadioButton();
            this.radioButtonMoths = new System.Windows.Forms.RadioButton();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.tableLayoutPanelMainMoths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMainMoths
            // 
            this.tableLayoutPanelMainMoths.ColumnCount = 5;
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelMainMoths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelMainMoths.Controls.Add(this.textBoxTag, 2, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.buttonSave, 3, 3);
            this.tableLayoutPanelMainMoths.Controls.Add(this.listViewMoths, 0, 0);
            this.tableLayoutPanelMainMoths.Controls.Add(this.buttonAdd, 4, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.comboBoxSpecies, 0, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.textBoxVoucher, 1, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.numericUpDownCount, 3, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.buttonReturnEdit, 0, 3);
            this.tableLayoutPanelMainMoths.Controls.Add(this.tableLayoutPanel1, 4, 0);
            this.tableLayoutPanelMainMoths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMainMoths.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMainMoths.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelMainMoths.Name = "tableLayoutPanelMainMoths";
            this.tableLayoutPanelMainMoths.RowCount = 4;
            this.tableLayoutPanelMainMoths.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMainMoths.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanelMainMoths.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanelMainMoths.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanelMainMoths.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMainMoths.Size = new System.Drawing.Size(948, 368);
            this.tableLayoutPanelMainMoths.TabIndex = 3;
            // 
            // textBoxTag
            // 
            this.textBoxTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTag.Location = new System.Drawing.Point(524, 308);
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(136, 20);
            this.textBoxTag.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.tableLayoutPanelMainMoths.SetColumnSpan(this.buttonSave, 2);
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSave.Location = new System.Drawing.Point(831, 342);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Finish and Save >>";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.tableLayoutPanelMainMoths.SetColumnSpan(this.listViewMoths, 4);
            this.listViewMoths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMoths.GridLines = true;
            this.listViewMoths.Location = new System.Drawing.Point(3, 3);
            this.listViewMoths.Name = "listViewMoths";
            this.listViewMoths.Size = new System.Drawing.Size(799, 299);
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
            this.buttonAdd.Location = new System.Drawing.Point(808, 308);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(137, 23);
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
            this.comboBoxSpecies.Location = new System.Drawing.Point(3, 308);
            this.comboBoxSpecies.Name = "comboBoxSpecies";
            this.comboBoxSpecies.Size = new System.Drawing.Size(373, 21);
            this.comboBoxSpecies.TabIndex = 3;
            // 
            // textBoxVoucher
            // 
            this.textBoxVoucher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxVoucher.Location = new System.Drawing.Point(382, 308);
            this.textBoxVoucher.Name = "textBoxVoucher";
            this.textBoxVoucher.Size = new System.Drawing.Size(136, 20);
            this.textBoxVoucher.TabIndex = 4;
            // 
            // numericUpDownCount
            // 
            this.numericUpDownCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownCount.Location = new System.Drawing.Point(666, 308);
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
            this.numericUpDownCount.Size = new System.Drawing.Size(136, 20);
            this.numericUpDownCount.TabIndex = 6;
            this.numericUpDownCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonReturnEdit
            // 
            this.buttonReturnEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonReturnEdit.Location = new System.Drawing.Point(3, 342);
            this.buttonReturnEdit.Name = "buttonReturnEdit";
            this.buttonReturnEdit.Size = new System.Drawing.Size(114, 23);
            this.buttonReturnEdit.TabIndex = 10;
            this.buttonReturnEdit.Text = "<< Edit Night Data";
            this.buttonReturnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonReturnEdit.UseVisualStyleBackColor = true;
            this.buttonReturnEdit.Click += new System.EventHandler(this.ReturnButtonClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.radioButtonRodents, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonMoths, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonRemove, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(805, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(143, 305);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // radioButtonRodents
            // 
            this.radioButtonRodents.AutoSize = true;
            this.radioButtonRodents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonRodents.Location = new System.Drawing.Point(3, 3);
            this.radioButtonRodents.Name = "radioButtonRodents";
            this.radioButtonRodents.Size = new System.Drawing.Size(137, 23);
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
            this.radioButtonMoths.Location = new System.Drawing.Point(3, 32);
            this.radioButtonMoths.Name = "radioButtonMoths";
            this.radioButtonMoths.Size = new System.Drawing.Size(137, 23);
            this.radioButtonMoths.TabIndex = 0;
            this.radioButtonMoths.TabStop = true;
            this.radioButtonMoths.Text = "Moths";
            this.radioButtonMoths.UseVisualStyleBackColor = true;
            this.radioButtonMoths.CheckedChanged += new System.EventHandler(this.CheckBoxCheckChange);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRemove.Location = new System.Drawing.Point(3, 279);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(137, 23);
            this.buttonRemove.TabIndex = 8;
            this.buttonRemove.Text = "Remove Selected";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.RemoveItemClicked);
            // 
            // InputData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 368);
            this.Controls.Add(this.tableLayoutPanelMainMoths);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(964, 407);
            this.Name = "InputData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Input Abundance Data";
            this.Resize += new System.EventHandler(this.FormResized);
            this.tableLayoutPanelMainMoths.ResumeLayout(false);
            this.tableLayoutPanelMainMoths.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Button buttonReturnEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}