using UnityEngine;
using System.Collections;

public class PlayerSelfManager : MonoBehaviour {

	[HideInInspector]
	public string name;
	public int id;
	public int leftLives;
	public double power;
	public Vector3 spawnPoint;
	public Vector3 respawnPoint;
	public bool isPlaying = false;

	private GameObject parent;
	
	private Vector3 inactivePosition = new Vector3(-100, -100, -100);
	private bool isColliding = false;
	
	// Use this for initialization
	void Start () {
		parent = gameObject.transform.parent.gameObject;
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
		
		this.leftLives-= 1; 
		if (leftLives >= 0) {
			gameObject.transform.position = respawnPoint;
			gameObject.transform.rotation.Set(0,0,0,1);
		} else {
			gameObject.SetActive(false);
			gameObject.transform.position = inactivePosition;
		}
	}
	
}
