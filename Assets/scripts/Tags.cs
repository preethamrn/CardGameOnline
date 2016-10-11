using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tags : MonoBehaviour {

	/*
	private struct Value {
		public int id;
		public int value;
	}
	*/
	
	private List<int> tags = new List<int>();

	public void AddTag(int id) {
		tags.Add(id);
	}

    public List<int> GetTags() {
        return tags;
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
