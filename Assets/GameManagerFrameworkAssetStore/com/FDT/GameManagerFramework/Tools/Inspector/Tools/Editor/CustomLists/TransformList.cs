using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace com.FDT.GameManagerFramework
{
	/*
	 *		CustomList<T> example 
	 */
	public class TransformList : CustomList<Transform> {

		public override float ControlHeight {	get {	return 20f;	}	}
		public override float ControlWidth {	get {	return 400;	}	}
		public override bool CustomListDrawHeader {	get {	return true; }	}
		public override float CustomListItemHeight {	get {	return 20f;	}	}
		
		public TransformList()
		{
			OnEdit += OnEditItem;
			OnNew += OnNewItem;
			OnDrop += OnDropSpawnPoint;
		}
		protected void OnNewItem(int idx)
		{
			AddItem(null, idx);
		}
		public void OnDropSpawnPoint(List<Transform> list, UnityEngine.Object dropped)
		{
			Transform addObj = null;
			if (dropped is Transform)
				addObj = dropped as Transform;
			else
				addObj = (dropped as GameObject).GetComponent<Transform> ();
			list.Add (addObj);
		}
		protected void OnEditItem(Transform item, int idx)
		{
			Selection.activeGameObject = item.gameObject;
			EditorGUIUtility.PingObject (item.gameObject);
			return;
		}
		protected override Transform DrawItem (Transform item, int idx, Rect r)
		{
			EditorGUI.LabelField (new Rect (r.x, r.y, 80, CustomListItemHeight), "Transform:");
			item = UGUI.ObjectField (new Rect (r.x + 85, r.y + 2, r.width - 85, 16f), item, true, undoObject);
			return item;
		}
	}
}