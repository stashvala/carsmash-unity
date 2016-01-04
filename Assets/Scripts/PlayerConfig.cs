using UnityEngine;
using System.Collections;
using com.FDT.GameManagerFramework;
using UnityEngine.UI;

public class PlayerConfig
{

	public int id;
	public string name;
	public int lives;

	public PlayerConfig (int id, string name, int lives)
	{
		this.id = id;
		this.name = name;
		this.lives = lives;
	}
}

