using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {

	public GameObject food;
	public string desiredTag;
	private GameObject clone;
    private int counter;

	void Awake() {
		GameObject clone = Instantiate (food, transform.position, transform.rotation);
        counter = 1;
	}

	void OnTriggerEnter (Collider col) {
		Debug.Log (desiredTag + " has been created");
	}

	void OnTriggerStay (Collider col){
		if (col.GetComponent<Collider> ().tag == desiredTag) {
			Debug.Log (col.GetComponent<Collider>().tag + " is in place.");
		}
	}

    void OnTriggerExit(Collider col)
    {
        if (counter <= 20)
        {
            Instantiate(food, transform.position, transform.rotation);
            Debug.Log("respawning " + desiredTag + "; counter = " + counter);
            counter++;
        }
	}

	public void RemoveClones (){
		Debug.Log ("removing clone of " + desiredTag);
		Destroy (gameObject);
	}

}
