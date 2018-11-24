using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScreen : MonoBehaviour {

	private GameObject menu;
	private bool isShowing;

	void Start(){
		isShowing = false;
	}

	void Update() {
		if (Input.GetKeyDown ("escape") || OVRInput.GetDown(OVRInput.Button.Three)) {
			if (isShowing == false) {
				menu.SetActive (true);
				isShowing = true;
				Debug.Log ("Showing order screen");
			} 
			else {
				menu.SetActive (false);
				isShowing = false;
				Debug.Log ("Hiding order screen");
			}
		}
	}

	public void SetOrderScreen(GameObject screen){
		menu = screen;
	}
}