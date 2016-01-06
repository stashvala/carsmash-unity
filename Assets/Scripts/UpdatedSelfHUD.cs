using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdatedSelfHUD : MonoBehaviour {

	[SerializeField] private Text nameField;
	[SerializeField] private Text leftLivesField;
	[SerializeField] private Text powerField;

	private PlayerSelfManager playerData;

	// Use this for initialization
	void Start () {
		playerData = gameObject.transform.parent.gameObject.GetComponent<PlayerSelfManager> ();
		nameField.text = playerData.name;
	}
	
	// Update is called once per frame
	void Update () {
		nameField.text = playerData.name;
		leftLivesField.text = playerData.leftLives.ToString ();
		powerField.text = playerData.power.ToString ();
	}
}
