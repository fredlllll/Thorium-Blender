using System;
using System.IO;
using System.Reflection;
using Thorium_Shared;
using Thorium_Storage_Service;

namespace Thorium_Blender
{
    public class BlenderExecutioner : AExecutioner
    {
        public const string ArgDataPackage = "dataPackage";
        public const string ArgFileName = "fileName";
        public const string ArgStartFrame = "startFrame";
        public const string ArgEndFrame = "endFrame";

        public BlenderExecutioner(LightweightTask t) : base(t)
        {
        }

        public override void Execute()
        {
            string pluginDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string dataPackage = Task.GetInfo<string>(ArgDataPackage);
            string workingDir = Path.Combine(Directories.TempDir, Task.ID);
            StorageService.MakeDataPackageAvailable(dataPackage, workingDir, UnzipThings);

            //string blenderExecutable = Task.GetInfo<string>("blenderExecutable");
            string fileName = Task.GetInfo<string>(ArgFileName);
            string filePath = Path.Combine(workingDir, fileName);
            int startFrame = Task.GetInfo<int>(ArgStartFrame);
            int endFrame = Task.GetInfo<int>(ArgEndFrame);

            //string outputDir = Path.Combine(Directories.TempDir, Task.JobID, Task.ID);
            //string outputFile = Path.Combine(outputDir, "frame####.png");

            RunExecutableAction rea = new RunExecutableAction
            {
                FileName = Thorium_Shared.Files.GetExecutablePath("bash")
            };
            rea.AddArgument(Path.Combine(pluginDir, "sarfis.sh"));
            rea.Environment["blendPath"] = filePath;
            rea.Environment["startFrame"] = startFrame.ToString();
            rea.Environment["endFrame"] = endFrame.ToString();
            rea.Environment["jobId"] = Task.JobID;
            rea.Environment["taskId"] = Task.ID;

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

            Directory.Delete(workingDir, true);
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
