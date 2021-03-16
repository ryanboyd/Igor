# Igor
 ![banner](img/banner.png)
 
 A console-based wrapper to Alturos.Yolo v3.0.6. Counts the number of each type of object found within each image.

## Download
 The compiled binary can be downloaded from https://www.ryanboyd.io/software/toolbox/#Igor

## System requirements
- [.NET Framework 4.6.1](https://www.microsoft.com/en-us/download/details.aspx?id=49982)
- [Microsoft Visual C++ Redistributable for Visual Studio 2015, 2017 and 2019 x64](https://aka.ms/vs/16/release/vc_redist.x64.exe)

## Use
 Run from the command line with two parameters: the folder that contains your input files, and the location where you would like to save your CSV file. For example:
 
 ```
 igor.exe "C:\DataFolder\Images" "C:\DataFolder\Output.csv"
 ```
 
 Igor will recursively search your input folder for all jpg and png files.
 
 Alternatively, you can run igorGUI.exe and perform the same analyses from a graphical interface. The underlying core does all of the same processing, but this version might be slightly more accessible to people who aren't as savvy/comfortable with command lines.
 
 ![banner](img/igorGUI.png)
 
 Finally, when you [download Igor](https://www.ryanboyd.io/software/toolbox), you may notice that there is a "modelDat" folder included in the archive. Within the modelDat folder, you will find several subfolders containing pre-trained models, plus a "modelSelect.txt" file. For the console-based version of Igor, you can change the details in your modelSelect.txt file to tell the software which pretrained model to use. In the GUI-based version of the software, you can use the interface/buttons to select your model files. Note that you will want to make sure that you're selecting all three files from the same model folder.
 

## GPU / CUDA

 By default, Igor processes images using your CPU. If you want Igor to use your GPU instead (which is *much* faster), you will need to do a little bit of extra setup by following these instructions from the original Alturos.Yolo repo:
 
 1. Install the latest Nvidia driver for your graphic device
 2. [Install Nvidia CUDA Toolkit 10.2](https://developer.nvidia.com/cuda-downloads) (must be installed with correct hardware driver for CUDA support)
 3. [Download Nvidia cuDNN v7.6.5 for CUDA 10.2](https://developer.nvidia.com/rdp/cudnn-download)
 4. Copy the `cudnn64_7.dll` into the same folder as igor.exe
 
 This assumes, of course, that you have a compatible GPU in the first place.
