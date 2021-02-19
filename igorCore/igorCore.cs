﻿using System;
using System.Collections.Generic;
using Alturos.Yolo;
using System.IO;
using System.Drawing;

namespace igorCore
{
    public class igorCore
    {
        public string modelDetailsCfg { get; set; }
        public string modelDetailsWeights { get; set; }
        public string modelDetailsNames { get; set; }
        public Dictionary<int, string> theBags { get; set;  }
        public bool gpuAvailable { get; set; } = false;
        private YoloWrapper yoloWrapper { get; set; } = null;
        public int bagCount { get; set; }



        /// <summary>
        /// Instantiates the model / gets the wrapper up and running
        /// </summary>
        public void WalkThisWay()
        {
            var gpuConfig = new GpuConfig();

            try
            {

                this.yoloWrapper = new YoloWrapper(configurationFilename: this.modelDetailsCfg,
                                                weightsFilename: this.modelDetailsWeights,
                                                namesFilename: this.modelDetailsNames,
                                                gpuConfig: gpuConfig);

                Console.WriteLine(" GPU is ready to be used. Welcome to the fast lane!");
                gpuAvailable = true;

            }
            catch
            {


                    Console.WriteLine(" GPU is not able to be used. See documentation for details.");
                    Console.WriteLine(" Running with CPU config instead...");
                    
                    this.yoloWrapper = new YoloWrapper(configurationFilename: this.modelDetailsCfg,
                                                    weightsFilename: this.modelDetailsWeights,
                                                    namesFilename: this.modelDetailsNames,
                                                    gpuConfig: null);

                gpuAvailable = false;

            }
        }



        /// <summary>
        /// This method tells Igor where to look for the model details, which are contained in a single file.
        /// </summary>
        public void Werewolf(string detailsPath)
        {

            try
            {

                string lines;

                // read the file with the model details.
                using (FileStream fileStream = new FileStream(detailsPath, FileMode.Open, FileAccess.Read))
                using (StreamReader streamRead = new StreamReader(fileStream, System.Text.Encoding.UTF8))
                {

                    lines = streamRead.ReadToEnd().Trim();
                }

                string[] modelDetails = lines.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (modelDetails.Length != 3)
                {
                    throw new Exception("There is an issue with your modelSelect.txt file. It does not have the correct number of details (3).");
                }

                for (int i = 0; i < modelDetails.Length; i++) modelDetails[i] = modelDetails[i].Trim();

                this.modelDetailsCfg = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "modelDat", modelDetails[0]));
                this.modelDetailsWeights = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "modelDat", modelDetails[1]));
                this.modelDetailsNames = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "modelDat", modelDetails[2]));

                Console.WriteLine(" Intended cfg file: " + this.modelDetailsCfg.Replace(Directory.GetCurrentDirectory(), ""));
                Console.WriteLine(" Intended weights file: " + this.modelDetailsWeights.Replace(Directory.GetCurrentDirectory(), ""));
                Console.WriteLine(" Intended names file: " + this.modelDetailsNames.Replace(Directory.GetCurrentDirectory(), ""));

            }
            catch
            {
                throw new Exception("There is an issue with your modelSelect.txt file.");
            }

            HelpMeWithTheBags(modelDetailsNames);

        }


        /// <summary>
        /// This method tells Igor what the model details are.
        /// </summary>
        public void ThereWolfThereCastle(string cfg, string weights, string names)
        {
            modelDetailsCfg = cfg;
            modelDetailsWeights = weights;
            modelDetailsNames = names;

            HelpMeWithTheBags(modelDetailsNames);

        }


        /// <summary>
        /// Has Igor read in the names for all possible objects in the files. Just so it knows.
        /// </summary>
        private void HelpMeWithTheBags(string namePath)
        {


           this.theBags = new Dictionary<int, string>(); ;

            using (StreamReader objectNamesFile = new StreamReader(namePath))
            {
                int counter = 0;
                string ln;

                while ((ln = objectNamesFile.ReadLine()) != null)
                {
                    theBags.Add(counter, ln.Trim());
                    counter++;
                }
                objectNamesFile.Close();

                this.bagCount = theBags.Count;

                Console.WriteLine(" Your model codes for " + counter.ToString() + " distinct objects.");
            }

        }


        /// <summary>
        /// Analyze an image, return list of all objects found
        /// </summary>
        public List<string> Blücher(string file)
        {


            //this tries to make sure that the image is valid before we pass it off to yolo, cuda, etc.
            //if we don't do this, a malformed image can crash cuda.
            try
            {
                Image newImage = Image.FromFile(file);
            }
            catch (Exception ex)
            {
                throw new Exception("This does not appear to be an image file, or it is broken/malformed in some way.");
            }


            List<string> AbbyNormal = new List<string>();

            //actually process the image, extract the objects

            var items = this.yoloWrapper.Detect(file);
            foreach (var item in items) AbbyNormal.Add(item.Type);
                


            //    //items[0].Type -> "Person, Car, ..."
            //    //items[0].Confidence -> 0.0 (low) -> 1.0 (high)
            //    //items[0].X -> bounding box
            //    //items[0].Y -> bounding box
            //    //items[0].Width -> bounding box
            //    //items[0].Height -> bounding box

            return AbbyNormal;

        }

        public void WhatHump()
        {
            if (yoloWrapper != null) yoloWrapper.Dispose();
        }


    }



    


}
