using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace MothNet
{
    /// <summary>
    /// The form that holds the array of night edit forms. Each edit form represents a unique night
    /// </summary>
    public partial class NightForm : Form
    {
        /// <summary>
        /// The parent form
        /// </summary>
        public EditSite EditSite { get; private set; }

        /// <summary>
        /// The path to the folder that contains the night data
        /// </summary>
        public string NightsDir
        {
            get
            {
                string val = Path.Combine(EditSite.FolderDirectory, "nights");
                Directory.CreateDirectory(val);
                return val;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">The parent EditSite form</param>
        public NightForm(EditSite data)
        {
            //Any other code goes in the try
            try
            {
                EditSite = data;
                string temp = NightsDir; //Force folder creation
                InitializeComponent();

                //Iterate through any pre-existing nights
                foreach (string s in Directory.EnumerateDirectories(NightsDir))
                {
                    if (Guid.TryParse(Path.GetFileName(s), out Guid guid))
                    {
                        //If the name is a valid GUID add it to the GUID list
                        listBoxNights.Items.Add(new GuidItem(guid, HelperFunctions.LoadName(s, "night_name.txt")));
                    }
                }
            }
            catch (Exception e) //Catch any exception
            {
                if (e is CannotLoadException)
                {
                    //If the exception is a cannot load exception, throw it up the stack again
                    throw;
                }
                //Otherwise. create a new exception with the origonal exception as the inner exception
                throw new CannotLoadException("Cannot load night data", e);
            }
            //Any other code goes in the try     
        }

        /// <summary>
        /// Event handler for the clicking of the remove button. Deletes the selected site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButtonClick(object sender, EventArgs e)
        {
            GuidItem item = (GuidItem)listBoxNights.Items[listBoxNights.SelectedIndex];

            //Check if user actually wants to delete the night
            if (MessageBox.Show("Are you sure you want to remove the night data for this night?\n" + item.Name, "Are you sure", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    //Delete the directory
                    Directory.Delete(Path.Combine(NightsDir, item.Guid.ToString("B")), true);
                }
                finally
                {
                    //Ignoring any exceptions (e.g. DirectoryNotFoundException), make sure the night is deleted from the list
                    listBoxNights.Items.RemoveAt(listBoxNights.SelectedIndex);
                }
            }
        }

        /// <summary>
        /// Event handler for clicking the add button
        /// </summary>
        /// <param name="sender">The add button</param>
        /// <param name="e">The event arguments</param>
        private void AddButtonClick(object sender, EventArgs e)
        {
            //Attempt to create a new night
            NightEdit edit;
            try
            {
                edit = new NightEdit(EditSite, Guid.Empty);
            }
            catch (CannotLoadException except)
            {
                MessageBox.Show(HelperFunctions.GetExceptionUserMessage(except), "Could Not Create Night Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //If successful, show the dialog.
            edit.ShowDialog();

            if (edit.Saving)
            {
                //If saving, add to the list
                listBoxNights.Items.Add(new GuidItem(edit.FileGuid, edit.NightID));
            }
            else
            {
                //If not saving, don't add to the list, and close the handles / delete any data
                edit.CloseHandles();
                edit.Close();
                edit.Dispose();
            }
        }

        /// <summary>
        /// Opens the selected night for editing
        /// </summary>
        private void EditNightData()
        {
            //Make sure that a night is actually selected
            if (listBoxNights.SelectedIndex >= 0)
            {
                //If show, open and show
                try
                {
                    NightEdit edit = new NightEdit(EditSite, ((GuidItem)listBoxNights.Items[listBoxNights.SelectedIndex]).Guid);
                    edit.ShowDialog();
                }
                catch (CannotLoadException except)
                {
                    MessageBox.Show(HelperFunctions.GetExceptionUserMessage(except), "Could Not Load Night Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Event handler for clicking the edit button
        /// </summary>
        /// <param name="sender">The edit button</param>
        /// <param name="e">The event arguments</param>
        private void EditButtonClick(object sender, EventArgs e)
        {
            EditNightData();
        }

        /// <summary>
        /// Event handler for double clicking a list item
        /// </summary>
        /// <param name="sender">The main list box</param>
        /// <param name="e">The event arguments</param>
        private void ListBoxDoubleClick(object sender, EventArgs e)
        {
            EditNightData();
        }

        /// <summary>
        /// Event handler for clicking the save button
        /// </summary>
        /// <param name="sender">The save button</param>
        /// <param name="e">The event arguments</param>
        private void SaveButtonClick(object sender, EventArgs e)
        {
            //Make sure that there are actually nights to save
            int count = listBoxNights.Items.Count;
            if (count == 0)
            {
                MessageBox.Show("There is no data for any nights", "No Nights", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Make sure that no nights are duplicated. Make all comparisons
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    NightEdit left = (NightEdit)listBoxNights.Items[i];
                    NightEdit right = (NightEdit)listBoxNights.Items[j];
                    if (left.dateTimePickerDate.Value.Date == right.dateTimePickerDate.Value.Date)
                    {
                        MessageBox.Show("Some nights have the same date", "Duplicate Nights", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            //Check if the user want's to export the data to a zip file
            if (MessageBox.Show("Do you want to export the data?", "Export Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Create a save file dialog. only allow zip files
                SaveFileDialog dialog = new SaveFileDialog
                {
                    AddExtension = true,
                    AutoUpgradeEnabled = true,
                    DefaultExt = ".zip",
                    Filter = "Zipped Archive (*.zip) | *.zip",
                    OverwritePrompt = true,
                    SupportMultiDottedExtensions = false,
                    Title = "Chose export location",
                    ValidateNames = true
                };

                //Return if they cancel
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                //Create a temportay directory, by (if required, deleting), and creating it
                string tempDir = Path.Combine(HelperFunctions.ParentDir, "temp");

                

                Directory.CreateDirectory(tempDir);

                try
                {
                    //Copy all required data, starting with the site data. Ignoring the site name file, it is not needed
                    File.Copy(EditSite.DataFileName, Path.Combine(tempDir, EditSite.textBoxSiteName.Text + ".txt"));

                    //Now copy all the night data
                    foreach (GuidItem item in listBoxNights.Items)
                    {
                        string parentPath = Path.Combine(HelperFunctions.SitesDir, EditSite.FileGuid.ToString(), "nights", item.ToString());
                        string idName = HelperFunctions.LoadName(parentPath, "night_name.txt");
                        File.Copy(Path.Combine(parentPath, "site_data.txt"), Path.Combine(tempDir, idName + ".txt"));

                        File.Copy(Path.Combine(parentPath, "ibutton_data.txt"), Path.Combine(tempDir, idName + "_ibutton.txt"));
                        File.Copy(Path.Combine(parentPath, "kestrel_data.txt"), Path.Combine(tempDir, idName + "_kestrel.txt"));

                        File.Copy(Path.Combine(parentPath, "moth_abundances.txt"), Path.Combine(tempDir, idName + "_inds.txt"));
                        File.Copy(Path.Combine(parentPath, "rodent_abundances.txt"), Path.Combine(tempDir, idName + "_inds_rodents.txt"));
                    }
                }
                catch (FileNotFoundException except)
                {
                    MessageBox.Show("Could not copy data - " + except.Message, "Unable to Export Data", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    try
                    {
                        Directory.Delete(tempDir, true);
                    }
                    catch (DirectoryNotFoundException) { }

                    return;
                }

                //Make sure no file already exists
                try
                {
                    File.Delete(dialog.FileName);
                }
                catch (FileNotFoundException) { }
                catch (DirectoryNotFoundException) { }
                //If the file we are deleting doesn't exist don't complain
                
                //Create a zip file
                ZipFile.CreateFromDirectory(tempDir, dialog.FileName);
            }
            this.Close();
        }

        /// <summary>
        /// Event handler for the form closing event. Closes open file handles
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">The event args</param>
        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            HelperFunctions.HandleFormClosing(sender, e);
        }
    }
}
