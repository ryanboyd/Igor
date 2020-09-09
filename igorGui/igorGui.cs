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

namespace igorGui
{
    public partial class igorForm : Form
    {

        BackgroundWorker bgworker;
        igorCore.igorCore igor;

        public igorForm()
        {
            InitializeComponent();
            bgworker = new BackgroundWorker();
            igor = new igorCore.igorCore();

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

                igor.WhatHump();
                igor = new igorCore.igorCore();

            }
            catch
            {

            }


        }

        private void processButton_Click(object sender, EventArgs e)
        {

        }

        private void cfgFileButton_Click(object sender, EventArgs e)
        {



            string filePath = GetFilePath(filterString: "YOLO Config File (*.cfg)|*cfg", defaultFile: "yolov3.cfg", startPath: GetDir(cfgTextBox.Text));
            if (filePath != null)
            {
                cfgTextBox.Text = filePath;
                cfgTextBox.SelectionStart = cfgTextBox.Text.Length;
                cfgTextBox.SelectionLength = 0;
            }
            else
            {
                cfgTextBox.Text = "";
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
            }
            else
            {
                weightsTextBox.Text = "";
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
            }
            else
            {
                namesTextBox.Text = "";
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




    }
}
