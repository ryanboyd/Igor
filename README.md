# Igor
 A console-based wrapper to Alturos.Yolo v3.0.6. Counts the number of each type of object found within each image.

# Download
 The compiled binary can be downloaded from https://www.ryanboyd.io/software/toolbox

## System requirements
- .NET Framework 4.6.1 or .NET standard 2.0
- [Microsoft Visual C++ Redistributable for Visual Studio 2015, 2017 and 2019 x64](https://aka.ms/vs/16/release/vc_redist.x64.exe)

# Use
 Run from the command line with two parameters: the folder that contains your input files, and the location where you would like to save your CSV file. For example:
 
 ```
 igor.exe "C:\DataFolder\Images" "C:\DataFolder\Output.csv"
 ```
 
 Igor will recursively search your input folder for all jpg and png files.

# GPU / CUDA

 If you want Igor to use your GPU for image analysis (which is *much* faster), you will need to follow these instructions from the original Alturos.Yolo repo:
 
 1. Install the latest Nvidia driver for your graphic device
 2. [Install Nvidia CUDA Toolkit 10.2](https://developer.nvidia.com/cuda-downloads) (must be installed add a hardware driver for cuda support)
 3. [Download Nvidia cuDNN v7.6.5 for CUDA 10.2](https://developer.nvidia.com/rdp/cudnn-download)
 4. Copy the `cudnn64_7.dll` into the same folder as igor.exe