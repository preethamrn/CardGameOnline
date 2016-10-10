using UnityEngine;
using System.Collections;

public class TagsAndValues : MonoBehaviour {

	private struct Tag {
		public int id;
		public string name;
	}

	private struct Value {
		public int id;
		public string name;
		public int value;
	}

	private List<Tag> tags = new List<Tag>();

	public void AddTag(int id, string name) {
		new Tag tag = new Tag();
		tag.id = id;
		tag.name = name;
		tags.Add(tag);
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
