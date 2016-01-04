using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		[DEPRECATED]
	 * 		Custom list main component.
	 * 
	 * 		Used to create custom lists for editors.
	 */
	public abstract class CustomList<T>:DragBox<T>
	{
		public static Dictionary<int, Vector2> posByControl = new Dictionary<int, Vector2>();
		public static Texture upTexture = null;
		public static Texture dwnTexture = null;
		public static Texture edtTexture = null;
		
		public virtual bool CustomListDrawHeader { get { return true; } }
		
		public virtual float CustomListItemHeight { get { return 20; } }
		
		public event System.Action<T,int,Rect> BeforeDrawItem;
		public event System.Action<T,int,Rect> AfterDrawItem;
		public event System.Action<T,int> BeforeDeleteItem;
		public event System.Action<T,int> AfterDeleteItem;
		public event System.Action<int> OnNew;
		public event System.Action<T,int> OnEdit;
		public event System.Action<T,int> OnChanged;
		
		protected bool itemChanged = false;
		
		protected float buttonHeight { get { return Mathf.Min(ControlHeight, CustomListItemHeight); } }
	
		public virtual bool canSort { get { return true; } }
		
		protected int controlId = -1;
		
		protected Vector2 scrollPos
		{
			get
			{
				if (!posByControl.ContainsKey(controlId))
				{
					posByControl[controlId] = Vector2.zero;
				}
				return posByControl[controlId];
			}
			set
			{
				if (!posByControl.ContainsKey(controlId))
				{
					posByControl[controlId] = Vector2.zero;
				}
				posByControl[controlId] = value;
			}
		}
		
		public virtual void DrawList()
		{
			DrawList(true, 0, 0, false, 0);
		}
		public virtual void DrawList(float x, float y)
		{
			DrawList(false, x, y, false, 0);
		}
		public virtual void DrawList (float x, float y, float scrollHeight)
		{
			DrawList(false, x, y, true, scrollHeight);
		}
		public virtual void DrawList (float scrollHeight)
		{
			DrawList(true, 0, 0, true, scrollHeight);
		}
		protected virtual void DrawList(bool layout, float x, float y, bool scroll, float scrollHeight)
		{
			if(upTexture==null)
				upTexture = Resources.Load<Texture>("customListUp");
			if(dwnTexture==null)
				dwnTexture = Resources.Load<Texture>("customListDwn");
			if(edtTexture==null)
				edtTexture = Resources.Load<Texture>("customListEdt");
			
			if (CustomListDrawHeader)
			{
				DrawBox (layout, x, y);
			}
			controlId = GUIUtility.GetControlID(FocusType.Passive);
					
			Rect r = dropAreaRect;
			
			if (OnNew != null && GUI.Button(new Rect(r.x + ( ControlWidth - 20f), r.y, 20f, ControlHeight), "+"))
			{
				OnNew(list.Count);
			}
				
			int idx = 0;
			
			float noLayoutCurrentY = r.y;
			
			if (scroll)
			{
				Rect scrollRect = new Rect(r.x, noLayoutCurrentY + ControlHeight, ControlWidth+15, scrollHeight-ControlHeight);
				GUI.Box(scrollRect, string.Empty);
				Vector2 newPos = scrollPos;
				newPos = GUI.BeginScrollView(scrollRect, newPos, new Rect(r.x,r.y, ControlWidth, (list.Count * CustomListItemHeight)/*+ ControlHeight*/));
				if (newPos != scrollPos)
				{
					scrollPos = newPos;
				}
			}
			
			for (; idx < list.Count; idx++)
			{
				if (layout)
				{
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					r = GUILayoutUtility.GetRect(Mathf.Max (81f, ControlWidth), Mathf.Max(16f, CustomListItemHeight));
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
				}
				else
				{
					r = new Rect(x,noLayoutCurrentY,Mathf.Max (81f, ControlWidth), Mathf.Max(16f, CustomListItemHeight));
				}
				
				GUI.Box(r, string.Empty);
				
				if (BeforeDrawItem!=null)
					BeforeDrawItem(list[idx], idx, r);

				GUI.changed = false;
				
				Rect itemRect =  new Rect(r.x, r.y, ( r.width - 81f), r.height);
				
				list[idx] = DrawItem(list[idx], idx, itemRect);

				if (GUI.changed)
				{
					GUI.changed = false;
					if (undoObject!=null)
					{
						EditorUtility.SetDirty(undoObject);
					}
					if (OnChanged!=null)
						OnChanged(list[idx], idx);
				}
				else if (GUI.changed)
				{
					if (OnChanged!=null)
						OnChanged(list[idx], idx);
				}
				if (OnEdit != null && GUI.Button(new Rect(r.x + ( r.width - 81f), r.y, 23f, buttonHeight), edtTexture))
				{
					OnEdit(list[idx], idx);
				}
				if (canSort)
				{
					if (idx>0 && GUI.Button(new Rect(r.x + ( r.width - 59f), r.y, 23f, buttonHeight), upTexture))
					{
						MoveUp(idx);
						return;
					}
					if (idx<list.Count-1 && GUI.Button(new Rect(r.x + ( r.width - 37f), r.y, 23f, buttonHeight), dwnTexture))
					{
						MoveDown(idx);
						return;
					}
				}
				if (GUI.Button(new Rect(r.x + ( r.width - 15f), r.y, 15f, buttonHeight), "-"))
				{
					if (DeleteItem(idx))
						return;
				}
				
				if (AfterDrawItem!=null)
					AfterDrawItem(list[idx], idx, r);
					
					
				Event evt = Event.current;
				
				if (evt.type == EventType.ContextClick)
				{
					Vector2 mousePos = evt.mousePosition;
					if (r.Contains (mousePos))
					{
						GenericMenu menu = new GenericMenu ();
						if (OnNew != null)
						{
							menu.AddItem (new GUIContent ("Insert Up"), false, ContextInsertUp, idx);
							menu.AddItem (new GUIContent ("Insert Down"), false, ContextInsertDown, idx);
							menu.AddSeparator ("");
						}
						if (canSort)
						{
							if (idx>0)
								menu.AddItem (new GUIContent ("Move Up"), false, ContextMoveUp, idx);
							if (idx<list.Count-1)
								menu.AddItem (new GUIContent ("Move Down"), false, ContextMoveDown, idx);
							menu.AddSeparator ("");
						}
						menu.AddItem (new GUIContent ("Delete"), false, ContextDelete, idx);
						menu.ShowAsContext ();
						evt.Use();
					}
				}
				noLayoutCurrentY+=CustomListItemHeight;
			}
			if (scroll)
			{
				GUI.EndScrollView();
			}
		}
		protected void ContextInsertUp(object o)
		{
			int i = (int)o;
			Debug.Log("ContextInsertUp " + i);
			OnNew(i);
		}
		protected void ContextInsertDown(object o)
		{
			int i = (int)o;
			Debug.Log("ContextInsertDown " + i);
			OnNew(i+1);
		}
		protected void ContextDelete(object o)
		{
			int i = (int)o;
			DeleteItem(i);
		}
		protected void ContextMoveUp(object o)
		{
			int i = (int)o;
			MoveUp(i);
		}
		protected void ContextMoveDown(object o)
		{
			int i = (int)o;
			MoveDown(i);
		}
		
		protected void MoveUp(int i)
		{
			if (undoObject!=null)
				Undo.RecordObject(undoObject, undoObject.name + " - object at index "+i+" moved UP");
			
			T temp = list[i-1];
			list[i-1] = list[i];
			list[i] = temp;
			
			if (undoObject!=null)
				EditorUtility.SetDirty(undoObject);
			
		}
		protected void MoveDown(int i)
		{
			if (undoObject!=null)
				Undo.RecordObject(undoObject, undoObject.name + " - object at index "+i+" moved DOWN");
			
			T temp = list[i+1];
			list[i+1] = list[i];
			list[i] = temp;
			
			if (undoObject!=null)
			{
				EditorUtility.SetDirty(undoObject);
			}
		}
		protected abstract T DrawItem (T item, int idx, Rect r);
		
		public virtual void AddItem(T item, int i)
		{
			itemChanged = true;
			if (undoObject!=null)
			{
				Undo.RegisterCompleteObjectUndo(undoObject, "CustomList item added");
				EditorUtility.SetDirty(undoObject);
			}
			list.Insert (i, item);
		}
		public virtual void AddItem(T item)
		{
			AddItem(item, list.Count);
		}
		protected bool DeleteItem(int idx)
		{
			if (EditorUtility.DisplayDialog("Remove", "Are you sure you want to remove this item ?", "Yes", "No"))
			{
				if (BeforeDeleteItem!=null)
					BeforeDeleteItem(list[idx], idx);
				
				if (undoObject!=null)
					Undo.RecordObject(undoObject, undoObject.name + " - object at index "+idx+" deleted");
				
				list.RemoveAt(idx);
				
				if (undoObject!=null)
					EditorUtility.SetDirty(undoObject);
				
				if (AfterDeleteItem!=null)
					AfterDeleteItem(list[idx], idx);
				return true;
			}
			return false;
		}
	}
}