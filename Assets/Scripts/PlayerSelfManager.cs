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

	private GameObject parent;

	// Use this for initialization
	void Start () {
		parent = gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
