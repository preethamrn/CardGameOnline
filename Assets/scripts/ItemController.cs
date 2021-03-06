﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {
    public Button sampleButton;                         // sample button prefab
    private List<ContextMenuItem> contextMenuItems;     // list of items in menu

    public void CreateMenu(HashSet<TagsManager.Operation> operations, List<GameObject> gameObjects, int param) {
        contextMenuItems = new List<ContextMenuItem>();
        foreach (TagsManager.Operation operation in operations) {
            Func<List<GameObject>, int> func = operation.Func();
            contextMenuItems.Add(new ContextMenuItem(operation.Label(), sampleButton, func));
        }
        contextMenuItems.Add(new ContextMenuItem("Close", sampleButton, (List<GameObject> o) => {
            FindObjectOfType<CameraScript>().DestroyContentPanel();
            return 1;
        }));
        //Vector3 pos = Camera.main.WorldToScreenPoint(transform.position); // get screen point of current transform where you've rightclicked
        ContextMenu.Instance.CreateContextMenu(contextMenuItems, new Vector2(0,0), gameObjects, param); //pass the gameobjects and any parameters you want to the context menu for actions
    }

    public void DestroyMenu() {
        ContextMenu.Instance.DestroyContentPanel();
    }
}