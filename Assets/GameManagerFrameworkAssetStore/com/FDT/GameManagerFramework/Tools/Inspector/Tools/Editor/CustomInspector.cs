using UnityEngine;
using System.Collections;
using UnityEditor;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		Inherit from this to make your custom inspectors
	 * 
	 * 		It automatically cast a targetObject for the required type
	 */
	public class CustomInspector<T> : Editor where T:Object
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