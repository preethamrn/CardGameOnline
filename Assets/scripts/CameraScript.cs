using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private RaycastHit2D hit;

    private int cardSortingOrder = 0;

    void Start()
    {

    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(mousePos, Vector2.zero); //constant raycasting
        if (hit.collider != null)
        {
            if (Input.GetButtonDown("Fire1")) //Left Clicks
            {
                if (hit.transform.tag == "Card")
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder = cardSortingOrder++;
                    Debug.Log("Clicked a card", hit.transform.gameObject);
                }

            }

            if (Input.GetButtonDown("Fire2")) //Right Clicks
            {

                if (hit.transform.tag == "Card")
                {
                    Debug.Log("TableFlipped a card", hit.transform.gameObject);
                    hit.collider.gameObject.GetComponent<DynamicObject>().FlipTable();
                }
                if (hit.transform.tag == "Background")
                {
                    Debug.Log("Spawned a card", hit.transform.gameObject);
                    GameObject newCard = Instantiate(Resources.Load("card"), new Vector3(mousePos.x, mousePos.y, hit.transform.position.z), Quaternion.identity) as GameObject;

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

                Debug.Log("Dragging2", hit.transform.gameObject);
                if (hit.transform.tag == "Card")
                {
                    Debug.Log("Dragging3", hit.transform.gameObject);
                    hit.transform.position = mousePos;
                    //hit.transform.rotation = Quaternion.identity;
                }

            }

            if (Input.GetButtonUp("Fire1"))
            {
                hit = new RaycastHit2D();
            }
        }
    }
}