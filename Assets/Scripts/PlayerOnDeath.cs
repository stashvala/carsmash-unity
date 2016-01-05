using UnityEngine;
using System.Collections;

public class PlayerOnDeath : MonoBehaviour {

	private Vector3 inactivePosition = new Vector3(-100, -100, -100);
	private PlayerSelfManager playerSelfManager;
	private bool isColliding = false;

	// Use this for initialization
	void Start () {
		playerSelfManager = gameObject.transform.Find ("PlayerData").GetComponent<PlayerSelfManager>();
	}
	
	// Update is called once per frame
	void Update () {
		isColliding = false;
	}

	void OnTriggerEnter(Collider other) {
		if (!isColliding && other.transform.parent.gameObject.name == "DeathZones") {
			onDeath();
		}
		isColliding = true;
	}

	private void onDeath() {
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 

		if (playerSelfManager.leftLives > 0) {
			playerSelfManager.leftLives-= 1; 
			gameObject.transform.position = playerSelfManager.respawnPoint;
			//gameObject.transform.rotation.Set(0,0,0,0); todo: fix
		} else {
			gameObject.SetActive(false);
			gameObject.transform.position = inactivePosition;
		}
	}
	
}
