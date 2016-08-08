using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour
{
    private List<GameObject> selected;
    private GameObject highlighted;
    private Vector2 offset;
    private RaycastHit2D[] hits = new RaycastHit2D[100];
    //private Color savedColor;

    private int cardSortingOrder = 0;

    void Start()
    {

    }

    void Update()
    {
        
        
    }
}