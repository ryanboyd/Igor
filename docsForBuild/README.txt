
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

A console-based wrapper to Alturos.Yolo v3.0.6. 
Counts the number of each type of object found 
within each image.

==========
How to Use
==========
 Run from the command line with two parameters: the folder that contains your input files, and the location where you would like to save your CSV file. For example:
 
	igor.exe "C:\DataFolder\Images" "C:\DataFolder\Output.csv"

Igor will recursively search your input folder for all jpg and png files.



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