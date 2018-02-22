using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MothNet
{
    /// <summary>
    /// The dialog for the user to choose the site to edit
    /// </summary>
    public partial class GetName : Form
    {
        /// <summary>
        /// The site the user has selected
        /// </summary>
        public EditSite SelectedSite { get; private set; }

        /// <summary>
        /// Constructor. Populates the GUID list and combo box
        /// </summary>
        public GetName()
        {
            InitializeComponent();

            //Enumerates the directories in the sites directory
            IEnumerable<string> folders = Directory.EnumerateDirectories(HelperFunctions.SitesDir, "*", SearchOption.TopDirectoryOnly);

            //If there are no folders i.e. no sites, then the user will be prompted to create one
            if (folders.Count() == 0)
            {
                if (MessageBox.Show(HelperFunctions.FormatResStr("MSG_TEXT_NO_SITES_CREATE_QUESTION"), HelperFunctions.FormatResStr("MSG_TITLE_NO_SITES_CREATE_QUESTION"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Create site, and run through the dialogs. Continues to the DialogResult set as we don't want this form to be shown
                    try
                    {
                        HelperFunctions.CreateSite();
                    }
                    catch (CannotLoadException except)
                    {
                        MessageBox.Show(HelperFunctions.FormatResStr("MSG_TEXT_CREATE_SITE_FAIL", HelperFunctions.GetExceptionUserMessage(except)), HelperFunctions.FormatResStr("MSG_TITLE_CREATE_SITE_FAIL"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //Set DialogResult to ensure that the window is simply closed, i.e. not shown
                DialogResult = DialogResult.Cancel;
                return;
            }

            //Iterate through all the directories, i.e. sites
            foreach (string s in folders)
            {
                //Try to parse the Guid. If the name is not a GUID, ignore it
                if (Guid.TryParseExact(Path.GetFileName(s), "B", out Guid guid))
                {
                    comboBoxSiteName.Items.Add(new GuidItem(guid, HelperFunctions.LoadName(s, "site_name.txt")));
                }
            }

            //Make sure that an item is selected to that something will appear in the combo box
            comboBoxSiteName.SelectedIndex = 0;

            //Set the dialog result to ok so that the dialog will be shown
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Event handler for the open button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKClick(object sender, EventArgs e)
        {
            //Sets result to ok, and opens the site for editing. Closes the dialog after finishing
            this.DialogResult = DialogResult.OK;
            try
            {
                SelectedSite = new EditSite(((GuidItem)comboBoxSiteName.Items[comboBoxSiteName.SelectedIndex]).Guid);
            }
            catch (CannotLoadException except)
            {
                MessageBox.Show(HelperFunctions.FormatResStr("MSG_TEXT_OPEN_SITE_FAIL", HelperFunctions.GetExceptionUserMessage(except)), HelperFunctions.FormatResStr("MSG_TITLE_OPEN_SITE_FAIL"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Set to abort to override form closing
                DialogResult = DialogResult.Abort;
                return;
            }
            this.Close();
        }

        /// <summary>
        /// Override for form closing handler. Avoids closing if DialogResult is set to abort
        /// </summary>
        /// <param name="e">The event args</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = DialogResult == DialogResult.Abort;
        }

        /// <summary>
        /// Event handler for if a control key is pressed. Used to handle a user pressing enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditControlKeyDown(object sender, KeyEventArgs e)
        {
            //If the pressed kay was enter, call the button click handler
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                OKClick(sender, new EventArgs());
            }
        }
    }
}
