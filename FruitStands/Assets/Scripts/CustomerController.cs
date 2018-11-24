using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour {

	public string food_ordered;
	public string food_position;
	public Transform exit;
	public Transform player;
	public Transform store_counter;
	private NavMeshAgent agent;

	//Dialogue
	public Canvas canvas;
	public Dialogue dialogue;

	//For the character animations
	private Animator anim;
	private bool has_ordered;
	private bool correct_order;
	private bool run_timer;
	private bool customer_left;
	private float speed;
	private float targetTime;

	// Use this for initialization
	void Start () {

		//canvas = GetComponent<Canvas> ();
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();

		canvas.enabled = false;
		has_ordered = false;
		correct_order = true;
		run_timer = true;
		customer_left = false;
		speed = agent.speed;
		targetTime = 20f;
	}

	public void SetFoodPosition (string position)
	{
		food_position = position;
	}

	public void SetFoodOrdered (string order)
	{
		food_ordered = order;
	}

	void TriggerDialogue(){
		dialogue.sentences [0] = "Can I please have " + food_position + "?";

		Debug.Log ("Dialogue Edited");
		FindObjectOfType<DialogueManager> ().StartDialogue (dialogue);
	}

	void OrderFulfilled(){
		correct_order = true;
		FindObjectOfType<DialogueManager> ().NextSentence ();
	}

	void OrderUnfulfilled(){
		correct_order = false;
		FindObjectOfType<DialogueManager> ().LastSentence ();
	}
		
	public bool CorrectOrderReceived (){
		return correct_order;
	}

	// Update is called once per frame
	void Update () {

		float entry_distance = Vector3.Distance (store_counter.position, transform.position);
		float exit_distance = Vector3.Distance (exit.position, transform.position);

		//Customer walks to the counter
		if (entry_distance >= agent.stoppingDistance && has_ordered == false) {
			anim.SetFloat ("MoveSpeed", speed);
			agent.SetDestination (store_counter.position);
		}

		else {
			anim.SetFloat ("MoveSpeed", 0);

			//Make an order
			if (has_ordered == false) {
				Order ();
				has_ordered = true;
				Debug.Log ("Making An Order");
			}

			if (run_timer == true) {
				Debug.Log (targetTime);
				//Face the cashier
				FaceTarget ();
				targetTime -= Time.deltaTime;

                string food_tag = FindObjectOfType<DeliveryManager> ().get_Tag();
				Debug.Log ("food tag :" + food_tag);
                Debug.Log("food tag == food ordered: " + (food_tag == food_ordered));

                if (targetTime <= 0.0f) {
					run_timer = false;
				}
				if (food_tag != food_ordered && run_timer == false) {
					Debug.Log ("Order Unfulfilled");
					correct_order = false;
					Debug.Log ("correct order: " + correct_order);
                    Debug.Log("food ordered: " + food_ordered);
                    OrderUnfulfilled ();
				}
				if (food_tag == food_ordered){
					Debug.Log ("Order Fullfilled");
					FindObjectOfType<DeliveryManager> ().removeItem();
					anim.Play("Pickup");
					//anim.Play("Wave");
					correct_order = true;
					Debug.Log ("correct order: " + correct_order);
                    Debug.Log("food ordered: " + food_ordered);
                    OrderFulfilled ();
					run_timer = false;
				}
			}
				
			//Leave the store
			if (run_timer == false && customer_left == false) {
				Debug.Log ("leaving..");
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
	}

	void FaceTarget(){
		Vector3 direction = (player.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void Order() {
		canvas.enabled = true;

		Text t = canvas.GetComponentInChildren<Text> ();
		Debug.Log (t);

		//anim.SetFloat ("MoveSpeed", 0);
		FindObjectOfType<DialogueManager> ().SetDialogue (t);
		TriggerDialogue ();
	}
}
