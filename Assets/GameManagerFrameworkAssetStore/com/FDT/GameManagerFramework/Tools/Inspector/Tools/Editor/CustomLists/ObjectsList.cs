using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		CustomList<T> example
	 */
	public class ObjectsList : CustomList<Object> {
		
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
		public ObjectsList()
		{
			OnEdit += OnEditItem;
			OnDrop += OnDropSpawnPoint;
			OnNew += OnNewItem;
			
		}
		protected void OnNewItem(int idx)
		{
			AddItem(null, idx);
		}
		public void OnDropSpawnPoint(List<UnityEngine.Object> list, UnityEngine.Object dropped)
		{
			UnityEngine.Object addObj = null;
			addObj = dropped as UnityEngine.Object;
			list.Add (addObj);
		}
		protected void OnEditItem(Object item, int idx)
		{
			Selection.activeObject = item;
			EditorGUIUtility.PingObject (item);
			return;
		}
		protected override Object DrawItem (Object item, int idx, Rect r)
		{
			EditorGUI.LabelField (new Rect (r.x, r.y, 80, CustomListItemHeight), "Object:");
			item = UGUI.ObjectField(new Rect(r.x + 85, r.y,180, CustomListItemHeight), item, true, undoObject );
						
			return item;
		}
	}
}