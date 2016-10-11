using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour
{
    private List<GameObject> selected = new List<GameObject>();
    private GameObject highlighted;
    private Vector2 mouseStart;
    private TableScript table;


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
            mouseStart = mousePos;
            if (topCard != null)
            {
                if (topCard.tag == "Card")
                {
                    if(!selected.Contains(topCard)) selected.Add(topCard);
                    table.updateSortingOrder(topCard);
                }

                if (topCard.tag == "Background")
                {
                    table.addCard(new Vector2(mousePos.x, mousePos.y));
                }
            }
        }

        if (Input.GetButton("Fire1"))
        {
            Vector2 posChange;
            posChange = mousePos - mouseStart;

            foreach (GameObject sel in selected)
            {
                if (sel.tag == "Card")
                {
                    sel.transform.position = new Vector3(sel.transform.position.x + posChange.x, sel.transform.position.y + posChange.y, sel.transform.position.z);
                }
            }
            mouseStart = mousePos;

        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (!Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.RightAlt))
            {
                selected.Clear();
            }
        }

    }

    

}