using UnityEngine;
using System.Collections;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		Singleton to use for objects that need to work only when a BaseScene is loaded and initialized, and to stop when GameManager is loading another scene.
	 * 
	 */
	public class ScenePlugSingleton<T> : SingletonGameObject<T> where T:MonoBehaviour 
	{
		public override void Initialize ()
		{
			
		}
		protected BaseScene GetCurrentScene()
		{
			return GameManager.Instance.currentScene;
		}
		protected bool initialized { get { return currentScene != null; } }
		protected bool _paused = false;
		
		public BaseScene currentScene = null;
		
		protected override void OnEnable ()
		{
			DontDestroyOnLoad(gameObject);
			
			base.OnEnable ();
			OnGameInit();
			
			if (GameManager.Instance.currentScene != null)
				OnSceneInit (GameManager.Instance.currentScene);
			GameManager.Instance.AfterUnloadLevel.AddListener(AfterUnloadLevel);
			GameManager.Instance.BeforeInitLevel.AddListener(OnSceneInit);
			GameManager.Instance.OnPauseEvent.AddListener(OnPause);
		}
		protected virtual IEnumerator OnBeforeUnloadLevelCoroutine()
		{
			yield return true;
		}
		protected virtual IEnumerator OnBeforeInitLevelCoroutine(BaseScene scene)
		{
			yield return true;
		}
		protected virtual void AfterUnloadLevel()
		{
			currentScene = null;
		}
		protected virtual void OnSceneInit(BaseScene scene)
		{
			currentScene = scene;
		}
		protected override void Update ()
		{
			base.Update ();
			if (initialized && !_paused)
				UnpausedUpdate();
			if (initialized)
				SceneActiveUpdate();
		}
		protected virtual void UnpausedUpdate()
		{
			
		}
		protected override void OnDisable ()
		{
			if (initialized)
				SceneActiveDisable ();
			base.OnDisable ();
		}
		protected override void OnDestroy ()
		{
			if (GameManager.exists)
			{
				GameManager.Instance.BeforeInitLevel.RemoveListener(OnSceneInit);
				GameManager.Instance.AfterUnloadLevel.RemoveListener(AfterUnloadLevel);
				
			}
			if (initialized)
				SceneActiveDestroy ();
			base.OnDestroy ();
		}
		protected virtual void OnGameInit()
		{
			
		}
		public virtual void SceneActiveUpdate()
		{
			
		}
		public virtual void SceneActiveDisable()
		{
			
		}
		public virtual void SceneActiveDestroy()
		{
			
		}
		protected virtual void OnPause(bool p)
		{
			_paused = p;
		}
		
	}
}