using UnityEngine;
using System.Collections;
using com.FDT.GameManagerFramework;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour {

	[SerializeField] private Text winnerNameDisplayField;

	private CustomConfig config;

	// Use this for initialization
	void Start () {
		config = (CustomConfig) GameManager.Instance.gameManagerObject;
		winnerNameDisplayField.text = config.playersConfig [config.winnerId].name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
