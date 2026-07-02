using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSAD.Service { }

public static class GetEntityService
{
    public static Entity GetEntity()
    {
        var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        var db = doc.Database;
        var ed = doc.Editor;
        
        var op = new PromptEntityOptions("\nSelect an entity: ");
        var res = ed.GetEntity(op);
        if (res.Status != PromptStatus.OK) return null;

        Entity ent = null;
        using (var tr = db.TransactionManager.StartTransaction())
        {
            ent = tr.GetObject(res.ObjectId, OpenMode.ForRead) as Entity;
            if(ent == null) return null;
            tr.Commit();
        }
        return ent;
    }
}
