namespace MothNet
{
    partial class NightForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NightForm));
            this.listBoxNights = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEditSite = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxNights
            // 
            resources.ApplyResources(this.listBoxNights, "listBoxNights");
            this.tableLayoutPanel1.SetColumnSpan(this.listBoxNights, 6);
            this.listBoxNights.Name = "listBoxNights";
            this.listBoxNights.DoubleClick += new System.EventHandler(this.ListBoxDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.buttonEdit, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonAdd, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.listBoxNights, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonEditSite, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonRemove, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonFinish, 5, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // buttonEdit
            // 
            resources.ApplyResources(this.buttonEdit, "buttonEdit");
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.EditButtonClick);
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.tableLayoutPanel1.SetColumnSpan(this.buttonAdd, 2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.AddButtonClick);
            // 
            // buttonEditSite
            // 
            resources.ApplyResources(this.buttonEditSite, "buttonEditSite");
            this.buttonEditSite.Name = "buttonEditSite";
            this.buttonEditSite.UseVisualStyleBackColor = true;
            this.buttonEditSite.Click += new System.EventHandler(this.EditSiteButtonClick);
            // 
            // buttonRemove
            // 
            resources.ApplyResources(this.buttonRemove, "buttonRemove");
            this.tableLayoutPanel1.SetColumnSpan(this.buttonRemove, 2);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.RemoveButtonClick);
            // 
            // buttonFinish
            // 
            resources.ApplyResources(this.buttonFinish, "buttonFinish");
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // NightForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NightForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonEditSite;
        private System.Windows.Forms.Button buttonEdit;
        public System.Windows.Forms.ListBox listBoxNights;
        private System.Windows.Forms.Button buttonFinish;
    }
}