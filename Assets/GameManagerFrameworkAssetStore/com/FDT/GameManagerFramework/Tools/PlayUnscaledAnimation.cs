using UnityEngine;
using System.Collections;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		Used by ExtensionMethods for Animation
	 * 
	 * 		Controls the unscaled animation play.
	 */
	public class PlayUnscaledAnimation : MonoBehaviour 
	{
		AnimationState cstate;
		float init = 0;

		public void Play(AnimationClip c)
		{
			cstate = GetComponent<Animation>() [c.name];
			cstate.normalizedTime = 0;
			cstate.enabled = true;
			cstate.weight = 1;
			init = Time.unscaledTime;
		}

		void LateUpdate()
		{
			if( cstate != null )
			{
				cstate.normalizedTime = (Time.unscaledTime - init) / cstate.length;
				if(  cstate.normalizedTime >= 1 )
				{
					Destroy(this);
				}
			}
		}
	}
}