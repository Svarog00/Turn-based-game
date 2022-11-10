using UnityEditor;
using System.IO;
using UnityEngine;

using static System.IO.Path;

public static class ToolsMenu
{
    [MenuItem("Tools/Setup/Create Default Folders")]
    public static void CreateDefaultFolders()
    {
        CreateDirectories("_Project", "Scripts", "Sprites", "Animations", "Scenes");
        AssetDatabase.Refresh();
    }

    public static void CreateDirectories(string root, params string[] directories)
    {
        string fullPath = Combine(Application.dataPath, root);
        foreach(string newDir in directories)
        {
            Directory.CreateDirectory(Combine(fullPath, newDir));
        }

    }
}
