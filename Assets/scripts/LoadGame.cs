using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class LoadGame : MonoBehaviour {
    
    string pathPreFix = @"file://";
    private TableScript table;

    private Dictionary<string, Texture2D> textures;

    // Use this for initialization
    void Start() {
        table = FindObjectOfType<TableScript>();
        textures = new Dictionary<string, Texture2D>();

        string path = ApplicationModel.gameDir;
        pathPreFix += path;
        
        string json = System.IO.File.ReadAllText(path + @"\properties.cgo");
        JObject pieces = JObject.Parse(json);

        foreach (KeyValuePair<string, JToken> piece in pieces) {
            string name = piece.Key;
            string type = (string) piece.Value["type"];
            List<string> tags = new List<string> { type }; //DEBUGGING: TEMP: use all the tags associated with this card
            table.addCard(new Vector2(0, 0), tags, piece); //Debug.Log(name + ": " + type);
        }
    }

    public IEnumerator SetSprite(GameObject go, string tstring, float scale) {
        Texture2D texTmp;
        if (!textures.ContainsKey(tstring)) {
            string pathTemp = pathPreFix + @"\" + tstring;
            Debug.Log(pathTemp);
            WWW www = new WWW(pathTemp);
            yield return www;
            texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texTmp);
            textures[tstring] = texTmp;
        } else {
            texTmp = textures[tstring];
        }

        go.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texTmp, new Rect(0, 0, texTmp.width, texTmp.height), new Vector2(0.5f, 0.5f));
        go.transform.localScale = new Vector3(scale/2, scale/2, scale/2);
    }
    
}
