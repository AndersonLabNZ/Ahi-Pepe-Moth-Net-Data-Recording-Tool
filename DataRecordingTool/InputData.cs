using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MothNet
{
    /// <summary>
    /// The form for entering the abundance data
    /// </summary>
    public partial class InputData : Form, IFileHandleHold
    {
        /// <summary>
        /// The night data directory
        /// </summary>
        public string Directory { get; private set; }

        /// <summary>
        /// The data for the moth abundances
        /// </summary>
        public DataTypeClass Moths { get; private set; }

        /// <summary>
        /// The data for the rodent abundances
        /// </summary>
        public DataTypeClass Rodents { get; private set; }

        //If the night data is being created, i.e. new
        private bool creating;

        string[] erebidae;
        string[] geometridae;
        string[] hepialidae;
        string[] noctuidae;
        string[] saturniidae;
        string[] sphingidae;
        string[] nolidae;

        List<String> spList = new List<String>();

        /// <summary>
        /// The constructor for the input data class
        /// </summary>
        /// <param name="directory">The directory for the night</param>
        /// <param name="create">Whether the night is being created i.e. new</param>
        /// <param name="region">The currently selected region</param>
        public InputData(string directory, bool create, char region)
        {
           //Any other code goes in the try
            try
            {
                creating = create;
                Directory = directory;
                InitializeComponent();

                //Update the list of species 
                UpdateSpeciesList(region);

                //Open or create the moth abundance files
                try
                {
                    Moths = new DataTypeClass(this, "moth", directory, "moth_abundances.txt", create, 3);
                    Rodents = new DataTypeClass(this, "tracking tunnel", directory, "rodent_abundances.txt", create, 0, 1, 2, 3);
                }
                catch (IOException except) when ((except is DirectoryNotFoundException || except is FileNotFoundException) && !create)
                {
                    throw new CannotLoadException("Could not moth or rodent file. The file was not found.", except);
                }

                //Load the resource files
                erebidae = HelperFunctions.GetResourceList("species_list_erebidae");
                geometridae = HelperFunctions.GetResourceList("species_list_geometridae");
                hepialidae = HelperFunctions.GetResourceList("species_list_hepialidae");
                noctuidae = HelperFunctions.GetResourceList("species_list_noctuidae");
                saturniidae = HelperFunctions.GetResourceList("species_list_saturniidae");
                sphingidae = HelperFunctions.GetResourceList("species_list_sphingidae");
                nolidae = HelperFunctions.GetResourceList("species_list_nolidae");

                //Add to the list of species
                spList.AddRange(erebidae);
                spList.AddRange(geometridae);
                spList.AddRange(hepialidae);
                spList.AddRange(noctuidae);
                spList.AddRange(saturniidae);
                spList.AddRange(sphingidae);
                spList.AddRange(nolidae);

                //Check which is selected - the moth or rodent abundances. Moths are the default
                CheckBoxCheckChange(null, EventArgs.Empty);
                prevRegion = region;
            }
            catch (Exception)
            {
                CloseHandles();
                throw;
            }
            //Any other code goes in the try
        }

        private char prevRegion;

        /// <summary>
        /// Loads the list of moths that are expected in the specified region
        /// </summary>
        /// <param name="region">The character representing the region the moths are expected in, e.g. A, B, C etc. An asterisk '*' is used to match any region</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string[] GetSpeciesList(char region, string name)
        {
            switch (name)
            {
                case "Moths":
                {
                    //Create the list which will hold the species
                    List<String> returnList = new List<String>();
                    
                    foreach (string item in spList)
                    {
                        string[] split = item.Split(',');
                        if (region == '*' || split[0].Contains(region.ToString()))
                        {
                            returnList.Add(split[1]);
                        }
                    }
                    return returnList.ToArray();
                }
                case "Rodents":
                {
                    //List have to load the rodent list. Small file so can just load each time
                    return HelperFunctions.GetResourceList("species_list_rodents");
                }
                default:
                    return null;
            }
        }

        /// <summary>
        /// Updates the active species list for the specified region
        /// </summary>
        /// <param name="region">The region to filter the species list for</param>
        public void UpdateSpeciesList(char region)
        {
            //Clears the combobox
            comboBoxSpecies.Items.Clear();
            comboBoxSpecies.Text = "";

            //Choose whether to update to moths or rodents
            if (radioButtonMoths.Checked)
            {
                comboBoxSpecies.Items.AddRange(GetSpeciesList(region, "Moths"));
            }
            else
            {
                comboBoxSpecies.Items.AddRange(GetSpeciesList(region, "Rodents"));
            }

            //Reset the prevRegion field to the current region
            prevRegion = region;
        }

        /// <summary>
        /// Close the file handles on closing
        /// </summary>
        public void CloseHandles()
        {
            Moths?.Dispose();
            Rodents?.Dispose();
        }

        /// <summary>
        /// Adds a list of items to the listview
        /// </summary>
        /// <param name="items">The items to add</param>
        private void AddRangeItem(ListViewItem[] items)
        {
            //Iterates through each and adds them to the list
            foreach (ListViewItem item in items)
            {
                AddItem(item);
            }
        }

        /// <summary>
        /// Adds a single item to the list view
        /// </summary>
        /// <param name="item">The item to add</param>
        private void AddItem(ListViewItem item)
        {
            //Adds to the rodent or moth list depending on which radio button is selected
            if (radioButtonMoths.Checked)
            {
                Moths.AddItem(item);
            }
            else if (radioButtonRodents.Checked)
            {
                Rodents.AddItem(item);
            }
        }

        /// <summary>
        /// Event handler for clicking the remove button
        /// </summary>
        /// <param name="sender">The remove button</param>
        /// <param name="e">The event arguments</param>
        private void RemoveItemClicked(object sender, EventArgs e)
        {
            //Iterates through all selected items and adds them to a list
            List<ListViewItem> items = new List<ListViewItem>();
            foreach (int i in listViewMoths.SelectedIndices)
            {
                items.Add(listViewMoths.Items[i]);
            }

            //Removes the items from the above list from the moth or rodent list, depending on which is selected.
            foreach (ListViewItem item in items)
            { 
                if (radioButtonMoths.Checked)
                {
                    Moths.RemoveItem(item);
                }
                else if (radioButtonRodents.Checked)
                {
                    Rodents.RemoveItem(item);
                }
            }
        }

        /// <summary>
        /// Event handler for the checkstate of the radio buttons changing
        /// </summary>
        /// <param name="sender">Either of the two radio buttons</param>
        /// <param name="e">The event arguments</param>
        private void CheckBoxCheckChange(object sender, EventArgs e)
        {
            //Clear the list view
            listViewMoths.Items.Clear();

            if (radioButtonMoths.Checked)
            {
                //If moths are checked then add the moths to the list view
                listViewMoths.Columns[1].Text = "Voucher Number";
                listViewMoths.Items.AddRange(Moths.Items);
            }
            else if (radioButtonRodents.Checked)
            {
                //If rodents are checked then add the rodents to lhe list view
                listViewMoths.Columns[1].Text = "Tracking Tunnel Number";
                listViewMoths.Items.AddRange(Rodents.Items);
            }

            //Update the species list to ensure that the rodents or moths are loaded as appropriate. Pass prevRegion as it should be set to the current region
            UpdateSpeciesList(prevRegion);
        }

        /// <summary>
        /// Event handler for clicking the save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButtonClicked(object sender, EventArgs e)
        {
            SaveAbundances();
        }

        /// <summary>
        /// Saves the abundance data
        /// </summary>
        public void SaveAbundances()
        {
            //If both save without the user cancelling, then hide the form
            if (Moths.Save(GetSpeciesList(prevRegion, "Moths")) && Rodents.Save(GetSpeciesList(prevRegion, "Rodents")))
            {
                //Yes means saving and closing
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        /// <summary>
        /// Event handler for clicking the add button
        /// </summary>
        /// <param name="sender">The add button</param>
        /// <param name="e">The event arguments</param>
        private void AddButtonClicked(object sender, EventArgs e)
        {
            //Only add if a tag and voucher (tracking tunnel in the case of the rodents) number is provided
            if (!String.IsNullOrWhiteSpace(textBoxTag.Text) && !String.IsNullOrWhiteSpace(textBoxVoucher.Text))
            {
                //If the selected index is -1, then the user has added something not on the list. Prompt the user to make sure this is intended
                if (comboBoxSpecies.SelectedIndex == -1)
                {
                    if (MessageBox.Show("Are you sure you want to enter a species that is not on the list?", "Species Not on List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                //Add to the list if okay. Use the combobox text, so that any text input is still valid
                ListViewItem item = new ListViewItem(new string[] { (string)comboBoxSpecies.Text, textBoxVoucher.Text, textBoxTag.Text, numericUpDownCount.Value.ToString() });
                AddItem(item);
            }
        }

        /// <summary>
        /// Event handler for clicking the return button
        /// </summary>
        /// <param name="sender">The return button</param>
        /// <param name="e">The event arguments</param>
        private void ReturnButtonClick(object sender, EventArgs e)
        {
            //OK to ensure the window opens
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        /// <summary>
        /// Event handler for resizing the form
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">The event arguments</param>
        private void FormResized(object sender, EventArgs e)
        {
            listViewMoths.Columns[0].Width = comboBoxSpecies.Width + 3;
            listViewMoths.Columns[1].Width = textBoxVoucher.Width + 3;
            listViewMoths.Columns[2].Width = textBoxTag.Width + 3;
            listViewMoths.Columns[3].Width = numericUpDownCount.Width + 3;
        }
    }

    /// <summary>
    /// The class to handle moth or rodent data
    /// </summary>
    public class DataTypeClass : IDisposable
    {
        /// <summary>
        /// If the data is going to be saves
        /// </summary>
        public bool Saving { get; private set; } = false;

        /// <summary>
        /// The items input by the user
        /// </summary>
        private List<ListViewItem> m_items;

        /// <summary>
        /// The items as an array
        /// </summary>
        public ListViewItem[] Items
        {
            get
            {
                return m_items.ToArray();
            }
        }

        /// <summary>
        /// The file stream for the data file
        /// </summary>
        private FileStream Stream { get; set; }

        /// <summary>
        /// The directory the data file is in
        /// </summary>
        private string m_directory;

        /// <summary>
        /// The filename of the data file
        /// </summary>
        private string m_filename;

        /// <summary>
        /// Whether the data is new or pre-existing
        /// </summary>
        private bool m_create;

        /// <summary>
        /// A list of indexes of the individual entries which specifies which ones can 
        /// </summary>
        private int[] m_allowDup;

        /// <summary>
        /// The name of the specified data
        /// </summary>
        private string m_name;

        /// <summary>
        /// The inputdata window that contains the listview
        /// </summary>
        private InputData m_inputData;

        /// <summary>
        /// The count of how many items there are
        /// </summary>
        public int Count
        {
            get
            {
                return m_items.Count;
            }
        }

        /// <summary>
        /// The path to the file that holds the abundance data
        /// </summary>
        public string FileDirectory
        {
            get
            {
                return Path.Combine(m_directory, m_filename);
            }
        }

        /// <summary>
        /// Constructor for a new moth or rodent list class
        /// </summary>
        /// <param name="data">The input data form</param>
        /// <param name="name">The name of the data</param>
        /// <param name="directory">The directory the data is in</param>
        /// <param name="filename">The filename of the data file in the 'directory' directory</param>
        /// <param name="create">Whether the data is being created. If not, it is loaded</param>
        /// <param name="allowDuplicates">The indicies to allow duplicate data accross</param>
        public DataTypeClass(InputData data, string name, string directory, string filename, bool create, params int[] allowDuplicates)
        {
            m_name = name;
            m_items = new List<ListViewItem>();
            m_allowDup = allowDuplicates;
            m_inputData = data;
            m_directory = directory;
            m_filename = filename;
            m_create = create;

            if (m_create)
            {
                Directory.CreateDirectory(directory);
                Stream = new FileStream(FileDirectory, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            }
            else
            {
                Stream = new FileStream(FileDirectory, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);

                //Load data
                Load();
            }
        }

        private void LoadFileCallBack(int index, string value)
        {
            //If line empty ignore
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            //Splits the CSV text file
            List<String> list = new List<String>(value.Split(','));

            //Removes any trailing CR characters
            list.Remove("\r");

            //Should be four
            if (list.Count != 4)
            {
                //There don't have to be any species - but if there are they should be in the correct format
                throw new CannotLoadException(string.Format("Expected four columns for species data - but got {0} instead", list.Count));
            }
            else if (!Int32.TryParse(list[3], out int count))
            {
                //The 4th item should be an integer, as it is the abundance
                throw new CannotLoadException(string.Format("Expected an integer for the amount but got \"{0}\" instaed", list[3]));
            }
            else
            {
                //Create the item and add it
                ListViewItem item = new ListViewItem(list.ToArray());
                AddItem(item);
            }
        }

        /// <summary>
        /// Check for duplicate data, e.g. duplicated moth entries
        /// </summary>
        /// <returns>If all is okay, i.e. no duplicates</returns>
        public bool CheckDuplicate()
        {
            //Iterate through the four fields
            for (int i = 0; i < 4; i++)
            {
                bool allowContinue = true;

                //If the current index is in the allowed duplicates list, skip it
                foreach (int l in m_allowDup)
                {
                    if (i == l)
                    {
                        allowContinue = false;
                        break;
                    }
                }
                if (allowContinue)
                {
                    //Compare all items O(n!) for duplicates
                    for (int j = 0; j < Count; j++)
                    {
                        for (int k = j + 1; k < Count; k++)
                        {
                            string left = m_items[j].SubItems[i].Text;
                            string right = m_items[k].SubItems[i].Text;
                            if (left == right)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Adds an item to the list and the list view
        /// </summary>
        /// <param name="item">The item to add</param>
        public void AddItem(ListViewItem item)
        {
            m_items.Add(item);
            m_inputData.listViewMoths.Items.Add(item);
        }

        /// <summary>
        /// Removes an item from the list and the list view
        /// </summary>
        /// <param name="item">The item to remove</param>
        public void RemoveItem(ListViewItem item)
        {
            m_items.Remove(item);
            m_inputData.listViewMoths.Items.Remove(item);
        }

        /// <summary>
        /// Removes an item from the list and the list view, specified by index
        /// </summary>
        /// <param name="item">The index of the item to remove</param>
        public void RemoveItem(int index)
        {
            m_items.RemoveAt(index);
            m_inputData.listViewMoths.Items.RemoveAt(index);
        }

        /// <summary>
        /// Loads the abundance data from file
        /// </summary>
        public void Load()
        {
            HelperFunctions.DoLoadFileHelper(Stream, LoadFileCallBack);
        }
        
        /// <summary>
        /// Saves the abundance data to file
        /// </summary>
        /// <param name="validSpecies"></param>
        /// <returns>Wether the save completed without the user cancelling</returns>
        public bool Save(string[] validSpecies)
        {
            //Iterate through each item
            foreach (ListViewItem iten in Items)
            {
                //Make sure not unrecognised species have been added
                bool ok = false;

                //Iterate through all valid species
                foreach (string valid in validSpecies)
                {
                    //If there is a match, all is ok
                    if (iten.SubItems[0].Text == valid)
                    {
                        ok = true;
                        break;
                    }
                }

                //If not, prompt the user
                if (!ok)
                {
                    if (MessageBox.Show("Some " + m_name + " species (e.g. " + iten.SubItems[0].Text + ") have been input that are not in the list provided.\nDo you want to continue?", "Invalid Species", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        //If this is intentional, then skip all validity checking
                        break;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            //Check for duplicates. If this is ok, then continue
            if (!CheckDuplicate())
            {
                if (MessageBox.Show("There is some duplicate data for the " + m_name + " abudances.\r\nDo you want to continue?", "Duplicate Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return false;
                }
            }

            //Set saving to true so that the file is not deleted on close
            Saving = true;

            //Seek to the beginning of the file
            Stream.Seek(0, SeekOrigin.Begin);

            StringBuilder sb = new StringBuilder();

            foreach (ListViewItem item in m_items)
            {
                //Write in a CSV format
                int count = 0;
                foreach (ListViewItem.ListViewSubItem value in item.SubItems)
                {
                    sb.Append(value.Text + (count < 3 ? "," : ""));
                    count++;
                }
                sb.AppendLine();
            }

            //Encode as UTF8
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString().ToCharArray());
            Stream.Write(bytes, 0, bytes.Length);

            //Flush and reset
            Stream.Flush();
            Stream.Seek(0, SeekOrigin.Begin);
            Stream.SetLength(bytes.LongLength);

            return true;
        }

        /// <summary>
        /// Closes the open data file handle
        /// </summary>
        public void Dispose()
        {
            Stream.Close();

            //Delete the data if the files are being created but the user doesn't want to save it
            if (m_create && !Saving)
            {
                HelperFunctions.SafeDeleteFile(FileDirectory);
            }
        }
    }
}
