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
        JArray cards_json = (JArray) piece.Value["cards"];
        if(cards_json != null) cards = cards_json.ToObject<Queue<string>>(); //DEBUGGING: error?
        else cards = new Queue<string>();

        float scale = (float) (piece.Value["scale"] != null ? piece.Value["scale"] : 1);
        StartCoroutine(FindObjectOfType<LoadGame>().SetSprite(this.gameObject, (string)piece.Value["texture"], scale));
        
        //load the tags, list of cards, sprite textures, etc... based on the properties that this card is loaded with
        //whenever a card is added, we only need to load the position(?), tags, and piece
    }


    public void shuffle() { }

    public void drawCard() {
        if (cards.Count <= 0) return; //return if no cards in deck (deck should be deleted before this is possible)

        string cardName = cards.Dequeue();
        KeyValuePair<string, JToken> newPiece = new KeyValuePair<string, JToken>(piece.Key, piece.Value.DeepClone());
        newPiece.Value["texture"] = cardName;
        newPiece.Value["type"] = "card";
        newPiece.Value["cards"].Parent.Remove();
        newPiece.Value["back"] = piece.Value["texture"];

        List<string> tags = new List<string>() { "card" }; //DEBUGGING: TEMP: should inherit deck tags as well
        FindObjectOfType<TableScript>().addCard(new Vector2(transform.position.x+1, transform.position.y+1), tags, newPiece);
        
    }

    public void flipCard() {
        if (piece.Value["back"] != null) {
            float scale = (float)(piece.Value["scale"] != null ? piece.Value["scale"] : 1);
            StartCoroutine(FindObjectOfType<LoadGame>().SetSprite(this.gameObject, (string)piece.Value["back"], scale));
            string back = (string) piece.Value["texture"];
            piece.Value["texture"] = piece.Value["back"];
            piece.Value["back"] = back;
        }
    }
    

    public Vector2 getPos() {
        return new Vector2(transform.position.x, transform.position.y);
    }
    public int deckSize() {
        return cards.Count;
    }
    public KeyValuePair<string, JToken> getPiece() {
        return piece;
    }

}
