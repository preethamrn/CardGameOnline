//TODO:
//Scroll bar in case games exceed screen space (either width or height)
//Make text prettier
//Select game and move to table scene (then load cards on start)

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ListGames : MonoBehaviour
{
    List<string> directories;
    
    void Start()
    {
        directories = GetDirectories(Application.dataPath+"/Games", "*");
    }

    void OnGUI()
    {
        int i = 0;
        foreach (string dir in directories) {
            GUI.Label(new Rect(0, 10*i, 1000, 100), dir);
            i++;
        }
    }

    private static List<string> GetDirectories(string path, string searchPattern)
    {
        try
        {
            return Directory.GetDirectories(path, searchPattern).ToList();
        }
        catch (UnauthorizedAccessException)
        {
            return new List<string>();
        }
    }
}