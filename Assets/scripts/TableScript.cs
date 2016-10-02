using UnityEngine;
using System.Collections;

public class TableScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private RaycastHit2D[] hits = new RaycastHit2D[100];
    private int cardSortingOrder = 0;

    public GameObject getTopCard(Vector2 mousePos) {
        hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition)); //Get all of the objects along the raycast
        //Insertion Sort
        int y;
        int so;
        int j;
        RaycastHit2D temp;

        /*
        string str = "";
        for (int i = 0; i < hits.Length; i++) {
            str += "; " + hits[i].transform.position.z + ", " + hits[i].transform.GetComponent<SpriteRenderer>().sortingOrder;
        }
        Debug.Log("1: " + str, this);
        */

        for (int i = 0; i < hits.Length; i++)
        {
            j = i;
            while (j > 0)
            {
                j--;
                if (hits[j + 1].transform.position.z < hits[j].transform.position.z)
                { //if the current z is more negative (closer to the camera) than the preceeding, swap
                    temp = hits[j];
                    hits[j] = hits[j + 1];
                    hits[j + 1] = temp;
                }

                else if (hits[j + 1].transform.position.z == hits[j].transform.position.z)
                { //if the order cannot be determined from the z position, try sorting order
                    if (hits[j + 1].transform.GetComponent<SpriteRenderer>().sortingOrder > hits[j].transform.GetComponent<SpriteRenderer>().sortingOrder)
                    {
                        temp = hits[j];
                        hits[j] = hits[j + 1];
                        hits[j + 1] = temp;
                    }
                }

            }
        }

        /*
        str = "";
        for (int i = 0; i < hits.Length; i++) {
            str += "; " + hits[i].transform.position.z + ", " + hits[i].transform.GetComponent<SpriteRenderer>().sortingOrder;
        }
        Debug.Log("2: " + str, this);
        */

        if (hits.Length > 0) return hits[0].transform.gameObject;
        else return null;
    }

    public void updateSortingOrder(GameObject card) {
        card.GetComponent<SpriteRenderer>().sortingOrder = (cardSortingOrder = (cardSortingOrder + 1) % 32768);
    }

    public void addCard(Vector2 pos, Sprite sprite = null)
    {
        GameObject newCard = (GameObject)Instantiate(Resources.Load("card"), new Vector3(pos.x, pos.y, -3.0f), Quaternion.identity);

        SpriteRenderer spriteRenderer = newCard.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = cardSortingOrder++;
        if (sprite != null) spriteRenderer.sprite = sprite;

        //Do anything you want with the new card, like load its graphics or something
        //Probably want to define the functions in CardActions, but anywhere is fine
        //newCard.GetComponent<DynamicObject>().FlipTable();

    }
}
