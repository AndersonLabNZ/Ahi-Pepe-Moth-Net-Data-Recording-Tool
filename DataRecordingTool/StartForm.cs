using System;
using System.IO;
using System.Windows.Forms;

namespace MothNet
{

    /// <summary>
    /// The initial form
    /// </summary>
    public partial class StartForm : Form
    {
        /// <summary>
        /// The ctor for the form. Starts by creating the relevent directories, although this isn't really necessary given that File.Open appears to create the directories anyway
        /// </summary>
        public StartForm()
        {
            Directory.CreateDirectory(HelperFunctions.ParentDir);
            Directory.CreateDirectory(HelperFunctions.SitesDir);
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for the create site button being clicked. Creates a site, and opens the edit site dialog
        /// </summary>
        /// <param name="sender">The create button</param>
        /// <param name="e">Event arguments</param>
        private void CreateSiteClick(object sender, EventArgs e)
        {
            try
            {
                HelperFunctions.CreateSite();
            }
            catch (CannotLoadException except)
            {
                MessageBox.Show(HelperFunctions.GetExceptionUserMessage(except), "Cannot Create Site", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens the modify dialog
        /// </summary>
        /// <param name="sender">The modify button</param>
        /// <param name="e">The event arguments</param>
        private void ModifySiteClick(object sender, EventArgs e)
        {
            GetName getName = new GetName();
            
            //Short circuit operator - ctor sets DialogResult so if DialogResult is not OK don't show
            if (getName.DialogResult == DialogResult.OK && getName.ShowDialog() == DialogResult.OK)
            {
                //Show the edit site dialog
                getName.SelectedSite.ShowDialog();

                //Close handles when done. Don't need a CloseHandles function since the site is created in this dialog.
                getName.Close();
                getName.Dispose();
            }
            return;
        }
    }
}
