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
	[CustomPropertyDrawer(typeof(BHeaderAttribute))]
	public class BHeaderAttributeDrawer : DecoratorDrawer
	{
		const int HeaderHeight = 23, TextHeight = 17, LineHeight = 2;
		const int HeaderY = 10, LineX = 15;
		
		public override void OnGUI(Rect position)
		{
			position.y += HeaderY;
			position.height = HeaderHeight;
			
			//DrawHeader
			EditorGUI.LabelField(position, headerAttribute.headerText, headerStyle);
			
			if (!string.IsNullOrEmpty(headerAttribute.text))
			{
				position.y += HeaderHeight - 4;
				EditorGUI.LabelField(position, headerAttribute.text, EditorStyles.largeLabel);
			}
			
			position.y += string.IsNullOrEmpty(headerAttribute.text) ? HeaderHeight : TextHeight;
			position.x += LineX;
			position.height = LineHeight;
			GUI.Box(position, "");
		}
		
		public override float GetHeight ()
		{
			return base.GetHeight () + HeaderHeight + (string.IsNullOrEmpty(headerAttribute.text) ? 0 : TextHeight);
		}
		BHeaderAttribute headerAttribute
		{
			get
			{
				return (BHeaderAttribute)attribute;
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