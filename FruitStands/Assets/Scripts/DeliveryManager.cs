using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

	private string food_tag;
	private Collider item;

	void OnTriggerEnter (Collider col) {
		food_tag = col.GetComponent<Collider> ().tag;
		item = col;
		Debug.Log (food_tag + " at checkout");

	}

	public string get_Tag(){
		return food_tag;
	}

	public void removeItem (){
		Debug.Log (food_tag + " removed");
		Destroy (item.gameObject);
	}
		
}
