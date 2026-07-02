using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace WSAD.Tool { }

public static class DataTool
{
    public static string GetTextFileContetnWithDialog()
    {
        var openFileDialog = new Microsoft.Win32.OpenFileDialog();
        openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        openFileDialog.Title = "Select a  file";

        if(openFileDialog.ShowDialog() == true)
        {
            if (openFileDialog.FileName == null)
                return null;
            return File.ReadAllText(openFileDialog.FileName);
        }
        return null;
    }

    public static List<List<Point3d>> GetPoint3dWithTxt(string content)
    {
        if (String.IsNullOrEmpty(content))
            return null;
        string[] lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        string pattern = @"\((\s*-?\d+(?:\.\d+)?)\s*,\s*(\s*-?\d+(?:\.\+d)?)\s*,\s*(\s*-?\d+(?:\.\d+)?)\s*\)";
        var pointss = new List<List<Point3d>>();

        foreach (var line in lines)
        {
            if (String.IsNullOrEmpty(line))
                continue;
            var points = new List<Point3d>();
            var matchs = Regex.Matches(line, pattern);
            foreach (Match match in matchs)
            {
                points.Add(new Point3d(
                    double.Parse(match.Groups[1].Value),
                    double.Parse(match.Groups[2].Value),
                    double.Parse(match.Groups[3].Value)));
            }
            pointss.Add(points);
        }
        return pointss;
    }

}