using UnityEngine;
using System.Collections;

public class DynamicObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //gameObject.tag = "Draggable";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FlipTable()
    {
        Debug.Log("Flipping!", this);
        Random.InitState(Mathf.RoundToInt(this.transform.position.x * 100) + Mathf.RoundToInt(this.transform.position.y * 100) + Mathf.RoundToInt(this.transform.position.z * 100));
        Vector3 ranPos = transform.position + new Vector3(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
        GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 0, -1 * Random.Range(3, 10)), ranPos);
    }

    public void addTag()
    {
        GameObject child = new GameObject();
        child.tag = "Draggable";
        child.transform.parent = this.gameObject.transform;
    }
}
