using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace com.FDT.GameManagerFramework
{

	/*
	 * 		CustomList<T> example
	 */
	public class GameObjectsList : CustomList<GameObject> {
		
		public override float ControlHeight {
			get {
				return 20f;
			}
		}
		public override float ControlWidth {
			get {
				return 400;
			}
		}
		public override bool CustomListDrawHeader {
			get {
				return true;
			}
		}
		public override float CustomListItemHeight {
			get {
				return 16f;
			}
		}
		public GameObjectsList()
		{
			OnEdit += OnEditItem;
			OnDrop += OnDropSpawnPoint;
			OnNew += OnNewItem;
		}
		protected void OnNewItem(int idx)
		{
			AddItem(null, idx);
		}
		public void OnDropSpawnPoint(List<GameObject> list, UnityEngine.Object dropped)
		{
			GameObject addObj = null;
			addObj = dropped as GameObject;
			list.Add (addObj);
		}
		protected void OnEditItem(GameObject item, int idx)
		{
			Selection.activeGameObject = item.gameObject;
			EditorGUIUtility.PingObject (item.gameObject);
			return;
		}
		protected override GameObject DrawItem (GameObject item, int idx, Rect r)
		{
			EditorGUI.LabelField (new Rect (r.x, r.y, 80, CustomListItemHeight), "GameObject:");
			item = UGUI.ObjectField(new Rect(r.x + 85, r.y,175, CustomListItemHeight), item, true, undoObject );
		
			return item;
		}
	}
}