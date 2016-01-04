using UnityEngine;
using System.Collections;
using com.FDT.EditorTools;

namespace com.FDT.GameManagerFramework
{

	/*
	 * 		BaseScene is the main scene class to use GameManager Framework.
	 * 
	 * 		It is necessary to have an instance of a GameManagerObject asset in every BaseScene to support Play from any scene.
	 */
	[DisallowMultipleComponent]
	public class BaseScene : CustomBase {

		protected bool _initialized = false;
		protected bool _paused = false;
		
		public bool initialized { get { return _initialized; } }

		[RedNullAttribute]
		public GameManagerObject gameManagerObject;

		protected override void Awake ()
		{
			if (!GameManager.exists)
			{
				GameManager.Instance.Call ();
			}
			base.Awake ();
		}
		protected virtual void OnLevelWasLoaded(int level) 
		{

		}
		protected override void OnEnable ()
		{
			base.OnEnable ();
			gameManager.OnPauseEvent.AddListener(OnPause);
		}
		protected virtual void OnPause(bool paused)
		{
			_paused = paused;
		}
		public virtual void Init()
		{
			Debug.Log ("Initializing Scene " + name);
			_initialized = true;
		}
	}
}
