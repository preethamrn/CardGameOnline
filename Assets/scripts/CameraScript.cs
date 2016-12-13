using UnityEngine;
using System.Collections.Generic;
using System;

public class CameraScript : MonoBehaviour {
    private List<GameObject> selected = new List<GameObject>();
    private HashSet<TagsManager.Operation> operations = new HashSet<TagsManager.Operation>();

    private GameObject prevCard = null;

    private Vector2 mouseStart;
    private TableScript table;
    private TagsManager tagManager;


    void Start() {
        table = FindObjectOfType<TableScript>();
        tagManager = FindObjectOfType<TagsManager>();
    }

    void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject topCard = table.getTopCard(mousePos);

        if (prevCard != topCard) {
            if (!selected.Contains(prevCard) && prevCard != null && prevCard.tag == "Card") prevCard.GetComponent<Highlighting>().Unselect();
            if (!selected.Contains(topCard) && topCard != null && topCard.tag == "Card") topCard.GetComponent<Highlighting>().Hover();
        }
        
        if (Input.GetButtonDown("Fire1")) {
            mouseStart = mousePos;
            if (topCard != null) {
                if (topCard.tag == "Card") {
                    if (!selected.Contains(topCard)) {
                        List<int> tags = topCard.GetComponent<Tags>().GetTags();
                        foreach (int tag in tags) {
                            if (selected.Count == 0) operations.UnionWith(tagManager.GetOperations(tag)); //if selected is empty before adding first card
                            else operations.IntersectWith(tagManager.GetOperations(tag));
                        }
                        selected.Add(topCard);
                        topCard.GetComponent<Highlighting>().Select();
                        table.updateSortingOrder(topCard);
                    }
                }
                
                if (topCard.tag == "Background") {
                    List<string> tags = new List<string>(); tags.Add("card");
                    table.addCard(new Vector2(mousePos.x, mousePos.y), tags);
                }
            }
        }

        if (Input.GetButtonDown("Fire2")) {
            //TODO: make the menu close after a click
            //TODO: destroy the current ContentPanel as soon as something else is clicked (another card/right click/background)
            List<GameObject> selectedRef = new List<GameObject>(selected);
            HashSet<TagsManager.Operation> operationsRef = new HashSet<TagsManager.Operation>(operations);
            GetComponent<ItemController>().CreateMenu(operationsRef, selectedRef, 0); //DEBUGGING: change the int param based on user input
            Debug.Log("Right Click. Create operations menu here linked to actions.");
        }

        if (Input.GetButton("Fire1")) {
            Vector2 posChange;
            posChange = mousePos - mouseStart;

            foreach (GameObject sel in selected) {
                if (sel.tag == "Card") {
                    sel.transform.position = new Vector3(sel.transform.position.x + posChange.x, sel.transform.position.y + posChange.y, sel.transform.position.z);
                }
            }
            mouseStart = mousePos;
        }

        if (Input.GetButtonUp("Fire1")) {
            if (!Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.RightAlt)) {
                foreach (GameObject card in selected) {
                    card.GetComponent<Highlighting>().Unselect();
                }
                selected.Clear();
                operations.Clear();
            }
        }

        prevCard = topCard; //renew the value of prevCard for future frames
    }
}