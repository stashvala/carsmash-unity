using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace com.FDT.GameManagerFramework
{
	[InitializeOnLoad]
	public class ExecutionOrderManager : Editor
	{
		static ExecutionOrderManager()
		{
			List<string> classes = new List<string> ();

			System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			foreach (var A in assemblies)
			{
				System.Type[] types = A.GetTypes();
				System.Type[] possible = (from System.Type type in types where type.IsSubclassOf(typeof(CustomBase)) select type).ToArray();
				foreach (Type t in possible)
				{
					classes.Add(t.Name);
				}
			}

			string gameManagerScriptName = typeof(GameManager).Name;

			foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
			{
				if (monoScript.name == gameManagerScriptName)
				{
					if (MonoImporter.GetExecutionOrder(monoScript) != -100)
					{
						MonoImporter.SetExecutionOrder(monoScript, -100);
					}
				}
				if ( monoScript.name != gameManagerScriptName && classes.Contains(monoScript.name))
				{
					if (MonoImporter.GetExecutionOrder(monoScript) != -99)
					{
						MonoImporter.SetExecutionOrder(monoScript, -99);
					}
				}
			}
		}
	}
}