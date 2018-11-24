using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashierScript : MonoBehaviour {

	//Dialogue
	public Canvas canvas;
	public Dialogue dialogue;

	// Use this for initialization
	void Start () {
		
		Text t = canvas.GetComponentInChildren<Text> ();
		Debug.Log (t);

		FindObjectOfType<CashierDialogueManager> ().SetDialogue (t);
	}

	void TriggerDialogue(){
		FindObjectOfType<CashierDialogueManager> ().StartDialogue (dialogue);
	}

	void OrderFulfilled(){
		FindObjectOfType<CashierDialogueManager> ().NextSentence ();
	}

	void OrderUnfulfilled(){
		FindObjectOfType<CashierDialogueManager> ().LastSentence ();
	}

	// Update is called once per frame
	void Update(){
		if (GameObject.FindGameObjectsWithTag ("customer").Length == 0) {
			canvas.enabled = false;
		}
	}

	public void CashierReply () {
		TriggerDialogue ();
		canvas.enabled = true;
		if (GameObject.FindGameObjectsWithTag ("customer").Length != 0) {

			Debug.Log ("Cashier is talking");
			if (FindObjectOfType<OtherCustomerController> ().OrderStatus () == true) {
				Debug.Log ("Order Fulfilled");
				OrderFulfilled ();
			} 
			else {
				Debug.Log ("Order Unfulfilled");
				OrderUnfulfilled ();
			}
		}
	}
}
