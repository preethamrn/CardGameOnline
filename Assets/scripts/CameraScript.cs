using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour
{
    private List<GameObject> selected = new List<GameObject>();
    private GameObject highlighted;
    private Vector2 offset;
    private RaycastHit2D[] hits = new RaycastHit2D[100];
    //private Color savedColor;

    private static int cardSortingOrder = 0;

    void Start()
    {

    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

        if (Input.GetButtonDown("Fire1"))
        {

            if (hits.Length > 0)
            {
                if (hits[0].transform.tag == "Card")
                {
                    selected.Add(hits[0].transform.gameObject);
                    //Debug.Log("Clicked a card", selected);
                    selected[0].GetComponent<SpriteRenderer>().sortingOrder = (cardSortingOrder = (cardSortingOrder + 1) % 32768);
                    //selected[0].GetComponent<DynamicObject>().Select();
                    //Debug.Log("Clicked a card", hit.transform.gameObject);
                    offset = mousePos - new Vector2(selected[0].transform.position.x, selected[0].transform.position.y);
                }

                if (hits[0].transform.tag == "Background")
                {
                    //Debug.Log("Spawned a card", hit.transform.gameObject);
                    GameObject newCard = (GameObject)Instantiate(Resources.Load("card"), new Vector3(mousePos.x, mousePos.y, -3.0f), Quaternion.identity);
                    //newCard.GetComponent<Transform>().position = new Vector3(hit.transform.position.x, hit.transform.position.y, -3.0f);
                    SpriteRenderer spriteRenderer = newCard.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingOrder = (cardSortingOrder = (cardSortingOrder + 1) % 32768);
                }
            }
        }

        if (Input.GetButton("Fire1"))
        {
            //Debug.Log("Dragging1", hit.transform.gameObject);
            //Debug.Log("Dragging2", hit.transform.gameObject);
            if (selected.Count > 0)
            {
                if (selected[0].tag == "Card")
                {
                    Vector2 newPos;
                    //Debug.Log("Dragging3", hit.transform.gameObject);
                    newPos = mousePos - offset;
                    selected[0].transform.position = new Vector3(newPos.x, newPos.y, hits[0].transform.position.z);
                    //hit.transform.rotation = Quaternion.identity;
                }
            }

        }

        if (Input.GetButtonUp("Fire1"))
        {
            //hits[0] = new RaycastHit2D();
            //selected[0].GetComponent<DynamicObject>().Unselect();
            selected.Clear();
        }

    }

    public static void addCard(Vector2 pos, Sprite sprite = null)
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