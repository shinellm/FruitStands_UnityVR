using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour {

	//Buttons
	public Button btn1;
	public Button btn2;
	public Button btn3;
	public Button btn4;
	public GameObject manager_order;

	//Desired food items
	public int amount_of_food;
	public string desired_food;

	//Needed by Manager
	private GameObject[] shelves;
	public GameObject food;
	public GameObject food_location;
	public Transform player;
	public Transform store_counter;
	private NavMeshAgent agent;
	public GameObject loadingScreen;
	public Slider slider;
	public Text progressText;

	//Dialogue
	public Canvas canvas;
	public Dialogue dialogue;

	//For the character animations
	//private Animator anim;
	//private float speed;
	private bool intro_complete;
	private bool started_talking;
	private bool correct_shelf;
	private bool correct_amount;
	private bool stock_complete;
	private bool ordered;
	private bool stock_correct;
	private bool serve_customer;
	private bool player_ready;
	private string food_tag;

	// Use this for initialization
	void Start () {

		food.SetActive (false);
		//canvas = GetComponent<Canvas> ();
		agent = GetComponent<NavMeshAgent> ();
        //anim = GetComponent<Animator> ();
        started_talking = false;
		intro_complete = false;
		correct_shelf = true;
		correct_amount = true;
		stock_complete = false;
		stock_correct = false;
		serve_customer = false;
		player_ready = false;
		ordered = false; 
		canvas.enabled = false;
		//speed = agent.speed;

		shelves = GameObject.FindGameObjectsWithTag("shelf");

		Text t = canvas.GetComponentInChildren<Text> ();
		Debug.Log (t);

		FindObjectOfType<DialogueManager> ().SetDialogue (t);
	}

	void TriggerDialogue(){
		FindObjectOfType<DialogueManager> ().StartDialogue (dialogue);
        started_talking = true;
    }

	void OrderFulfilled(){
		FindObjectOfType<DialogueManager> ().NextNextSentence ();
	}

	void OrderUnfulfilled(){
		FindObjectOfType<DialogueManager> ().NextSentence ();
	}

	// Update is called once per frame
	void Update () {
		float entry_distance = Vector3.Distance (player.position, transform.position);
		float order_position = Vector3.Distance (store_counter.position, transform.position);

		if (entry_distance >= agent.stoppingDistance && started_talking == false) {
			//anim.SetFloat ("MoveSpeed", speed);
			agent.SetDestination (player.position);
		} else {
			//anim.SetFloat ("MoveSpeed", 0);
			FaceTarget ();
			if (started_talking == false) {
				canvas.enabled = true;
				TriggerDialogue ();
				started_talking = true;
			}

			if (btn1.IsActive ()) {
				if (Input.GetKeyDown (KeyCode.RightArrow)|| OVRInput.GetDown(OVRInput.Button.One))
                {
					//FindObjectOfType<DialogueManager>().NextSentence();
					btn1.onClick.Invoke ();
				}
			}
			if (intro_complete == true && stock_complete == false) {
				btn2.gameObject.SetActive (true);
				if (Input.GetKeyDown (KeyCode.RightArrow) || OVRInput.GetDown(OVRInput.Button.One)) {
					btn2.onClick.Invoke ();
				}
			}
			if (stock_complete == true && serve_customer == false && btn4.IsActive () == false) {
				agent.stoppingDistance = 0.2f;
				if (order_position >= 1) {
					agent.SetDestination (store_counter.position);
					Debug.Log (order_position);
				} else {
					if (ordered == false) {
						Debug.Log ("Button3 Active");
						btn3.gameObject.SetActive (true);
					}
					if (Input.GetKeyDown (KeyCode.RightArrow) || OVRInput.GetDown(OVRInput.Button.One) && btn3.IsActive ()) {
						btn3.onClick.Invoke ();
					}
					food_tag = FindObjectOfType<DeliveryManager> ().get_Tag ();

					if (food_tag == desired_food && ordered == true) {
						FindObjectOfType<DeliveryManager> ().removeItem (); 
						serve_customer = true;
						Talking ();
					}
				}
			}
			if (serve_customer == true && player_ready == false) {
				btn4.gameObject.SetActive (true);
				if (Input.GetKeyDown (KeyCode.RightArrow) || OVRInput.GetDown(OVRInput.Button.One)) {
					btn4.onClick.Invoke ();
				}
			}
			if (btn4.IsActive ()) {
				serve_customer = true;
			}
		}
	}

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void Talking()
    {
        FindObjectOfType<DialogueManager>().NextSentence();
    }

    public void Intro()
    {
        StopAllCoroutines();
        StartCoroutine(Intro_dialogue());
        return;
    }

    IEnumerator Intro_dialogue()
    {
        for (var i = 0; i < 5; i++)
        {
            Talking();
			Debug.Log ("Intro");
            yield return new WaitForSeconds(5);
        }
		intro_complete = true;
		btn1.gameObject.SetActive(false);
		food.SetActive (true);
    }

	public void Level4_Intro()
	{
		StopAllCoroutines();
		StartCoroutine(Intro4_dialogue());
		return;
	}

	IEnumerator Intro4_dialogue()
	{
		for (var i = 0; i < 5; i++)
		{
			Talking();
			Debug.Log ("Intro");
			yield return new WaitForSeconds(5);
		}
		intro_complete = true;
		btn1.gameObject.SetActive(false);
		food.SetActive (true);
		FindObjectOfType<OrderScreen> ().SetOrderScreen (manager_order);
	}

	public void CheckShelves()
	{
		foreach (GameObject shelf in shelves) {
			if (!shelf.GetComponent<TrackCollisions> ().ContainsFood ()) {
				correct_shelf = false;
				Debug.Log ("Shelf has the incorrect food: " + shelf.GetComponent<TrackCollisions> ().ContainsFood ());
			}
			if (shelf.GetComponent<TrackCollisions> ().AmountOfFood () != amount_of_food) {
				correct_amount = false;
				Debug.Log ("Shelf has incorrect amount: " + shelf.GetComponent<TrackCollisions> ().AmountOfFood ());
			}
		}
		if (correct_shelf && correct_amount) {
			Debug.Log ("Everything is correct");
			stock_complete = true;
			btn2.gameObject.SetActive (false);
			OrderFulfilled ();
			stock_correct = true;
			//food.GetComponent<FoodManager> ().RemoveClones ();
			//food.SetActive (false);
		} 
		else {
			Debug.Log ("Correct items not found, but we'll continue");
			stock_complete = true;
			btn2.gameObject.SetActive(false);
			OrderUnfulfilled ();
			stock_correct = false;
			//food.GetComponent<FoodManager> ().RemoveClones ();
			//food.SetActive (false);
		}
	}

	public void StandardCheck()
	{
		foreach (GameObject shelf in shelves) {
			if (!shelf.GetComponent<TrackCollisions> ().ContainsFood ()) {
				correct_shelf = false;
				Debug.Log ("Shelf has the incorrect food: " + shelf.GetComponent<TrackCollisions> ().ContainsFood ());
			}
			if (shelf.GetComponent<TrackCollisions> ().AmountOfFood () != amount_of_food) {
				correct_amount = false;
				Debug.Log ("Shelf has incorrect amount: " + shelf.GetComponent<TrackCollisions> ().AmountOfFood ());
			}
		}
		if (correct_shelf && correct_amount) {
			Debug.Log ("Everything is correct");
			OrderFulfilled ();
			stock_correct = true;
		} 
		else 
		{
			Debug.Log ("Correct items not found, but we'll continue");
			OrderUnfulfilled ();
			stock_correct = false;
		}
		btn2.gameObject.SetActive(false);
		btn4.gameObject.SetActive(true);
		stock_complete = true;
	}

	public void SingleCheck(){
		if (food_location.GetComponent<GroceryItemManager> ().ItemInStock () == true) {
			Debug.Log ("Everything is correct");
			OrderFulfilled ();
			stock_correct = true;
		} 
		else {
			Debug.Log ("Correct items not found, but we'll continue");
			OrderUnfulfilled ();
			stock_correct = false;
		}
		btn2.gameObject.SetActive(false);
		btn4.gameObject.SetActive(true);
		stock_complete = true;
	}

	public void PretendCustomer()
	{
		if (stock_correct == false) {
			for (var i = 0; i < 1; i++) {
				FindObjectOfType<DialogueManager> ().NextNextSentence ();
				Debug.Log ("Ordering: ");
			}
		} 
		else {
			for (var i = 0; i < 1; i++) {
				Talking ();
				Debug.Log ("Ordering: skipped sentence");
			}
		}
		ordered = true;
		btn3.gameObject.SetActive(false);
	}

	public void StartLevel()
	{
		//SceneManager.LoadScene(SceneManager.GetActiveScene ().buildIndex + 1
		StartCoroutine (LoadingProgress (SceneManager.GetActiveScene ().buildIndex + 1));
	}

	IEnumerator LoadingProgress (int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

		loadingScreen.SetActive (true);

		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / 0.9f);

			slider.value = progress;
			progressText.text = progress * 100f + "%";

			yield return null;
		}
	}

}
