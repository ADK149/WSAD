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

    [CommandMethod((nameof(W_GETENTITY)))]
    public static void W_GETENTITY()
    {
        var doc = Application.DocumentManager.MdiActiveDocument;
        var db = doc.Database;
        var ed = doc.Editor;
        ed.WriteMessage("welcome WSAD");
    }

    [CommandMethod((nameof(W_DRAWPOLYLINEBYSPACE)))]
    public static void W_DRAWPOLYLINEBYSPACE()
    {
        var content = DataTool.GetTextFileContetnWithDialog();
        var data = DataTool.GetPoint3dWithTxt(content);
        if (data == null)
            return;
        foreach (var item in data)
        {
            DrawEntityService.DrawPolylineBySpace(item);
        }
    }
}
