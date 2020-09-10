using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using igorCore;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlTypes;
using igorCore;

namespace igorGui
{
    public partial class igorForm : Form
    {

        BackgroundWorker bgworker;
        private bool gotCfg { get; set; }
        private bool gotWeight { get; set; }
        private bool gotNames { get; set; }

        public igorForm()
        {
            InitializeComponent();
            
            Igor igor = new Igor();

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
            catch
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

            Bags bags = new Bags(cfgTextBox.Text, weightsTextBox.Text, namesTextBox.Text);


            bgworker = new BackgroundWorker();
            bgworker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            bgworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            bgworker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);

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

                    Igor igor = new Igor();
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

        }


        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.progressBar1.Value = e.ProgressPercentage;
        }


        private class Bags
        {

            string cfg;
            string weights;
            string names;
            public string folderIn { get; set; }
            public string fileOut { get; set; }

            public Bags(string c, string w, string n)
            {
                cfg = c;
                weights = w;
                names = n;
            }

            public string GetCfg() { return cfg; }
            public string GetWeight() { return weights; }
            public string GetNames() { return names; }


        }



    }
}
