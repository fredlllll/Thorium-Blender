using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thorium_Blender
{
    public class Interval : IEnumerable<int>
    {
        public int Start { get; set; }
        public int End { get; set; }
        public Interval() { }
        public Interval(int start, int end)
        {
            Start = start;
            End = end;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return Enumerable.Range(Start, End - Start + 1).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Interval Parse(string str)
        {
            Interval interval = new Interval();
            if(str.Contains('-'))
            {
                var sa = str.Split('-');
                interval.Start = int.Parse(sa[0]);
                interval.End = int.Parse(sa[1]);
            }
            else
            {
                interval.Start = interval.End = int.Parse(str);
            }
            return interval;
        }
    }
}