using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MothNet
{
    /// <summary>
    /// Dialog for the site editing function
    /// </summary>
    public partial class EditSite : Form, IFileHandleHold, IGuidFileList
    {

        /// <summary>
        /// The file stream for the settings file
        /// </summary>
        public FileStream SettingsStream { get; private set; }

        /// <summary>
        /// The file stream for the name file. The name file is used to load the name without having to load the settings file
        /// </summary>
        public FileStream NameStream { get; private set; }

        /// <summary>
        /// The GUID for this site.
        /// </summary>
        public Guid FileGuid { get; set; }

        /// <summary>
        /// The directory the site data is in. Based on the GUID
        /// </summary>
        public string FolderDirectory
        {
            get
            {
                return Path.Combine(HelperFunctions.SitesDir, FileGuid.ToString("B").ToUpper());
            }
        }

        /// <summary>
        /// The fully qualified path to the data file (in the folder directory)
        /// </summary>
        public string DataFileName
        {
            get
            {
                return Path.Combine(FolderDirectory, "site_data.txt");
            }
        }

        /// <summary>
        /// The fully qualified path to the data file (in the folder directory)
        /// </summary>
        public string NameFileName
        {
            get
            {
                return Path.Combine(FolderDirectory, "site_name.txt");
            }
        }

        /// <summary>
        /// The name of the site. Saved in the name file (and the settings file)
        /// </summary>
        public string SiteName
        {
            get
            {
                return textBoxSchoolGroup.Text + "_" + MothNet.HelperFunctions.GetOutputFileValue(comboBoxSiteType);
            }
        }

        private char MothRegion
        {
            get
            {
                return ((ComboBoxItem)comboBoxRegions.Items[comboBoxRegions.SelectedIndex]).FileValue.ToCharArray()[0];
            }
        }

        /// <summary>
        /// Whether the user wants to save the data on close, i.e. are they saving the site or cancelling
        /// </summary>
        public bool Saving { get; private set; } = false;

        /// <summary>
        /// Whether the site is a new site, i.e. is being created
        /// </summary>
        private bool Creating { get; set; } = false;

        /// <summary>
        /// The form for the night data
        /// </summary>
        public NightForm Nights { get; private set; }

        //Whether the data has been loaded yet
        private bool loaded = false;

        //The previous region. Used to check if a combobox selection event was caused by a change in selected region
        char prevRegion;

        /// <summary>
        /// Constructor. Loads site and fills combo box items
        /// </summary>
        /// <param name="guid">The site GUID. Guid.Empty if creating the site</param>
        public EditSite(Guid guid)
        {
            //Any other code goes in the try   
            try
            {
                //If guid is Guid.Empty then this is a new site
                Creating = (guid == Guid.Empty);
                InitializeComponent();

                //Load options into the combo boxes
                HelperFunctions.AddFileItemsToComboBox(comboBoxSurroundingVegetation, false, "surrounding_vegetation");
                HelperFunctions.AddFileItemsToComboBox(comboBoxSiteType, false, "site_type");
                HelperFunctions.AddFileItemsToComboBox(comboBoxVegetationRestoration, false, "vegetation_restoration");
                HelperFunctions.AddFileItemsToComboBox(comboBoxPredatorRemoval, false, "predator_removal");
                HelperFunctions.AddFileItemsToComboBox(comboBoxCanopyHeight, false, "canopy_height");
                HelperFunctions.AddFileItemsToComboBox(comboBoxShrubDistance, false, "distance_shrub_tree");
                HelperFunctions.AddFileItemsToComboBox(comboBoxAspect, false, "aspect");
                HelperFunctions.AddFileItemsToComboBox(comboBoxRegions, false, "regions");

                //Subregions based on regions, so should be updated
                UpdateSubRegions();

                if (Creating)
                {
                    //Create a GUID for this site. Odds of a collision very very small
                    FileGuid = Guid.NewGuid();

                    //Create the site directory. Any exceptions thrown here would be because of a software, as opposed to a user error, so don't catch
                    Directory.CreateDirectory(FolderDirectory);

                    //Create the setings and name files. Again, no exceptions here would be caused by the user doing somethign stupid
                    SettingsStream = new FileStream(DataFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                    NameStream = new FileStream(NameFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                }
                else
                {
                    //Set Guid and load
                    FileGuid = guid;

                    try
                    {
                        //Load files
                        SettingsStream = new FileStream(DataFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                        NameStream = new FileStream(NameFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    }
                    catch (IOException except) when (except is DirectoryNotFoundException || except is FileNotFoundException)
                    {
                        throw new CannotLoadException(HelperFunctions.FormatResStr("EXCEPT_SET_FILE_NOT_FOUND"), except);
                    }

                    //Load the data into the fields
                    LoadSite();
                }

                //Create the nights form
                Nights = new NightForm(this);

                //Loading complete
                loaded = true;

                //Set the previous region field. Used to check whether the moth species list need updating
                prevRegion = ((ComboBoxItem)comboBoxRegions.Items[comboBoxRegions.SelectedIndex]).FileValue.ToCharArray()[0];

                //Update the species lists for the night form
                UpdateSpeciesLists();

                //Check all is ok.
                ValidationHelper();
            }
            catch (Exception)
            {
                //Close the files and close the form
                CloseHandles();
                throw;
            }
            //Any other code goes in the try
        }

        /// <summary>
        /// Makes sure that all fields have valid data
        /// </summary>
        /// <returns>Whether all the user input data is valid / in range. If not, disallow saving, etc.</returns>
        private bool ValidationHelper()
        {
            //Deals with text boxes for text data, and integral and non-integral numbers
            bool success = true;

            //Plain text boxes
            if (!HelperFunctions.TextBoxHelper(textBoxLocation))
            {
                success = false;
            }
            if (!HelperFunctions.TextBoxHelper(textBoxSchoolGroup))
            { 
                success = false;
            }

            //Fill in the site name field. This is simply a combination of the school group name and the site type. Is used as a user friendly name
            textBoxSiteName.Text = SiteName;

            //Integral number text boxes
            if (!HelperFunctions.NumberTextBoxHelper(textBoxAccuracy, false))
            {
                success = false;
            }
            if (!HelperFunctions.NumberTextBoxHelper(textBoxAltitude, false))
            {
                success = false;
            }

            //Decimal number text boxes
            if (!HelperFunctions.NumberTextBoxHelper(textBoxLatitude, true))
            {
                success = false;
            }
            if (!HelperFunctions.NumberTextBoxHelper(textBoxLongitude, true))
            {
                success = false;
            }
            if (!HelperFunctions.NumberTextBoxHelper(textBoxSlope, true))
            {
                success = false;
            }

            //Only enable save button if everything OK
            buttonSaveSite.Enabled = success;
            buttonNight.Enabled = success;
            return success;
        }

        /// <summary>
        /// Updates the list of subregions whenever the reqion is changed
        /// </summary>
        private void UpdateSubRegions()
        {
            if (prevRegion != MothRegion)
            {
                //Only update if region changes
                HelperFunctions.AddFileItemsToComboBox(comboBoxSubRegions, true, "subregions_" + ((ComboBoxItem)comboBoxRegions.Items[comboBoxRegions.SelectedIndex]).Value.ToLower().Replace(' ', '_'));
            }
        }

        /// <summary>
        /// Updates the list of species that would be expected in the region
        /// </summary>
        private void UpdateSpeciesLists()
        {            
            //Don't show the message / update list if there are no nights with data anyway
            if (Nights.listBoxNights.Items.Count  > 0 && MothRegion != prevRegion)
            {
                string prevRegionFull = string.Empty;
                foreach (ComboBoxItem item in comboBoxRegions.Items)
                {
                    if (item.FileValue[0] == prevRegion)
                    {
                        prevRegionFull = item.Value;
                        break;
                    }
                }

                if (MessageBox.Show(HelperFunctions.FormatResStr("MSG_TEXT_SP_LIST_UPDATE_REGION", prevRegionFull, ((ComboBoxItem)comboBoxRegions.Items[comboBoxRegions.SelectedIndex]).Value), HelperFunctions.FormatResStr("MSG_TITLE_SP_LIST_UPDATE_REGION"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //If desired, update the nights
                    foreach (NightEdit edit in Nights.listBoxNights.Items)
                    {
                        edit.InputDataForm.UpdateSpeciesList(MothRegion);
                    }
                }
                else
                {
                    //If the user didn't want to change the region, unwind the change, i.e. restore previously selected region
                    for (int i = 0; i < comboBoxRegions.Items.Count; i++)
                    {
                        //Find the index of the region. This is based on file value so IndexOf wouldn't work, as a new ComboBoxItem would need to be made
                        if (((ComboBoxItem)comboBoxRegions.Items[i]).FileValue == prevRegion.ToString())
                        {
                            //This will cause this function top to be called again, but species will equal prevRegion, so all will be fine, and this will be skipped, so there should be no recursion.
                            comboBoxRegions.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Callback function for file loading purposes. Used as a (limited - only checks that the data makes sense) validation checker for the loading helper function, and to load in the data
        /// </summary>
        /// <param name="index">The line that is stored in value</param>
        /// <param name="value">The text in the line represented by index</param>
        /// <returns>Whether the value for the line is acceptable</returns>
        public void LoadFileCallback(int index, string value)
        {
            switch (index)
            {
                case 0:
                    //No requirements, just load
                    textBoxSchoolGroup.Text = value;
                    break;
                case 1:
                    //No requirements, just load
                    textBoxLocation.Text = value;
                    break;
                case 2:
                    //Checks that the combobox has the particular value to add, and if so, loads it.
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxSiteType, value))
                    {
                        throw new CannotLoadException(HelperFunctions.FormatResStr("EXCEPT_SET_SITE_TYPE", value));
                    }
                    break;
                case 3:
                    //No requirements, just load
                    textBoxSiteName.Text = value;
                    break;
                case 4:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxVegetationRestoration, value))
                    {
                        throw new CannotLoadException(HelperFunctions.FormatResStr("EXCEPT_SET_SITE_VEG_RES", value));
                    }
                    break;
                case 5:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxPredatorRemoval, value))
                    {
                        throw new CannotLoadException(HelperFunctions.FormatResStr("EXCEPT_SET_SITE_PRED_REMOVE", value));
                    }
                    break;
                case 6:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxRegions, value))
                    {
                        throw new CannotLoadException(HelperFunctions.FormatResStr("EXCEPT_SET_SITE_REGION", value));
                    }
                    UpdateSubRegions();
                    break;
                case 7:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxSubRegions, value))
                    {
                        throw new CannotLoadException(HelperFunctions.FormatResStr("EXCEPT_SET_SITE_DISTRICT", value));
                    }
                    break;
                case 8:
                    //No requirements, just load
                    textBoxLatitude.Text = value;
                    break;
                case 9:
                    //No requirements, just load
                    textBoxLongitude.Text = value;
                    break;
                case 10:
                    //No requirements, just load
                    textBoxAccuracy.Text = value;
                    break;
                case 11:
                    //No requirements, just load
                    textBoxAltitude.Text = value;
                    break;
                case 12:
                    //No requirements, just load
                    textBoxSlope.Text = value;
                    break;
                case 13:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxAspect, value))
                    {
                        throw new CannotLoadException(HelperFunctions.FormatResStr("EXCEPT_SET_SITE_SLOPE_ASPECT", value));
                    }
                    break;
                case 14:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxSurroundingVegetation, value))
                    {
                        throw new CannotLoadException(HelperFunctions.FormatResStr("EXCEPT_SET_SITE_SURROUNDING_VEG", value));
                    }
                    break;
                case 15:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxCanopyHeight, value))
                    {
                        throw new CannotLoadException(HelperFunctions.FormatResStr("EXCEPT_SET_SITE_CANOPY_HEIGHT", value));
                    }
                    break;
                case 16:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxShrubDistance, value))
                    {
                        throw new CannotLoadException(HelperFunctions.FormatResStr("EXCEPT_SET_SITE_SHRUB_DIST", value));
                    }
                    break;
            }
        }

        /// <summary>
        /// Loads the site data from file
        /// </summary>
        private void LoadSite()
        {
            //Uses the load file helper to load the data into the appropriate fields - and check some aspects of the data
            HelperFunctions.DoLoadFileHelper(SettingsStream, new HelperFunctions.LoadFileCallBack(LoadFileCallback), "setting_headers_site");
        }

        /// <summary>
        /// Saves the site data to file
        /// </summary>
        public void SaveSite()
        {
            //Seek to the start of the file streams
            SettingsStream.Seek(0, SeekOrigin.Begin);

            NameStream.Seek(0, SeekOrigin.Begin);

            //Convert the name to a byte array and write it
            byte[] raw = System.Text.Encoding.UTF8.GetBytes(textBoxSiteName.Text);
            NameStream.Write(raw, 0, raw.Length);

            //Flush and reset the file pointer to the start
            NameStream.Flush();
            NameStream.Seek(0, SeekOrigin.Begin);
            NameStream.SetLength(raw.LongLength);

            //Get the resource list for the settings to be appended to
            string[] list = HelperFunctions.GetResourceList("setting_headers_site");
            StringBuilder sb = new StringBuilder();

            //Write the settings to the string builder
            sb.AppendLine(list[0] + "," + textBoxSchoolGroup.Text);
            sb.AppendLine(list[1] + "," + textBoxLocation.Text);
            sb.AppendLine(list[2] + "," + HelperFunctions.GetOutputFileValue(comboBoxSiteType));
            sb.AppendLine(list[3] + "," + textBoxSiteName.Text);
            sb.AppendLine(list[4] + "," + HelperFunctions.GetOutputFileValue(comboBoxVegetationRestoration));
            sb.AppendLine(list[5] + "," + HelperFunctions.GetOutputFileValue(comboBoxPredatorRemoval));
            sb.AppendLine(list[6] + "," + HelperFunctions.GetOutputFileValue(comboBoxRegions));
            sb.AppendLine(list[7] + "," + HelperFunctions.GetOutputFileValue(comboBoxSubRegions));
            sb.AppendLine(list[8] + "," + textBoxLatitude.Text);
            sb.AppendLine(list[9] + "," + textBoxLongitude.Text);
            sb.AppendLine(list[10] + "," + textBoxAccuracy.Text);
            sb.AppendLine(list[11] + "," + textBoxAltitude.Text);
            sb.AppendLine(list[12] + "," + textBoxSlope.Text);
            sb.AppendLine(list[13] + "," + HelperFunctions.GetOutputFileValue(comboBoxAspect));
            sb.AppendLine(list[14] + "," + HelperFunctions.GetOutputFileValue(comboBoxSurroundingVegetation));
            sb.AppendLine(list[15] + "," + HelperFunctions.GetOutputFileValue(comboBoxCanopyHeight));
            sb.AppendLine(list[16] + "," + HelperFunctions.GetOutputFileValue(comboBoxShrubDistance));

            //Convert to a byte array and write
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString().ToCharArray());
            SettingsStream.Write(bytes, 0, bytes.Length);

            //Flush and reset the file pointer to the start
            SettingsStream.Flush();
            SettingsStream.Seek(0, SeekOrigin.Begin);
            SettingsStream.SetLength(bytes.Length);

            //Set saving to true, to note that the data has been saved
            Saving = true;
        }

        /// <summary>
        /// Common event handler for the text boxes changing. Simply calls the validation helper function
        /// </summary>
        /// <param name="sender">The text box the text change event came from</param>
        /// <param name="e">The event arguments</param>
        private void TextBoxesTextChanged(object sender, EventArgs e)
        {
            ValidationHelper();
        }

        /// <summary>
        /// Common event handler for the combo box selection changing. Simply calls the validation helper function
        /// </summary>
        /// <param name="sender">The combo box the index change event came from</param>
        /// <param name="e">The event arguments</param>
        private void ComboBoxSelectionChangeIndex(object sender, EventArgs e)
        {
            ///During the loading phase the index will change but don't check the data until it's all there
            if (loaded)
            {
                //Update the species list, as well as the subregions. Then check that the data is valid, much like the text box event handler
                UpdateSpeciesLists();
                UpdateSubRegions();
                ValidationHelper();

                //Update the previous region
                prevRegion = MothRegion;
            }
        }

        /// <summary>
        /// Closes any open file handler
        /// </summary>
        public void CloseHandles()
        { 
            //Closes the file handles
            SettingsStream?.Close();
            NameStream?.Close();
            Nights?.Close();
            Nights?.Dispose();

            //If the site is a new site, and the user doesn't want to save it, then just delete it
            if (Creating && !Saving)
            {
                HelperFunctions.SafeDeleteDirectory(FolderDirectory);
            }
        }

        /// <summary>
        /// Form closing event handler. Closes any open handles
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">The event arguments</param>
        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            CloseHandles();
        }

        /// <summary>
        /// Event handler for clicking the go to night data button. Opens the nights dialog
        /// </summary>
        /// <param name="sender">The nights button</param>
        /// <param name="e">The event arguments</param>
        private void ButtonNightClick(object sender, EventArgs e)
        {
            this.Hide();

            //Manually set size and location to "replace" this window
            Nights.StartPosition = FormStartPosition.Manual;

            Nights.Size = this.Size;
            Nights.Location = this.Location;

            DialogResult res = Nights.ShowDialog();

            //Closing without saving
            if (res == DialogResult.Cancel)
            {
                this.Close();
            }
            else if (res == DialogResult.Yes) //Closing and saving
            {
                SaveSite();
                this.Close();
            }
            else //Going back
            {
                this.Size = Nights.Size;
                this.Location = Nights.Location;

                this.Show();
            }
        }

        /// <summary>
        /// Event handler for clicking the save button. Saves all the data without having any night data saved
        /// </summary>
        /// <param name="sender">The save button</param>
        /// <param name="e">The event arguments</param>
        private void ButtonSaveClick(object sender, EventArgs e)
        {
            //Save the sites then close the form (and the file handles)
            SaveSite();
            CloseHandles();
            Close();
            Dispose();
        }
    }
}
