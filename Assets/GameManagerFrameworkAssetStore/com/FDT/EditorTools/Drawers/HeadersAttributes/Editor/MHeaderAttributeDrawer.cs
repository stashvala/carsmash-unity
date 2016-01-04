﻿using UnityEngine;
using UnityEditor;
#region Header
/**
 *
 * original version available in https://github.com/anchan828/property-drawer-collection
 * 
**/
#endregion
namespace com.FDT.EditorTools
{
	[CustomPropertyDrawer(typeof(MHeaderAttribute))]
	public class MHeaderAttributeDrawer : DecoratorDrawer
	{
		const int TextHeight = 20, LineHeight = 2;
		const int LineX = 15;
		
		public override void OnGUI(Rect position)
		{
			float originalX = position.x;
			position.height = TextHeight;
			
			if (string.IsNullOrEmpty(headerAttribute.text))
			{
				headerAttribute.text = "missing parameter";
			}
			position.y += TextHeight - 4;
			EditorGUI.LabelField(position, headerAttribute.text, EditorStyles.largeLabel);
			
			position.y += TextHeight;
			position.x += LineX;
			position.height = LineHeight;
			GUI.Box(position, "");
			position.x = originalX;
		}
		
		public override float GetHeight ()
		{
			return base.GetHeight () + TextHeight + 8;
		}
		MHeaderAttribute headerAttribute
		{
			get
			{
				return (MHeaderAttribute)attribute;
			}
		}
		
		static GUIStyle headerStyle
		{
			get
			{
				GUIStyle style = new GUIStyle(EditorStyles.largeLabel);
				style.fontStyle = FontStyle.Bold;
				style.fontSize = 18;
				style.normal.textColor = EditorGUIUtility.isProSkin ? new Color(0.7f, 0.7f, 0.7f, 1f) : new Color(0.4f, 0.4f, 0.4f, 1f);
				return style;
			}
		}
	}
}