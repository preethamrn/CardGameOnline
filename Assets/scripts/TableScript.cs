using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class TableScript : MonoBehaviour {

    TagsManager tagsManager;

    // Use this for initialization
    void Start () {
        tagsManager = FindObjectOfType<TagsManager>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    private RaycastHit2D[] hits = new RaycastHit2D[100];
    private int cardSortingOrder = 0;

    public GameObject getTopCard(Vector2 mousePos) {
        hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition)); //Get all of the objects along the raycast
        //Insertion Sort
        int j;
        RaycastHit2D temp;

        for (int i = 0; i < hits.Length; i++) {
            j = i;
            while (j > 0) {
                j--;
                if (hits[j + 1].transform.position.z < hits[j].transform.position.z) { //if the current z is more negative (closer to the camera) than the preceeding, swap
                    temp = hits[j];
                    hits[j] = hits[j + 1];
                    hits[j + 1] = temp;
                }

                else if (hits[j + 1].transform.position.z == hits[j].transform.position.z) { //if the order cannot be determined from the z position, try sorting order
                    if (hits[j + 1].transform.GetComponent<SpriteRenderer>().sortingOrder > hits[j].transform.GetComponent<SpriteRenderer>().sortingOrder) {
                        temp = hits[j];
                        hits[j] = hits[j + 1];
                        hits[j + 1] = temp;
                    }
                }

            }
        }
        if (hits.Length > 0) return hits[0].transform.gameObject;
        else return null;
    }

    public void updateSortingOrder(GameObject card) {
        card.GetComponent<SpriteRenderer>().sortingOrder = (cardSortingOrder = (cardSortingOrder + 1) % 32768);
    }

    public void addCard(Vector2 pos, List<string> tags, KeyValuePair<string, JToken> piece)
    {
        GameObject newCard = (GameObject) PhotonNetwork.Instantiate("Card", new Vector3(pos.x, pos.y, -1.0f), Quaternion.identity, 0);

        SpriteRenderer spriteRenderer = newCard.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = cardSortingOrder++;

        foreach (string tag in tags) {
            tagsManager.AddTagToCard(tag, newCard);
        }

        newCard.GetComponent<Properties>().LoadCard(piece);
    }
}
