using UnityEngine;

public class Highlighting : MonoBehaviour {
    public void Select() {
        GetComponent<Renderer>().material.color = Color.green;
    }

    public void Unselect() {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void Hover() {
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}
