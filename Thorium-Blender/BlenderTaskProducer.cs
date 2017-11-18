using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Thorium_Shared;

namespace Thorium_Blender
{
    public class BlenderTaskProducer : ATaskProducer
    {
        public const string ArgStartFrame = "startFrame";
        public const string ArgEndFrame = "endFrame";
        public const string ArgFramesPerTask = "framesPerTask";

        //Interval frames;
        int startFrame, endFrame;
        int framesPerTask;

        public BlenderTaskProducer(Job job) : base(job)
        {
            startFrame = Job.Information.Get<int>(ArgStartFrame);
            endFrame = Job.Information.Get<int>(ArgEndFrame);
            framesPerTask = Job.Information.Get<int>(ArgFramesPerTask);
        }

        IEnumerator<Task> GetTaskEnumerator()
        {
            for(int i = startFrame; i <= endFrame; i += framesPerTask)
            {
                JObject info = new JObject
                {
                    ["startFrame"] = i,
                    ["endFrame"] = Math.Min(i + framesPerTask - 1, endFrame)
                };

                yield return new Task(Job, Utils.GetRandomID(), info);
            }
        }

        IEnumerator<Task> taskEnum = null;
        public override Task GetNextTask()
        {
            if(taskEnum == null)
            {
                taskEnum = GetTaskEnumerator();
            }
            if(taskEnum.MoveNext())
            {
                return taskEnum.Current;
            }
            return null;
        }
    }
}
