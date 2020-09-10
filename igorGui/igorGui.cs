using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using igorCore;
using System.Windows.Forms;


namespace igorGui
{
    public partial class igorForm : Form
    {

        BackgroundWorker bgworker;
        private bool gotCfg { get; set; }
        private bool gotWeight { get; set; }
        private bool gotNames { get; set; }
        private bool gotFileIn { get; set; }
        private bool gotFileOut { get; set; }

        public igorForm()
        {
            InitializeComponent();

            igorCore.igorCore igor = new igorCore.igorCore();
            gotFileIn = false;
            gotFileOut = false;
            RecursiveSearchCheckbox.Checked = true;

            try
            {
                string currentDir = Directory.GetCurrentDirectory();
                igor.Werewolf(Path.Combine(currentDir, "modelDat", "modelSelect.txt"));
                cfgTextBox.Text = Path.GetFullPath(igor.modelDetailsCfg);
                weightsTextBox.Text = Path.GetFullPath(igor.modelDetailsWeights);
                namesTextBox.Text = Path.GetFullPath(igor.modelDetailsNames);

                cfgTextBox.SelectionStart = cfgTextBox.Text.Length;
                cfgTextBox.SelectionLength = 0;
                weightsTextBox.SelectionStart = weightsTextBox.Text.Length;
                weightsTextBox.SelectionLength = 0;
                namesTextBox.SelectionStart = namesTextBox.Text.Length;
                namesTextBox.SelectionLength = 0;

                numberOfObjectsLabel.Text = "The selected model will code images for " + igor.theBags.Count() + " different object classes.";

                igor.WalkThisWay();

                if (igor.gpuAvailable)
                {
                    gpuLabel.BackColor = Color.LightGreen;
                    cpuLabel.BackColor = Color.DarkGray;
                }
                else
                {
                    cpuLabel.BackColor = Color.LightGreen;
                    gpuLabel.BackColor = Color.Red;
                }

                gotCfg = true;
                gotWeight = true;
                gotNames = true;

            }
            catch (Exception ex)
            {
                gotCfg = false;
                gotWeight = false;
                gotNames = false;
            }
            finally
            {
                igor.WhatHump();
            }


        }

        private void processButton_Click(object sender, EventArgs e)
        {

            #region Make sure that the user has the settings all lined up and ready to go
            if (!gotCfg)
            {
                MessageBox.Show("Please select a \"Cfg\" file before proceeding to process your images.", "I Ain't Got No Body", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!gotWeight)
            {
                MessageBox.Show("Please select a \"Weights\" file before proceeding to process your images.", "I Ain't Got No Body", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!gotNames)
            {
                MessageBox.Show("Please select a \"Names\" file before proceeding to process your images.", "I Ain't Got No Body", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!gotNames)
            {
                MessageBox.Show("Please select a \"Names\" file before proceeding to process your images.", "I Ain't Got No Body", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            #endregion


            Bags bags = new Bags(cfgTextBox.Text, weightsTextBox.Text, namesTextBox.Text,
                                FolderInputTextbox.Text, FileOutTextBox.Text);
            bags.setRecursive(RecursiveSearchCheckbox.Checked);


            //keep the user from fiddling with the interface
            ControlsEnabled(false);


            processingLabel.Text = "Initializing...";
            bgworker = new BackgroundWorker();
            bgworker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            bgworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgworkerReportRunWorkerCompleted);
            bgworker.WorkerReportsProgress = true;
            bgworker.WorkerSupportsCancellation = true;
            bgworker.ProgressChanged += new ProgressChangedEventHandler(bgworkerProgressChanged);


            bgworker.RunWorkerAsync(bags);

        }

        private void cfgFileButton_Click(object sender, EventArgs e)
        {



            string filePath = GetFilePath(filterString: "YOLO Config File (*.cfg)|*cfg", defaultFile: "yolov3.cfg", startPath: GetDir(cfgTextBox.Text));
            if (filePath != null)
            {
                cfgTextBox.Text = filePath;
                cfgTextBox.SelectionStart = cfgTextBox.Text.Length;
                cfgTextBox.SelectionLength = 0;
                gotCfg = true;
            }
            else
            {
                cfgTextBox.Text = "";
                gotCfg = false;
            }


        }
        private string GetDir(string filepath)
        {
            string filePath = "";

            try
            {
                filePath = Path.GetDirectoryName(cfgTextBox.Text);
            }
            catch
            {
                filePath = null;
            }


            return filepath;

        }
        private void weightsFileButton_Click(object sender, EventArgs e)
        {
            string filePath = GetFilePath(filterString: "YOLO Weights File (*.weights)|*weights", defaultFile: "yolov3.weights", startPath: GetDir(weightsTextBox.Text));
            if (filePath != null)
            {
                weightsTextBox.Text = filePath;
                weightsTextBox.SelectionStart = weightsTextBox.Text.Length;
                weightsTextBox.SelectionLength = 0;
                gotWeight = true;
            }
            else
            {
                weightsTextBox.Text = "";
                gotWeight = false;
            }
        }
        private void namesFileButton_Click(object sender, EventArgs e)
        {
            string filePath = GetFilePath(filterString: "YOLO Names File (*.names)|*names", defaultFile: "coco.names", startPath: GetDir(namesTextBox.Text));
            if (filePath != null)
            {

                namesTextBox.Text = filePath;
                namesTextBox.SelectionStart = namesTextBox.Text.Length;
                namesTextBox.SelectionLength = 0;

                try
                {

                    igorCore.igorCore igor = new igorCore.igorCore();
                    igor.ThereWolfThereCastle(cfg: "", weights: "", namesTextBox.Text);
                    numberOfObjectsLabel.Text = "The selected model will code images for " + igor.theBags.Count() + " different object classes.";
                    igor.WhatHump();
                    gotNames = true;

                }
                catch
                {
                    numberOfObjectsLabel.Text = "There was a problem with your \"names\" file.";
                    namesTextBox.Text = "";
                    MessageBox.Show("There was a problem with your \"names\" file. Igor was unable to read the list of objects contained within. " +
                                    "Please check that your file is correctly formatted and is accessible for Igor to read.", "I Ain't Got No Body", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gotNames = false;
                }

            }
            else
            {
                namesTextBox.Text = "";
                numberOfObjectsLabel.Text = "There is no \"names\" file selected.";
                gotNames = false;
            }
        }

        internal string GetFilePath(string filterString, string defaultFile, string startPath)
        {
            string filePath = null;

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = filterString;
            openFile.CheckFileExists = true;
            openFile.Title = "Choose the appropriate model file";
            openFile.FileName = defaultFile;
            openFile.Multiselect = false;
            openFile.ValidateNames = true;
            openFile.AddExtension = true;

            if (startPath != null)
            {
                openFile.InitialDirectory = Path.GetDirectoryName(startPath);
                openFile.FileName = Path.GetFileName(startPath);
            }



            DialogResult result = openFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                filePath = openFile.FileName;
            }



            return filePath;
        }



        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
        
            
            
            Bags bags = (Bags)e.Argument;
            igorCore.igorCore igor = new igorCore.igorCore();

            try 
            { 
                igor.ThereWolfThereCastle(bags.GetCfg(), bags.GetWeight(), bags.GetNames());
                igor.WalkThisWay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem initializing your YOLO model. Please share the following information with the software developer:" + Environment.NewLine + ex.ToString(),
                    "Damn Your Eyes!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            var inputFiles = Directory.EnumerateFiles(bags.GetFolderIn(), searchPattern: "*.*", searchOption: bags.getDirDepth())
                .Where(s => s.EndsWith(".jpg") || s.EndsWith(".png"));



            using (FileStream fileStream = new FileStream(bags.GetFileOut(), FileMode.Append, FileAccess.Write, FileShare.Read))
            using (StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
            {

                string headerRow = "\"Filename\"";
                for (int i = 0; i < igor.bagCount; i++) headerRow += ",\"" + igor.theBags[i] + "\"";

                streamWriter.WriteLine(headerRow);


                foreach (string file in inputFiles)
                {

                    if (bgworker.CancellationPending) break;
                    bgworker.ReportProgress(0, file.Replace(bags.GetFolderIn(), ""));
                    
                    Dictionary<string, int> imageObjects = new Dictionary<string, int>();
                    for (int i = 0; i < igor.bagCount; i++) imageObjects.Add(igor.theBags[i], 0);
                    
                    try
                    {
                        List<string> items = igor.Blücher(file);

                        foreach (string item in items) imageObjects[item]++;

                        #region build/write output
                        
                        StringBuilder outputRow = new StringBuilder();

                        //add the filename to the output
                        outputRow.Append("\"" + file.Replace(bags.GetFolderIn(), "").Replace("\"", "\"\"") + "\"");
                        for (int i = 0; i < igor.bagCount; i++)
                        {
                            outputRow.Append(',');
                            outputRow.Append(imageObjects[igor.theBags[i]]);
                        }

                        streamWriter.WriteLine(outputRow.ToString());
                        #endregion

                    }
                    catch (Exception ex)
                    {
                            using (FileStream fs = new FileStream(bags.GetFileOut() + ".log", FileMode.Create, FileAccess.Write, FileShare.Read))
                            using (StreamWriter logWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
                            {
                                logWriter.WriteLine("================================================");
                                logWriter.WriteLine("Error processing " + file);
                                logWriter.WriteLine(Environment.NewLine);
                                logWriter.WriteLine(ex.ToString());
                            }
                        

                    }



                }



            }

            igor.WhatHump();








        }






        private void bgworkerReportRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            processingLabel.Text = "Finished!";
            MessageBox.Show("Igor is finished processing your images.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ControlsEnabled(true);
        }

        private void bgworkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            processingLabel.Text = "Processing: " + e.UserState as String;
        }


        private class Bags
        {

            string cfg;
            string weights;
            string names;
            SearchOption directoryDepth;

            string folderIn;
            string fileOut;


            public Bags(string c, string w, string n, string fi, string fo)
            {
                cfg = c;
                weights = w;
                names = n;
                folderIn = fi;
                fileOut = fo;
            }

            public string GetCfg() { return cfg; }
            public string GetWeight() { return weights; }
            public string GetNames() { return names; }
            public string GetFolderIn() { return folderIn; }
            public string GetFileOut() { return fileOut; }

            public void setRecursive(bool recursiveSearch)
            {
                if (recursiveSearch)
                {
                    directoryDepth = SearchOption.AllDirectories;
                }
                else
                {
                    directoryDepth = SearchOption.TopDirectoryOnly;
                }

            }
            public SearchOption getDirDepth() { return directoryDepth; }


        }

        private void ControlsEnabled(bool enabled)
        {

            cfgFileButton.Enabled = enabled;
            weightsFileButton.Enabled = enabled;
            namesFileButton.Enabled = enabled;
            InputFolderButton.Enabled = enabled;
            OutputFileButton.Enabled = enabled;
            RecursiveSearchCheckbox.Enabled = enabled;
            processButton.Enabled = enabled;
            //the cancellation button should always be the opposite of the other buttons
            CancelButton.Enabled = !enabled;
            
        }

        private void InputFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderIn = new FolderBrowserDialog();
            folderIn.Description = "Choose the folder with all of your images.";
            folderIn.ShowNewFolderButton = false;
            if (!string.IsNullOrEmpty(FileOutTextBox.Text)) folderIn.SelectedPath = Path.GetDirectoryName(FileOutTextBox.Text);

            DialogResult = folderIn.ShowDialog();

            if (DialogResult == DialogResult.OK)
            {
                FolderInputTextbox.Text = folderIn.SelectedPath;
                FolderInputTextbox.SelectionStart = FolderInputTextbox.Text.Length;
                FolderInputTextbox.SelectionLength = 0;
                gotFileIn = true;
            }
            else
            {
                FolderInputTextbox.Text = "";
                gotFileIn = false;
            }

        }

        private void OutputFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileOut = new SaveFileDialog();
            fileOut.Filter = "CSV File (*.csv)|*.csv";
            fileOut.DefaultExt = ".csv";
            fileOut.FileName = "IgorOutput.csv";
            fileOut.Title = "Choose your output file to save.";
            fileOut.ValidateNames = true;
            fileOut.OverwritePrompt = true;
            fileOut.CheckFileExists = false;
            fileOut.CheckPathExists = true;

            if (!string.IsNullOrEmpty(FolderInputTextbox.Text)) fileOut.InitialDirectory = FolderInputTextbox.Text;

            DialogResult result = fileOut.ShowDialog();

            if (result == DialogResult.OK)
            {

                try
                {

                    // Create the file, or overwrite if the file exists.
                    using (FileStream fileStream = new FileStream(fileOut.FileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                    using (StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
                    {
                    }

                    // Create the file, or overwrite if the file exists.
                    using (FileStream fileStream = new FileStream(fileOut.FileName + ".log", FileMode.Create, FileAccess.Write, FileShare.Read))
                    using (StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
                    {
                    }

                    FileOutTextBox.Text = fileOut.FileName;
                    FileOutTextBox.SelectionStart = FileOutTextBox.Text.Length;
                    FileOutTextBox.SelectionLength = 0;
                    gotFileOut = true;

                }
                catch
                {
                    MessageBox.Show("There was a problem initializing your output file and/or associated log file. Please ensure that you have appropriate access to the " +
                                    "file that you are trying to create, and that the file is not currently open in another application.", "Call it... \"a hunch\"", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FileOutTextBox.Text = "";
                    gotFileOut = false;
                }

            }
            else
            {
                FileOutTextBox.Text = "";
                gotFileOut = false;
            }
                    
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (bgworker.IsBusy) bgworker.CancelAsync();
        }
    }
}
