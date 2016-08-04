using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private GameObject selected;
    private RaycastHit2D hit;
    private Vector2 offset;
    private Vector2 newPos;
    //private Color savedColor;

    private int cardSortingOrder = 0;

    void Start()
    {

    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        //Debug.Log("Clicked a card", selected);
        if (hits.Length > 0)
            hit = hits[0]; //constant raycasting
        if (hit.collider != null)
        {
            if (selected == null)
            {
                //Debug.Log("Selected is null!", selected);
                if (hit.transform.tag == "Card")
                {
                    //hit.transform.GetComponent<Renderer>().material.color = Color.green;
                }
            }
            if (Input.GetButtonDown("Fire1")) //Left Clicks
            {
                if (hit.transform.tag == "Card")
                {
                    selected = hit.transform.gameObject;
                    Debug.Log("Clicked a card", selected);
                    selected.GetComponent<SpriteRenderer>().sortingOrder = cardSortingOrder++;
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
                    GameObject newCard = (GameObject)Instantiate(Resources.Load("card"), new Vector3(mousePos.x, mousePos.y, -3.0f), Quaternion.identity);
                    //newCard.GetComponent<Transform>().position = new Vector3(hit.transform.position.x, hit.transform.position.y, -3.0f);

                    SpriteRenderer spriteRenderer = newCard.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingOrder = cardSortingOrder++;
                    //Do anything you want with the new card, like load its graphics or something
                    //Probably want to define the functions in CardActions, but anywhere is fine
                    //newCard.GetComponent<DynamicObject>().FlipTable();
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
}