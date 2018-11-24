using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCollisions : MonoBehaviour {

	public string index;
	public string negative_index;
	public string desiredTag;
	private bool correct_food;

	List<Collider> collidedObjects = new List<Collider> ();
		
	void Start(){
		correct_food = false;
	}

	void FixedUpdate () {
		//collidedObjects.Clear ();
	}
	
	void OnCollisionEnter(Collision col){
		Debug.Log ("Tag: " + col.collider.tag);
		if (!collidedObjects.Contains(col.collider) && col.collider.tag == desiredTag){
			collidedObjects.Add(col.collider);
			correct_food = true;
		}
	}

	void OnCollisionStay(Collision col){
		//OnCollisionEnter (col);
		Debug.Log (col.collider.tag + " is still here.");
	}

	void OnCollisionExit(Collision col){
		collidedObjects.Remove(col.collider);
	}

	void Update () {
		var numberOfColliders = collidedObjects.Count;
		Debug.Log ("# of food items: " + numberOfColliders);

		//collidedObjects.Clear ();
	}

	public bool ContainsFood()
	{
		return correct_food;
	}

	public int AmountOfFood()
	{
		return collidedObjects.Count;
	}
	
	public string ShelfIndex()
	{
		return index;
	}

	public string NegativeIndex()
	{
		return negative_index;
	}

	public string FoodName()
	{
		return desiredTag;
	}
}
