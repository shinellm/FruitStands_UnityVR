using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class OtherCustomerController : MonoBehaviour {

	public GameObject food_item;
	public Transform store_counter;
	public Transform exit;
	public Transform cashier;
	private NavMeshAgent agent;

	//Dialogue
	public Canvas canvas;
	public Dialogue dialogue;

	//For the character animations
	private Animator anim;
	private bool collected_items;
	private bool correct_orders;
	private bool at_cashier;
	private bool run_timer;
	private bool talked;
	private bool customer_left;
	private float speed;
	private float targetTime;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();

		canvas.enabled = false;
		collected_items = false;
		correct_orders = true;
		at_cashier = false;
		run_timer = true;
		talked = false;
		customer_left = false;
		speed = agent.speed;
		targetTime = 2f;
	}

	void TriggerDialogue(){
		FindObjectOfType<DialogueManager> ().StartDialogue (dialogue);
	}

	void OrderFulfilled(){
		FindObjectOfType<DialogueManager> ().NextSentence ();
	}

	void OrderUnfulfilled(){
		FindObjectOfType<DialogueManager> ().LastSentence ();
	}

	public bool OrderStatus (){
		return correct_orders;
	}

	public void SetFoodItems(GameObject food){
		food_item = food;
		Debug.Log("Food desired: " + food.GetComponent<GroceryItemManager> ().desiredItem ());
	}

	// Update is called once per frame
	void Update () {
		float counter_distance = Vector3.Distance (store_counter.position, transform.position);
		float exit_distance = Vector3.Distance (exit.position, transform.position);

		//Collect your food items
		if (collected_items == false) {
			float item_distance = Vector3.Distance (food_item.transform.position, transform.position);

			if (item_distance >= agent.stoppingDistance + 1.25) {
				anim.SetFloat ("MoveSpeed", speed);
                Debug.Log("Walking over to item");
				Debug.Log("Item Distance: " + item_distance);
				Debug.Log("Agent Distance: " + agent.stoppingDistance);
				FaceTarget (food_item.transform);
				agent.SetDestination (food_item.transform.position);
			} 
			else {
				if (food_item.GetComponent<GroceryItemManager> ().ItemInStock () == true) {
					FaceTarget (food_item.transform);
					anim.Play ("Pickup");
					Debug.Log ("Walked up and collected item");
					food_item.GetComponent<GroceryItemManager> ().DestroyItem();
					collected_items = true;
				} 
				else {
					FaceTarget (food_item.transform);
					Debug.Log ("Walked up but item wasn't there");
					correct_orders = false;
					collected_items = true;

				}
			}
		}

		//Customer walks to the counter
		if (at_cashier == false && collected_items == true) {
			if (counter_distance >= agent.stoppingDistance && collected_items == true) {
				anim.SetFloat ("MoveSpeed", speed);
				agent.SetDestination (store_counter.position);
			}
			else {
				at_cashier = true;
			}
		}

		//At counter talking
		if (run_timer == true && collected_items == true && at_cashier == true) {
			anim.SetFloat("MoveSpeed", 0);
			Debug.Log (targetTime);
			//Face the cashier
			FaceTarget (cashier);
			canvas.enabled = true;
			targetTime -= Time.deltaTime;

			if (targetTime <= 0.0f) {
				FindObjectOfType<CashierScript> ().CashierReply ();
				run_timer = false;
			}

			if (talked == false) {
				Text t = canvas.GetComponentInChildren<Text> ();
				Debug.Log (t);

				FindObjectOfType<DialogueManager> ().SetDialogue (t);
				TriggerDialogue ();

				if (correct_orders == false) {
					Debug.Log ("Order Unfulfilled");
					OrderUnfulfilled ();
					talked = true;
				}
				if (correct_orders == true){
					Debug.Log ("Order Fullfilled");
					OrderFulfilled ();
					talked = true;
				}
			}
		}

		//Leave the store
		if (run_timer == false && customer_left == false) {
			Debug.Log ("leaving..");
            canvas.enabled = false;
            if (exit_distance >= agent.stoppingDistance) {
				Debug.Log ("exit distance" + exit_distance);
				Debug.Log ("stopping distance" + agent.stoppingDistance);
				anim.SetFloat ("MoveSpeed", speed);
				agent.SetDestination (exit.position);

			} 
			else {
				customer_left = true;
				anim.SetFloat ("MoveSpeed", 0);
			}
		}

		//Remove customer
		if (customer_left == true) {
			Destroy (gameObject);
		}
	}

	void FaceTarget(Transform target){
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}
