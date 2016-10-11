using UnityEngine;
using System;
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
		private int id;
		private string name;
		private List<Action> actions;
		public TagTemplate(int i, string n) {
			id = i;
			name = n;
			actions = new List<Action>();
		}
		public int Id() { return id; }
		public string Name() { return name; }
		public List<Action> Actions() { return actions; }
		public void AddAction(Action a) { actions.Add(a); }
	}

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

	public struct Action {
		private string label;
		private Func<TagsValues, int> func;
		public Action(string l) {
			label = l;
			func = (TagsValues tv) => { return 0; };
		}
		public string Label() { return label; }
		public Func<TagsValues, int> Func() { return func; }	//Using a lambda function like this: action.Func().Invoke();
		public void setFunc(Func<TagsValues, int> f) { func = f; }
	}

	private List<TagTemplate> tagsList = new List<TagTemplate>();
	private List<ValueTemplate> valuesList = new List<ValueTemplate>();

	public void NewTagTemplate(string tag) {
		TagTemplate newTag = new TagTemplate(tagsList.Count, tag);
		tagsList.Add(newTag);
	}

	public void NewValueTemplate(string value) {
		ValueTemplate newValue = new ValueTemplate(valuesList.Count, value);
		valuesList.Add(newValue);
	}

	public int GetIdOfTag(string tag) {
		foreach (TagTemplate tt in tagsList) {
			if (tt.Name() == tag) {
				return tt.Id();
			}
		}
		return -1;
	}

	public void AddTagToCard(int id, TagsValues tv) {
		tv.AddTag(id);
	}

	public void AddTagToCard(string tag, TagsValues tv) {
		AddTagToCard(GetIdOfTag(tag), tv);
	}

	public List<Action> GetActions(int id) {
		return tagsList[id].Actions();
	}

	public List<Action> GetActions(string tag) {
		return GetActions(GetIdOfTag(tag));
	}

	public void AddActionToTag(Func<TagsValues, int> f, string lable, int id) {
		Action a = new Action(lable);
		a.setFunc(f);
		tagsList[id].AddAction(a);
	}

	public void AddActionToTag(Func<TagsValues, int> f, string lable, string tag) {
		AddActionToTag(f, lable, GetIdOfTag(tag));
	}

	void Start () {
		//remove code from here eventually
		//example setup for a new tag
		NewTagTemplate("card");
		//Build functions to make these functions easier
		Func<TagsValues, int> deleteFunction = (TagsValues tv) => {
			tv.gameObject.GetComponent<ActionsComponent>().DeleteThis();
			return 1; };
		AddActionToTag(deleteFunction, "Delete this card", "card");
	}
}
