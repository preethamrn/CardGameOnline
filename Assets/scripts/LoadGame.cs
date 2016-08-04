using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {

    string[] files;
    string pathPreFix;

    // Use this for initialization
    void Start() {
        string path = ApplicationModel.gameDir;
        pathPreFix = @"file://";
        print(path);
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
            CameraScript.addCard(Vector2.zero, Sprite.Create(texTmp, new Rect(0,0,texTmp.width,texTmp.height), new Vector2(0.5f, 0.5f)));
        }

    }
}
