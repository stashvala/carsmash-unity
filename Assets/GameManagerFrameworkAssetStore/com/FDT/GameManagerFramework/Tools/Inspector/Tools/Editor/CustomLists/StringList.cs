using UnityEngine;
using System.Collections;
using UnityEditor;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		CustomList<T> example
	 */
	public class StringList : CustomList<string> {
		
		public override float ControlHeight {
			get {
				return 20f;
			}
		}
		public override float ControlWidth {
			get {
				return 350;
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
		protected override string DrawItem (string item, int idx, Rect r)
		{
			EditorGUI.LabelField (new Rect (r.x, r.y, 80, CustomListItemHeight), "Transform:");
			item = EditorGUI.TextField (new Rect(r.x + 85, r.y, r.width-85, CustomListItemHeight), item);
			return item;
		}
	}
}
