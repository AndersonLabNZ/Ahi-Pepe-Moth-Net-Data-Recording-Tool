using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MothNet
{
    public partial class EnvironmentalData : Form, IFileHandleHold
    {
        /// <summary>
        /// The folder the night data is stored in
        /// </summary>
        public string NightDataLocation { get; private set; }

        /// <summary>
        /// The kestral file data
        /// </summary>
        public DataFileItem KestrelFile { get; private set; }

        /// <summary>
        /// The ibutton file data
        /// </summary>
        public DataFileItem IButtonFile { get; private set; }

        /// <summary>
        /// Whether the dialog closed in a state with valid data
        /// </summary>
        public bool Success { get; private set; } = false;

        public EnvironmentalData(string folderLocation, bool create)
        {
            //Any other code goes in the try
            try
            {
                NightDataLocation = folderLocation;
                InitializeComponent();
                HelperFunctions.AddFileItemsToComboBox(comboBoxWindDir, false, "aspect");
                CheckBoxEnable();
                KestrelFile = DataFileItem.Hold(Path.Combine(NightDataLocation, "kestrel_data.txt"), create);
                IButtonFile = DataFileItem.Hold(Path.Combine(NightDataLocation, "ibutton_data.txt"), create);
                buttonSave.Enabled = false;
            }
            catch (Exception e)
            {
                CloseHandles();
                throw new CannotLoadException("Could not load Kestrel or iButton files", e);
            }
            //Any other code goes in the try
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

        public bool ValidationHelper()
        {
            bool ok = true;

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
            return buttonSave.Enabled;
        }

        /// <summary>
        /// Event handler for clicking the save button
        /// </summary>
        /// <param name="sender">The save button</param>
        /// <param name="e">The event args</param>
        private void SaveButtonClick(object sender, EventArgs e)
        {
            //Note if the close occured with successful data. If the user closes without clicking save this could be false, but in this case should be true
            Success = ValidationHelper();
            
            //Shouldn't be able to click this button without data being ok
            this.Hide();
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
        /// Closes any open file handles i.e. the krestel and ibutton files
        /// </summary>
        public void CloseHandles()
        { 
            KestrelFile?.Dispose();
            IButtonFile?.Dispose();
        }

        /// <summary>
        /// Event handler for the text box text change events
        /// </summary>
        /// <param name="sender">A text box</param>
        /// <param name="e">The event arguments</param>
        private void UpdateTextBoxes(object sender, EventArgs e)
        {
            ValidationHelper();
        }

        /// <summary>
        /// Event handler for the form closing. Prevents closing and hides the form - closing happens when the night closes
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">The event argumets</param>
        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            HelperFunctions.HandleFormClosing(sender, e);
        }
    }
}
