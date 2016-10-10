using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TagsValuesManager : MonoBehaviour {
	/*
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
	
	}
	*/

	private struct TagTemplate {
		public int id;
		public string name;
		//public list of actions
	}

	private struct ValueTemplate {
		public int id;
		public string name;
	}

	private List<TagTemplate> tagsList = new List<TagTemplate>();
	private List<ValueTemplate> valuesList = new List<ValueTemplate>();

	public void NewTagTemplate(string tag) {
		TagTemplate newTag = new TagTemplate();
		newTag.id = tagsList.Count;
		newTag.name = tag;
		tagsList.Add(newTag);
	}

	public void NewValueTemplate(string value) {
		ValueTemplate newValue = new ValueTemplate();
		newValue.id = valuesList.Count;
		newValue.name = value;
		valuesList.Add(newValue);
	}



	void Start () {
		NewTagTemplate("card"); //Default tags that exist
	}
	

}
