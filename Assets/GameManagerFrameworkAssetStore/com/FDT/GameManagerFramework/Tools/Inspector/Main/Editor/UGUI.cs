using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace com.FDT.GameManagerFramework
{
/// <summary>
/// GUI Utilities to make Editor Scripts with included automatic Undo capability
/// </summary>
	public static class UGUI
	{
		public static bool itemChanged = false;
		
		private static int undoNum;
		
		#region ObjectField
		public static Y ObjectField<Y>(Rect r, Y item, bool scene, UnityEngine.Object undoObj) where Y:UnityEngine.Object
		{
			return ObjectField (r, null, item, scene, undoObj);
		}
		public static Y ObjectField<Y>(Rect r, string label, Y item, bool scene, UnityEngine.Object undoObj) where Y:UnityEngine.Object
		{
			EditorGUI.BeginChangeCheck();
			Y newValue = item;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUI.ObjectField (r, item, typeof(Y), scene) as Y;
			else
				newValue = EditorGUI.ObjectField (r, label, item, typeof(Y), scene) as Y;
			
			if (EditorGUI.EndChangeCheck() || newValue != item)
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				item = newValue;
			}
			return item;
		}
		#endregion
		#region IntField
		public static int IntField(Rect r, int value, UnityEngine.Object undoObj)
		{
			return IntField (r, null, value, undoObj);
		}
		public static int IntField(Rect r, string label, int value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			int newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUI.IntField (r, value);
			else
				newValue = EditorGUI.IntField (r, label,  value);
			
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				value = newValue;
			}
			return value;
		}
		#endregion
		#region FloatField
		public static float FloatField(Rect r, float value, UnityEngine.Object undoObj)
		{
			return FloatField (r, null, value, undoObj);
		}
		public static float FloatField(Rect r, string label, float value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			float newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUI.FloatField (r, value);
			else
				newValue = EditorGUI.FloatField (r, label,  value);
			
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				value = newValue;
			}
			return value;
		}
		#endregion
		#region Vector2Field
		public static Vector2 Vector2Field(Rect r, string label, Vector2 value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			Vector2 newValue = EditorGUI.Vector2Field (r, label, value);
			
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				value = newValue;
			}
			return value;
		}
		#endregion
		#region Vector3Field
		public static Vector3 Vector3Field(Rect r, string label, Vector3 value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			Vector3 newValue = EditorGUI.Vector3Field (r, label, value);
			
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				value = newValue;
			}
			return value;
		}
		#endregion
		#region Toggle
		public static bool Toggle(Rect r, bool value, UnityEngine.Object undoObj)
		{
			return Toggle (r, null, value, undoObj);
		}
		public static bool Toggle(Rect r, string label, bool value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			bool newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUI.Toggle (r, value);
			else
				newValue = EditorGUI.Toggle (r, label, value);
			
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				value = newValue;
			}
			return value;
		}
		#endregion
		#region ToggleButton
		public static bool ToggleButton(Rect r, string label, bool value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			bool newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = GUI.Toggle (r, value, "Button");
			else
				newValue = GUI.Toggle (r, value, label, "Button");
			
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				value = newValue;
			}
			return value;
		}
		#endregion
		#region TextField
		public static string TextField(Rect r, string value, UnityEngine.Object undoObj)
		{
			return TextField(r,null,value,undoObj);
		}
		public static string TextField(Rect r, string label, string value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			string newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUI.TextField (r, value);
			else
			    newValue = EditorGUI.TextField (r, label, value);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				value = newValue;
			}
			return value;
		}
		#endregion
		#region TextArea
		public static string TextArea(Rect r, string value, UnityEngine.Object undoObj)
		{
			return TextArea (r, value, undoObj, null);
		}
		public static string TextArea(Rect r, string value, UnityEngine.Object undoObj, GUIStyle style)
		{
			EditorGUI.BeginChangeCheck();
			
			string newValue = string.Empty;
			if (style!= null)
				newValue = EditorGUI.TextArea (r, value, style);
			else
				newValue = EditorGUI.TextArea (r, value);
			
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				value = newValue;
			}
			return value;
		}
		#endregion
		#region GUIEnum
		public static Enum EnumPopup (Rect r, Enum value, UnityEngine.Object undoObj)
		{
			return EnumPopup(r, null, value, undoObj);
		}
		public static Enum EnumPopup (Rect r, string label, Enum value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			Enum newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUI.EnumPopup (r, value);
			else
				newValue = EditorGUI.EnumPopup (r, label, value);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				value = newValue;
			}
			return value;
		}
		#endregion
		#region Slider
		public static float Slider(Rect r, float value, float leftValue, float rightValue, UnityEngine.Object undoObj)
		{
			return Slider (r, null, value, leftValue, rightValue, undoObj);
		}
		public static float Slider(Rect r, string label, float value, float leftValue, float rightValue, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			float newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUI.Slider (r, value, leftValue, rightValue);
			else
				newValue = EditorGUI.Slider (r, label,  value, leftValue, rightValue);
			
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RegisterCompleteObjectUndo(undoObj, "Undo " + (undoNum++));
				if (undoObj!=null)
				{
					EditorUtility.SetDirty(undoObj);
				}
				value = newValue;
			}
			return value;
		}
		#endregion
	}
}