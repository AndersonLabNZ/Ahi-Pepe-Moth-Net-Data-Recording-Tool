using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MothNet
{
    /// <summary>
    /// The form that allows a user to edit an individual night
    /// </summary>
    public partial class NightEdit : Form, IFileHandleHold, IGuidFileList
    {
        /// <summary>
        /// The GUID for the night this form represents. Used as the night data folder name
        /// </summary>
        public Guid FileGuid { get; set; }

        /// <summary>
        /// The path to the night data file
        /// </summary>
        public string DataFileName
        {
            get
            {
                return Path.Combine(FolderDirectory, "night_data.txt");
            }
        }

        /// <summary>
        /// The path to the night name file
        /// </summary>
        public string NameFileName
        {
            get
            {
                return Path.Combine(FolderDirectory, "night_name.txt");
            }
        }

        /// <summary>
        /// The path to the night folder represented by this form
        /// </summary>
        public string FolderDirectory
        {
            get
            {
                return Path.Combine(EditSite.FolderDirectory, "nights", FileGuid.ToString("B").ToUpper());
            }
        }

        /// <summary>
        /// This night's ID
        /// </summary>
        public String NightID
        {
            get { return textBoxSiteNight.Text; }
        }

        /// <summary>
        /// The form for editing the environmental data
        /// </summary>
        public EnvironmentalData EnvironmentalDataForm { get; private set; }

        /// <summary>
        /// The form for editing the abundance data
        /// </summary>
        public InputData InputDataForm { get; private set; }

        /// <summary>
        /// The parent site form
        /// </summary>
        public EditSite EditSite { get; private set; }

        /// <summary>
        /// The file stream for the night settings file
        /// </summary>
        public FileStream NightStream { get; private set; }

        /// <summary>
        /// The file stream for the night name file
        /// </summary>
        public FileStream NameStream { get; private set; }

        /// <summary>
        /// Whether the data is being saved. If true, it is, if false, the user has canceled saving
        /// </summary>
        public bool Saving { get; private set; } = false;

        //If this is a new night (true), or if it is one being edited (false)
        private bool creating = false;

        /// <summary>
        /// Constructor for the night edit form
        /// </summary>
        /// <param name="editSite">The parent site edit form</param>
        /// <param name="guid">The site GUID. Guid.Empty creates a new site (and site GUID)</param>
        public NightEdit(EditSite editSite, Guid guid)
        {
            try
            {
                creating = (guid == Guid.Empty);
                EditSite = editSite;
                InitializeComponent();

                //Sets the site id field to the ID of the parent site.
                textBoxSiteName.Text = editSite.SiteName;

                //e.g. Saturday, 28 January, 2017
                dateTimePickerDate.CustomFormat = "dddd, d MMMM, yyyy";
                HelperFunctions.AddFileItemsToComboBox(comboBoxMoonPhase, false, "moon_phase");

                //Load the options into the checked list box (and uncheck all of the options)
                string[] list = HelperFunctions.GetResourceList("alt_light");
                foreach (string item in list)
                {
                    ComboBoxItem cbItem = new ComboBoxItem(item, false);
                    checkedListBoxAltLight.Items.Add(cbItem);
                    checkedListBoxAltLight.SetItemChecked(checkedListBoxAltLight.Items.IndexOf(cbItem), false);
                }

                //Add the options to the combo boxes
                HelperFunctions.AddFileItemsToComboBox(comboBoxCloud, false, "cloud_cover");
                HelperFunctions.AddFileItemsToComboBox(comboBoxPrecipitation, false, "precipitation");

                if (creating)
                {
                    //Get random GUID, and use it for the new folder created in the next line
                    FileGuid = Guid.NewGuid();
                    Directory.CreateDirectory(FolderDirectory);

                    //Create the child forms
                    EnvironmentalDataForm = new EnvironmentalData(FolderDirectory, true);
                    InputDataForm = new InputData(FolderDirectory, true, ((ComboBoxItem)EditSite.comboBoxRegions.Items[EditSite.comboBoxRegions.SelectedIndex]).FileValue.ToCharArray()[0]);

                    //Create the file streams
                    NightStream = new FileStream(DataFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                    NameStream = new FileStream(NameFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                }
                else
                {
                    //Load and open files and create the child forms
                    FileGuid = guid;
                    EnvironmentalDataForm = new EnvironmentalData(FolderDirectory, false);
                    InputDataForm = new InputData(FolderDirectory, false, ((ComboBoxItem)EditSite.comboBoxRegions.Items[EditSite.comboBoxRegions.SelectedIndex]).FileValue.ToCharArray()[0]);
                    NightStream = new FileStream(DataFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    NameStream = new FileStream(NameFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    LoadNight();
                }

                //Run the validation functions
                EnvironmentalDataForm.ValidationHelper();
                ValidationHelper();
            }
            catch (Exception e)
            {
                CloseHandles();
                if (e is CannotLoadException)
                {
                    throw;
                }
                else
                {
                    throw new CannotLoadException(e.Message + "\r\nCould not open night data", e);
                }
            }
            //Any other code goes in the try
        }

        /// <summary>
        /// Callback function for file loading purposes. Used as a (limited - only checks that the data makes sense) validation checker for the loading helper function, and to load in the data
        /// </summary>
        /// <param name="index">The line that is stored in value</param>
        /// <param name="value">The text in the line represented by index</param>
        /// <returns>Whether the value for the line is acceptable</returns>
        public void LoadFileCallBack(int index, string value)
        {
            switch (index)
            {
                case 0:
                    //No requirements, just load and return true
                    textBoxSiteName.Text = value;
                    break;
                case 1:
                    try
                    {
                        //Load the date into the date picker. If the format is incorrect, catch the exception, throw another so the message gets to the user and return false
                        dateTimePickerDate.Value = HelperFunctions.GetStrFormatDate(value);
                    }
                    catch (Exception)
                    {
                        throw new CannotLoadException("The date format for the night is invalid");
                    } 
                    break;
                case 2:
                    //No requirements, just load and return true
                    textBoxSiteNight.Text = value;
                    break;
                case 3:
                    //Paese the time, and if successful, load the fields. Otherwise, throw
                    if (Int32.TryParse(value, out int sunset))
                    {
                        //The hour is the floor of the number divided by 60, the minute is the remainder
                        numericUpDownHhSunset.Value = Math.DivRem(sunset, 60, out int rem);
                        numericUpDownMmSunset.Value = rem;
                    }
                    else
                    {
                        throw new CannotLoadException("The sunset time was not an integer - got \"" + value + "\" instead");
                    }
                    break;
                case 4:
                    //Paese the time, and if successful, load the fields. Otherwise, throw
                    if (Int32.TryParse(value, out int sunrise))
                    {
                        numericUpDownHhSunrise.Value = Math.DivRem(sunrise, 60, out int rem);
                        numericUpDownMmSunrise.Value = rem;
                    }
                    else
                    {
                        throw new CannotLoadException("The sunrise time was not an integer - got \"" + value + "\" instead");
                    }
                    break;
                case 5:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxMoonPhase, value))
                    {
                        throw new CannotLoadException(string.Format("Settings item \"{0}\" not an option for moon phase", value));
                    }
                    break;
                case 6:
                    //Split the value into a list of items
                    string[] split = value.Split('\t');

                    //Iterate through the list
                    foreach (string line in split)
                    {
                        //Ignore if empty
                        if (String.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }
                        bool hit = false;
                        int i = 0;

                        //Iterate trough all check box options. If one is found, set the check state to true, from false. If one is not found, return false
                        foreach (ComboBoxItem cbItem in checkedListBoxAltLight.Items)
                        {
                            if (cbItem.Value == line)
                            {
                                checkedListBoxAltLight.SetItemChecked(i, true);
                                hit = true;
                                break;
                            }
                            i++;
                        }

                        //If it is not found then throw
                        if (!hit)
                        {
                            throw new CannotLoadException(string.Format("Settings item \"{0}\" not an option for an alternate light source", value));
                        }
                    }
                    break;
                case 7:
                    //No requirements, just load and return true
                    textBoxDistanceToLightSource.Text = value;
                    break;
                case 8:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxCloud, value))
                    {
                        throw new CannotLoadException(string.Format("Settings item \"{0}\" not an option for cloud cover", value));
                    }
                    break;
                case 9:
                    //Checks that the combobox has the particular value to add, and if so, loads it
                    if (!HelperFunctions.SetComboBoxFileValue(comboBoxPrecipitation, value))
                    {
                        throw new CannotLoadException(string.Format("Settings item \"{0}\" not an option for precipitation", value));
                    }
                    break;
                case 10:
                    //If not N/A, then there is a file, so set the availability to true.
                    if (value != "N/A")
                    {
                        EnvironmentalDataForm.textBoxKestrelSerial.Text = value;
                        EnvironmentalDataForm.KestrelFile.SetAvailable();
                    }

                    //Set the back colour to red if there is no file. This will be reset to grey/white if one is not needed
                    EnvironmentalDataForm.buttonSelectKestrelFile.BackColor = EnvironmentalDataForm.KestrelFile.IsAvailable ? Color.FromArgb(225, 225, 225) : Color.Tomato;

                    //Set the check state
                    EnvironmentalDataForm.checkBoxKestFileAvailable.Checked = EnvironmentalDataForm.KestrelFile.IsAvailable;
                    break;
                case 11:
                    //If not N/A, then there is a file, so set the availability to true.
                    if (value != "N/A")
                    {
                        EnvironmentalDataForm.textBoxIButtonSerial.Text = value;
                        EnvironmentalDataForm.IButtonFile.SetAvailable();
                    }

                    //Set the back colour to red if there is no file. This will be reset to grey/white if one is not needed
                    EnvironmentalDataForm.buttonIbutonSelect.BackColor = EnvironmentalDataForm.IButtonFile.IsAvailable ? Color.FromArgb(225, 225, 225) : Color.Tomato;

                    //Set the check state
                    EnvironmentalDataForm.checkBoxIbuttonFileAvailable.Checked = EnvironmentalDataForm.IButtonFile.IsAvailable;
                    break;
                case 12:
                    //If the kestral file data is available, then set the checked value to true, and start loading
                    EnvironmentalDataForm.checkBoxKestrelAvailable.Checked = value != "N/A";

                    //If available, then we know that this must have a value, and as such can be loaded (this one is redundant, but kept for readibility)
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxAirAvg.Text = value;
                    }
                    break;
                case 13:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxAirMin.Text = value;
                    }
                    break;
                case 14:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxAirMax.Text = value;
                    }
                    break;
                case 15:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxHumidAvg.Text = value;
                    }
                    break;
                case 16:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxHumidMin.Text = value;
                    }
                    break;
                case 17:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxHumidMax.Text = value;
                    }
                    break;
                case 18:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxWindAvg.Text = value;
                    }
                    break;
                case 19:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxWindMin.Text = value;
                    }
                    break;
                case 20:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxWindMax.Text = value;
                    }
                    break;
                case 21:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxPressureAvg.Text = value;
                    }
                    break;
                case 22:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxPressureMin.Text = value;
                    }
                    break;
                case 23:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        EnvironmentalDataForm.textBoxPressureMax.Text = value;
                    }
                    break;
                case 24:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (EnvironmentalDataForm.checkBoxKestrelAvailable.Checked)
                    {
                        //If not a valid combo box value then throw the exception
                        if (!HelperFunctions.SetComboBoxFileValue(EnvironmentalDataForm.comboBoxWindDir, value))
                        {
                            throw new CannotLoadException(string.Format("Settings item \"{0}\" not an option for wind direction", value));
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Loads the night data
        /// </summary>
        private void LoadNight()
        {
            HelperFunctions.DoLoadFileHelper(NightStream, new HelperFunctions.LoadFileCallBack(LoadFileCallBack), "setting_headers_night");
        }
        
        /// <summary>
        /// Saves the night data
        /// </summary>
        private void SaveNight()
        {
            //Save the name in the name file
            NameStream.Seek(0, SeekOrigin.Begin);

            byte[] nameBytes = Encoding.UTF8.GetBytes(NightID.ToCharArray());

            NameStream.Write(nameBytes, 0, nameBytes.Length);
            NameStream.Flush();
            NameStream.Seek(0, SeekOrigin.Begin);

            NightStream.Seek(0, SeekOrigin.Begin);
            string[] list = HelperFunctions.GetResourceList("setting_headers_night");

            //Save all the settings data
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(list[0] + "," + EditSite.SiteName);
            sb.AppendLine(list[1] + "," + HelperFunctions.GetDateFormatStr(dateTimePickerDate.Value));
            sb.AppendLine(list[2] + "," + textBoxSiteNight.Text);
            sb.AppendLine(list[3] + "," + (numericUpDownHhSunset.Value * 60 + numericUpDownMmSunset.Value));
            sb.AppendLine(list[4] + "," + (numericUpDownHhSunrise.Value * 60 + numericUpDownMmSunrise.Value));
            sb.AppendLine(list[5] + "," + HelperFunctions.GetOutputFileValue(comboBoxMoonPhase));

            //Save any checked items from the combobox
            sb.Append(list[6] + ",");
            int i = 0;
            foreach (ComboBoxItem item in checkedListBoxAltLight.Items)
            {
                if (checkedListBoxAltLight.GetItemChecked(i))
                {
                    sb.Append(item.Value + "\t");
                }
                i++;
            }
            sb.AppendLine();

            sb.AppendLine(list[7] + "," + textBoxDistanceToLightSource.Text);
            sb.AppendLine(list[8] + "," + HelperFunctions.GetOutputFileValue(comboBoxCloud));
            sb.AppendLine(list[9] + "," + HelperFunctions.GetOutputFileValue(comboBoxPrecipitation));

            //Environmental data
            sb.AppendLine(list[10] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestFileAvailable, EnvironmentalDataForm.textBoxKestrelSerial));
            sb.AppendLine(list[11] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxIbuttonFileAvailable, EnvironmentalDataForm.textBoxIButtonSerial));
            sb.AppendLine(list[12] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxAirAvg));
            sb.AppendLine(list[13] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxAirMin));
            sb.AppendLine(list[14] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxAirMax));
            sb.AppendLine(list[15] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxHumidAvg));
            sb.AppendLine(list[16] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxHumidMin));
            sb.AppendLine(list[17] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxHumidMax));
            sb.AppendLine(list[18] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxWindAvg));
            sb.AppendLine(list[19] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxWindMin));
            sb.AppendLine(list[20] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxWindMax));
            sb.AppendLine(list[21] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxPressureAvg));
            sb.AppendLine(list[22] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxPressureMin));
            sb.AppendLine(list[23] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, EnvironmentalDataForm.textBoxPressureMax));
            sb.AppendLine(list[24] + "," + HelperFunctions.GetEnableString(EnvironmentalDataForm.checkBoxKestrelAvailable, HelperFunctions.GetOutputFileValue(EnvironmentalDataForm.comboBoxWindDir)));

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString().ToCharArray());
            NightStream.Write(bytes, 0, bytes.Length);
            NightStream.Flush();
            NightStream.Seek(0, SeekOrigin.Begin);
        }

        /// <summary>
        /// Closes any open file handles, if the user is creating a night and decides to cancel it, then delete the directory
        /// </summary>
        public void CloseHandles()
        { 
            EnvironmentalDataForm?.KestrelFile?.Dispose();
            EnvironmentalDataForm?.IButtonFile?.Dispose();
            InputDataForm?.Moths?.Dispose();
            InputDataForm?.Rodents?.Dispose();
            NightStream?.Close();
            NameStream?.Close();
            if (creating && !Saving)
            {
                try
                {
                    Directory.Delete(FolderDirectory, true);
                }
                catch (DirectoryNotFoundException) { }
            }
        }

        /// <summary>
        /// Event handler for clicking the save button. Saves the night data, and then closes the forms
        /// </summary>
        /// <param name="sender">The save button</param>
        /// <param name="e">The event arguments</param>
        private void SaveButtonClick(object sender, EventArgs e)
        {
            Saving = true;
            SaveNight();
            CloseHandles();
            Close();
        }

        /// <summary>
        /// Very little to check here, just that the distance to nearest light source is actually a number and that the environmental data is ok
        /// </summary>
        /// <returns>If it is okay to save</returns>
        bool ValidationHelper()
        {
            textBoxSiteNight.Text = textBoxSiteName.Text + "_" + HelperFunctions.GetDateFormatStr(dateTimePickerDate.Value.Date).Replace(" ", "");
            buttonSave.Enabled = HelperFunctions.NumberTextBoxHelper(textBoxDistanceToLightSource, true) && EnvironmentalDataForm.Success;
            buttonEnvironment.BackColor = EnvironmentalDataForm.Success ? Color.FromArgb(225,225,225) : Color.Tomato;
            return buttonSave.Enabled;
        }

        /// <summary>
        /// Event handler for the datt time picker date changing
        /// </summary>
        /// <param name="sender">The date time picker</param>
        /// <param name="e">The event arguments</param>
        private void DateChanged(object sender, EventArgs e)
        {
            ValidationHelper();
        }

        /// <summary>
        /// Event handler for clicking the environmental data form
        /// </summary>
        /// <param name="sender">The input environmental data button</param>
        /// <param name="e">The event arguments</param>
        private void EnvironmentalButtonClick(object sender, EventArgs e)
        {
            //Show modally and then check once complete
            EnvironmentalDataForm.ShowDialog();
            ValidationHelper();
        }

        /// <summary>
        /// Event handler for the text changing in the distance to nearest light source text box
        /// </summary>
        /// <param name="sender">The distance to nearest light source text box</param>
        /// <param name="e">The event arguments</param>
        private void EditControlTextChanged(object sender, EventArgs e)
        {
            ValidationHelper();
        }

        /// <summary>
        /// Event handler for clicking the input species data button
        /// </summary>
        /// <param name="sender">The input species data button</param>
        /// <param name="e">The event arguments</param>
        private void ButtonSpeciesCountsClick(object sender, EventArgs e)
        {
            InputDataForm.ShowDialog();
        }

        /// <summary>
        /// Event handler for the form closing
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">The event arguments</param>
        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            CloseHandles();
        }
    }
}
