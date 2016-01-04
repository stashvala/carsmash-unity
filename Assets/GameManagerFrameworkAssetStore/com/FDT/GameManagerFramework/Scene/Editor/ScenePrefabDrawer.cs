using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Reflection;

namespace com.FDT.GameManagerFramework
{
	[CustomPropertyDrawer(typeof(ScenePrefab))]
	public class ScenePrefabDrawer : PropertyDrawer
	{
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty (position, label, property);
			int separator = 5;
			int enabledWidth = (int)(20);
			int prefabWidth = (int)((position.width - enabledWidth - (separator*2))* 0.5f);
			int modeWidth = (int)((position.width  - enabledWidth - (separator*2))* 0.5f);
			SerializedProperty prefab = property.FindPropertyRelative ("prefab");
			SerializedProperty mode = property.FindPropertyRelative ("mode");
			SerializedProperty enabled = property.FindPropertyRelative ("enabled");
			SerializedProperty isGameManager = property.FindPropertyRelative ("isGameManager");
			Rect r1 = new Rect (position);
			r1.width = prefabWidth;
			r1.height = 16f;

			Rect r2 = new Rect (position);
			r2.xMin = position.xMin + separator + prefabWidth;
			r2.width = modeWidth;
			r2.height = 16f;

			Rect r3 = new Rect (position);
			r3.xMin = position.xMin + (separator*2)+ prefabWidth + modeWidth;
			r3.width = enabledWidth;
			r3.height = 16f;

			EditorGUI.BeginChangeCheck ();
			EditorGUI.PropertyField (r1, prefab, new GUIContent(string.Empty));
			if (EditorGUI.EndChangeCheck())
			{
				if (prefab.objectReferenceValue != null)
				{
					ISingleton a = (prefab.objectReferenceValue as GameObject).GetComponent(typeof(ISingleton)) as ISingleton;
					if( a == null)
					{
						prefab.objectReferenceValue = null;
					}
					isGameManager.boolValue = (a is GameManager);
				}
				property.serializedObject.ApplyModifiedProperties();
			}
			EditorGUI.PropertyField (r2, mode, new GUIContent(string.Empty));
			EditorGUI.PropertyField (r3, enabled, new GUIContent(string.Empty));

			EditorGUI.EndProperty ();

		}
	
		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight (property, label);
		}
	}
}