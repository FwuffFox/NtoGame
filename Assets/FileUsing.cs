using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public static class FileUsing
{
    private static readonly string fullPath=Application.persistentDataPath+"/quests.txt";

    public static bool SaveExists() => File.Exists(fullPath);
    public static void WriteFile(IEnumerable<int> list) {
        // Write file using StreamWriter
        using (StreamWriter writer = new StreamWriter(fullPath))
        {
            foreach (var num in list)
                writer.Write(num + " ");
        }
    }
    public static IEnumerable<int> ReadFile()
    {
        // Read a file
        string readText = File.ReadAllText(fullPath);
        return readText.Split().Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse);
    }
}