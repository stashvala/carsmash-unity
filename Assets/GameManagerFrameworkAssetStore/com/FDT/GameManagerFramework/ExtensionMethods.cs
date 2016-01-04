using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace com.FDT.GameManagerFramework
{
	/*
	 *	 Extension methods
	 */
	public static class ExtensionMethods 
	{
		public static List<MethodInfo> GetMethods(this MonoBehaviour m, BindingFlags bindingFlags)
		{
			return GetMethods (m, bindingFlags, -1);
		}
		public static List<MethodInfo> GetMethods(this MonoBehaviour m, BindingFlags bindingFlags, int parameters)
		{
			List<MethodInfo> minfos = new List<MethodInfo>();
			
			string a = string.Empty;
			string b = string.Empty;
			System.Type currentType = m.GetType();
			do
			{
				var fieldValues = currentType.GetMethods( bindingFlags);
				foreach (MethodInfo v in fieldValues)
				{
					if (parameters!=-1)
					{
						ParameterInfo[] pinfo = v.GetParameters();
						if (pinfo.Length == parameters)
							minfos.Add(v);
					}
					else
					{
						minfos.Add(v);
					}
					
				}
				a = currentType.BaseType.ToString();
				b = (typeof(MonoBehaviour)).ToString();
				currentType = currentType.BaseType;
			}
			while (a != b);
			return minfos;
		}
		public static List<string> GetMethodNames(this MonoBehaviour m, BindingFlags bindingFlags)
		{
			return GetMethodNames (m, bindingFlags, -1);
		}
		public static List<string> GetMethodNames(this MonoBehaviour m, BindingFlags bindingFlags, int parameters)
		{
			List<string> minfos = new List<string>();
			
			string a = string.Empty;
			string b = string.Empty;
			System.Type currentType = m.GetType();
			do
			{
				var fieldValues = currentType.GetMethods( bindingFlags);
				foreach (MethodInfo v in fieldValues)
				{
					if (parameters!=-1)
					{
						ParameterInfo[] pinfo = v.GetParameters();
						if (pinfo.Length == parameters)
							minfos.Add(v.Name);
					}
					else
					{
						minfos.Add(v.Name);
					}
					
				}
				a = currentType.BaseType.ToString();
				b = (typeof(MonoBehaviour)).ToString();
				currentType = currentType.BaseType;
			}
			while (a != b);
			return minfos;
		}
		public static string GetTypeNoNameSpace(this object o)
		{
			string type = o.GetType().ToString();
			int lastindex = type.LastIndexOf(".");
			if (lastindex != -1)
				type = type.Substring(lastindex + 1);
			return type;
		}
		public static void Clear(this Animation anim)
		{
			List<AnimationState> toRemove = new List<AnimationState> ();
			foreach (AnimationState a in anim)
			{
				toRemove.Add(a);
			}
			foreach (AnimationState a in toRemove)
			{
				anim.RemoveClip(a.clip);
			}
		}
		public static void PlayUnscaled (this Animation anim, AnimationClip c)
		{
			PlayUnscaledAnimation a = anim.gameObject.AddComponent<PlayUnscaledAnimation>();
			a.Play (c);
		}
	}
}