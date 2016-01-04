using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace com.FDT.GameManagerFramework
{
	/// <summary>
	/// GUILayout Utilities to make Editor Scripts with included automatic Undo capability
	/// </summary>
	public static class UGUILayout
	{
		public static bool itemChanged = false;
		
		private static int undoNum;
		
		public static Y ObjectField<Y>(string label, Y item, bool scene, UnityEngine.Object undoObj) where Y:UnityEngine.Object
		{
			EditorGUI.BeginChangeCheck();
			Y newValue = item;
			if (!string.IsNullOrEmpty(label))
				newValue = EditorGUILayout.ObjectField (label, item, typeof(Y), scene) as Y;
			else
				newValue = EditorGUILayout.ObjectField (item, typeof(Y), scene) as Y;
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
		
		public static Y ObjectField<Y>(Y item, bool scene, UnityEngine.Object undoObj) where Y:UnityEngine.Object
		{
			return ObjectField (null, item, scene, undoObj);
		}
		public static int IntField(int value, UnityEngine.Object undoObj)
		{
			return IntField (null, value, undoObj);
		}
		public static int IntField(string label, int value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			int newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUILayout.IntField (value);
			else
				newValue = EditorGUILayout.IntField (label,  value);
			
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
		public static float FloatField(float value, UnityEngine.Object undoObj)
		{
			return FloatField (null, value, undoObj);
		}
		public static float FloatField(string label, float value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			float newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUILayout.FloatField (value);
			else
				newValue = EditorGUILayout.FloatField (label,  value);
			
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
		
		public static Vector2 Vector2Field(string label, Vector2 value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			Vector2 newValue = EditorGUILayout.Vector2Field (label, value);
			
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
		public static Vector3 Vector3Field(string label, Vector3 value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			Vector3 newValue = EditorGUILayout.Vector3Field (label, value);
			
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
		public static bool Toggle(bool value, UnityEngine.Object undoObj)
		{
			return Toggle (null, value, undoObj);
		}
		public static bool Toggle( string label, bool value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			bool newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUILayout.Toggle (value);
			else
				newValue = EditorGUILayout.Toggle (label, value);
			
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
		public static bool ToggleButton( string label, bool value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			bool newValue = value;
			
			newValue = GUILayout.Toggle (value, label, "Button");
			
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
		public static string TextField(string value, UnityEngine.Object undoObj)
		{
			return TextField(null,value,undoObj);
		}
		public static string TextField(string label, string value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			string newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUILayout.TextField (value);
			else
				newValue = EditorGUILayout.TextField (label, value);
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
		#region TextArea
		public static string TextArea(string value, UnityEngine.Object undoObj)
		{
			return TextArea (value, undoObj, null);
		}
		public static string TextArea(string value, UnityEngine.Object undoObj, GUIStyle style)
		{
			EditorGUI.BeginChangeCheck();
			
			string newValue = string.Empty;
			if (style!= null)
				newValue = EditorGUILayout.TextArea ( value, style);
			else
				newValue = EditorGUILayout.TextArea ( value);
			
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
		public static Enum EnumPopup (Enum value, UnityEngine.Object undoObj)
		{
			return EnumPopup(null, value, undoObj);
		}
		public static Enum EnumPopup (string label, Enum value, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			Enum newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUILayout.EnumPopup (value);
			else
				newValue = EditorGUILayout.EnumPopup (label, value);
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
		public static float Slider(float value, float leftValue, float rightValue, UnityEngine.Object undoObj)
		{
			return Slider (null, value, leftValue, rightValue, undoObj);
		}
		public static float Slider(string label, float value, float leftValue, float rightValue, UnityEngine.Object undoObj)
		{
			EditorGUI.BeginChangeCheck();
			float newValue = value;
			if (string.IsNullOrEmpty(label))
				newValue = EditorGUILayout.Slider (value, leftValue, rightValue);
			else
				newValue = EditorGUILayout.Slider (label,  value, leftValue, rightValue);
			
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