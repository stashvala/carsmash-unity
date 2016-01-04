using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace com.FDT.GameManagerFramework
{
	[System.Serializable]
	public class GMFEventBool:UnityEvent<bool>
	{
	}

	[System.Serializable]
	public class GMFEventBaseScene:UnityEvent<BaseScene>
	{
	}
}