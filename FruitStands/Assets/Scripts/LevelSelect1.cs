using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect1 : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider slider;
	public Text progressText;

	public void Level1Btn () {
		StartCoroutine (LoadingProgress (SceneManager.GetActiveScene ().buildIndex + 1));
	}

	public void Level2Btn () {
		StartCoroutine (LoadingProgress (SceneManager.GetActiveScene ().buildIndex + 3));
	}

	public void Level3Btn () {
		StartCoroutine (LoadingProgress (SceneManager.GetActiveScene ().buildIndex + 5));
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
