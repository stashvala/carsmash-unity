using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace com.FDT.GameManagerFramework
{
	/// <summary>
	///Helper class for instantiating ScriptableObjects.
	///
	///http://www.tallior.com/
	///
	///I adapted this for the new Asset Store guidelines and for my framework specific needs
	/// </summary>
	public class CustomScriptableObjectFactory : ScriptableObjectFactory 
	{
		
		[MenuItem("Tools/FDT/GameManager/Assets/Create GameManagerObject")]
		public static void CreateCustom()
		{
			var assembly = GetAssembly ();
			
			List<Type> allScriptableObjectsList = new List<Type> ();

			foreach (Type t in assembly.GetTypes())
			{
				if (t.IsSubclassOf(typeof(GameManagerObject)) || t == typeof(GameManagerObject) )
				{
					allScriptableObjectsList.Add(t);
				}
			}

			var allScriptableObjects = allScriptableObjectsList.ToArray ();
			// Show the selection window.
			var window = EditorWindow.GetWindow<ScriptableObjectWindow>(true, "Create a new ScriptableObject", true);
			window.ShowPopup();
			
			window.Types = allScriptableObjects;
		}
	}
}
