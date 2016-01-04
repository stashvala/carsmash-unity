using UnityEngine;
using System.Collections;
using com.FDT.GameManagerFramework;
using UnityEditor;

public class LevelManager : MonoBehaviour {

	private Vector3 inactivePosition = new Vector3(-100, -100, -100);

	[SerializeField] private bool customRespawnPoints = false;
	[SerializeField] private Vector3[] respawnPositions = new Vector3[4];
	[SerializeField] private GameObject[] players = new GameObject[4];
	[SerializeField] private GameObject[] playersHUD = new GameObject[4];

	private PlayerConfig[] debugPlayersConfig = new PlayerConfig[]{
		new PlayerConfig(0, "Bob", 2), new PlayerConfig(1, "Job", 2), 
		new PlayerConfig(2, "Blue", 2), new PlayerConfig(3, "Dr.Ultra", 2),
	};

	// nared se death zone;

	// Use this for initialization
	void Start () {

		CustomConfig config = (CustomConfig) GameManager.Instance.gameManagerObject;

		PlayerConfig[] playersConfig = config.playersConfig;
		if (playersConfig == null) { // for debug purposes, so this scene can be started standalone
			playersConfig = debugPlayersConfig;
		}

		for (int i = 0; i < playersConfig.Length; i++) {
			if (playersConfig[i] != null) {
				Vector3 spawnPoint = players[i].transform.position; // avto je ze na svojmu startu
				Vector3 respawnPoint = customRespawnPoints ? respawnPositions[i] : spawnPoint; 
				int lives = playersConfig[i].lives;
				int id = playersConfig[i].id;

				PlayerSelfManager pd = players[i].transform.Find ("PlayerData").GetComponent<PlayerSelfManager>();
				pd.name = playersConfig[i].name;
				pd.id = id;
				pd.leftLives = lives;
				pd.spawnPoint = spawnPoint;
				pd.respawnPoint = respawnPoint;
				EditorUtility.SetDirty(players[i].transform.Find ("PlayerData"));
			} else {
				playersHUD[i].SetActive(false);
				players[i].SetActive(false);
				players[i].transform.position = inactivePosition;
			}

		}
	
	}
	
	// Update is called once per frame
	void Update () {

		// prever ce je se zmerej veljavna igra (imajo ljudje zivljenja)
		// ce ne koncaj in daj na winner sceno
	
	}
}
