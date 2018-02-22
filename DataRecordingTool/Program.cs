using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MothNet
{

    /// <summary>
    /// Interface for forms which handle a folder that must be uniquely accessable (even if they have the same name)
    /// </summary>
    public interface IGuidFileList
    {

        /// <summary>
        /// The file GUID
        /// </summary>
        Guid FileGuid { get; set; }

        /// <summary>
        /// The folder directory - containing the GUID
        /// </summary>
        string FolderDirectory { get; }

        /// <summary>
        /// The name of the data file
        /// </summary>
        string DataFileName { get; }

        /// <summary>
        /// The name of the name file
        /// </summary>
        string NameFileName { get; }
    }

    /// <summary>
    /// Interface for any forms that hold on to file handles to make sure they are not deleted while the user is editing informations
    /// </summary>
    public interface IFileHandleHold
    {
        /// <summary>
        /// Closes any file handles when they can be released
        /// </summary>
        void CloseHandles();
    }

    public class DataFileItem : IDisposable
    {
        /// <summary>
        /// The file stream representing the directory specified in filedir
        /// </summary>
        public FileStream Stream { get; private set; }

        /// <summary>
        /// The path to the data file
        /// </summary>
        public string FileDir { get; private set; }

        /// <summary>
        /// If the file is available
        /// </summary>
        public bool IsAvailable { get; private set; } = false;

        /// <summary>
        /// Private ctor to ensure either the static Create() or Load() methods are used
        /// </summary>
        private DataFileItem() { }

        /// <summary>
        /// Loads a data file
        /// </summary>
        /// <param name="filename">The path to the data file e.g. in the night data directory</param>
        /// <param name="create">Whether the data file item is being created</param>
        /// <returns>A new DataFileItemInstance</returns>
        public static DataFileItem Hold(string filename, bool create)
        {
            DataFileItem item = new DataFileItem
            {
                FileDir = filename,
                Stream = new FileStream(filename, create? FileMode.Create : FileMode.Open, FileAccess.ReadWrite, FileShare.Read),
                IsAvailable = false
            };
            return item;
        }

        public void SetAvailable()
        {
            IsAvailable = true;
        }

        /// <summary>
        /// Copies the user selected data file to the data storage location (FileDir)
        /// </summary>
        /// <param name="origFile"></param>
        public void SetCopy(string origFile)
        {
            //Open the user's file
            FileStream openStream = File.Open(origFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] data = new byte[openStream.Length + 1];
            openStream.Read(data, 0, (int)openStream.Length);
            Stream.Seek(0, SeekOrigin.Begin);

            //Save the file
            Stream.Write(data, 0, (int)openStream.Length);
            Stream.Flush();
            Stream.SetLength(openStream.Length);
            //Note success
            IsAvailable = true;
            openStream.Close();
        }

        /// <summary>
        /// CLears the saved file data
        /// </summary>
        public void Clear()
        {
            Stream.Seek(0, SeekOrigin.Begin);
            Stream.SetLength(0);
        }

        /// <summary>
        /// Dispose of the file handle and set is available to false
        /// </summary>
        public void Dispose()
        {
            Stream?.Close();
            IsAvailable = false;
        }

    }

    /// <summary>
    /// A struct that represents the 
    /// </summary>
    public struct AbundanceItem
    {
        /// <summary>
        /// The species (or moth or rodent etc.)
        /// </summary>
        public string Species { get; private set; }

        /// <summary>
        /// The voucher number for the species
        /// </summary>
        public int VoucherNumber { get; private set; }

        /// <summary>
        /// The tag
        /// </summary>
        public string TagName { get; private set; }

        /// <summary>
        /// How many there were
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Constructor for an abundance item
        /// </summary>
        /// <param name="species">The species</param>
        /// <param name="voucherNumber">The voucher number</param>
        /// <param name="tagName">The tag name</param>
        /// <param name="count">The count</param>
        public AbundanceItem(string species, int voucherNumber, string tagName, int count)
        {
            Species = species;
            VoucherNumber = voucherNumber;
            TagName = tagName;
            Count = count;
        }
        
        /// <summary>
        /// Used to get all the data together e.g. for writing to a file
        /// </summary>
        /// <returns>The species, metadata and count seperated | by | pipe | characters</returns>
        public override string ToString()
        {
            return Species + " | " + VoucherNumber.ToString() + " | " + TagName + " | " + Count.ToString();
        }
    }

    /// <summary>
    /// An exception raised when the settings can't be loaded - e.g. if there is some data that is supposed to be there but isn't
    /// </summary>
    public class CannotLoadException : Exception
    {
        /// <summary>
        /// Ctor if there is a relevent inner exception such as an IoException
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception - i.e. what caused this to be raised</param>
        public CannotLoadException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Ctor if there is no inner exception - i.e. there is a problem with the data itself and not e.g. an IoException
        /// </summary>
        /// <param name="message">The error message</param>
        public CannotLoadException(string message) : base(message) { }
    }

    /// <summary>
    /// A struct that represents an item in a combobox. Used when the human readable version and computer readable version are different, e.g. NZDN vs Dunedin Airport
    /// </summary>
    public struct ComboBoxItem
    {
        /// <summary>
        /// The human readable value
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// The value to go into the file. This may have to deal with requirements such as no spaces which would make it not as human readable
        /// </summary>
        public string FileValue { get; private set; }

        /// <summary>
        /// Whether to show the file value as well as the human readable value
        /// </summary>
        private bool showFileValueComboBox;

        /// <summary>
        /// The constructor for an item where the value and file value are known
        /// </summary>
        /// <param name="value">The value to be displayed</param>
        /// <param name="fileValue">The value to write in the file</param>
        /// <param name="showFileValue">Whether to use value and filevalue</param>
        public ComboBoxItem(string value, string fileValue, bool showFileValue)
        {
            
            Value = value;
            FileValue = fileValue;
            showFileValueComboBox = showFileValue;
        }

        /// <summary>
        /// The constructor for an item where the file value is in brackets - such as in the resource files in the executable
        /// </summary>
        /// <param name="line">The line of text to parse</param>
        /// <param name="showFileValue">Whether to use value and filevalue</param>
        public ComboBoxItem(string line, bool showFileValue)
        {
            showFileValueComboBox = showFileValue;

            //Split the line such that str1 (str2) becomes an array str1,str2
            string[] arr = line.Split('(', ')');

            //If the array length is 1, then assume that there was nothing in brackets (we know what files we are using so it is a reasonable assumption
            if (arr.Length == 1)
            {
                //Remove any EOL characters (CR, LF)
                Value = line.TrimEnd('\r').TrimEnd('\n');
                FileValue = string.Empty;
            }
            else
            {
                //Remove any spaces (e.g. between 1 and ( in the str1 (str2) examples) and set the file value
                Value = arr[0].Trim(' ');
                FileValue = arr[1];
            }
        }

        /// <summary>
        /// Effectily undoes the line ctor operation, if showFileValueCombobox is true. Otherwhise simply returns the human readable value
        /// </summary>
        /// <returns>A string representation of the combo box item</returns>
        public override string ToString()
        {
            return Value + (showFileValueComboBox ? " (" + FileValue + ")" : "");
        }
    }

    public struct GuidItem
    {
        /// <summary>
        /// The GUID of this particular item
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        /// The human readable name of this item
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Constructor. Takes a GUID and a name
        /// </summary>
        /// <param name="guid">The GUID</param>
        /// <param name="name">The name</param>
        public GuidItem (Guid guid, string name)
        {
            Guid = guid;
            Name = name;
        }

        /// <summary>
        /// A text representation of this item
        /// </summary>
        /// <returns>Returns the name property</returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Assorted helper functions
    /// </summary>
    static class HelperFunctions
    {
        /// <summary>
        /// Not actually a function - but returns the AppData folder path
        /// </summary>
        public static string ParentDir
        {
            get
            {
                //Uncomment below if you don't trust the user
                //return Environment.CurrentDirectory;

                //Orginisation is AndersonLabsNZ, name is MothNetRecord
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AndersonLabsNZ", "MothNetRecord");
            }
        }

        /// <summary>
        /// Not actually a function - but returns the location of the site data 
        /// </summary>
        public static string SitesDir
        {
            get
            {
                return Path.Combine(ParentDir, "Sites");
            }
        }

        /// <summary>
        /// Not actually a function - but returns the current resource manager
        /// </summary>
        public static ResourceManager Resources
        {
            get
            {
                return resman;
            }
        }

        /// <summary>
        /// The resource manager returned by resources
        /// </summary>
        private static ResourceManager resman = new ResourceManager("MothNet.Properties.Resources", typeof(Program).Assembly);

        /// <summary>
        /// Retreives a localisable resource string
        /// </summary>
        /// <param name="resStrID">The resource name</param>
        /// <param name="args">Any args that need to be provided if the resource string is a format string</param>
        /// <returns>The formatted string</returns>
        public static string FormatResStr(string resStrID, params object[] args)
        {
            return string.Format(resman.GetString(resStrID), args);
        }

        /// <summary>
        /// Creates a new edit site dialog and the associated file system structrues
        /// </summary>
        public static void CreateSite()
        {
            //Create and show modally for a new message loop
            EditSite site = new EditSite(Guid.Empty);
            site.ShowDialog();
        }

        /// <summary>
        /// Gets the name of a month given the month's ID (i.e. month of year)
        /// </summary>
        /// <param name="month">The month of year</param>
        /// <returns>The name of the month</returns>
        public static string GetMonthFormat(int month)
        {
            //Subtract 1 to make Jan, month 1 give an index of 0.
            return GetResourceList("months")[month - 1];
        }

        /// <summary>
        /// Gets the month of year from the month
        /// </summary>
        /// <param name="format">The month</param>
        /// <returns>The "index" of the month, e.g. Jan is 1, Feb 2</returns>
        public static int GetFormatMonths(string format)
        {
            //Add 1 to make Jan, index 0 have a month of 1
            return Array.IndexOf(GetResourceList("months"), (string)format) + 1;
        }

        /// <summary>
        /// Get the data time in the format required for the project. ISO 8601 was not an option in this case
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns>A string representation of the date</returns>
        public static string GetDateFormatStr(DateTime date)
        {
            return String.Format("{0} {1} {2}", date.Day, GetMonthFormat(date.Month), date.Year);
        }

        /// <summary>
        /// Gets a DateTime from the project specific format. Again, ISO 8601 (the one true time format) was sadly not used
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetStrFormatDate(string date)
        {
            //Split by spaces, and parse
            string[] vals = date.Split(' ');
            DateTime time = new DateTime(Int32.Parse(vals[2]), GetFormatMonths(vals[1]), Int32.Parse(vals[0]));
            return time;
        }

        /// <summary>
        /// Used to control colour state. Ok for input ok, Invalid for an illegal / invalid input
        /// </summary>
        public enum State
        {
            OK,
            Invalid
        }

        /// <summary>
        /// Slighttly simpler that the control.BackColour = (state == State.Ok ? okColour : Colour.Tomato); control.Invalidate(); Control.Update(); for each time. 
        /// Used as a helper function for some other helper functions
        /// </summary>
        /// <param name="control">The control th set the colour state</param>
        /// <param name="okColour">The ok colour</param>
        /// <param name="state">The state, i.e. ok or not</param>
        public static void SetColourState(Control control, Color okColour, State state)
        {
            switch (state)
            {
                case State.OK:
                    {
                        control.BackColor = okColour;
                        break;
                    }
                case State.Invalid:
                    {
                        control.BackColor = Color.Tomato;
                        break;
                    }
            }

            //Invalidate and update to force a redraw
            control.Invalidate();
            control.Update();
        }

        /// <summary>
        /// Helper for text boxes - simply checks if they are empty and if they are sets the invalid state
        /// </summary>
        /// <param name="textBox">The text box control</param>
        /// <param name="okColour">The colour if it is ok</param>
        /// <returns>Whether the state is ok</returns>
        internal static bool TextBoxHelper(TextBox textBox, Color okColour)
        {
            if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                SetColourState(textBox, okColour, State.Invalid);
                return false;
            }
            else
            {
                SetColourState(textBox, okColour, State.OK);
                return true;
            }
        }

        /// <summary>
        /// Helper to use with a default ok colour
        /// </summary>
        /// <param name="textBox">The text box control</param>
        /// <returns>Whether the state is ok</returns>
        public static bool TextBoxHelper(TextBox textBox)
        {
            return TextBoxHelper(textBox, Color.White);
        }

        /// <summary>
        /// Helper for text boxes - checks if the input is a number, and optionaly, if is is an integer
        /// </summary>
        /// <param name="textBox">The text box control</param>
        /// /// <param name="allowDecimals">Whether decimals are allowed</param>
        /// <param name="okColour">The colour if it is ok</param>
        /// <returns>Whether the state is ok</returns>
        public static bool NumberTextBoxHelper(TextBox textBox, bool allowDecimals, Color okColour)
        {
            if (HelperFunctions.IsNumber(textBox.Text, allowDecimals))
            {
                SetColourState(textBox, okColour, State.OK);
                return true;
            }
            else
            {
                SetColourState(textBox, okColour, State.Invalid);
                return false;
            }
        }

        /// <summary>
        /// Helper to use with numeric text boxes and a default ok colour
        /// </summary>
        /// <param name="textBox">The text box control</param>
        /// <param name="allowDecimals">Whether decimals are allowed</param>
        /// <returns>Whether the state is ok</returns>
        public static bool NumberTextBoxHelper(TextBox textBox, bool allowDecimals)
        {
            return NumberTextBoxHelper(textBox, allowDecimals, Color.White);
        }

        /// <summary>
        /// Checks if a text input is an integer number
        /// </summary>
        /// <param name="text">The text to check</param>
        /// <returns>If the text is a number</returns>
        public static bool IsNumber(string text)
        {
            return IsNumber(text, false);
        }

        /// <summary>
        /// Checks if a text input is an number (integer or decimal)
        /// </summary>
        /// <param name="text">The text to check</param>
        /// <param name="allowDecimals">Whether decimal numbers are acceptable or not</param>
        /// <returns>If the text is a number</returns>
        public static bool IsNumber(string text, bool allowDecimals)
        {
            return IsNumber(text, allowDecimals, out object value);
        }

        /// <summary>
        /// Checks if a text input is an number (integer or decimal)
        /// </summary>
        /// <param name="text">The text to check</param>
        /// <param name="allowDecimals">Whether decimal numbers are acceptable or not</param>
        /// <param name="value">The converted value, if it is a number</param>
        /// <returns>If the text is a number</returns>
        public static bool IsNumber(string text, bool allowDecimals, out object value)
        {
            //If the string is empty it is not a number
            if (String.IsNullOrWhiteSpace(text))
            {
                value = (object)0; //TryParse will also yield 0 on failure
                return false;
            }

            //Treat it as a double if decimals are allowed
            if (allowDecimals)
            {
                bool success = Double.TryParse(text, out double result);
                value = (object)result;
                return success;
            }

            //Treat it as a integer if decimals are not allowed
            else
            {
                bool success = Int32.TryParse(text, out int result);
                value = (object)result;
                return success;
            }
        }
        
        /// <summary>
        /// Gets the region name from the specified ID
        /// </summary>
        /// <param name="id">The region ID</param>
        /// <returns>The full region name</returns>
        public static string GetRegionFromID(char id)
        {
            //Split by region
            string[] list = GetResourceList("regions");
            
            //Iterate through each item
            foreach (string line in list)
            {
                //Ignore if empty or commented
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                {
                    continue;
                }

                //Substring the bit between the brackets
                int start = line.IndexOf('(') + 1;
                string match = line.Substring(start, line.IndexOf(')') - start);

                //Check the match
                if (match == id.ToString())
                {
                    //If so, return the region name only
                    return line.Substring(0, start - 3);
                }
            }
            throw new ArgumentOutOfRangeException(nameof(id), "No region matches the specified ID");
        }

        /// <summary>
        /// Geths the index of where the value in data is in the combo box box.
        /// </summary>
        /// <param name="box">The combo box to check</param>
        /// <param name="data">The value to find the index of</param>
        /// <returns>The index of data in box, or if data is not in box, -1</returns>
        public static int GetComboBoxIndexFromFile(ComboBox box, string data)
        {
            int count = box.Items.Count;

            //Iterate through each item and check if the value or file value matches
            for (int i = 0; i < count; i++)
            {
                ComboBoxItem item = (ComboBoxItem)box.Items[i];
                if (item.FileValue == data)
                {
                    return i;
                }
                else if (item.Value == data)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Checks that the item in value is present in box, and selects it if it is.
        /// </summary>
        /// <param name="box">The check box</param>
        /// <param name="value">The item that should be in box</param>
        /// <returns>Whether value is found in box</returns>
        public static bool SetComboBoxFileValue(ComboBox box, string value)
        {
            int index = HelperFunctions.GetComboBoxIndexFromFile(box, value);
            box.SelectedIndex = index;
            if (index == -1)
            {
                return false;
            }
            box.Text = value;
            return true;
        }

        /// <summary>
        /// Load the name of an item (e.g. site or night) from it's name file
        /// </summary>
        /// <param name="itemFolder">The folder containing the item data</param>
        /// <param name="fileName">The filename of the name file</param>
        /// <returns></returns>
        public static string LoadName(string itemFolder, string fileName)
        {
            string name = null;
            try
            {
                using (FileStream stream = File.OpenRead(Path.Combine(itemFolder, fileName)))
                {
                    name = GetFileTextLines(stream)[0];
                }
            }
            catch (Exception) { } //Swallow exception - doesn't matter (to the user) why it can't be read, just that it can't

            if (string.IsNullOrWhiteSpace(name))
            {
                return FormatResStr("STR_NAME_UNKNOWN");
            }
            else
            {
                return name;
            }
        }

        /// <summary>
        /// Gets a message from an exception that is somewhat user friendly.
        /// </summary>
        /// <param name="e">The exception</param>
        /// <returns>A user message</returns>
        public static string GetExceptionUserMessage(CannotLoadException e)
        {
            if (e.InnerException == null)
            {
                return e.Message;
            }
            else
            {
                return FormatResStr("STR_EXCEPT_INNER_EXCEPT", e.Message, e.InnerException.Message);
            }
        }

        /// <summary>
        /// Function for deleting a directory that won't raise exceptions if something normal happens
        /// </summary>
        /// <param name="dir">The directory to delete</param>
        public static void SafeDeleteDirectory(string dir)
        {   
            try
            {
                Directory.Delete(dir, true);
            }
            catch (DirectoryNotFoundException)
            {
                //OK if doesn't exist
            }
        }

        /// <summary>
        /// Function for deleting a directory that won't raise exceptions if something normal happens
        /// </summary>
        /// <param name="file">The file to delete</param>
        public static void SafeDeleteFile(string file)
        {
            try
            {
                File.Delete(file);
            }
            catch (IOException ioex) when (ioex is DirectoryNotFoundException || ioex is FileNotFoundException)
            {
                //OK if doesn't exist
            }
        }

        /// <summary>
        /// Gets the text of the currently selected item in the combo box box
        /// </summary>
        /// <param name="box">The combobox to get the value from</param>
        /// <returns>The currently selected item's text - either the value of file value depending on the item</returns>
        public static string GetOutputFileValue(ComboBox box)
        {
            ComboBoxItem item = ((ComboBoxItem)box.Items[box.SelectedIndex]);
            if (String.IsNullOrWhiteSpace(item.FileValue))
            {
                return item.Value;
            }
            else
            {
                return item.FileValue;
            }
        }

        /// <summary>
        /// Gets a resource and splits it by line
        /// </summary>
        /// <param name="resource">The resource</param>
        /// <returns>The resource as a list as opposed to a whole string</returns>
        public static string[] GetResourceList(string resource)
        {
            //Gets the resource and splits it by line
            string[] items = ((string)Resources.GetObject(resource, Thread.CurrentThread.CurrentCulture)).Split('\n');
            int length = items.Length;
            List<String> list = new List<string>();

            //Iterate through the lines and add them to the list if they are not empty, and if they are not commented out with //
            for (int i = 0; i < length; i++)
            {
                if (!String.IsNullOrWhiteSpace(items[i]) && !items[i].StartsWith("//"))
                {
                    //Trim the line end
                    list.Add(items[i].Trim('\r'));
                }
            }
            //Convert to an array
            return list.ToArray();
        }

        /// <summary>
        /// The callback definition for parsing the file being loaded
        /// </summary>
        /// <param name="index">The current line in the file (e.g. line 1, 2, 3)</param>
        /// <param name="value">The value of the current line in the file (i.e. if the index was 10 and the 11th line was the only one with text and it contaiined the work BOB then this would contain the string BOB)</param>
        public delegate void LoadFileCallBack(int index, string value);

        /// <summary>
        /// Reads text from the stream while doing some other auxuilary work, e.g. reset file pointer
        /// </summary>
        /// <param name="stream">The stream to read from</param>
        /// <returns>The text from the file</returns>
        private static string GetFileText(FileStream stream)
        {
            //Reset the file pointer to the start
            stream.Seek(0, SeekOrigin.Begin);

            //Load file as a byte array
            byte[] arr = new byte[stream.Length];
            stream.Read(arr, 0, (int)stream.Length);

            //Convert to a string (UTF8) and split by line
            return Encoding.UTF8.GetString(arr);
        }

        /// <summary>
        /// Reads text from the stream and splits it into lines
        /// </summary>
        /// <param name="stream">The stream to read from</param>
        /// <returns>The text from the file, as a string[] array</returns>
        private static string[] GetFileTextLines(FileStream stream)
        {
            return GetFileText(stream).Split('\n');
        }

        /// <summary>
        /// File load function for where no checking is required
        /// </summary>
        /// <param name="stream">The file stream to load</param>
        /// <param name="callback">The function to handle the data</param>
        public static void DoLoadFileHelper(FileStream stream, LoadFileCallBack callback)
        {
            //Get data as array of lines
            string[] str = GetFileTextLines(stream);

            int index = 0;

            //Iterate through each line
            foreach (string line in str)
            {
                callback(index, line);
                index++;
            }
        }

        /// <summary>
        /// File load function where checking is required
        /// </summary>
        /// <param name="stream">The file stream to load</param>
        /// <param name="callback">The function to handle the data</param>
        /// <param name="resourceList">The list of settings that the loaded file should contain, in the order they appear in this paramater</param>
        public static void DoLoadFileHelper(FileStream stream, LoadFileCallBack callback, string resourceList)
        {
            //Get the data from the resource
            string[] list;
            list = HelperFunctions.GetResourceList(resourceList);

            int lineIndex = 0;

            //Get data as array of lines
            string[] str = GetFileTextLines(stream);

            if (list.Length + 1 != str.Length)
            {
                throw new CannotLoadException(FormatResStr("EXCEPT_SET_TOO_FEW", str.Length, list.Length));
            }

            foreach (string line in str)
            {
                //Trim any trailing newline characters
                string value = line.Trim('\r').Trim('\n');

                //Skip if the line is empty
                if (String.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                //Split the line in the file being loaded based on commas (i.e. it's a csv file)
                string[] commaDelineated = value.Split(',');

                //Check that the setting name matches. If not, note the failure
                if (list[lineIndex] == commaDelineated[0])
                {
                    //Load and check that the data is valid
                    callback(lineIndex, commaDelineated[1]);
                }
                else
                {
                    throw new CannotLoadException(FormatResStr("EXCEPT_SET_NOT_ON_LIST", commaDelineated[0], list[lineIndex]));
                }
                //Only increment the line index if the line wasn't blank. This is to ensure that the settings file - which may contain empty lines for formatting doesn't desynchronise from the resource file
                lineIndex++;
            }

            //Reset file pointer to the start
            stream.Seek(0, SeekOrigin.Begin);
        }

        /// <summary>
        /// Reduces the code needed to write the value only if a checkbox is checked
        /// </summary>
        /// <param name="checkBox">The check box to check it's check state</param>
        /// <param name="control">The control which contains the text</param>
        /// <returns>Returns the text of control if checkBox is checked. Otherwise, return N/A</returns>
        public static string GetEnableString(CheckBox checkBox, Control control)
        {
            return GetEnableString(checkBox, control.Text);
        }

        /// <summary>
        /// Reduces the code needed to write the value only if a checkbox is checked
        /// </summary>
        /// <param name="checkBox">The check box to check it's check state</param>
        /// <param name="str">The text to write (or not)</param>
        /// <returns>Returns the text of control if checkBox is checked. Otherwise, return N/A</returns>
        public static string GetEnableString(CheckBox checkBox, string str)
        {
            if (checkBox.Checked)
            {
                return str;
            }
            else
            {
                return "N/A";
            }
        }

        /// <summary>
        /// Adds options from a resource file to the specified combo box
        /// </summary>
        /// <param name="box">The ComboBox control</param>
        /// <param name="showFileValue">Whether the file value should be shown as well</param>
        /// <param name="resource">The resource to populate the combo box with</param>
        public static void AddFileItemsToComboBox(ComboBox box, bool showFileValue, string resource)
        {
            //Clear items
            box.Items.Clear();

            //Get the resource items as a list and populate the combo box
            string[] list = GetResourceList(resource);
            foreach (string s in list)
            {
                //Add as a combobox item
                box.Items.Add(new ComboBoxItem(s, showFileValue));
            }

            //Clear any selections
            box.SelectedIndex = 0;
            box.SelectedText = "";
        }

        /// <summary>
        /// Checks if min, max, and avg are all numbers and sanity checks the results, min <= avg <= max
        /// </summary>
        /// <param name="min">The minimum value text box</param>
        /// <param name="max">The maximum value text box</param>s
        /// <param name="avg">The average value text box</param>
        /// <returns>Whether the triplet is ok</returns>
        public static bool IsMinMaxAvgTripletOK(TextBox min, TextBox max, TextBox avg)
        {
            bool ok;
            try
            {
                //Ok if sanity check succeeds
                ok = (GetTextBoxValue(min) <= GetTextBoxValue(max)) && (GetTextBoxValue(max) >= GetTextBoxValue(avg)) && (GetTextBoxValue(min) <= GetTextBoxValue(avg));
            }
            catch (Exception)
            {
                //If some are not numbers, an exception will be raised, so ok can be set to false
                ok = false;
            }
            if (ok)
            {
                //Sets the colour to ok
                min.BackColor = Color.White;
                max.BackColor = Color.White;
                avg.BackColor = Color.White;
            }
            else
            {
                //Sets the colour to red if the inputs are invalid, or gold if the sanity check is what failed
                NumberTextBoxHelper(min, true, Color.Gold);
                NumberTextBoxHelper(max, true, Color.Gold);
                NumberTextBoxHelper(avg, true, Color.Gold);
            }
            return ok;
        }

        /// <summary>
        /// Simply parses the text box text as a double, raises an exception on failure
        /// </summary>
        /// <param name="textBox">The text box to parse</param>
        /// <returns>A double representing the text of the text box</returns>
        public static double GetTextBoxValue(TextBox textBox)
        {
            return Double.Parse(textBox.Text);
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if ENGLISH
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-NZ");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-NZ");
#elif MAORI
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("mi");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("mi");
#else
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-NZ");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-NZ");
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Start the message loop
            Application.Run(new StartForm());
        }
    }
}
