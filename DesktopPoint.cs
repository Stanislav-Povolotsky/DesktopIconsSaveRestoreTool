// This code was taken from
// http://www.codeproject.com/Articles/639486/Save-and-restore-icon-positions-on-desktop
// License: http://www.codeproject.com/info/cpol10.aspx
//
// Modified:
//      19.06.2016 by Stanislav Povolotsky <stas.dev[at]povolotsky.info>
//                 Removed IsolatedStorageFile to use Storage class in console application
using System.Runtime.InteropServices;

namespace IconsSaveRestore.Code
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DesktopPoint
    {
        public int X;
        public int Y;

        public DesktopPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public struct NamedDesktopPoint
    {
        public string Name;
        public int X;
        public int Y;

        public NamedDesktopPoint(string name, int x, int y)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
        }
    }
}
