using UnityEngine;
using System.Collections;
using com.FDT.GameManagerFramework;
using UnityEngine.UI;
using System;

public class PlayerSettingsConfigLoader : MonoBehaviour {
	

	[SerializeField] private GameObject[] playersConfig = new GameObject[4];

	public void loadPlayerSettings () {

		CustomConfig config = (CustomConfig) GameManager.Instance.gameManagerObject;
		config.playersConfig = new PlayerConfig[playersConfig.Length];

		InputField livesInputField = GameObject.Find("LivesInputField").GetComponent<InputField> ();

		for (int i = 0; i < playersConfig.Length; i++) {
			if (playersConfig[i] != null) {
				InputField inputField = playersConfig [i].transform.Find ("InputField").GetComponent<InputField> ();
				Toggle toggle = playersConfig [i].transform.Find ("IsPlayerEnabled").GetComponent<Toggle> ();
				if (toggle.isOn) {
					config.playersConfig[i] = new PlayerConfig(i, inputField.text, Int32.Parse(livesInputField.text));
				}
			}
		}

	
	}


}
