using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Thorium_Shared;

namespace Thorium_Blender
{
    public class BlenderTaskProducer : ATaskProducer
    {
        Interval frames;

        public BlenderTaskProducer(Job job) : base(job)
        {
            int start = Job.Information.Get<int>("framesStart");
            int end = Job.Information.Get<int>("framesEnd");
            frames = new Interval(start, end);
        }

        IEnumerator<Task> GetTaskEnumerator()
        {
            foreach(var frame in frames)
            {
                JObject info = new JObject
                {
                    ["startFrame"] = frame,
                    ["endFrame"] = frame
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
