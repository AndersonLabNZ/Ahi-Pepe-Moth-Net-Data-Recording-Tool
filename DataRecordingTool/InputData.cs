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
        /// The 
        /// </summary>
        public DataTypeClass Moths { get; private set; }

        public DataTypeClass Rodents { get; private set; }

        private bool creating;

        public InputData(string directory, bool create, char region)
        {
           //Any other code goes in the try
            try
            {
                creating = create;
                Directory = directory;
                InitializeComponent();
                UpdateSpeciesList(region);
                Moths = new DataTypeClass(this, "moth", directory, "moth_abundances.txt", create, 3);
                Rodents = new DataTypeClass(this, "tracking tunnel", directory, "rodent_abundances.txt", create, 0, 1, 2, 3);
                CheckBoxCheckChange(null, EventArgs.Empty);
                prevRegion = region;
            }
            catch (Exception e)
            {
                CloseHandles();
                if (e is CannotLoadException)
                {
                    throw;
                }
                throw new CannotLoadException("Cannot load moth abundance data: " + e.Message, e);
            }
            //Any other code goes in the try
        }

        private char prevRegion;

        public string[] GetSpeciesList(char region, string name)
        {
            switch (name)
            {
                case "Moths":
                {
                    List<String> returnList = new List<String>();
                    string[] erebidae = HelperFunctions.GetResourceList("species_list_erebidae");
                    string[] geometridae = HelperFunctions.GetResourceList("species_list_geometridae");
                    string[] hepialidae = HelperFunctions.GetResourceList("species_list_hepialidae");
                    string[] noctuidae = HelperFunctions.GetResourceList("species_list_noctuidae");
                    string[] saturniidae = HelperFunctions.GetResourceList("species_list_saturniidae");
                    string[] sphingidae = HelperFunctions.GetResourceList("species_list_sphingidae");
                    string[] nolidae = HelperFunctions.GetResourceList("species_list_nolidae");
                    List<String> list = new List<String>();
                    list.AddRange(erebidae);
                    list.AddRange(geometridae);
                    list.AddRange(hepialidae);
                    list.AddRange(noctuidae);
                    list.AddRange(saturniidae);
                    list.AddRange(sphingidae);
                    list.AddRange(nolidae);

                    foreach (string item in list)
                    {
                        string[] split = item.Split(',');
                        if (split[0].Contains(region.ToString()))
                        {
                            returnList.Add(split[1]);
                        }
                    }
                    return returnList.ToArray();
                }
                case "Rodents":
                {
                    return HelperFunctions.GetResourceList("species_list_rodents");
                }
                default:
                    return null;
            }
        }

        public void UpdateSpeciesList(char region)
        {
            comboBoxSpecies.Items.Clear();
            comboBoxSpecies.Text = "";
            if (radioButtonMoths.Checked)
            {
                comboBoxSpecies.Items.AddRange(GetSpeciesList(region, "Moths"));
            }
            else
            {
                comboBoxSpecies.Items.AddRange(GetSpeciesList(region, "Rodents"));
            }
            prevRegion = region;
        }

        public void CloseHandles()
        {
            Moths.Dispose();
            Rodents.Dispose();
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            HelperFunctions.HandleFormClosing(sender, e);
        }

        private void AddRangeItem(ListViewItem[] items)
        {
            foreach (ListViewItem item in items)
            {
                AddItem(item);
            }
        }

        private void AddItem(ListViewItem item)
        {
            if (radioButtonMoths.Checked)
            {
                Moths.AddItem(item);
            }
            else if (radioButtonRodents.Checked)
            {
                Rodents.AddItem(item);
            }
        }

        private void RemoveItemClicked(object sender, EventArgs e)
        {
            List<ListViewItem> items = new List<ListViewItem>();
            foreach (int i in listViewMoths.SelectedIndices)
            {
                items.Add(listViewMoths.Items[i]);
            }
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

        private void CheckBoxCheckChange(object sender, EventArgs e)
        {
            listViewMoths.Items.Clear();
            if (radioButtonMoths.Checked)
            {
                
                listViewMoths.Columns[1].Text = "Voucher Number";
                listViewMoths.Items.AddRange(Moths.Items);
            }
            else if (radioButtonRodents.Checked)
            {
                listViewMoths.Columns[1].Text = "Tracking Tunnel Number";
                listViewMoths.Items.AddRange(Rodents.Items);
            }
            UpdateSpeciesList(prevRegion);
        }

        private void SaveButtonClicked(object sender, EventArgs e)
        {
            if (Moths.Save(GetSpeciesList(prevRegion, "Moths")) && Rodents.Save(GetSpeciesList(prevRegion, "Rodents")))
            {
                this.Hide();
            }
        }

        private void AddButtonClicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxTag.Text) && !String.IsNullOrWhiteSpace(textBoxVoucher.Text))
            {
                if (comboBoxSpecies.SelectedIndex == -1)
                {
                    if (MessageBox.Show("Are you sure you want to enter a species that is not on the list?", "Species Not on List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                ListViewItem item = new ListViewItem(new string[] { (string)comboBoxSpecies.Text, textBoxVoucher.Text, textBoxTag.Text, numericUpDownCount.Value.ToString() }); //Use the combobox text, so that any text input is still valid
                AddItem(item);
            }
        }
    }

    public class DataTypeClass : IDisposable
    {
        public bool Saving { get; private set; } = false;

        private List<ListViewItem> m_items;

        public ListViewItem[] Items
        {
            get
            {
                return m_items.ToArray();
            }
        }

        private FileStream Stream { get; set; }

        private string m_directory;

        private string m_filename;

        private bool m_create;

        private int[] m_allowDup;

        private string m_name;

        private InputData m_inputData;

        public int Count
        {
            get
            {
                return m_items.Count;
            }
        }

        public string FileDirectory
        {
            get
            {
                return Path.Combine(m_directory, m_filename);
            }
        }

        public DataTypeClass(InputData data, string name, string directory, string filename, bool create, params int[] allowDuplicates)
        {
            m_name = name;
            m_items = new List<ListViewItem>();
            m_allowDup = allowDuplicates;
            m_inputData = data;
            m_directory = directory;
            m_filename = filename;
            m_create = create;
            Stream = new FileStream(FileDirectory, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            if (m_create)
            {
                Directory.CreateDirectory(directory);
            }
            else
            {
                Load();
            }
        }

        private void LoadFileCallBack(int index, string value)
        {
            List<String> list = new List<String>(value.Split(','));
            list.Remove("\r");
            if (list.Count != 4)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    //There don't have to be any species - but if there are they should be in the correct format
                    throw new CannotLoadException(string.Format("Expected four columns for species data - but got {0} instead", list.Count));
                }
            }
            if (!Int32.TryParse(list[3], out int count))
            {
                throw new CannotLoadException(string.Format("Expected an integer for the amount but got \"{0}\" instaed", list[3]));
            }

            ListViewItem item = new ListViewItem(list.ToArray());
            AddItem(item);
        }

        public bool CheckDuplicate()
        {
            for (int i = 0; i < 4; i++)
            {
                bool allowContinue = true;
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
                    for (int j = 0; j < Count; j++)
                    {
                        for (int k = i + 1; k < Count; k++)
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

        public bool AddItem(ListViewItem item)
        {
            m_items.Add(item);
            m_inputData.listViewMoths.Items.Add(item);
            return true;
        }

        public void RemoveItem(ListViewItem item)
        {
            m_items.Remove(item);
            m_inputData.listViewMoths.Items.Remove(item);
        }

        public void RemoveItem(int index)
        {
            m_items.RemoveAt(index);
            m_inputData.listViewMoths.Items.RemoveAt(index);
        }

        public void Load()
        {
            HelperFunctions.DoLoadFileHelper(Stream, LoadFileCallBack);
        }
        
        public bool Save(string[] validSpecies)
        {
            foreach (ListViewItem iten in Items)
            {
                bool ok = false;
                foreach (string valid in validSpecies)
                {
                    if (iten.SubItems[0].Text == valid)
                    {
                        ok = true;
                        break;
                    }
                }
                if (!ok)
                {
                    if (MessageBox.Show("Some " + m_name + " species (e.g. " + iten.SubItems[0].Text + ") have been input that are not in the list provided.\nDo you want to continue?", "Invalid Species", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        break; //Don't check for any others in this one
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            Saving = true;

            if (!CheckDuplicate())
            {
                if (MessageBox.Show("There is some duplicate data for the " + m_name + " abudances.\r\nDo you want to continue?", "Duplicate Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return false;
                }
            }

            Stream.Seek(0, SeekOrigin.Begin);

            StringBuilder sb = new StringBuilder();

            foreach (ListViewItem item in m_items)
            {
                int count = 0;
                foreach (ListViewItem.ListViewSubItem value in item.SubItems)
                {
                    sb.Append(value.Text + (count < 3 ? "," : ""));
                    count++;
                }
                sb.AppendLine();
            }

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString().ToCharArray());
            Stream.Write(bytes, 0, bytes.Length);
            Stream.Flush();
            Stream.Seek(0, SeekOrigin.Begin);

            return true;
        }

        public void Dispose()
        {
            Stream.Close();
            if (m_create && !Saving)
            {
                File.Delete(FileDirectory);
            }
        }
    }
}
