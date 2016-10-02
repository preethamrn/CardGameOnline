using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private GameObject selected;
    private RaycastHit2D hit;
    private Vector2 offset;
    private Vector2 newPos;
    //private Color savedColor;

    private static int cardSortingOrder = 0;

    void Start()
    {

    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        //NOTE: Optimize by using GetRayIntersectionNonAlloc!!!
        //Debug.Log("Clicked a card", selected);
        if (hits.Length > 0) {
            int maxSortingOrder = hits[0].transform.GetComponent<SpriteRenderer>().sortingOrder;
            hit = hits[0];
            for (int i = 1; i < hits.Length; i++) {
                if (hits[i].transform.tag == "Card") {
                    int hitSortingOrder = hits[i].transform.GetComponent<SpriteRenderer>().sortingOrder;
                    if (hitSortingOrder > maxSortingOrder) {
                        maxSortingOrder = hitSortingOrder;
                        hit = hits[i];
                    }
                }
            }
        }
        if (hit.collider != null)
        {
            if (selected == null)
            {
                //Debug.Log("Selected is null!", selected);
                if (hit.transform.tag == "Card")
                {
                    //Debug.Log("Hovering over Card", hit.transform);
                    //hit.transform.GetComponent<Renderer>().material.color = Color.green;
                }
            }
            if (Input.GetButtonDown("Fire1")) //Left Clicks
            {
                if (hit.transform.tag == "Card")
                {
                    selected = hit.transform.gameObject;
                    Debug.Log("Clicked a card", selected);
                    selected.GetComponent<SpriteRenderer>().sortingOrder = (cardSortingOrder = (cardSortingOrder+1)%32768);
                    selected.GetComponent<DynamicObject>().Select();
                    //Debug.Log("Clicked a card", hit.transform.gameObject);
                    offset = mousePos - new Vector2(selected.transform.position.x, selected.transform.position.y);
                }

            }

            if (Input.GetButtonDown("Fire2")) //Right Clicks
            {

                if (hit.transform.tag == "Card")
                {
                    //Debug.Log("TableFlipped a card", hit.transform.gameObject);
                    //hit.collider.gameObject.GetComponent<DynamicObject>().FlipTable();
                }
                if (hit.transform.tag == "Background")
                {
                    Debug.Log("Spawned a card", hit.transform.gameObject);
                    addCard(mousePos);
                }

            }

            if (Input.GetButton("Fire1"))
            {
                //Debug.Log("Dragging1", hit.transform.gameObject);

                //Debug.Log("Dragging2", hit.transform.gameObject);
                if (selected.tag == "Card")
                {
                    Debug.Log("Dragging3", hit.transform.gameObject);
                    newPos = mousePos - offset;
                    selected.transform.position = new Vector3(newPos.x, newPos.y, hit.transform.position.z);
                    //hit.transform.rotation = Quaternion.identity;
                }

            }

            if (Input.GetButtonUp("Fire1"))
            {
                hit = new RaycastHit2D();
                selected.GetComponent<DynamicObject>().Unselect();
                selected = null;
            }
        }
    }

    public static void addCard(Vector2 pos, Sprite sprite = null)
    {
        GameObject newCard = Instantiate(Resources.Load("card"), new Vector2(pos.x, pos.y), Quaternion.identity) as GameObject;

        SpriteRenderer spriteRenderer = newCard.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = cardSortingOrder++;
        if (sprite != null) spriteRenderer.sprite = sprite;

        //Do anything you want with the new card, like load its graphics or something
        //Probably want to define the functions in CardActions, but anywhere is fine
        //newCard.GetComponent<DynamicObject>().FlipTable();
    }

}