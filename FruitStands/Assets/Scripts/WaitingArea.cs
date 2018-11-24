using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingArea : MonoBehaviour {

	private bool waiting;

	void Start(){
		waiting = false;
	}
	//Dialogue dialogue = FindObjectOfType<CustomerController>().dialogue;

	void OnTriggerEnter (Collider other) {
		if (other.GetComponent<Collider> ().tag == "customer") {
			Debug.Log ("Customer Entered");
			waiting = true;
			//FindObjectOfType<DialogueManager> ().StartDialogue (dialogue);
		}
	}

	void OnTriggerStay (Collider other) {
		if (other.GetComponent<Collider> ().tag == "customer") {
			//Debug.Log ("Customer Waiting");
			//FindObjectOfType<DialogueManager> ().NextSentence ();
			waiting = true;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.GetComponent<Collider> ().tag == "customer") {
			Debug.Log ("Customer Left");
			waiting = false;
		}
	}

	public bool CustomerAtRegister () {
		return waiting;
	}
}
