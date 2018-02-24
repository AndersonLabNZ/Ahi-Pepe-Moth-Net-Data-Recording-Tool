namespace MothNet
{
    partial class EditSite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSite));
            this.buttonNight = new System.Windows.Forms.Button();
            this.comboBoxShrubDistance = new System.Windows.Forms.ComboBox();
            this.comboBoxCanopyHeight = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelData = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxSlope = new System.Windows.Forms.TextBox();
            this.textBoxAltitude = new System.Windows.Forms.TextBox();
            this.textBoxAccuracy = new System.Windows.Forms.TextBox();
            this.textBoxLongitude = new System.Windows.Forms.TextBox();
            this.textBoxLatitude = new System.Windows.Forms.TextBox();
            this.comboBoxPredatorRemoval = new System.Windows.Forms.ComboBox();
            this.comboBoxVegetationRestoration = new System.Windows.Forms.ComboBox();
            this.labelPredatorRemove = new System.Windows.Forms.Label();
            this.labelVegetationRestore = new System.Windows.Forms.Label();
            this.labelAccuracy = new System.Windows.Forms.Label();
            this.comboBoxSubRegions = new System.Windows.Forms.ComboBox();
            this.comboBoxRegions = new System.Windows.Forms.ComboBox();
            this.comboBoxAspect = new System.Windows.Forms.ComboBox();
            this.labelShrub = new System.Windows.Forms.Label();
            this.labelVegetation = new System.Windows.Forms.Label();
            this.labelCanopy = new System.Windows.Forms.Label();
            this.labelAspect = new System.Windows.Forms.Label();
            this.labelSlope = new System.Windows.Forms.Label();
            this.labelLong = new System.Windows.Forms.Label();
            this.labelLat = new System.Windows.Forms.Label();
            this.labelAltitude = new System.Windows.Forms.Label();
            this.labelSubregion = new System.Windows.Forms.Label();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.textBoxSiteName = new System.Windows.Forms.TextBox();
            this.comboBoxSurroundingVegetation = new System.Windows.Forms.ComboBox();
            this.labelRegion = new System.Windows.Forms.Label();
            this.labelSiteType = new System.Windows.Forms.Label();
            this.labelSiteName = new System.Windows.Forms.Label();
            this.labelLocation = new System.Windows.Forms.Label();
            this.comboBoxSiteType = new System.Windows.Forms.ComboBox();
            this.textBoxSchoolGroup = new System.Windows.Forms.TextBox();
            this.labelSchoolGroup = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSaveSite = new System.Windows.Forms.Button();
            this.tableLayoutPanelData.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNight
            // 
            resources.ApplyResources(this.buttonNight, "buttonNight");
            this.buttonNight.Name = "buttonNight";
            this.buttonNight.UseVisualStyleBackColor = true;
            this.buttonNight.Click += new System.EventHandler(this.ButtonNightClick);
            // 
            // comboBoxShrubDistance
            // 
            resources.ApplyResources(this.comboBoxShrubDistance, "comboBoxShrubDistance");
            this.comboBoxShrubDistance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShrubDistance.FormattingEnabled = true;
            this.comboBoxShrubDistance.Name = "comboBoxShrubDistance";
            this.comboBoxShrubDistance.Tag = "DISTANCE_TO_NEAREST_SHRUB_M";
            this.comboBoxShrubDistance.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // comboBoxCanopyHeight
            // 
            resources.ApplyResources(this.comboBoxCanopyHeight, "comboBoxCanopyHeight");
            this.comboBoxCanopyHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCanopyHeight.FormattingEnabled = true;
            this.comboBoxCanopyHeight.Name = "comboBoxCanopyHeight";
            this.comboBoxCanopyHeight.Tag = "HEIGHT_OF_SURROUNDING_VEG_M";
            this.comboBoxCanopyHeight.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // tableLayoutPanelData
            // 
            resources.ApplyResources(this.tableLayoutPanelData, "tableLayoutPanelData");
            this.tableLayoutPanelData.Controls.Add(this.textBoxSlope, 1, 12);
            this.tableLayoutPanelData.Controls.Add(this.textBoxAltitude, 1, 11);
            this.tableLayoutPanelData.Controls.Add(this.textBoxAccuracy, 1, 10);
            this.tableLayoutPanelData.Controls.Add(this.textBoxLongitude, 1, 9);
            this.tableLayoutPanelData.Controls.Add(this.textBoxLatitude, 1, 8);
            this.tableLayoutPanelData.Controls.Add(this.comboBoxPredatorRemoval, 1, 5);
            this.tableLayoutPanelData.Controls.Add(this.comboBoxVegetationRestoration, 1, 4);
            this.tableLayoutPanelData.Controls.Add(this.labelPredatorRemove, 0, 5);
            this.tableLayoutPanelData.Controls.Add(this.labelVegetationRestore, 0, 4);
            this.tableLayoutPanelData.Controls.Add(this.labelAccuracy, 0, 10);
            this.tableLayoutPanelData.Controls.Add(this.comboBoxSubRegions, 1, 7);
            this.tableLayoutPanelData.Controls.Add(this.comboBoxRegions, 1, 6);
            this.tableLayoutPanelData.Controls.Add(this.comboBoxAspect, 1, 13);
            this.tableLayoutPanelData.Controls.Add(this.labelShrub, 0, 16);
            this.tableLayoutPanelData.Controls.Add(this.labelVegetation, 0, 14);
            this.tableLayoutPanelData.Controls.Add(this.labelCanopy, 0, 15);
            this.tableLayoutPanelData.Controls.Add(this.labelAspect, 0, 13);
            this.tableLayoutPanelData.Controls.Add(this.labelSlope, 0, 12);
            this.tableLayoutPanelData.Controls.Add(this.labelLong, 0, 9);
            this.tableLayoutPanelData.Controls.Add(this.labelLat, 0, 8);
            this.tableLayoutPanelData.Controls.Add(this.labelAltitude, 0, 11);
            this.tableLayoutPanelData.Controls.Add(this.labelSubregion, 0, 7);
            this.tableLayoutPanelData.Controls.Add(this.textBoxLocation, 1, 1);
            this.tableLayoutPanelData.Controls.Add(this.textBoxSiteName, 1, 3);
            this.tableLayoutPanelData.Controls.Add(this.comboBoxSurroundingVegetation, 1, 14);
            this.tableLayoutPanelData.Controls.Add(this.labelRegion, 0, 6);
            this.tableLayoutPanelData.Controls.Add(this.labelSiteType, 0, 2);
            this.tableLayoutPanelData.Controls.Add(this.labelSiteName, 0, 3);
            this.tableLayoutPanelData.Controls.Add(this.labelLocation, 0, 1);
            this.tableLayoutPanelData.Controls.Add(this.comboBoxSiteType, 1, 2);
            this.tableLayoutPanelData.Controls.Add(this.comboBoxCanopyHeight, 1, 15);
            this.tableLayoutPanelData.Controls.Add(this.comboBoxShrubDistance, 1, 16);
            this.tableLayoutPanelData.Controls.Add(this.textBoxSchoolGroup, 1, 0);
            this.tableLayoutPanelData.Controls.Add(this.labelSchoolGroup, 0, 0);
            this.tableLayoutPanelData.Name = "tableLayoutPanelData";
            // 
            // textBoxSlope
            // 
            resources.ApplyResources(this.textBoxSlope, "textBoxSlope");
            this.textBoxSlope.Name = "textBoxSlope";
            this.textBoxSlope.Tag = "SLOPE_DEG";
            this.textBoxSlope.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // textBoxAltitude
            // 
            resources.ApplyResources(this.textBoxAltitude, "textBoxAltitude");
            this.textBoxAltitude.Name = "textBoxAltitude";
            this.textBoxAltitude.Tag = "ALTITUDE_M";
            this.textBoxAltitude.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // textBoxAccuracy
            // 
            resources.ApplyResources(this.textBoxAccuracy, "textBoxAccuracy");
            this.textBoxAccuracy.Name = "textBoxAccuracy";
            this.textBoxAccuracy.Tag = "ACCURACY_M";
            this.textBoxAccuracy.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // textBoxLongitude
            // 
            resources.ApplyResources(this.textBoxLongitude, "textBoxLongitude");
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Tag = "LONG_DECDEG";
            this.textBoxLongitude.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // textBoxLatitude
            // 
            resources.ApplyResources(this.textBoxLatitude, "textBoxLatitude");
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.Tag = "LAT_DECDEG";
            this.textBoxLatitude.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // comboBoxPredatorRemoval
            // 
            resources.ApplyResources(this.comboBoxPredatorRemoval, "comboBoxPredatorRemoval");
            this.comboBoxPredatorRemoval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPredatorRemoval.FormattingEnabled = true;
            this.comboBoxPredatorRemoval.Name = "comboBoxPredatorRemoval";
            this.comboBoxPredatorRemoval.Tag = "PREDATOR_REMOVAL_REGIME";
            // 
            // comboBoxVegetationRestoration
            // 
            resources.ApplyResources(this.comboBoxVegetationRestoration, "comboBoxVegetationRestoration");
            this.comboBoxVegetationRestoration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVegetationRestoration.FormattingEnabled = true;
            this.comboBoxVegetationRestoration.Name = "comboBoxVegetationRestoration";
            this.comboBoxVegetationRestoration.Tag = "VEG_RES_REGIME";
            // 
            // labelPredatorRemove
            // 
            resources.ApplyResources(this.labelPredatorRemove, "labelPredatorRemove");
            this.labelPredatorRemove.Name = "labelPredatorRemove";
            // 
            // labelVegetationRestore
            // 
            resources.ApplyResources(this.labelVegetationRestore, "labelVegetationRestore");
            this.labelVegetationRestore.Name = "labelVegetationRestore";
            // 
            // labelAccuracy
            // 
            resources.ApplyResources(this.labelAccuracy, "labelAccuracy");
            this.labelAccuracy.Name = "labelAccuracy";
            // 
            // comboBoxSubRegions
            // 
            resources.ApplyResources(this.comboBoxSubRegions, "comboBoxSubRegions");
            this.comboBoxSubRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSubRegions.FormattingEnabled = true;
            this.comboBoxSubRegions.Name = "comboBoxSubRegions";
            this.comboBoxSubRegions.Tag = "SUB_REGION";
            // 
            // comboBoxRegions
            // 
            resources.ApplyResources(this.comboBoxRegions, "comboBoxRegions");
            this.comboBoxRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRegions.FormattingEnabled = true;
            this.comboBoxRegions.Name = "comboBoxRegions";
            this.comboBoxRegions.Tag = "REGION";
            this.comboBoxRegions.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // comboBoxAspect
            // 
            resources.ApplyResources(this.comboBoxAspect, "comboBoxAspect");
            this.comboBoxAspect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAspect.FormattingEnabled = true;
            this.comboBoxAspect.Name = "comboBoxAspect";
            this.comboBoxAspect.Tag = "ASPECT";
            this.comboBoxAspect.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // labelShrub
            // 
            resources.ApplyResources(this.labelShrub, "labelShrub");
            this.labelShrub.Name = "labelShrub";
            // 
            // labelVegetation
            // 
            resources.ApplyResources(this.labelVegetation, "labelVegetation");
            this.labelVegetation.Name = "labelVegetation";
            // 
            // labelCanopy
            // 
            resources.ApplyResources(this.labelCanopy, "labelCanopy");
            this.labelCanopy.Name = "labelCanopy";
            // 
            // labelAspect
            // 
            resources.ApplyResources(this.labelAspect, "labelAspect");
            this.labelAspect.Name = "labelAspect";
            // 
            // labelSlope
            // 
            resources.ApplyResources(this.labelSlope, "labelSlope");
            this.labelSlope.Name = "labelSlope";
            // 
            // labelLong
            // 
            resources.ApplyResources(this.labelLong, "labelLong");
            this.labelLong.Name = "labelLong";
            this.labelLong.Tag = "";
            // 
            // labelLat
            // 
            resources.ApplyResources(this.labelLat, "labelLat");
            this.labelLat.Name = "labelLat";
            // 
            // labelAltitude
            // 
            resources.ApplyResources(this.labelAltitude, "labelAltitude");
            this.labelAltitude.Name = "labelAltitude";
            // 
            // labelSubregion
            // 
            resources.ApplyResources(this.labelSubregion, "labelSubregion");
            this.labelSubregion.Name = "labelSubregion";
            // 
            // textBoxLocation
            // 
            resources.ApplyResources(this.textBoxLocation, "textBoxLocation");
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Tag = "LOCATION";
            this.textBoxLocation.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // textBoxSiteName
            // 
            resources.ApplyResources(this.textBoxSiteName, "textBoxSiteName");
            this.textBoxSiteName.Name = "textBoxSiteName";
            this.textBoxSiteName.ReadOnly = true;
            this.textBoxSiteName.Tag = "SITE_NAME";
            this.textBoxSiteName.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // comboBoxSurroundingVegetation
            // 
            resources.ApplyResources(this.comboBoxSurroundingVegetation, "comboBoxSurroundingVegetation");
            this.comboBoxSurroundingVegetation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSurroundingVegetation.FormattingEnabled = true;
            this.comboBoxSurroundingVegetation.Name = "comboBoxSurroundingVegetation";
            this.comboBoxSurroundingVegetation.Tag = "SURROUNDING_VEG";
            this.comboBoxSurroundingVegetation.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // labelRegion
            // 
            resources.ApplyResources(this.labelRegion, "labelRegion");
            this.labelRegion.Name = "labelRegion";
            // 
            // labelSiteType
            // 
            resources.ApplyResources(this.labelSiteType, "labelSiteType");
            this.labelSiteType.Name = "labelSiteType";
            // 
            // labelSiteName
            // 
            resources.ApplyResources(this.labelSiteName, "labelSiteName");
            this.labelSiteName.Name = "labelSiteName";
            // 
            // labelLocation
            // 
            resources.ApplyResources(this.labelLocation, "labelLocation");
            this.labelLocation.Name = "labelLocation";
            // 
            // comboBoxSiteType
            // 
            resources.ApplyResources(this.comboBoxSiteType, "comboBoxSiteType");
            this.comboBoxSiteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSiteType.FormattingEnabled = true;
            this.comboBoxSiteType.Name = "comboBoxSiteType";
            this.comboBoxSiteType.Tag = "SITE_TYPE";
            this.comboBoxSiteType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // textBoxSchoolGroup
            // 
            resources.ApplyResources(this.textBoxSchoolGroup, "textBoxSchoolGroup");
            this.textBoxSchoolGroup.Name = "textBoxSchoolGroup";
            this.textBoxSchoolGroup.Tag = "SCHOOL_GROUP";
            this.textBoxSchoolGroup.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // labelSchoolGroup
            // 
            resources.ApplyResources(this.labelSchoolGroup, "labelSchoolGroup");
            this.labelSchoolGroup.Name = "labelSchoolGroup";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelData, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelButtons, 0, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanelButtons
            // 
            resources.ApplyResources(this.tableLayoutPanelButtons, "tableLayoutPanelButtons");
            this.tableLayoutPanelButtons.Controls.Add(this.buttonSaveSite, 0, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.buttonNight, 2, 0);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            // 
            // buttonSaveSite
            // 
            resources.ApplyResources(this.buttonSaveSite, "buttonSaveSite");
            this.buttonSaveSite.Name = "buttonSaveSite";
            this.buttonSaveSite.UseVisualStyleBackColor = true;
            this.buttonSaveSite.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // EditSite
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "EditSite";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
            this.tableLayoutPanelData.ResumeLayout(false);
            this.tableLayoutPanelData.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelRegion;
        private System.Windows.Forms.Label labelSiteType;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Label labelSchoolGroup;
        private System.Windows.Forms.Label labelSiteName;
        private System.Windows.Forms.Label labelSubregion;
        private System.Windows.Forms.Label labelShrub;
        private System.Windows.Forms.Label labelVegetation;
        private System.Windows.Forms.Label labelCanopy;
        private System.Windows.Forms.Label labelAspect;
        private System.Windows.Forms.Label labelSlope;
        private System.Windows.Forms.Label labelLong;
        private System.Windows.Forms.Label labelLat;
        private System.Windows.Forms.Label labelAltitude;
        private System.Windows.Forms.Label labelAccuracy;
        private System.Windows.Forms.Label labelPredatorRemove;
        private System.Windows.Forms.Label labelVegetationRestore;
        public System.Windows.Forms.ComboBox comboBoxShrubDistance;
        public System.Windows.Forms.ComboBox comboBoxCanopyHeight;
        public System.Windows.Forms.ComboBox comboBoxSiteType;
        public System.Windows.Forms.ComboBox comboBoxSurroundingVegetation;
        public System.Windows.Forms.TextBox textBoxSchoolGroup;
        public System.Windows.Forms.TextBox textBoxSiteName;
        public System.Windows.Forms.TextBox textBoxLocation;
        public System.Windows.Forms.ComboBox comboBoxAspect;
        public System.Windows.Forms.ComboBox comboBoxSubRegions;
        public System.Windows.Forms.ComboBox comboBoxRegions;
        public System.Windows.Forms.ComboBox comboBoxPredatorRemoval;
        public System.Windows.Forms.ComboBox comboBoxVegetationRestoration;
        public System.Windows.Forms.TextBox textBoxSlope;
        public System.Windows.Forms.TextBox textBoxAltitude;
        public System.Windows.Forms.TextBox textBoxAccuracy;
        public System.Windows.Forms.TextBox textBoxLongitude;
        public System.Windows.Forms.TextBox textBoxLatitude;
        private System.Windows.Forms.Button buttonSaveSite;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
    }
}