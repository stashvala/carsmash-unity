using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using com.FDT.GameManagerFramework;

[CustomEditor(typeof(GameManagerObject), true)]
public class GameManagerObjectInspector : Editor 
{
	private ReorderableList SingletonPrefabsList;
	private ReorderableList PrefabsList;

	public override void OnInspectorGUI ()
	{
		serializedObject.Update();
		SingletonPrefabsList.DoLayoutList();
		PrefabsList.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
		DrawDefaultInspector ();
	}
	
	protected virtual void OnEnable() {
		SingletonPrefabsList = new ReorderableList (serializedObject, 
		                            serializedObject.FindProperty ("prefabsSingletons"), 
		                            true, true, true, true);
		SingletonPrefabsList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
			var element = SingletonPrefabsList.serializedProperty.GetArrayElementAtIndex (index);
			EditorGUI.PropertyField (rect, element, GUIContent.none);
		};
		
		SingletonPrefabsList.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Singleton Prefabs");
		};
		SingletonPrefabsList.onSelectCallback = (ReorderableList l) => {
			var prefab = l.serializedProperty.GetArrayElementAtIndex (l.index).FindPropertyRelative ("prefab").objectReferenceValue as GameObject;
			if (prefab)
			EditorGUIUtility.PingObject (prefab.gameObject);
		};
		SingletonPrefabsList.onCanRemoveCallback = (ReorderableList l) => {
			return l.count > 0;
		};
		SingletonPrefabsList.onRemoveCallback = (ReorderableList l) => {
			if (EditorUtility.DisplayDialog ("Warning!", "Are you sure you want to delete this item?", "Yes", "No")) {
				ReorderableList.defaultBehaviours.DoRemoveButton (l);
			}
		};

		PrefabsList = new ReorderableList (serializedObject, 
		                                            serializedObject.FindProperty ("prefabs"), 
		                                            true, true, true, true);
		PrefabsList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
			var element = PrefabsList.serializedProperty.GetArrayElementAtIndex (index);
			Color oldColor = GUI.backgroundColor;
			if (element.objectReferenceValue == null)
				GUI.backgroundColor = Color.red;
			EditorGUI.PropertyField (new Rect(rect.xMin, rect.yMin+2, rect.width, 16), element, GUIContent.none);
			GUI.backgroundColor = oldColor;
		};
		
		PrefabsList.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Prefabs");
		};
		PrefabsList.onSelectCallback = (ReorderableList l) => {
			var prefab = l.serializedProperty.GetArrayElementAtIndex (l.index).objectReferenceValue as Object;
			if (prefab)
			EditorGUIUtility.PingObject (prefab);
		};
		PrefabsList.onCanRemoveCallback = (ReorderableList l) => {
			return l.count > 0;
		};
		PrefabsList.onRemoveCallback = (ReorderableList l) => {
			if (EditorUtility.DisplayDialog ("Warning!", "Are you sure you want to delete this item?", "Yes", "No")) {
				ReorderableList.defaultBehaviours.DoRemoveButton (l);
			}
		};
	}
}
