using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace com.FDT.GameManagerFramework
{
/// <summary>
/// Helpers to use with Custom Editors
/// </summary>
	public static class GUITools 
	{
		public static Rect Position(Rect src, float x, float y, float w, float h)
		{
			Rect result = new Rect(src.xMin + x, src.yMin +y, w, h);
			return result;
		}
	}
}
