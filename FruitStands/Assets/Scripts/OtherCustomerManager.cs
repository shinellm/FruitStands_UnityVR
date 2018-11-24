using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherCustomerManager : MonoBehaviour {

	public GameObject customer;
	public GameObject instructionScreen;
	public GameObject continueScreen;
	public GameObject fullHealth;
	public GameObject halfHealth;
	public GameObject lastHealth;
	public GameObject noHealth;
	public GameObject gameOverScreen;

	public List<GameObject> orders_screens;

	public int current_health;
	public Transform[] spawnPoints;
	public Transform counter;
	public List<GameObject> customer_orders;
	public List<GameObject> items;

	private GameObject clone;
	private int cur_order;
	private bool health_checked;
	private bool health_displayed;
	private bool order_set;
	private float targetTime;


	// Use this for initialization
	void Start () {
		//customer_orders = new List<string>();
		cur_order = 0;
		health_checked = true;
		order_set = false;
		targetTime = 40f;
	}

	void Update() {

        if (instructionScreen.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || OVRInput.Get(OVRInput.Button.One))
            {
                instructionScreen.GetComponentInChildren<Button>().onClick.Invoke();
            }
        }
        if (gameOverScreen.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || OVRInput.Get(OVRInput.Button.One))
            {
                gameOverScreen.GetComponentInChildren<Button>().onClick.Invoke();
            }
        }
        if (continueScreen.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || OVRInput.Get(OVRInput.Button.One))
            {
                continueScreen.GetComponentInChildren<Button>().onClick.Invoke();
            }
        }

        if (health_displayed == false && instructionScreen.activeInHierarchy == false) {
			if (current_health == 3) {
				Debug.Log ("We have full health");
				DisplayHealth (fullHealth);
			}
			if (current_health == 2) {
				Debug.Log ("We have two heart");
				DisplayHealth (halfHealth);
			}
			if (current_health == 1) {
				Debug.Log ("Only one heart left");
				DisplayHealth (lastHealth);
			}
			if (current_health == 0) {
				Debug.Log ("GameOver");
				DisplayHealth (noHealth);
				GameOver (gameOverScreen);
			}
		}

		if (GameObject.FindGameObjectsWithTag("customer").Length != 0){

			bool order_received = FindObjectOfType<OtherCustomerController> ().OrderStatus ();
			//bool order_received = customer.GetComponent<CustomerController> ().CorrectOrderReceived ();
			Debug.Log (order_received);

			if (order_received == false && health_checked == false) {
				Debug.Log ("Correct Order: " + customer.GetComponent<OtherCustomerController> ().OrderStatus ());
				Debug.Log ("We have an unhappy customer");
				current_health--;
				health_checked = true;
				health_displayed = false;
			}
		}

		if (cur_order != customer_orders.Count && current_health != 0) {
			Debug.Log ("Order list is not empty");
			Debug.Log (customer_orders.Count);
			//Manually Spawn
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				Debug.Log ("Spawning Customer " + cur_order);
				Spawn ();
			}
			//Automatically Spawn
			if (GameObject.FindGameObjectsWithTag ("customer").Length == 0 && instructionScreen.activeInHierarchy == false) {
				Debug.Log ("Spawning Customer " + cur_order);
				targetTime -= Time.deltaTime;

				if (order_set == false){
					//New cutsomer order screen
					FindObjectOfType<OrderScreen> ().SetOrderScreen (orders_screens[cur_order]);
                    //Put the desired item on the counter for restocking
                    Instantiate(items[cur_order], counter.position, counter.rotation);
                    order_set = true;
				}
				Debug.Log (targetTime + " before customer appears");

				if (targetTime <= 0.0f) {
					Spawn ();
					targetTime = 40f;
				}
			}
		}
		if (cur_order == customer_orders.Count && GameObject.FindGameObjectsWithTag("customer").Length == 0 && current_health != 0) {
			Debug.Log ("customers = " + GameObject.FindGameObjectsWithTag ("customer").Length);
			continueScreen.SetActive (true);
		}
	}

	void Spawn () {
		
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		customer.GetComponent<OtherCustomerController> ().SetFoodItems (customer_orders[cur_order]);
		Instantiate (customer, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);

		health_checked = false;
		order_set = false;

        //customer.GetComponent<OtherCustomerController>().SetFoodItems(customer_orders[cur_order]);
        cur_order++;
	}

	void DisplayHealth(GameObject screen)
	{
		StopAllCoroutines();
		screen.SetActive (true);
		StartCoroutine(CurrentHealth(screen));
		return;
	}

	IEnumerator CurrentHealth(GameObject screen)
	{
		health_displayed = true;
		yield return new WaitForSeconds (1);
		screen.SetActive (false);
		Debug.Log ("Health Screen Deactivated");
	}

	void GameOver(GameObject screen)
	{
		screen.SetActive (true);
	}
}