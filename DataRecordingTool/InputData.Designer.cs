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
            resources.ApplyResources(this.tableLayoutPanelMainMoths, "tableLayoutPanelMainMoths");
            this.tableLayoutPanelMainMoths.Controls.Add(this.textBoxTag, 2, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.buttonSave, 3, 3);
            this.tableLayoutPanelMainMoths.Controls.Add(this.listViewMoths, 0, 0);
            this.tableLayoutPanelMainMoths.Controls.Add(this.buttonAdd, 4, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.comboBoxSpecies, 0, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.textBoxVoucher, 1, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.numericUpDownCount, 3, 1);
            this.tableLayoutPanelMainMoths.Controls.Add(this.buttonReturnEdit, 0, 3);
            this.tableLayoutPanelMainMoths.Controls.Add(this.tableLayoutPanel1, 4, 0);
            this.tableLayoutPanelMainMoths.Name = "tableLayoutPanelMainMoths";
            // 
            // textBoxTag
            // 
            resources.ApplyResources(this.textBoxTag, "textBoxTag");
            this.textBoxTag.Name = "textBoxTag";
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.tableLayoutPanelMainMoths.SetColumnSpan(this.buttonSave, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.SaveButtonClicked);
            // 
            // listViewMoths
            // 
            resources.ApplyResources(this.listViewMoths, "listViewMoths");
            this.listViewMoths.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderVoucher,
            this.columnHeaderTag,
            this.columnHeaderCount});
            this.tableLayoutPanelMainMoths.SetColumnSpan(this.listViewMoths, 4);
            this.listViewMoths.GridLines = true;
            this.listViewMoths.Name = "listViewMoths";
            this.listViewMoths.UseCompatibleStateImageBehavior = false;
            this.listViewMoths.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderVoucher
            // 
            resources.ApplyResources(this.columnHeaderVoucher, "columnHeaderVoucher");
            // 
            // columnHeaderTag
            // 
            resources.ApplyResources(this.columnHeaderTag, "columnHeaderTag");
            // 
            // columnHeaderCount
            // 
            resources.ApplyResources(this.columnHeaderCount, "columnHeaderCount");
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.AddButtonClicked);
            // 
            // comboBoxSpecies
            // 
            resources.ApplyResources(this.comboBoxSpecies, "comboBoxSpecies");
            this.comboBoxSpecies.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSpecies.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSpecies.FormattingEnabled = true;
            this.comboBoxSpecies.Name = "comboBoxSpecies";
            // 
            // textBoxVoucher
            // 
            resources.ApplyResources(this.textBoxVoucher, "textBoxVoucher");
            this.textBoxVoucher.Name = "textBoxVoucher";
            // 
            // numericUpDownCount
            // 
            resources.ApplyResources(this.numericUpDownCount, "numericUpDownCount");
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
            this.numericUpDownCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonReturnEdit
            // 
            resources.ApplyResources(this.buttonReturnEdit, "buttonReturnEdit");
            this.buttonReturnEdit.Name = "buttonReturnEdit";
            this.buttonReturnEdit.UseVisualStyleBackColor = true;
            this.buttonReturnEdit.Click += new System.EventHandler(this.ReturnButtonClick);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.radioButtonRodents, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonMoths, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonRemove, 0, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // radioButtonRodents
            // 
            resources.ApplyResources(this.radioButtonRodents, "radioButtonRodents");
            this.radioButtonRodents.Name = "radioButtonRodents";
            this.radioButtonRodents.UseVisualStyleBackColor = true;
            this.radioButtonRodents.CheckedChanged += new System.EventHandler(this.CheckBoxCheckChange);
            // 
            // radioButtonMoths
            // 
            resources.ApplyResources(this.radioButtonMoths, "radioButtonMoths");
            this.radioButtonMoths.Checked = true;
            this.radioButtonMoths.Name = "radioButtonMoths";
            this.radioButtonMoths.TabStop = true;
            this.radioButtonMoths.UseVisualStyleBackColor = true;
            this.radioButtonMoths.CheckedChanged += new System.EventHandler(this.CheckBoxCheckChange);
            // 
            // buttonRemove
            // 
            resources.ApplyResources(this.buttonRemove, "buttonRemove");
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.RemoveItemClicked);
            // 
            // InputData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelMainMoths);
            this.Name = "InputData";
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