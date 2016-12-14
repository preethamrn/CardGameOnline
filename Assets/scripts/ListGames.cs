using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            string game = Regex.Match(dir, @"([^\\]*)", RegexOptions.RightToLeft).Value;
            if (GUI.Button(new Rect(0, 30 * i, 1000, 30), game, GUI.skin.box))
            {
                ApplicationModel.gameName = game;
                ApplicationModel.gameDir = dir;
                SceneManager.LoadScene("table");
            }
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