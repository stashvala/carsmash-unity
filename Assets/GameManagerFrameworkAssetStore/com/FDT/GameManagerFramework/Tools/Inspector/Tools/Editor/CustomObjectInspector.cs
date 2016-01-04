using UnityEngine;
using System.Collections;
using UnityEditor;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		[DEPRECATED]
	 * 		Inherit from this to make your custom inspectors for ScriptableObject
	 * 
	 * 		It automatically cast a targetObject for the required type
	 * 
	 */
	public class CustomObjectInspector<T> : Editor where T:ScriptableObject
	{
		protected T targetObject;
		
		protected virtual void OnEnable()
		{
			targetObject = target as T;
		}
		public override void OnInspectorGUI ()
		{
			targetObject = target as T;
		}
		protected virtual void OnDisable()
		{
		}
	}
}