using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thorium_Blender
{
    public struct Interval
    {
        public int start, end;

        public IEnumerable<int> GetNumberIterator()
        {
            return Enumerable.Range(start, end - start + 1);
        }

        public static Interval Parse(string str)
        {
            Interval retVal = new Interval();
            if(str.Contains('-'))
            {
                var sa = str.Split('-');
                retVal.start = int.Parse(sa[0]);
                retVal.end = int.Parse(sa[1]);
            }
            else
            {
                retVal.start = retVal.end = int.Parse(str);
            }
            return retVal;
        }
    }
}
