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
        /// The kestral file data
        /// </summary>
        public DataFileItem KestrelFile { get; private set; }

        /// <summary>
        /// The ibutton file data
        /// </summary>
        public DataFileItem IButtonFile { get; private set; }

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
                HelperFunctions.AddFileItemsToComboBox(comboBoxWindDir, false, "aspect");

                if (creating)
                {
                    //Get random GUID, and use it for the new folder created in the next line. no exception handling for directory creating - any raised would be a bug not user oddity
                    FileGuid = Guid.NewGuid();
                    Directory.CreateDirectory(FolderDirectory);

                    //Create the child form
                    InputDataForm = new InputData(FolderDirectory, true, ((ComboBoxItem)EditSite.comboBoxRegions.Items[EditSite.comboBoxRegions.SelectedIndex]).FileValue.ToCharArray()[0]);

                    //Create the file streams. No exception handling for the create - no exceptions caused by user oddities
                    NightStream = new FileStream(DataFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                    NameStream = new FileStream(NameFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                }
                else
                {
                    //Load and open files and create the child form
                    FileGuid = guid;
                    InputDataForm = new InputData(FolderDirectory, false, ((ComboBoxItem)EditSite.comboBoxRegions.Items[EditSite.comboBoxRegions.SelectedIndex]).FileValue.ToCharArray()[0]);

                    try
                    { 
                        NightStream = new FileStream(DataFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                        NameStream = new FileStream(NameFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    }
                    catch (IOException except) when(except is DirectoryNotFoundException || except is FileNotFoundException)
                    {
                        throw new CannotLoadException("Could not load settings or name file. The file was not found.", except);
                    }
                }

                try
                {
                    //Put after to make sure there is a FileGuid set
                    KestrelFile = DataFileItem.Hold(Path.Combine(FolderDirectory, "kestrel_data.txt"), creating);
                    IButtonFile = DataFileItem.Hold(Path.Combine(FolderDirectory, "ibutton_data.txt"), creating);
                }
                
                catch (IOException except) when (except is DirectoryNotFoundException || except is FileNotFoundException)
                {
                    if (!creating)
                    {
                        throw new CannotLoadException("Could not load settings or name file. The file was not found.", except);
                    }
                    else
                    {
                        throw;
                    }
                }

                //Handle load sepretley as kestrel / ibutton files need to be loaded
                if (!creating)
                {
                    LoadNight();
                }

                CheckBoxEnable();
                ValidationHelper();
            }
            catch (Exception)
            {
                CloseHandles();
                throw;
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
                        textBoxKestrelSerial.Text = value;
                        KestrelFile.SetAvailable();
                    }

                    //Set the back colour to red if there is no file. This will be reset to grey/white if one is not needed
                    buttonSelectKestrelFile.BackColor = KestrelFile.IsAvailable ? Color.FromArgb(225, 225, 225) : Color.Tomato;

                    //Set the check state
                    checkBoxKestFileAvailable.Checked = KestrelFile.IsAvailable;
                    break;
                case 11:
                    //If not N/A, then there is a file, so set the availability to true.
                    if (value != "N/A")
                    {
                        textBoxIButtonSerial.Text = value;
                        IButtonFile.SetAvailable();
                    }

                    //Set the back colour to red if there is no file. This will be reset to grey/white if one is not needed
                    buttonIbutonSelect.BackColor = IButtonFile.IsAvailable ? Color.FromArgb(225, 225, 225) : Color.Tomato;

                    //Set the check state
                    checkBoxIbuttonFileAvailable.Checked = IButtonFile.IsAvailable;
                    break;
                case 12:
                    //If the kestral file data is available, then set the checked value to true, and start loading
                    checkBoxKestrelAvailable.Checked = value != "N/A";

                    //If available, then we know that this must have a value, and as such can be loaded (this one is redundant, but kept for readibility)
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxAirAvg.Text = value;
                    }
                    break;
                case 13:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxAirMin.Text = value;
                    }
                    break;
                case 14:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxAirMax.Text = value;
                    }
                    break;
                case 15:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxHumidAvg.Text = value;
                    }
                    break;
                case 16:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxHumidMin.Text = value;
                    }
                    break;
                case 17:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxHumidMax.Text = value;
                    }
                    break;
                case 18:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxWindAvg.Text = value;
                    }
                    break;
                case 19:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxWindMin.Text = value;
                    }
                    break;
                case 20:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxWindMax.Text = value;
                    }
                    break;
                case 21:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxPressureAvg.Text = value;
                    }
                    break;
                case 22:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxPressureMin.Text = value;
                    }
                    break;
                case 23:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        textBoxPressureMax.Text = value;
                    }
                    break;
                case 24:
                    //If available, then we know that this must have a value, and as such can be loaded
                    if (checkBoxKestrelAvailable.Checked)
                    {
                        //If not a valid combo box value then throw the exception
                        if (!HelperFunctions.SetComboBoxFileValue(comboBoxWindDir, value))
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

            //Flush and seek to beginning
            NameStream.Flush();
            NameStream.Seek(0, SeekOrigin.Begin);
            NameStream.SetLength(nameBytes.LongLength);

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
            sb.AppendLine(list[10] + "," + HelperFunctions.GetEnableString(checkBoxKestFileAvailable, textBoxKestrelSerial));
            sb.AppendLine(list[11] + "," + HelperFunctions.GetEnableString(checkBoxIbuttonFileAvailable, textBoxIButtonSerial));
            sb.AppendLine(list[12] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxAirAvg));
            sb.AppendLine(list[13] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxAirMin));
            sb.AppendLine(list[14] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxAirMax));
            sb.AppendLine(list[15] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxHumidAvg));
            sb.AppendLine(list[16] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxHumidMin));
            sb.AppendLine(list[17] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxHumidMax));
            sb.AppendLine(list[18] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxWindAvg));
            sb.AppendLine(list[19] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxWindMin));
            sb.AppendLine(list[20] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxWindMax));
            sb.AppendLine(list[21] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxPressureAvg));
            sb.AppendLine(list[22] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxPressureMin));
            sb.AppendLine(list[23] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, textBoxPressureMax));
            sb.AppendLine(list[24] + "," + HelperFunctions.GetEnableString(checkBoxKestrelAvailable, HelperFunctions.GetOutputFileValue(comboBoxWindDir)));

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString().ToCharArray());
            NightStream.Write(bytes, 0, bytes.Length);
            NightStream.Flush();
            NightStream.Seek(0, SeekOrigin.Begin);
            NightStream.SetLength(bytes.LongLength);
        }

        /// <summary>
        /// Opens a file (kestral or ibutton). It them saves it in the appropriate place, specified by destination. In effect, a glorified copy.
        /// </summary>
        /// <param name="destination">The location to save the file to</param>
        private void SelectFile(DataFileItem item)
        {
            //Get the path of the file to load
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".txt",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Multiselect = false
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //Open the file selected by the user
                item.SetCopy(dialog.FileName);

                //Run the validation function again as the availibility of this file may contribute to having a full data set
                ValidationHelper();
            }
        }

        /// <summary>
        /// Event handler for the clicking the kestral button
        /// </summary>
        /// <param name="sender">The kestral button</param>
        /// <param name="e">The event arguments</param>
        private void SelectKestrelFile(object sender, EventArgs e)
        {
            //Select the file and check validity
            SelectFile(KestrelFile);
            ValidationHelper();
        }

        /// <summary>
        /// Event handler for the clicking the ibutton button
        /// </summary>
        /// <param name="sender">The ibutton button</param>
        /// <param name="e">The event arguments</param>
        private void SelectIButtonFile(object sender, EventArgs e)
        {
            //Select the file and check validity
            SelectFile(IButtonFile);
            ValidationHelper();
        }

        /// <summary>
        /// Closes any open file handles, if the user is creating a night and decides to cancel it, then delete the directory
        /// </summary>
        public void CloseHandles()
        { 
            KestrelFile?.Dispose();
            IButtonFile?.Dispose();
            InputDataForm?.CloseHandles();
            InputDataForm?.Close();
            NightStream?.Close();
            NameStream?.Close();
            if (creating && !Saving)
            {
                HelperFunctions.SafeDeleteDirectory(FolderDirectory);
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
            InputDataForm.SaveAbundances();
            CloseHandles();
            Close();
        }

        /// <summary>
        /// Very little to check here, just that the distance to nearest light source is actually a number and that the environmental data is ok
        /// </summary>
        /// <returns>If it is okay to save</returns>
        bool ValidationHelper()
        {
            bool ok = true;

            textBoxSiteNight.Text = textBoxSiteName.Text + "_" + HelperFunctions.GetDateFormatStr(dateTimePickerDate.Value.Date).Replace(" ", "");
            if (!HelperFunctions.NumberTextBoxHelper(textBoxDistanceToLightSource, true))
            {
                ok = false;
            }

            //If the kestrel data is available, then check the data
            if (checkBoxKestrelAvailable.Checked)
            {
                //Need to make sure that all have actual (floating point) data, and that for each triplet, min <= avg <= max
                if (!(HelperFunctions.IsMinMaxAvgTripletOK(textBoxAirMin, textBoxAirMax, textBoxAirAvg) & HelperFunctions.IsMinMaxAvgTripletOK(textBoxHumidMin, textBoxHumidMax, textBoxHumidAvg) & HelperFunctions.IsMinMaxAvgTripletOK(textBoxWindMin, textBoxWindMax, textBoxWindAvg) & HelperFunctions.IsMinMaxAvgTripletOK(textBoxPressureMin, textBoxPressureMax, textBoxPressureAvg))) //Don't use short circuit operators otherwise colours aren't updated
                {
                    ok = false;
                }
            }
            else
            {
                //If the user does not have the data, then the text boxes will be disabled, and will need their colour reset, in case it was red before
                textBoxAirMin.BackColor = Color.FromArgb(240, 240, 240);
                textBoxAirMax.BackColor = Color.FromArgb(240, 240, 240);
                textBoxAirAvg.BackColor = Color.FromArgb(240, 240, 240);

                textBoxHumidMin.BackColor = Color.FromArgb(240, 240, 240);
                textBoxHumidMax.BackColor = Color.FromArgb(240, 240, 240);
                textBoxHumidAvg.BackColor = Color.FromArgb(240, 240, 240);

                textBoxWindMin.BackColor = Color.FromArgb(240, 240, 240);
                textBoxWindMax.BackColor = Color.FromArgb(240, 240, 240);
                textBoxWindAvg.BackColor = Color.FromArgb(240, 240, 240);

                textBoxPressureMin.BackColor = Color.FromArgb(240, 240, 240);
                textBoxPressureMax.BackColor = Color.FromArgb(240, 240, 240);
                textBoxPressureAvg.BackColor = Color.FromArgb(240, 240, 240);
            }

            //If the kestrel file (as opposed to just the data) is available, then check it
            if (checkBoxKestFileAvailable.Checked)
            {
                //If available set coulour to ok
                buttonSelectKestrelFile.BackColor = KestrelFile.IsAvailable ? Color.FromArgb(225, 225, 225) : Color.Tomato;

                //Can't simply set ok to the expresion (without the not), as it may set a previously set false back to tu
                if (!(HelperFunctions.TextBoxHelper(textBoxKestrelSerial) && KestrelFile.IsAvailable))
                {
                    ok = false;
                }
            }
            else
            {
                //If this is not set as available, then set to the disabled colour
                buttonSelectKestrelFile.BackColor = Color.FromArgb(204, 204, 204);
                textBoxKestrelSerial.BackColor = Color.FromArgb(240, 240, 240);
            }
            if (checkBoxIbuttonFileAvailable.Checked)
            {
                //If available set coulour to ok
                buttonIbutonSelect.BackColor = IButtonFile.IsAvailable ? Color.FromArgb(225, 225, 225) : Color.Tomato;
                if (!(HelperFunctions.TextBoxHelper(textBoxIButtonSerial) && IButtonFile.IsAvailable))
                {
                    ok = false;
                }
            }
            else
            {
                //If this is not set as available, then set to the disabled colour
                buttonIbutonSelect.BackColor = Color.FromArgb(204, 204, 204);
                textBoxIButtonSerial.BackColor = Color.FromArgb(240, 240, 240);
            }

            //All data may be ok but at least one form of environmental data must be selected 
            buttonSave.Enabled = ok && (checkBoxKestrelAvailable.Checked || checkBoxKestFileAvailable.Checked || checkBoxIbuttonFileAvailable.Checked);
            buttonSpecies.Enabled = buttonSave.Enabled;
            return buttonSave.Enabled;
        }

        /// <summary>
        /// Enables the controls in the group box regions based on whather the user has specified that data is available
        /// </summary>
        private void CheckBoxEnable()
        {
            //Kestrel
            buttonSelectKestrelFile.Enabled = checkBoxKestFileAvailable.Checked;
            textBoxKestrelSerial.Enabled = checkBoxKestFileAvailable.Checked;

            //iButton
            buttonIbutonSelect.Enabled = checkBoxIbuttonFileAvailable.Checked;
            textBoxIButtonSerial.Enabled = checkBoxIbuttonFileAvailable.Checked;

            //Air temprature
            textBoxAirMin.Enabled = checkBoxKestrelAvailable.Checked;
            textBoxAirMax.Enabled = checkBoxKestrelAvailable.Checked;
            textBoxAirAvg.Enabled = checkBoxKestrelAvailable.Checked;

            //Relative humidity
            textBoxHumidMin.Enabled = checkBoxKestrelAvailable.Checked;
            textBoxHumidMax.Enabled = checkBoxKestrelAvailable.Checked;
            textBoxHumidAvg.Enabled = checkBoxKestrelAvailable.Checked;

            //Wind speed
            textBoxWindMin.Enabled = checkBoxKestrelAvailable.Checked;
            textBoxWindMax.Enabled = checkBoxKestrelAvailable.Checked;
            textBoxWindAvg.Enabled = checkBoxKestrelAvailable.Checked;

            //Air pressure
            textBoxPressureMin.Enabled = checkBoxKestrelAvailable.Checked;
            textBoxPressureMax.Enabled = checkBoxKestrelAvailable.Checked;
            textBoxPressureAvg.Enabled = checkBoxKestrelAvailable.Checked;

            comboBoxWindDir.Enabled = checkBoxKestrelAvailable.Checked;

            //Run the validation function to check if we are ok to enable saving
            ValidationHelper();
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
        /// Event handler for the text changing in the distance to nearest light source text box
        /// </summary>
        /// <param name="sender">The distance to nearest light source text box</param>
        /// <param name="e">The event arguments</param>
        private void EditControlTextChanged(object sender, EventArgs e)
        {
            ValidationHelper();
        }

        /// <summary>
        /// Event handler for the ... available check boxes, incl. kestral data, kestrel file and ibutton file
        /// </summary>
        /// <param name="sender">A checkbox</param>
        /// <param name="e">The event arguments</param>
        private void AvailableCheckChanged(object sender, EventArgs e)
        {
            //Sets which controls are enabled or not now. Validation is performed in this function
            CheckBoxEnable();
        }

        /// <summary>
        /// Event handler for clicking the input species data button
        /// </summary>
        /// <param name="sender">The input species data button</param>
        /// <param name="e">The event arguments</param>
        private void ButtonSpeciesCountsClick(object sender, EventArgs e)
        {
            this.Hide();

            //Manually set size and location to "replace" this window
            InputDataForm.StartPosition = FormStartPosition.Manual;

            InputDataForm.Size = this.Size;
            InputDataForm.Location = this.Location;

            DialogResult res = InputDataForm.ShowDialog();
            
            //Closing without saving
            if (res == DialogResult.Cancel)
            {
                this.Close();
            }
            else if (res == DialogResult.Yes) //Closing and saving
            {
                SaveNight();
                this.Close();
            }
            else if (res == DialogResult.OK) //Going back
            {
                this.Size = InputDataForm.Size;
                this.Location = InputDataForm.Location;

                this.Show();
            }
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
