namespace MothNet
{
    partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.buttonCreateSite = new System.Windows.Forms.Button();
            this.buttonOpenSite = new System.Windows.Forms.Button();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreateSite
            // 
            resources.ApplyResources(this.buttonCreateSite, "buttonCreateSite");
            this.buttonCreateSite.Name = "buttonCreateSite";
            this.buttonCreateSite.UseVisualStyleBackColor = true;
            this.buttonCreateSite.Click += new System.EventHandler(this.CreateSiteClick);
            // 
            // buttonOpenSite
            // 
            resources.ApplyResources(this.buttonOpenSite, "buttonOpenSite");
            this.buttonOpenSite.Name = "buttonOpenSite";
            this.buttonOpenSite.UseVisualStyleBackColor = true;
            this.buttonOpenSite.Click += new System.EventHandler(this.ModifySiteClick);
            // 
            // pictureBoxImage
            // 
            resources.ApplyResources(this.pictureBoxImage, "pictureBoxImage");
            this.pictureBoxImage.BackgroundImage = global::MothNet.Properties.Resources.Ahi_Pepe___Resized;
            this.pictureBoxImage.InitialImage = global::MothNet.Properties.Resources.Ahi_Pepe___Resized;
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.TabStop = false;
            // 
            // StartForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.buttonOpenSite);
            this.Controls.Add(this.buttonCreateSite);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateSite;
        private System.Windows.Forms.Button buttonOpenSite;
        private System.Windows.Forms.PictureBox pictureBoxImage;
    }
}

