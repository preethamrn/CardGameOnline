using UnityEngine;
using System.Collections;

public class DynamicObject : MonoBehaviour {

	void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        //scaling
    }

    public void FlipTable()
    {
        Debug.Log("Flipping!", this);
        Random.InitState(Mathf.RoundToInt(this.transform.position.x * 100) + Mathf.RoundToInt(this.transform.position.y * 100) + Mathf.RoundToInt(this.transform.position.z * 100));
        GetComponent<Rigidbody>().AddExplosionForce(500.0f, transform.position - new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -1.0f), 3.0f, 0.0f, ForceMode.Acceleration);
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void Select()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void Unselect()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
    
}
