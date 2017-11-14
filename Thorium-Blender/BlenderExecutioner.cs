using System;
using System.IO;
using Thorium_Shared;
using Thorium_Storage_Service;

namespace Thorium_Blender
{
    public class BlenderExecutioner : AExecutioner
    {
        public BlenderExecutioner(LightweightTask t) : base(t)
        {
        }

        public override void Execute()
        {
            string dataPackage = Task.GetInfo<string>("dataPackage");
            string workingDir = Path.Combine(Directories.TempDir, Task.ID);
            StorageService.MakeDataPackageAvailable(dataPackage, workingDir, UnzipThings);

            //string blenderExecutable = Task.GetInfo<string>("blenderExecutable");
            string fileName = Task.GetInfo<string>("fileName");
            string filePath = Path.Combine(workingDir, fileName);
            int startFrame = Task.GetInfo<int>("startFrame");
            int endFrame = Task.GetInfo<int>("nedFrame");

            //string outputDir = Path.Combine(Directories.TempDir, Task.JobID, Task.ID);
            //string outputFile = Path.Combine(outputDir, "frame####.png");

            RunExecutableAction rea = new RunExecutableAction
            {
                FileName = Thorium_Shared.Files.GetExecutablePath("bash")
            };
            rea.AddArgument("/scripts/sarfis.sh");
            rea.AddArgument(filePath);
            rea.AddArgument(startFrame.ToString());
            rea.AddArgument(endFrame.ToString());
            rea.AddArgument(Task.JobID);
            rea.AddArgument(Task.ID);

            //rea.AddArgument("-b"); //console mode "background"
            //rea.AddArgument(filename);
            //rea.AddArgument("-y");//auto run python scripts
            //rea.AddArgument("-o");//output file
            //rea.AddArgument(outputFile);
            //rea.AddArgument("-F"); //format
            //rea.AddArgument("PNG"); //PNG
            //rea.AddArgument("-E"); //engine
            //rea.AddArgument("cycles"); //cycles
            //rea.AddArgument("-P"); //run this script on startup
            //rea.AddArgument("somescript.py");//the script
            //rea.AddArgument("-a");// use settings from blend file
            //rea.AddArgument("-f");
            //rea.AddArgument(frame.ToString());
            //rea.AddArgument("--"); //blender stops parsing after this, but can be parsed by python scripts
            //TODO: add custom arguments

            rea.StartAndWait();

            /*System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
            {
                StorageService.UploadResults(Task.JobID, Task.ID, outputDir, true);
            });
            task.Start();*/
        }

        void UnzipThings(string downloadFolder, string targetFolder)
        {
            RunExecutableAction rea = new RunExecutableAction
            {
                FileName = Thorium_Shared.Files.GetExecutablePath("unzip")
            };
            rea.AddArgument(Path.Combine(downloadFolder, "datapackage.zip"));
            rea.AddArgument("-d");
            rea.AddArgument(targetFolder);
        }
    }
}
