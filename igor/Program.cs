using Alturos.Yolo;
using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;


namespace igor
{
    class Program
    {


        const ConsoleColor workingColor = ConsoleColor.Green;
        const ConsoleColor neutralColor = ConsoleColor.White;
        const ConsoleColor errColor = ConsoleColor.Red;
        const ConsoleColor exampleColor = ConsoleColor.Yellow;
        const ConsoleColor bannerColor = ConsoleColor.Cyan;
        const ConsoleColor bannerBGColor = ConsoleColor.DarkMagenta;
        const ConsoleColor strongLineColor = ConsoleColor.DarkGray;
        const string strongLine = "===================================================";

        
        

        static void Main(string[] args)
        {


            

            string execpath = Directory.GetCurrentDirectory();



            //Console.WriteLine(args.Length.ToString());
            //foreach (string arg in args) Console.WriteLine(arg);


            //Console.WriteLine(args.Length.ToString());
            //Thread.Sleep(3000);

            //print the header
            PrintHeader();


            Dictionary<string, string> modelDetails = GetModelSpecs(Path.Combine(execpath, "modelDat", "modelSelect.txt"));


            string cfgPath = Path.Combine(execpath, "modelDat", modelDetails["cfg"]);
            string weightPath = Path.Combine(execpath, "modelDat", modelDetails["weights"]);
            string namePath = Path.Combine(execpath, "modelDat", modelDetails["names"]);



            Dictionary<int, string> objectDict = GetModelNames(namePath);
            int numObjectCategories = objectDict.Count();

            bool ableToLog = false;




            //double check that we have the right number of arguments
            #region check arguments
            if (args.Length != 2)
            {
                WriteText("Igor requires 2 arguments: the input folder and", errColor);
                WriteText("output file location. For example:", errColor);
                WriteText("  igor.exe \"C:\\imagefiles\" \"C:\\OutputData.csv\" ", exampleColor);
                Console.WriteLine();
                WriteText("In some cases, the error is caused by unintentionally ending your", errColor);
                WriteText("input folder parameter with an escape. For example:", errColor);
                WriteText("  igor.exe \"C:\\imagefiles\\\" \"C:\\OutputData.csv\" ", exampleColor);
                Console.WriteLine();
                WriteText("Note the extra \"\\\" that is is not in the original example.", errColor);
                EndProg();
            }

            try
            {
                if (!Directory.Exists(args[0]))
                {
                    WriteText("Your input folder does not appear to be valid.", errColor);
                    EndProg();
                }
            }
            catch (Exception ex)
            {
                PrintStrongLine();
                Console.WriteLine(ex.ToString(), errColor);
                PrintStrongLine();

                WriteText("Your input parameters appear to be incorrect. For example:", errColor);
                WriteText("  igor.exe \"C:\\imagefiles\" \"C:\\OutputData.csv\" ", exampleColor);
                EndProg();
            }

            #region try to initialize file
            try
            {

                // Create the file, or overwrite if the file exists.
                using (FileStream fileStream = new FileStream(args[1], FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
                {

                    string headerRow = "\"Filename\"";
                    for (int i = 0; i < numObjectCategories; i++) headerRow += ",\"" + objectDict[i] + "\"";

                    streamWriter.WriteLine(headerRow);

                }

            }
            catch (Exception ex)
            {
                PrintStrongLine();
                Console.WriteLine(ex.ToString(), errColor);
                PrintStrongLine();
                WriteText("Your output file does not appear to be valid. Please check to ensure that", errColor);
                WriteText("the file location correct and that the file is not open in another program.", errColor);
                EndProg();
            }
            #endregion

            // now that we've vetted the arguments, we can rename them to actual
            //variables for better readability
            string inputDir = args[0];
            string outputFile = args[1];
            string logFile = outputFile + ".log";

            #region check to see if we can create a log
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fileStream = new FileStream(logFile, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
                {
                }

                ableToLog = true;

            }
            catch (Exception ex)
            {
                //do nothing since ableToLog is false by default
            }
            #endregion



            #endregion





            //var yoloConfig = new YoloConfiguration();
            var gpuConfig = new GpuConfig();
            //gpuConfig.GpuIndex = 0;
            

            YoloWrapper yoloWrapper = null;

            try
            {
                
                yoloWrapper = new YoloWrapper(configurationFilename: cfgPath,
                                                weightsFilename: weightPath,
                                                namesFilename: namePath,
                                                gpuConfig: gpuConfig);

                WriteText(" GPU is ready to be used. Welcome to the fast lane!", ConsoleColor.Yellow);
                Thread.Sleep(1000);

            }
            catch
            {
                try
                {
                    WriteText(" GPU is not able to be used. See documentation for details.", ConsoleColor.Yellow);
                    WriteText(" Running with CPU config instead...", ConsoleColor.Yellow);
                    Thread.Sleep(5000);
                                yoloWrapper = new YoloWrapper(configurationFilename: cfgPath,
                                                                weightsFilename: weightPath,
                                                                namesFilename: namePath,
                                                                gpuConfig: null);
                }
                catch (Exception ex)
                {
                    PrintStrongLine();
                    WriteText(ex.ToString(), errColor);
                    PrintStrongLine();
                    WriteText("There was an error loading the model files. Please see the error text", errColor);
                    WriteText("above for additional details and debugging.", errColor);
                    EndProg();
                }
            }
            



            var inputFiles = Directory.EnumerateFiles(inputDir, searchPattern: "*.*", searchOption: SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".jpg") || s.EndsWith(".png")); ;



            using (FileStream fileStream = new FileStream(outputFile, FileMode.Append, FileAccess.Write, FileShare.Read))
            using (StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
            {
                

                    WriteText(" -> ...model has been loaded.");

                    foreach (string file in inputFiles)
                    {

                        Dictionary<string, int> imageObjects = new Dictionary<string, int>();
                        for (int i = 0; i < numObjectCategories; i++) imageObjects.Add(objectDict[i], 0);

                        PrintFile(file, inputDir);

                        try 
                        {
                            var items = yoloWrapper.Detect(file);

                            foreach (var item in items) imageObjects[item.Type]++;

                            #region build/write output
                            //write our output
                            string headerRow = "\"Filename\"";
                            for (int i = 0; i < numObjectCategories; i++) headerRow += ",\"" + objectDict[i] + "\"";

                            StringBuilder outputRow = new StringBuilder();

                            //add the filename to the output
                            outputRow.Append("\"" + file.Replace(inputDir, "").Replace("\"", "\"\"") + "\"");
                            for (int i = 0; i < numObjectCategories; i++)
                            {
                                outputRow.Append(',');
                                outputRow.Append(imageObjects[objectDict[i]]);
                            }

                            streamWriter.WriteLine(outputRow.ToString());
                            #endregion

                        }
                        catch (Exception ex)
                        {
                            WriteText("     -> There was an error processing this file.", errColor);
                            WriteText("     -> Please see .log file for details.", errColor);

                        if (ableToLog)
                            {
                                using (FileStream fs = new FileStream(logFile, FileMode.Create, FileAccess.Write, FileShare.Read))
                                using (StreamWriter logWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
                                {
                                    logWriter.WriteLine(strongLine);
                                    logWriter.WriteLine("Error processing " + file);
                                    logWriter.WriteLine(Environment.NewLine);
                                    logWriter.WriteLine(ex.ToString());
                                }
                            }

                        }
                        

                        //    //items[0].Type -> "Person, Car, ..."
                        //    //items[0].Confidence -> 0.0 (low) -> 1.0 (high)
                        //    //items[0].X -> bounding box
                        //    //items[0].Y -> bounding box
                        //    //items[0].Width -> bounding box
                        //    //items[0].Height -> bounding box
                    }

                

            }

            yoloWrapper.Dispose();

            EndProg();



        }

        static void WriteText(string error, ConsoleColor textcolor = neutralColor)
        {
            Console.ForegroundColor = textcolor;
            Console.WriteLine(error);
            Console.ForegroundColor = neutralColor;


        }


        static void EndProg()
        {
            PrintStrongLine();
            Console.ForegroundColor = workingColor;
            Console.WriteLine("Igor is finished running. Press any key to continue.");
            Console.ReadKey();
            System.Environment.Exit(1);
        }


        static void PrintStrongLine()
        {
            Console.ForegroundColor = strongLineColor;
            Console.WriteLine("");
            Console.WriteLine(strongLine);
            Console.WriteLine("");
            Console.ForegroundColor = neutralColor;
            return;
        }


        static void PrintHeader()
        {

            Console.ForegroundColor = bannerColor;
            Console.BackgroundColor = bannerBGColor;
            Console.WriteLine("                                        ");
            Console.WriteLine("  ______                                ");
            Console.WriteLine(" /      |                               ");
            Console.WriteLine(" $$$$$$/   ______    ______    ______   ");
            Console.WriteLine("   $$ |   /      \\  /      \\  /      \\  ");
            Console.WriteLine("   $$ |  /$$$$$$  |/$$$$$$  |/$$$$$$  | ");
            Console.WriteLine("   $$ |  $$ |  $$ |$$ |  $$ |$$ |  $$/  ");
            Console.WriteLine("  _$$ |_ $$ \\__$$ |$$ \\__$$ |$$ |       ");
            Console.WriteLine(" / $$   |$$    $$ |$$    $$/ $$ |       ");
            Console.WriteLine(" $$$$$$/  $$$$$$$ | $$$$$$/  $$/        ");
            Console.WriteLine("         /  \\__$$ |                     ");
            Console.WriteLine("         $$    $$/                      ");
            Console.WriteLine("          $$$$$$/                       ");
            Console.WriteLine("                                        ");
            Console.BackgroundColor = ConsoleColor.Black;
            
            Console.WriteLine("");

            Console.ForegroundColor = neutralColor;
            Console.WriteLine(" Igor (v " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")");
            Console.WriteLine(" (c) 2020-present, Ryan L. Boyd, Ph.D. ");

            PrintStrongLine();

            Console.WriteLine(" Preparing to load model...");

            Thread.Sleep(2000);

            return;

        }

        static void PrintFile(string filePath, string rootDir)
        {
            Console.ForegroundColor = workingColor;
            Console.WriteLine("    -> " + filePath.Replace(rootDir, ""));
            Console.ForegroundColor = neutralColor;
            return;
        }





        static Dictionary<int, string> GetModelNames(string namePath)
        {
            
            
            Dictionary<int, string> objectDict = new Dictionary<int, string>(); ;

            using (StreamReader objectNamesFile = new StreamReader(namePath))
            {
                int counter = 0;
                string ln;

                while ((ln = objectNamesFile.ReadLine()) != null)
                {
                    objectDict.Add(counter, ln.Trim());
                    counter++;
                }
                objectNamesFile.Close();
                
                Console.WriteLine(" Your model codes for " + counter.ToString() + " distinct objects.");
            }

            PrintStrongLine();
            Thread.Sleep(1000);

            return objectDict;

        }








        static Dictionary<string, string> GetModelSpecs(string detailsPath)
        {

            try { 

                Dictionary<string, string> detailsDict = new Dictionary<string, string>(); ;

                string lines;

                // read the file with the model details.
                using (FileStream fileStream = new FileStream(detailsPath, FileMode.Open, FileAccess.Read))
                using (StreamReader streamRead = new StreamReader(fileStream, System.Text.Encoding.UTF8))
                {

                    lines = streamRead.ReadToEnd().Trim();

                }

                string[] modelDetails = lines.Split( new[] {"\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (modelDetails.Length != 3)
                {
                    WriteText("There is an issue with your modelSelect.txt file.", errColor);
                    WriteText("It does not have the correct number of details (3).", errColor);
                    EndProg();
                }
                
                for (int i = 0; i < modelDetails.Length; i++) modelDetails[i] = modelDetails[i].Trim();

                detailsDict.Add("cfg", modelDetails[0]);
                detailsDict.Add("weights", modelDetails[1]);
                detailsDict.Add("names", modelDetails[2]);

                PrintStrongLine();
                WriteText(" Intended cfg file: " + detailsDict["cfg"], exampleColor);
                WriteText(" Intended weights file: " + detailsDict["weights"], exampleColor);
                WriteText(" Intended names file: " + detailsDict["names"], exampleColor);


                PrintStrongLine();
                Thread.Sleep(1000);

                return detailsDict;

            }
            catch
            {
                WriteText("There is an issue with your modelSelect.txt file.", errColor);
                EndProg();
                return new Dictionary<string, string>();
            }

        }


    }
}
