using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class LoadGame : MonoBehaviour {

    string[] files;
    string pathPreFix = @"file://";
    private TableScript table;

    // Use this for initialization
    void Start() {
        table = FindObjectOfType<TableScript>();

        string path = ApplicationModel.gameDir;
        string json = System.IO.File.ReadAllText(path + @"\properties.cgo");

        JObject pieces = JObject.Parse(json);

        foreach (System.Collections.Generic.KeyValuePair<string, JToken> piece in pieces) {
            string name = piece.Key;
            string type = (string) piece.Value["type"];
            print(name + ": " + type);
        }

        files = System.IO.Directory.GetFiles(path, "*.png");
        StartCoroutine(LoadImages());
    }

    private IEnumerator LoadImages()
    {

        foreach (string tstring in files)
        {
            string pathTemp = pathPreFix + tstring;
            WWW www = new WWW(pathTemp);
            yield return www;
            Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texTmp);
            table.addCard(Vector2.zero, null, Sprite.Create(texTmp, new Rect(0,0,texTmp.width,texTmp.height), new Vector2(0.5f, 0.5f))); //DEBUGGING: load the tags and each separate object recursively
        }

    }
}
