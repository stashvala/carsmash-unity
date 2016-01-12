using UnityEngine;
using System.Collections;
using com.FDT.GameManagerFramework;

public class ButtonFunctions : MonoBehaviour {

	public string nextScene;

	public void loadNextScene() {
		GameManager.Instance.LoadLevel (nextScene);
	}

	public void pauseResumeGame() {
		GameManager.Instance.DoPauseResume ();
	}

	public void toggleSelfIsActive() {
		gameObject.SetActive (!gameObject.activeSelf);
	}

}
