using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using com.FDT.EditorTools;

namespace com.FDT.GameManagerFramework
{
	public enum ScenePrefabMode
	{
		[EnumAlias("On Initialization")] ON_INIT, 
		[EnumAlias("On Demand")] ON_DEMAND
	};
	[System.Serializable]
	public class ScenePrefab
	{
		[RedNull]
		public Object prefab;
		public ScenePrefabMode mode;
		public bool enabled;
		public bool isGameManager = false;
	}
	/*
	 * 		Main Configuration Class to inherit from. It's mandatory to create a class and an object for this class inheriting this to the framework to work.
	 * 
	 */
	public class GameManagerObject : ScriptableObject 
	{
		[HideInInspector] public List<ScenePrefab> prefabsSingletons = new List<ScenePrefab>();
		[HideInInspector] public List<Object> prefabs = new List<Object>();

	}
}