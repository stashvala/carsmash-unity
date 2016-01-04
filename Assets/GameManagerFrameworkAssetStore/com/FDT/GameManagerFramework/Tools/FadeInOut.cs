using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.FDT.EditorTools;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		Fade In and Out with timescale pause and fade to Image / UI Panel.
	 * 
	 * 		Must be part of a prefab and have a Image instance.
	 */
	[DisallowMultipleComponent]
	public class FadeInOut : ScenePlugSingleton<FadeInOut> {
	
		public float fadeTime = 2f;
		[RedNull]
		public Image colorScreen;
		[SerializeField] Color screenColor = Color.white;
		
		protected override void OnGameInit ()
		{
			base.OnGameInit ();
			GameManager.Instance.BeforeUnloadLevelCoroutine.Add<BaseScene>(doingFadeOut, GetCurrentScene);
			GameManager.Instance.AfterInitLevelCoroutine.Add<BaseScene>(doingFadeIn, GetCurrentScene);
	
		}
		protected IEnumerator doingFadeOut(BaseScene scene)
		{
			yield return new WaitForFixedUpdate();
			Time.timeScale = 0.0f;
			colorScreen.enabled = true;
			float init = Time.unscaledTime + Time.unscaledDeltaTime;
			float end = init + fadeTime ;
			float ratio = 0;
			Color c = screenColor;
			c.a = 0;
			while (ratio < 1f)
			{
				ratio = Mathf.InverseLerp(init, end, Time.unscaledTime);
				c.a = ratio;
				colorScreen.color = c;
				yield return new WaitForEndOfFrame();
			}
			c.a = 1f;
			colorScreen.color = c;
			Time.timeScale = 1f;
		}
		protected IEnumerator doingFadeIn(BaseScene scene)
		{
			yield return new WaitForFixedUpdate();
			Time.timeScale = 0.0f;
			colorScreen.enabled = true;
			float init = Time.unscaledTime + Time.unscaledDeltaTime;
			float end = init + fadeTime;
			float ratio = 0;
			Color c = screenColor;
			c.a = 1f;
			while (ratio < 1f)
			{
				ratio = Mathf.InverseLerp(init, end, Time.unscaledTime);
				c.a = (1f-ratio);
				colorScreen.color = c;
				yield return new WaitForEndOfFrame();
			}
			c.a = 0f;
			colorScreen.enabled = false;
			Time.timeScale = 1f;
			
		}
	}
}
