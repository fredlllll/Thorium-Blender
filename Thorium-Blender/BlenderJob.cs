using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thorium_Shared;

namespace Thorium_Blender
{
    public class BlenderJob : AJob
    {
        internal List<Interval> framebounds = new List<Interval>();
        internal int tilesPerFrame;
        internal List<Layer> layers = new List<Layer>();
        internal Resolution resolution;

        public BlenderJob(JobInformation information) : base(information)
        {
            TaskInformationProducer = new BlenderTaskInformationProducer(this);
        }

        public override void Initialize()
        {
            string framebounds = JobInformation.Config.Get("framebounds");
            foreach(var s in framebounds.Split(','))
            {
                var i = Interval.Parse(s);
                this.framebounds.Add(i);
            }

            tilesPerFrame = JobInformation.Config.Get<int>("tilesperframe");

            string layers = JobInformation.Config.Get("layers");
            foreach(var s in layers.Split(','))
            {
                var l = Layer.Parse(s);
            }

            string res = JobInformation.Config.Get("resolution");
            resolution = Resolution.Parse(res);

            //TODO: file stuff
        }
    }
}
