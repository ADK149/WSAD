using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSAD
{
    public class Class1
    {
        public static string GetPoint3dssInfo(List<List<Point3d>> point3dss)
        {
            var info = new StringBuilder();
            var index = 0;
            foreach (var point3ds in point3dss)
            {
                var data = "";
                foreach (var point3d in point3ds)
                {
                    data += $"({point3d.X},{point3d.Y},{point3d.Z})";
                }
                info.Append($"{++index}:"+data+"\n");
            }
            return info.ToString();
        }
    }
}
