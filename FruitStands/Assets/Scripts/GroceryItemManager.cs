using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryItemManager : MonoBehaviour {

	public string desiredTag;
	private bool instock;
	private Collider item;

	void Start(){
		instock = false;
	}

	void OnTriggerEnter (Collider col) {
		if (col.GetComponent<Collider> ().tag == desiredTag) {
			Debug.Log (desiredTag + " has been restocked");
			instock = true;
			item = col;
		}
	}

	void OnTriggerStay (Collider col){
		if (col.GetComponent<Collider> ().tag == desiredTag) {
			Debug.Log (col.GetComponent<Collider>().tag + " is in place.");
			instock = true;
			item = col;
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.GetComponent<Collider> ().tag == desiredTag) {
			Debug.Log (desiredTag + " has been removed from shelf");
			instock = false;
		}
	}

	public bool ItemInStock (){
		Debug.Log (desiredTag + " in stock :" + instock);
		return instock;
	}

	public string desiredItem(){
		return desiredTag;
	}

	public void DestroyItem(){
		Debug.Log (desiredTag + " has been removed");
		Destroy(item.gameObject);
	}

}