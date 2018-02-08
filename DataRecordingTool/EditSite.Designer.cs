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
            this.buttonSaveSite = new System.Windows.Forms.Button();
            this.tableLayoutPanelData.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNight
            // 
            this.buttonNight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonNight.Location = new System.Drawing.Point(3, 445);
            this.buttonNight.Name = "buttonNight";
            this.buttonNight.Size = new System.Drawing.Size(474, 23);
            this.buttonNight.TabIndex = 16;
            this.buttonNight.Text = "Input Night Data...";
            this.buttonNight.UseVisualStyleBackColor = true;
            this.buttonNight.Click += new System.EventHandler(this.ButtonNightClick);
            // 
            // comboBoxShrubDistance
            // 
            this.comboBoxShrubDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxShrubDistance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShrubDistance.FormattingEnabled = true;
            this.comboBoxShrubDistance.Location = new System.Drawing.Point(203, 419);
            this.comboBoxShrubDistance.Name = "comboBoxShrubDistance";
            this.comboBoxShrubDistance.Size = new System.Drawing.Size(274, 21);
            this.comboBoxShrubDistance.TabIndex = 16;
            this.comboBoxShrubDistance.Tag = "DISTANCE_TO_NEAREST_SHRUB_M";
            this.comboBoxShrubDistance.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // comboBoxCanopyHeight
            // 
            this.comboBoxCanopyHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxCanopyHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCanopyHeight.FormattingEnabled = true;
            this.comboBoxCanopyHeight.Location = new System.Drawing.Point(203, 393);
            this.comboBoxCanopyHeight.Name = "comboBoxCanopyHeight";
            this.comboBoxCanopyHeight.Size = new System.Drawing.Size(274, 21);
            this.comboBoxCanopyHeight.TabIndex = 15;
            this.comboBoxCanopyHeight.Tag = "HEIGHT_OF_SURROUNDING_VEG_M";
            this.comboBoxCanopyHeight.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // tableLayoutPanelData
            // 
            this.tableLayoutPanelData.ColumnCount = 2;
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            this.tableLayoutPanelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelData.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelData.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelData.Name = "tableLayoutPanelData";
            this.tableLayoutPanelData.RowCount = 17;
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelData.Size = new System.Drawing.Size(480, 442);
            this.tableLayoutPanelData.TabIndex = 7;
            // 
            // textBoxSlope
            // 
            this.textBoxSlope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSlope.Location = new System.Drawing.Point(203, 315);
            this.textBoxSlope.Name = "textBoxSlope";
            this.textBoxSlope.Size = new System.Drawing.Size(274, 20);
            this.textBoxSlope.TabIndex = 12;
            this.textBoxSlope.Tag = "SLOPE_DEG";
            this.textBoxSlope.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // textBoxAltitude
            // 
            this.textBoxAltitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAltitude.Location = new System.Drawing.Point(203, 289);
            this.textBoxAltitude.Name = "textBoxAltitude";
            this.textBoxAltitude.Size = new System.Drawing.Size(274, 20);
            this.textBoxAltitude.TabIndex = 11;
            this.textBoxAltitude.Tag = "ALTITUDE_M";
            this.textBoxAltitude.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // textBoxAccuracy
            // 
            this.textBoxAccuracy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAccuracy.Location = new System.Drawing.Point(203, 263);
            this.textBoxAccuracy.Name = "textBoxAccuracy";
            this.textBoxAccuracy.Size = new System.Drawing.Size(274, 20);
            this.textBoxAccuracy.TabIndex = 10;
            this.textBoxAccuracy.Tag = "ACCURACY_M";
            this.textBoxAccuracy.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLongitude.Location = new System.Drawing.Point(203, 237);
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Size = new System.Drawing.Size(274, 20);
            this.textBoxLongitude.TabIndex = 9;
            this.textBoxLongitude.Tag = "LONG_DECDEG";
            this.textBoxLongitude.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLatitude.Location = new System.Drawing.Point(203, 211);
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.Size = new System.Drawing.Size(274, 20);
            this.textBoxLatitude.TabIndex = 8;
            this.textBoxLatitude.Tag = "LAT_DECDEG";
            this.textBoxLatitude.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // comboBoxPredatorRemoval
            // 
            this.comboBoxPredatorRemoval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxPredatorRemoval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPredatorRemoval.FormattingEnabled = true;
            this.comboBoxPredatorRemoval.Location = new System.Drawing.Point(203, 133);
            this.comboBoxPredatorRemoval.Name = "comboBoxPredatorRemoval";
            this.comboBoxPredatorRemoval.Size = new System.Drawing.Size(274, 21);
            this.comboBoxPredatorRemoval.TabIndex = 5;
            this.comboBoxPredatorRemoval.Tag = "PREDATOR_REMOVAL_REGIME";
            // 
            // comboBoxVegetationRestoration
            // 
            this.comboBoxVegetationRestoration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxVegetationRestoration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVegetationRestoration.FormattingEnabled = true;
            this.comboBoxVegetationRestoration.Location = new System.Drawing.Point(203, 107);
            this.comboBoxVegetationRestoration.Name = "comboBoxVegetationRestoration";
            this.comboBoxVegetationRestoration.Size = new System.Drawing.Size(274, 21);
            this.comboBoxVegetationRestoration.TabIndex = 4;
            this.comboBoxVegetationRestoration.Tag = "VEG_RES_REGIME";
            // 
            // labelPredatorRemove
            // 
            this.labelPredatorRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPredatorRemove.Location = new System.Drawing.Point(3, 133);
            this.labelPredatorRemove.Margin = new System.Windows.Forms.Padding(3);
            this.labelPredatorRemove.Name = "labelPredatorRemove";
            this.labelPredatorRemove.Size = new System.Drawing.Size(194, 20);
            this.labelPredatorRemove.TabIndex = 34;
            this.labelPredatorRemove.Text = "Predator Removal Regime";
            this.labelPredatorRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelVegetationRestore
            // 
            this.labelVegetationRestore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVegetationRestore.Location = new System.Drawing.Point(3, 107);
            this.labelVegetationRestore.Margin = new System.Windows.Forms.Padding(3);
            this.labelVegetationRestore.Name = "labelVegetationRestore";
            this.labelVegetationRestore.Size = new System.Drawing.Size(194, 20);
            this.labelVegetationRestore.TabIndex = 33;
            this.labelVegetationRestore.Text = "Vegetation Restoration Regime";
            this.labelVegetationRestore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAccuracy
            // 
            this.labelAccuracy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAccuracy.Location = new System.Drawing.Point(3, 263);
            this.labelAccuracy.Margin = new System.Windows.Forms.Padding(3);
            this.labelAccuracy.Name = "labelAccuracy";
            this.labelAccuracy.Size = new System.Drawing.Size(194, 20);
            this.labelAccuracy.TabIndex = 32;
            this.labelAccuracy.Text = "Accuracy (m)";
            this.labelAccuracy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSubRegions
            // 
            this.comboBoxSubRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSubRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSubRegions.FormattingEnabled = true;
            this.comboBoxSubRegions.Location = new System.Drawing.Point(203, 185);
            this.comboBoxSubRegions.Name = "comboBoxSubRegions";
            this.comboBoxSubRegions.Size = new System.Drawing.Size(274, 21);
            this.comboBoxSubRegions.TabIndex = 7;
            this.comboBoxSubRegions.Tag = "SUB_REGION";
            // 
            // comboBoxRegions
            // 
            this.comboBoxRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRegions.FormattingEnabled = true;
            this.comboBoxRegions.Location = new System.Drawing.Point(203, 159);
            this.comboBoxRegions.Name = "comboBoxRegions";
            this.comboBoxRegions.Size = new System.Drawing.Size(274, 21);
            this.comboBoxRegions.TabIndex = 6;
            this.comboBoxRegions.Tag = "REGION";
            this.comboBoxRegions.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // comboBoxAspect
            // 
            this.comboBoxAspect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAspect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAspect.FormattingEnabled = true;
            this.comboBoxAspect.Location = new System.Drawing.Point(203, 341);
            this.comboBoxAspect.Name = "comboBoxAspect";
            this.comboBoxAspect.Size = new System.Drawing.Size(274, 21);
            this.comboBoxAspect.TabIndex = 13;
            this.comboBoxAspect.Tag = "ASPECT";
            this.comboBoxAspect.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // labelShrub
            // 
            this.labelShrub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShrub.Location = new System.Drawing.Point(3, 419);
            this.labelShrub.Margin = new System.Windows.Forms.Padding(3);
            this.labelShrub.Name = "labelShrub";
            this.labelShrub.Size = new System.Drawing.Size(194, 20);
            this.labelShrub.TabIndex = 27;
            this.labelShrub.Text = "Distance to nearest shrub / tree (m)";
            this.labelShrub.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelVegetation
            // 
            this.labelVegetation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVegetation.Location = new System.Drawing.Point(3, 367);
            this.labelVegetation.Margin = new System.Windows.Forms.Padding(3);
            this.labelVegetation.Name = "labelVegetation";
            this.labelVegetation.Size = new System.Drawing.Size(194, 20);
            this.labelVegetation.TabIndex = 26;
            this.labelVegetation.Text = "Surrounding vegetation";
            this.labelVegetation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelCanopy
            // 
            this.labelCanopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCanopy.Location = new System.Drawing.Point(3, 393);
            this.labelCanopy.Margin = new System.Windows.Forms.Padding(3);
            this.labelCanopy.Name = "labelCanopy";
            this.labelCanopy.Size = new System.Drawing.Size(194, 20);
            this.labelCanopy.TabIndex = 25;
            this.labelCanopy.Text = "Height of surrounding canopy (m)";
            this.labelCanopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAspect
            // 
            this.labelAspect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAspect.Location = new System.Drawing.Point(3, 341);
            this.labelAspect.Margin = new System.Windows.Forms.Padding(3);
            this.labelAspect.Name = "labelAspect";
            this.labelAspect.Size = new System.Drawing.Size(194, 20);
            this.labelAspect.TabIndex = 24;
            this.labelAspect.Text = "Aspect";
            this.labelAspect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSlope
            // 
            this.labelSlope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSlope.Location = new System.Drawing.Point(3, 315);
            this.labelSlope.Margin = new System.Windows.Forms.Padding(3);
            this.labelSlope.Name = "labelSlope";
            this.labelSlope.Size = new System.Drawing.Size(194, 20);
            this.labelSlope.TabIndex = 23;
            this.labelSlope.Text = "Slope (degrees)";
            this.labelSlope.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelLong
            // 
            this.labelLong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLong.Location = new System.Drawing.Point(3, 237);
            this.labelLong.Margin = new System.Windows.Forms.Padding(3);
            this.labelLong.Name = "labelLong";
            this.labelLong.Size = new System.Drawing.Size(194, 20);
            this.labelLong.TabIndex = 22;
            this.labelLong.Tag = "";
            this.labelLong.Text = "Longitude (decimal degres)";
            this.labelLong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelLat
            // 
            this.labelLat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLat.Location = new System.Drawing.Point(3, 211);
            this.labelLat.Margin = new System.Windows.Forms.Padding(3);
            this.labelLat.Name = "labelLat";
            this.labelLat.Size = new System.Drawing.Size(194, 20);
            this.labelLat.TabIndex = 21;
            this.labelLat.Text = "Latitude (decimal degrees)";
            this.labelLat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAltitude
            // 
            this.labelAltitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAltitude.Location = new System.Drawing.Point(3, 289);
            this.labelAltitude.Margin = new System.Windows.Forms.Padding(3);
            this.labelAltitude.Name = "labelAltitude";
            this.labelAltitude.Size = new System.Drawing.Size(194, 20);
            this.labelAltitude.TabIndex = 20;
            this.labelAltitude.Text = "Altitude (m)";
            this.labelAltitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSubregion
            // 
            this.labelSubregion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSubregion.Location = new System.Drawing.Point(3, 185);
            this.labelSubregion.Margin = new System.Windows.Forms.Padding(3);
            this.labelSubregion.Name = "labelSubregion";
            this.labelSubregion.Size = new System.Drawing.Size(194, 20);
            this.labelSubregion.TabIndex = 19;
            this.labelSubregion.Text = "Subregion";
            this.labelSubregion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLocation.Location = new System.Drawing.Point(203, 29);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(274, 20);
            this.textBoxLocation.TabIndex = 1;
            this.textBoxLocation.Tag = "LOCATION";
            this.textBoxLocation.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // textBoxSiteName
            // 
            this.textBoxSiteName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSiteName.Location = new System.Drawing.Point(203, 81);
            this.textBoxSiteName.Name = "textBoxSiteName";
            this.textBoxSiteName.ReadOnly = true;
            this.textBoxSiteName.Size = new System.Drawing.Size(274, 20);
            this.textBoxSiteName.TabIndex = 2;
            this.textBoxSiteName.Tag = "SITE_NAME";
            this.textBoxSiteName.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // comboBoxSurroundingVegetation
            // 
            this.comboBoxSurroundingVegetation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSurroundingVegetation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSurroundingVegetation.FormattingEnabled = true;
            this.comboBoxSurroundingVegetation.Location = new System.Drawing.Point(203, 367);
            this.comboBoxSurroundingVegetation.Name = "comboBoxSurroundingVegetation";
            this.comboBoxSurroundingVegetation.Size = new System.Drawing.Size(274, 21);
            this.comboBoxSurroundingVegetation.TabIndex = 14;
            this.comboBoxSurroundingVegetation.Tag = "SURROUNDING_VEG";
            this.comboBoxSurroundingVegetation.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // labelRegion
            // 
            this.labelRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRegion.Location = new System.Drawing.Point(3, 159);
            this.labelRegion.Margin = new System.Windows.Forms.Padding(3);
            this.labelRegion.Name = "labelRegion";
            this.labelRegion.Size = new System.Drawing.Size(194, 20);
            this.labelRegion.TabIndex = 16;
            this.labelRegion.Text = "Region";
            this.labelRegion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSiteType
            // 
            this.labelSiteType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSiteType.Location = new System.Drawing.Point(3, 55);
            this.labelSiteType.Margin = new System.Windows.Forms.Padding(3);
            this.labelSiteType.Name = "labelSiteType";
            this.labelSiteType.Size = new System.Drawing.Size(194, 20);
            this.labelSiteType.TabIndex = 15;
            this.labelSiteType.Text = "Site Type";
            this.labelSiteType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSiteName
            // 
            this.labelSiteName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSiteName.Location = new System.Drawing.Point(3, 81);
            this.labelSiteName.Margin = new System.Windows.Forms.Padding(3);
            this.labelSiteName.Name = "labelSiteName";
            this.labelSiteName.Size = new System.Drawing.Size(194, 20);
            this.labelSiteName.TabIndex = 14;
            this.labelSiteName.Text = "Site Name";
            this.labelSiteName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelLocation
            // 
            this.labelLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLocation.Location = new System.Drawing.Point(3, 29);
            this.labelLocation.Margin = new System.Windows.Forms.Padding(3);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(194, 20);
            this.labelLocation.TabIndex = 14;
            this.labelLocation.Text = "Location";
            this.labelLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSiteType
            // 
            this.comboBoxSiteType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSiteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSiteType.FormattingEnabled = true;
            this.comboBoxSiteType.Location = new System.Drawing.Point(203, 55);
            this.comboBoxSiteType.Name = "comboBoxSiteType";
            this.comboBoxSiteType.Size = new System.Drawing.Size(274, 21);
            this.comboBoxSiteType.TabIndex = 3;
            this.comboBoxSiteType.Tag = "SITE_TYPE";
            this.comboBoxSiteType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChangeIndex);
            // 
            // textBoxSchoolGroup
            // 
            this.textBoxSchoolGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSchoolGroup.Location = new System.Drawing.Point(203, 3);
            this.textBoxSchoolGroup.Name = "textBoxSchoolGroup";
            this.textBoxSchoolGroup.Size = new System.Drawing.Size(274, 20);
            this.textBoxSchoolGroup.TabIndex = 0;
            this.textBoxSchoolGroup.Tag = "SCHOOL_GROUP";
            this.textBoxSchoolGroup.TextChanged += new System.EventHandler(this.TextBoxesTextChanged);
            // 
            // labelSchoolGroup
            // 
            this.labelSchoolGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSchoolGroup.Location = new System.Drawing.Point(3, 3);
            this.labelSchoolGroup.Margin = new System.Windows.Forms.Padding(3);
            this.labelSchoolGroup.Name = "labelSchoolGroup";
            this.labelSchoolGroup.Size = new System.Drawing.Size(194, 20);
            this.labelSchoolGroup.TabIndex = 13;
            this.labelSchoolGroup.Text = "School Group";
            this.labelSchoolGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.buttonSaveSite, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelData, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonNight, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(480, 553);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // buttonSaveSite
            // 
            this.buttonSaveSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSaveSite.Location = new System.Drawing.Point(3, 474);
            this.buttonSaveSite.Name = "buttonSaveSite";
            this.buttonSaveSite.Size = new System.Drawing.Size(474, 23);
            this.buttonSaveSite.TabIndex = 17;
            this.buttonSaveSite.Text = "Save Site Data";
            this.buttonSaveSite.UseVisualStyleBackColor = true;
            this.buttonSaveSite.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // EditSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 553);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditSite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Site Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
            this.tableLayoutPanelData.ResumeLayout(false);
            this.tableLayoutPanelData.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
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
    }
}