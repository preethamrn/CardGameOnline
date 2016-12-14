using UnityEngine;
using System.Collections.Generic;
using System;

public class CameraScript : MonoBehaviour {
    private List<GameObject> selected = new List<GameObject>();
    private HashSet<TagsManager.Operation> operations = new HashSet<TagsManager.Operation>();
    private bool contentPanelOpen = false;

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
            if (contentPanelOpen) return;

            mouseStart = mousePos;
            if (topCard != null) {
                if (topCard.tag == "Card") {
                    if (!selected.Contains(topCard)) {
                        List<int> tags = topCard.GetComponent<Tags>().GetTags();
                        HashSet<TagsManager.Operation> topCardOps = new HashSet<TagsManager.Operation>();
                        foreach (int tag in tags) {
                            topCardOps.UnionWith(tagManager.GetOperations(tag));
                        }
                        if (selected.Count == 0) operations.UnionWith(topCardOps); //if selected is empty before adding first card
                        else operations.IntersectWith(topCardOps);

                        selected.Add(topCard);
                        topCard.GetComponent<Highlighting>().Select();
                        table.updateSortingOrder(topCard);
                    }
                }
                else if (topCard.tag == "Background") {
                    List<string> tags = new List<string>() { "card" };
                    KeyValuePair<string, Newtonsoft.Json.Linq.JToken> piece = new KeyValuePair<string, Newtonsoft.Json.Linq.JToken>();
                    table.addCard(new Vector2(mousePos.x, mousePos.y), tags, piece);
                }
            }
        }

        else if (Input.GetButtonDown("Fire2")) {
            if (contentPanelOpen) return;

            //TODO: single card selected with only right click
            if (selected.Count != 0) {
                List<GameObject> selectedRef = new List<GameObject>(selected);
                HashSet<TagsManager.Operation> operationsRef = new HashSet<TagsManager.Operation>(operations);
                GetComponent<ItemController>().CreateMenu(operationsRef, selectedRef, 0); //DEBUGGING: change the int param based on user input
                contentPanelOpen = true;
            }
        }

        else if (Input.GetButton("Fire1")) {
            if (contentPanelOpen) return;
            Vector2 posChange;
            posChange = mousePos - mouseStart;

            foreach (GameObject sel in selected) {
                if (sel.tag == "Card") {
                    sel.transform.position = new Vector3(sel.transform.position.x + posChange.x, sel.transform.position.y + posChange.y, sel.transform.position.z);
                }
            }
            mouseStart = mousePos;
        }

        else if (Input.GetButtonUp("Fire1")) {
            if (contentPanelOpen) return;
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

    public void DestroyContentPanel() {
        GetComponent<ItemController>().DestroyMenu();
        contentPanelOpen = false;
    }
}