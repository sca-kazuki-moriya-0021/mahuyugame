
using System.Diagnostics;
using UnityEditor;
using UnityEngine;


public class Restarter : MonoBehaviour
{
    [MenuItem("File/Restart")]
    private static void Restart()
    {
        var filename = EditorApplication.applicationPath;
        var arguments = "-projectPath " + Application.dataPath.Replace("/Assets", string.Empty);
        var startInfo = new ProcessStartInfo
        {
            FileName = filename,
            Arguments = arguments,
        };
        Process.Start(startInfo);

        EditorApplication.Exit(0);
    }
}
