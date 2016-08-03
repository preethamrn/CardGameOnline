using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{

    float gravity = 9.81f;

    void Start ()
    {
        Physics.gravity = new Vector3(0, 0, gravity);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }
}
