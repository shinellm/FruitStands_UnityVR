using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Continue : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider slider;
	public Text progressText;

	public void RestartBtn(){
		StartCoroutine (LoadingProgress (SceneManager.GetActiveScene ().buildIndex));
		//SceneManager.GetActiveScene ().buildIndex + 1;
	}

	public void ContinueBtn () {
		StartCoroutine (LoadingProgress (SceneManager.GetActiveScene ().buildIndex + 1));
		//SceneManager.GetActiveScene ().buildIndex + 1;
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
