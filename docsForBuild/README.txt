
  ______                                
 /      |                               
 $$$$$$/   ______    ______    ______   
   $$ |   /      \  /      \  /      \  
   $$ |  /$$$$$$  |/$$$$$$  |/$$$$$$  | 
   $$ |  $$ |  $$ |$$ |  $$ |$$ |  $$/  
  _$$ |_ $$ \__$$ |$$ \__$$ |$$ |       
 / $$   |$$    $$ |$$    $$/ $$ |       
 $$$$$$/  $$$$$$$ | $$$$$$/  $$/        
         /  \__$$ |                     
         $$    $$/                      
          $$$$$$/                       
                                        

 by Ryan L. Boyd, Ph.D.

A wrapper to Alturos.Yolo v3.0.6. 
Counts the number of each type of object found 
within each image.

==========
How to Use
==========

 Run igorGUI.exe, which will open a window with settings/options.
 Choose your preferred model to run, the folder with your images
 that you would like to process, and where you would like to save
 your output file, then click the [Process Images!] button.
 
 Run from the command line with two parameters: the folder that contains your input files, and the location where you would like to save your CSV file. For example:
 
	igor.exe "C:\DataFolder\Images" "C:\DataFolder\Output.csv"

Igor will recursively search your input folder for all jpg and png files.


=========================
Which Model Should I Use?
=========================

 That's a great question, and I can give you an unequivocal answer: it definitely depends.
 Most people will probably want to use either YoloV2 or V3, specifically, the "tiny" versions
 of one of these models. Both models code for the same 80 object classes, and both are much
 faster than their non-tiny equivalents.

 Igor also comes with a model that was trained on the OpenImages corpus, which has something
 like 600 object classes that it codes for. Igor does not come with a "tiny" version of this
 model, which means that it can only be done "the slow way" with a CPU (speed is less of an
 issue if you are processing from a GPU; see below).


==========
GPU / CUDA
==========

If you want Igor to use your GPU for image analysis (which is *much* faster), you will need to follow these instructions from the original Alturos.Yolo repo:
 
1. Install the latest Nvidia driver for your graphic device

2. Install Nvidia CUDA Toolkit 10.2 (must be installed with correct hardware driver for CUDA support)
		https://developer.nvidia.com/cuda-downloads

3. Download Nvidia cuDNN v7.6.5 for CUDA 10.2
		https://developer.nvidia.com/rdp/cudnn-download

4. Copy the "cudnn64_7.dll" into the same folder as igor.exe

If you have correctly followed all of the above steps, Igor will run *at least* 10x faster. Note that these instructions presume that you have a compatible NVidia card. If you do not or if there is an issue during the initialization of your GPU, Igor will fall back to just running analyses on your CPU.