using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashierDialogueManager : MonoBehaviour {

	private Text dialogueText;
	private Queue<string> sentences;

	void Start (){
		sentences = new Queue<string>();
	}

	public void SetDialogue(Text text){
		dialogueText = text;
	}

	public void StartDialogue(Dialogue dialogue){
		Debug.Log ("dialogue start");
		sentences.Clear ();

		foreach (string sentence in dialogue.sentences) {
			Debug.Log ("new sentence added");
			sentences.Enqueue (sentence);
		}

		Debug.Log ("calling next sentence...");
		NextSentence ();
	}

	public void NextSentence(){
		Debug.Log (sentences.Count);
		if (sentences.Count == 0) {
			EndDialogue ();
			return;
		}
		string sentence = sentences.Dequeue ();
		dialogueText.text = sentence;
		Debug.Log (sentence);
		StopAllCoroutines();
		StartCoroutine (TypeSentence (sentence));
	}

	public void LastSentence(){
		string sentence = sentences.Dequeue();
		Debug.Log ("Last: " + sentences.Count);

		if (sentences.Count == 0) {
			dialogueText.text = sentence;
			Debug.Log ("Last: " + sentence);
			StopAllCoroutines ();
			StartCoroutine (TypeSentence (sentence));

			EndDialogue ();
			return;
		} 
		LastSentence ();
	}

	IEnumerator TypeSentence (string sentence){
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			//yield return new WaitForSeconds(5);
			yield return null;
		}
	}

	void EndDialogue(){

	}
}

