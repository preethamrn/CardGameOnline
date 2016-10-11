using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TagsManager : MonoBehaviour {
	/*
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
	
	}
	*/

	private struct TagTemplate {
		private int id;
		private string name;
		private List<Operation> operations;
		public TagTemplate(int i, string n) {
			id = i;
			name = n;
			operations = new List<Operation>();
		}
		public int Id() { return id; }
		public string Name() { return name; }
		public List<Operation> Operations() { return operations; }
		public void AddOperation(Operation a) { operations.Add(a); }
	}

	public struct Operation {
		private string label;
		private Func<GameObject, int> func;
		public Operation(string l) {
			label = l;
			func = (GameObject go) => { return 0; };
		}
		public string Label() { return label; }
		public Func<GameObject, int> Func() { return func; }	//Using a lambda function like this: action.Func().Invoke();
		public void setFunc(Func<GameObject, int> f) { func = f; }
	}

	private List<TagTemplate> tagsList = new List<TagTemplate>();

	public void NewTagTemplate(string tag) {
		TagTemplate newTag = new TagTemplate(tagsList.Count, tag);
		tagsList.Add(newTag);
	}

	public int GetIdOfTag(string tag) {
		foreach (TagTemplate tt in tagsList) {
			if (tt.Name() == tag) {
				return tt.Id();
			}
		}
		return -1;
	}

	public void AddTagToCard(int id, GameObject o) {
		o.GetComponent<Tags>().AddTag(id);
	}

	public void AddTagToCard(string tag, GameObject o) {
		AddTagToCard(GetIdOfTag(tag), o);
	}

	public List<Operation> GetOperations(int id) {
		return tagsList[id].Operations();
	}

	public List<Operation> GetOperations(string tag) {
		return GetOperations(GetIdOfTag(tag));
	}

	public void AddOperationToTag(Func<GameObject, int> f, string lable, int id) {
		Operation a = new Operation(lable);
		a.setFunc(f);
		tagsList[id].AddOperation(a);
	}

	public void AddOperationToTag(Func<GameObject, int> f, string lable, string tag) {
		AddOperationToTag(f, lable, GetIdOfTag(tag));
	}

	void Start () {
		//remove code from here eventually
		//example setup for a new tag
		NewTagTemplate("card");
		//Build functions to make these functions easier
		Func<GameObject, int> deleteFunction = (GameObject o) => {
			o.GetComponent<OperationsComponent>().DeleteThis();
			return 1; };
		AddOperationToTag(deleteFunction, "Delete this card", "card");
	}


	/* Move to seperate class
	private List<ValueTemplate> valuesList = new List<ValueTemplate>();

	private struct ValueTemplate {
		private int id;
		private string name;
		public ValueTemplate(int i, string n) {
			id = i;
			name = n;
		}
		public int Id() { return id; }
		public string Name() { return name; }
	}

	public void NewValueTemplate(string value) {
		ValueTemplate newValue = new ValueTemplate(valuesList.Count, value);
		valuesList.Add(newValue);
	}

	*/
}
