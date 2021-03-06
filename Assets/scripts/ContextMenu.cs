﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ContextMenuItem {
    // this class - just a box to some data

    public string text;             // text to display on button
    public Button button;           // sample button prefab
    public Func<List<GameObject>, int> action;    // delegate to method that needs to be executed when button is clicked

    public ContextMenuItem(string text, Button button, Func<List<GameObject>, int> action) {
        this.text = text;
        this.button = button;
        this.action = action;
    }
}

public class ContextMenu : MonoBehaviour {
    public Image contentPanel;              // content panel prefab
    private Image panel;                    // current ContentPanel
    public Canvas canvas;                   // link to main canvas, where will be Context Menu

    private static ContextMenu instance;    // some kind of singleton here

    public static ContextMenu Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<ContextMenu>();
                if (instance == null) {
                    instance = new ContextMenu();
                }
            }
            return instance;
        }
    }

    public void CreateContextMenu(List<ContextMenuItem> items, Vector2 position, List<GameObject> gameObjects, int param) {
        DestroyContentPanel(); // destroy the current panel if it still exists

        // here we are creating and displaying Context Menu
        panel = Instantiate(contentPanel, new Vector3(position.x, position.y, 0), Quaternion.identity) as Image;
        panel.transform.SetParent(canvas.transform);
        panel.transform.SetAsLastSibling();
        panel.rectTransform.anchoredPosition = position;

        foreach (var item in items) {
            ContextMenuItem tempReference = item;
            Button button = Instantiate(item.button) as Button;
            Text buttonText = button.GetComponentInChildren(typeof(Text)) as Text;
            buttonText.text = item.text;
            button.onClick.AddListener(delegate { tempReference.action(gameObjects); });
            button.transform.SetParent(panel.transform);
        }
    }

    public void DestroyContentPanel() {
        if (panel != null) Destroy(panel.gameObject);
    }
}