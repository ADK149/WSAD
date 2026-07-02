using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;
using System.Linq;


namespace WSAD.Service { }

public static class DrawEntityService
{
    public static void DrawEntity(Entity entity)
    {
        var doc = Application.DocumentManager.MdiActiveDocument;
        var db = doc.Database;
        var ed = doc.Editor;
        ed.WriteMessage("welcome WSAD");

        using (var tr = db.TransactionManager.StartTransaction())
        {
            var bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
            var btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
            btr.AppendEntity(entity);
            tr.AddNewlyCreatedDBObject(entity, true);
            tr.Commit();
        }
    }

    public static void DrawRectangle(Point3d fristPoint,Point3d secondPoint)
    {
        var rectangle = new Polyline();
        rectangle.AddVertexAt(0, new Point2d(fristPoint.X, fristPoint.Y), 0, 0, 0);
        rectangle.AddVertexAt(1, new Point2d(secondPoint.X, fristPoint.Y), 0, 0, 0);
        rectangle.AddVertexAt(2, new Point2d(secondPoint.X, secondPoint.Y), 0, 0, 0);
        rectangle.AddVertexAt(3, new Point2d(fristPoint.X, secondPoint.Y), 0, 0, 0);
        rectangle.Closed = true;
        DrawEntity(rectangle);
    }

    public static void DrawCircle(Point3d center, double radius)
    {
        var circle = new Circle(center, Vector3d.ZAxis, radius);
        DrawEntity(circle);
    }

    public static void DrawPolyline(List<Point3d> points,double bulge = 0, bool closed = false, double startWidth = 0, double endWidth = 0)
    {
        var polyline = new Polyline();
        for (int i = 0; i < points.Count(); i++)
        {
            var point = points.ElementAt(i);
            polyline.AddVertexAt(i, new Point2d(point.X, point.Y), bulge, startWidth, endWidth);
        }
        polyline.Closed = closed;
        DrawEntity(polyline);
    }

    public static void DrawPolylineBySpace(List<Point3d> points)
    {
        var doc = Application.DocumentManager.MdiActiveDocument;
        var db = doc.Database;
        var ed = doc.Editor;

        var firstPoint = points.FirstOrDefault();
        var secondPoint = points.LastOrDefault();

        for (int i = 1;i<points.Count;i++)
        {
            secondPoint = points[i];

            PromptStringOptions pso = new PromptStringOptions("\n按 空格键 绘制polyline，按 Enter 结束当前多段线: ");
            pso.AllowSpaces = false; // 关键：禁止在输入字符串中包含空格，这样按下空格就会立即返回[reference:4]
            pso.UseDefaultValue = false;

            PromptResult pr = ed.GetString(pso);
            if (pr.Status == PromptStatus.OK)
            {
                DrawPolyline(new List<Point3d> { firstPoint, secondPoint },startWidth:10,endWidth:0);
                firstPoint = secondPoint;
            }
            else if (pr.Status == PromptStatus.Cancel)
            {
                return;
            }
            else
            {
                // 用户按了 Enter（或其他情况），结束当前多段线的绘制
                break;
            }
        }
    }
}
