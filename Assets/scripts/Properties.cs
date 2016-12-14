using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

//This class just contains public functions that help access the properties of a Card
public class Properties : MonoBehaviour {

    //contains all properties from properties file
    //validation is done in LoadGames.cs
    KeyValuePair<string, JToken> piece;

    Queue<string> cards;


    public void LoadCard(KeyValuePair<string, JToken> _piece) {
        piece = _piece;
        JArray cards_json = (JArray)piece.Value["cards"];
        if(cards_json != null) cards = cards_json.ToObject<Queue<string>>(); //DEBUGGING: error?
        else cards = new Queue<string>();

        Texture2D texTmp = FindObjectOfType<LoadGame>().GetTexture((string)piece.Value["texture"]);
        float scale = ((float)piece.Value["scale"]);
        if(texTmp != null) GetComponent<SpriteRenderer>().sprite = Sprite.Create(texTmp, new Rect(0, 0, texTmp.width, texTmp.height), new Vector2(scale/2, scale/2));
        
        //load the tags, list of cards, sprite textures, etc... based on the properties that this card is loaded with
        //whenever a card is added, we only need to load the position(?), tags, and piece
    }


    public void shuffle() { }

    public void drawCard() {
        if (cards.Count <= 0) return; //return if no cards in deck (deck should be deleted before this is possible)

        string cardName = cards.Dequeue();
        KeyValuePair<string, JToken> newPiece = piece;
        newPiece.Value["texture"] = cardName;
        newPiece.Value["cards"].Parent.Remove();

        List<string> tags = new List<string>() { "card" }; //DEBUGGING: TEMP: should inherit deck tags as well
        FindObjectOfType<TableScript>().addCard(getPos(), tags, newPiece);
    }
    

    public Vector2 getPos() {
        return new Vector2(transform.position.x, transform.position.y);
    }
    
}
