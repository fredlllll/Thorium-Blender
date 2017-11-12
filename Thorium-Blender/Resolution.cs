/*namespace Thorium_Blender
{
    internal class Resolution
    {
        public int width, height;

        public override string ToString()
        {
            return width + "x" + height;
        }

        public static Resolution Parse(string str)
        {
            Resolution r = new Resolution();

            string[] sa = str.Split('x');
            r.width = int.Parse(sa[0]);
            r.height = int.Parse(sa[1]);
            return r;
        }

    }
}*/