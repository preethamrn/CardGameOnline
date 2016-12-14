using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TagsManager : MonoBehaviour {

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
		private Func<List<GameObject>, int> func;
		public Operation(string l) {
			label = l;
			func = (List<GameObject> go) => { return 0; };
		}
		public string Label() { return label; }
		public Func<List<GameObject>, int> Func() { return func; }	//Using a lambda function like this: action.Func().Invoke();
		public void setFunc(Func<List<GameObject>, int> f) { func = f; }
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
		return -1; //TODO: possibly create a new ID here and return it?
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

	public void AddOperationToTag(Func<List<GameObject>, int> f, string lable, int id) {
		Operation a = new Operation(lable);
		a.setFunc(f);
		tagsList[id].AddOperation(a);
	}

	public void AddOperationToTag(Func<List<GameObject>, int> f, string lable, string tag) {
		AddOperationToTag(f, lable, GetIdOfTag(tag));
	}


    //TODO: add default tags like "card" and "deck" with the default operations
	void Start () {
        //tag: CARD operations
		NewTagTemplate("card");

		Func<List<GameObject>, int> deleteFunction = (List<GameObject> o) => {
            foreach (GameObject obj in o) obj.GetComponent<OperationsComponent>().DeleteThis();
            FindObjectOfType<CameraScript>().DestroyContentPanel(); ///DEBUGGING: This should be added to every action??
            return 0;
        };
		AddOperationToTag(deleteFunction, "Delete card(s)", "card"); //DEBUGGING: temporary testing. Have the operation and tags stored in the properties.cgo file

        //tag: DECK operations
        NewTagTemplate("deck");

        Func<List<GameObject>, int> drawFunction = (List<GameObject> o) => {
            if (o.Count != 1) return 1;
            o[0].GetComponent<Properties>().drawCard();
            return 0;
        };
        Func<List<GameObject>, int> deckSize = (List<GameObject> o) => {
            if (o.Count != 1) return 1;
            Debug.Log(o[0].GetComponent<Properties>().deckSize());
            return 0;
        };
        AddOperationToTag(drawFunction, "Draw card", "deck");
        AddOperationToTag(deckSize, "Print deck size", "deck");
        AddOperationToTag(deleteFunction, "Delete deck(s)", "deck");

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
