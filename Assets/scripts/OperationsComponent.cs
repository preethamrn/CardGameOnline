using UnityEngine;
using System.Collections;

public class OperationsComponent : MonoBehaviour {

	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
	public void DeleteThis() {
		Destroy(gameObject);
	}
}
