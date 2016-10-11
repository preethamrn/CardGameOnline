using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TagsAndValues : MonoBehaviour {

	private struct Value {
		public int id;
		public int value;
	}

	private List<int> tags = new List<int>();

	public void AddTag(int id) {
		tags.Add(id);
	}
	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
}
