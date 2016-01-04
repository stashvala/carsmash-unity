using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		[DEPRECATED]
	 * 		Drag box main component.
	 * 
	 * 		Use this to create a drag box for editor scripts
	 */
	public abstract class DragBox<T>
	{
		public virtual float ControlWidth { get { return 200f; } }
		public virtual float ControlHeight { get { return 50f; } }
		public string title = "DragBox";
		public List<T> list;

		public UnityEngine.Object undoObject;
		public event System.Action<List<T>, UnityEngine.Object> OnDrop;

		protected Rect dropAreaRect;
		
		public virtual void DrawBox()
		{
			DrawBox(true, 0, 0);
		}
		public virtual void DrawBox(float x, float y)
		{
			DrawBox(false, x, y);
		}
		protected virtual void DrawBox(bool layout, float x, float y)
		{
			Event evt = Event.current;
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (layout)
				dropAreaRect = GUILayoutUtility.GetRect (ControlWidth, ControlHeight, GUILayout.ExpandWidth (true));
			else
				dropAreaRect = new Rect(x,y,ControlWidth, ControlHeight);
				
			GUI.Box (dropAreaRect, title);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal ();

			if (OnDrop != null)
			{
				switch (evt.type) 
				{
				case EventType.DragUpdated:
				case EventType.DragPerform:
					if (!dropAreaRect.Contains (evt.mousePosition))
						return;
					
					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
					
					if (evt.type == EventType.DragPerform) 
					{
						if (undoObject!=null)
							Undo.RecordObject(undoObject, undoObject.name + " - Added " + DragAndDrop.objectReferences.Length + " objects");
						DragAndDrop.AcceptDrag ();
						foreach (UnityEngine.Object dragged_object in DragAndDrop.objectReferences) 
						{
							OnDrop(list, dragged_object);
						}
						evt.Use();
						if (undoObject!=null)
							EditorUtility.SetDirty(undoObject);
					}
					break;
				}
			}
		}
	}
}
