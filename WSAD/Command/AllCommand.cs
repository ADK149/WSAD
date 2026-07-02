using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using WSAD.Service;
using WSAD.Tool;

namespace WSAD.Command
{ }

public static class AllCommand
{
    [CommandMethod((nameof(WSAD)))]
    public static void WSAD()
    {
        var doc = Application.DocumentManager.MdiActiveDocument;
        var db = doc.Database;
        var ed = doc.Editor;
        ed.WriteMessage("welcome WSAD");
    }

    [CommandMethod((nameof(W_GetEntity)))]
    public static void W_GetEntity()
    {
        var doc = Application.DocumentManager.MdiActiveDocument;
        var db = doc.Database;
        var ed = doc.Editor;

        var et = GetEntityService.SelectEntityOption();
        var etTypeName = et.GetType().Name;

        ed.WriteMessage($"选择的图形类型是：{etTypeName}");
    }

    [CommandMethod((nameof(W_DrawPolylinesWithTxt)))]
    public static void W_DrawPolylinesWithTxt()
    {
        var content = DataTool.GetTextFileContetnWithDialog();
        var data = DataTool.GetPoint3dWithTxt(content);
        if(data == null)
            return;
        foreach (var item in data)
        {
            DrawEntityService.DrawPolyline(item, startWidth: 10, endWidth: 0);
        }
    }

    [CommandMethod((nameof(W_DrawCirclesWithTxt)))]
    public static void W_DrawCirclesWithTxt()
    {
        var content = DataTool.GetTextFileContetnWithDialog();
        var data = DataTool.GetPoint3dWithTxt(content);
        if (data == null)
            return;
        foreach(var points in data)
        {
            foreach (var point in points)
            {
                DrawEntityService.DrawCircle(point, 3);
            }
        }
    }

    [CommandMethod((nameof(W_DrawPolylineBySpace)))]
    public static void W_DrawPolylineBySpace()
    {
        var content = DataTool.GetTextFileContetnWithDialog();
        var data = DataTool.GetPoint3dWithTxt(content);
        if (data == null)
            return;
        foreach (var item in data)
        {
            var isContinue = DrawEntityService.DrawPolylineBySpace(item);
            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("一条数据绘制完成");
            if (!isContinue)
                break;
        }
    }
}
