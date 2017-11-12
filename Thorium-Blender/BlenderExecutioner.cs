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
            string blenderExecutable = Task.GetInfo<string>("blenderExecutable");
            string filename = Task.GetInfo<string>("filename");
            int frame = Task.GetInfo<int>("frame");

            string outputDir = Path.Combine(Directories.TempDir, Task.JobID, Task.ID);
            string outputFile = Path.Combine(outputDir, "frame" + frame + ".png");

            RunExecutableAction rea = new RunExecutableAction
            {
                FileName = blenderExecutable
            };
            rea.AddArgument("-b"); //console mode "background"
            rea.AddArgument(filename);
            //rea.AddArgument("-y");//auto run python scripts
            rea.AddArgument("-o");//output file
            rea.AddArgument(outputFile);
            //rea.AddArgument("-F"); //format
            //rea.AddArgument("PNG"); //PNG
            //rea.AddArgument("-E"); //engine
            //rea.AddArgument("cycles"); //cycles
            //rea.AddArgument("-P"); //run this script on startup
            //rea.AddArgument("somescript.py");//the script
            //rea.AddArgument("-a");// use settings from blend file
            rea.AddArgument("-f");
            rea.AddArgument(frame.ToString());
            rea.AddArgument("--"); //blender stops parsing after this, but can be parsed by python scripts

            //TODO: add custom arguments
            rea.StartAndWait();

            System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
            {
                StorageService.UploadResults(Task.JobID, Task.ID, outputDir, true);
            });
            task.Start();
        }
    }
}
