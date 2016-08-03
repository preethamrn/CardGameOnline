﻿using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    private Camera myCamera;
    private RaycastHit hit;
    private float liftHeight = -3.0f;

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
                    Debug.Log("Clicked a card", hit.transform.gameObject);
                }
            }
            
        }
        /*
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
            }

        }
        */
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Dragging1", hit.transform.gameObject);
            if (hit.collider != null)
            {
                Debug.Log("Dragging2", hit.transform.gameObject);
                if (hit.transform.tag == "Card")
                {
                    Debug.Log("Dragging3", hit.transform.gameObject);
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