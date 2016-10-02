using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour
{
    private List<GameObject> selected = new List<GameObject>();
    private GameObject highlighted;
    private Vector2 offset;
    private TableScript table;

    //private Color savedColor;


    void Start()
    {
        table = FindObjectOfType<TableScript>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject topCard = table.getTopCard(mousePos);

        if (Input.GetButtonDown("Fire1"))
        {

            if (topCard != null)
            {
                if (topCard.tag == "Card")
                {
                    selected.Add(topCard);
                    //Debug.Log("Clicked a card", selected);
                    table.updateSortingOrder(selected[0]);
                    //selected[0].GetComponent<DynamicObject>().Select();
                    //Debug.Log("Clicked a card", hit.transform.gameObject);
                    offset = mousePos - new Vector2(selected[0].transform.position.x, selected[0].transform.position.y);
                }

                if (topCard.tag == "Background")
                {
                    table.addCard(new Vector2(mousePos.x, mousePos.y));
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
                    selected[0].transform.position = new Vector3(newPos.x, newPos.y, topCard.transform.position.z);
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

    

}