using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    private Camera myCamera;
    private RaycastHit hit;
    private float liftHeight = -3.0f;
    private int cardSortingOrder = 0;

    void Start()
    {
        myCamera = GetComponent<Camera>(); 
    }

    void Update()
    {
        Vector2 mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButtonDown("Fire1")) //Left Clicks
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Card")
                {
                	hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder = cardSortingOrder++;
                    Debug.Log("Clicked a card", hit.transform.gameObject);
                }
            }
            
        }
        
        if (Input.GetButtonDown("Fire2")) //Right Clicks
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Card")
                {
                    Debug.Log("TableFlipped a card", hit.transform.gameObject);
                    hit.collider.gameObject.GetComponent<CardActions>().FlipTable();
                }
                if (hit.transform.tag == "Background")
                {
                    Debug.Log("Spawned a card", hit.transform.gameObject);
                    GameObject newCard = Instantiate(Resources.Load("card"), new Vector3(mousePos.x, mousePos.y, liftHeight), Quaternion.identity) as GameObject;
                    
                    SpriteRenderer spriteRenderer = newCard.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingOrder = cardSortingOrder++;
                    
                    //Do anything you want with the new card, like load its graphics or something
                    //Probably want to define the functions in CardActions, but anywhere is fine
                    //newCard.GetComponent<CardActions>().FlipTable();
                }
            }

        }
        
        if (Input.GetButton("Fire1"))
        {
            //Debug.Log("Dragging1", hit.transform.gameObject);
            if (hit.collider != null)
            {
                //Debug.Log("Dragging2", hit.transform.gameObject);
                if (hit.transform.tag == "Card")
                {
                    //Debug.Log("Dragging3", hit.transform.gameObject);
                    hit.transform.position = new Vector3(mousePos.x, mousePos.y, liftHeight);
                    hit.transform.rotation = Quaternion.identity;
                }
            }
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            hit = new RaycastHit();
        }
    }
}