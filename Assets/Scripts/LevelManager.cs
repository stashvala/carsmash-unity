using UnityEngine;
using System.Collections;
using com.FDT.GameManagerFramework;
using UnityEditor;

public class LevelManager : MonoBehaviour {
	
	[SerializeField] private bool customRespawnPoints = false;
	[SerializeField] private Vector3[] respawnPositions = new Vector3[4];
	[SerializeField] private GameObject[] players = new GameObject[4];
	[SerializeField] private GameObject[] playersHUD = new GameObject[4];

	[SerializeField] private string endGameSceneName;

	private CustomConfig config;
	private Vector3 inactivePosition = new Vector3(-100, -100, -100);
	private PlayerSelfManager[] playerSelfManagers = new PlayerSelfManager[4];

	private PlayerConfig[] debugPlayersConfig = new PlayerConfig[]{
		new PlayerConfig(0, "Bob", 2), new PlayerConfig(1, "Job", 2), null, null
		//new PlayerConfig(2, "Blue", 2), new PlayerConfig(3, "Dr.Ultra", 2),
	};

	// nared se death zone;

	// Use this for initialization
	void Start () {
		config = (CustomConfig) GameManager.Instance.gameManagerObject;

		PlayerConfig[] playersConfig = config.playersConfig;
		if (playersConfig == null) { // for debug purposes, so this scene can be started standalone
			playersConfig = debugPlayersConfig;
		}

		for (int i = 0; i < playersConfig.Length; i++) {
			if (playersConfig[i] != null) {
				Vector3 spawnPoint = players[i].transform.position + new Vector3(0, 10, 0); // avto je ze na svojmu startu
				Vector3 respawnPoint = customRespawnPoints ? respawnPositions[i] : spawnPoint; 
				int lives = playersConfig[i].lives;
				int id = playersConfig[i].id;

				PlayerSelfManager pd = players[i].GetComponent<PlayerSelfManager>();
				pd.name = playersConfig[i].name;
				pd.id = id;
				pd.leftLives = lives;
				pd.spawnPoint = spawnPoint;
				pd.respawnPoint = respawnPoint;
				pd.isPlaying = true;
				playerSelfManagers[i] = pd;
				EditorUtility.SetDirty(pd);
			} else {
				playersHUD[i].SetActive(false);
				players[i].SetActive(false);
				players[i].transform.position = inactivePosition;
			}

		}
	
	}
	
	// Update is called once per frame
	void Update () {

		int numberOfPlayersAlive = 0;
		int potentialWinnerId = 0;
		for (int i = 0; i < players.Length; i++) {
			PlayerSelfManager psd = playerSelfManagers[i];
			if 	(psd != null && psd.isPlaying && psd.leftLives >= 0) {
				numberOfPlayersAlive += 1;
				potentialWinnerId = i;
			}
		}

		if (numberOfPlayersAlive <= 1) {
			EndGame (potentialWinnerId);
		}
	}

	void EndGame(int winnerId) {
		// switch to end scene
		config.winnerId = winnerId;
		GameManager.Instance.LoadLevel (endGameSceneName);
	}
}
