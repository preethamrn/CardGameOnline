using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    private Camera myCamera; //For storing the camera
    private RaycastHit2D puzzlePiece; //For storing the puzzle piece we are dragging

    void Start()
    {
        myCamera = GetComponent<Camera>(); //For getting the camera
    }

    void Update()
    {

        //Store our mouse position at the beginning of the frame for use later
        Vector2 mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);

        //Did we mouse click? "Fire1" is set to use Mouse0 in Edit > Project Settings > Input Manager
        if (Input.GetButtonDown("Fire1"))
        {

            //Shoot a ray at the exact position of our mouse, and store the returned result into puzzlePiece
            puzzlePiece = Physics2D.Raycast(mousePos, Vector2.zero);
        }

        //Are we holding the mouse button down?
        if (Input.GetButton("Fire1"))
        {

            //Is the collider of our puzzlePiece RaycastHit2D variable NOT null?
            if (puzzlePiece.collider != null)
            {

                //Set the position of our puzzlePiece to be equal to our mouse position
                puzzlePiece.collider.transform.position = mousePos;

                //Optional: If using Z-Axis to determine sprite render order, use these lines instead
                //Transform puzzTrans = puzzlePiece.collider.transform;
                //puzzTrans.position = new Vector3(mousePos.x, mousePos.y, puzzTrans.position.z);
            }
        }

        //Did we let go of the mouse button?
        if (Input.GetButtonUp("Fire1"))
        {

            //Reset the puzzlePiece to null
            puzzlePiece = new RaycastHit2D();
        }
    }
}