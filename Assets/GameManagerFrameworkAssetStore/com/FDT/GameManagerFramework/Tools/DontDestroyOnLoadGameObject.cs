using UnityEngine;
using System.Collections;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		Used to avoid destroying a GameObject on a scene change.
	 * 
	 */
	public class DontDestroyOnLoadGameObject : MonoBehaviour 
	{
		void OnEnable()
		{
			DontDestroyOnLoad (gameObject);
		}
	}
}