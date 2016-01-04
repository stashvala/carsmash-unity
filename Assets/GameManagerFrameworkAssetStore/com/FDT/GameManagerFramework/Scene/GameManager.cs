using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		Controller for the BaseScene load and initialization.
	 * 
	 * 		Instantiated after the first use.
	 */ 
	[DisallowMultipleComponent]
	public class GameManager : SingletonGameManager
	{

		protected string currentSceneName;
		protected bool _paused = false;

		protected bool _loading = false;
		public bool isLoading { get { return _loading; } }

		public BaseScene currentScene;
		protected GameManagerObject _gameManagerObject;
		public GameManagerObject gameManagerObject { get { return _gameManagerObject;  } }

		[SerializeField] protected BaseScene nextScene;
		
		
		public CoroutineStack BeforeUnloadLevelCoroutine = null;
		public CoroutineStack AfterUnloadLevelCoroutine = null;
		public CoroutineStack BeforeInitLevelCoroutine = null;
		public CoroutineStack AfterInitLevelCoroutine = null;

		[Space(20)]

		public GMFEventBool OnPauseEvent = new GMFEventBool();
		public UnityEvent AfterUnloadLevel = new UnityEvent();
		public GMFEventBaseScene BeforeUnloadLevel = new GMFEventBaseScene();
		public GMFEventBaseScene BeforeInitLevel = new GMFEventBaseScene();
		public GMFEventBaseScene AfterInitLevel = new GMFEventBaseScene();

		protected override void Awake ()
		{
			if (isSceneInstance)
			{
				Debug.LogError("GameManager can't be part of a Scene. It can be called as a singleton or instantiated from the gameManagerObject list as a prefab Singleton.");
				Application.Quit();
			}
			base.Awake ();
		}
		public override void Initialize ()
		{
			BeforeUnloadLevelCoroutine = CoroutineStackManager.Instance.Create();
			AfterUnloadLevelCoroutine = CoroutineStackManager.Instance.Create();
			BeforeInitLevelCoroutine = CoroutineStackManager.Instance.Create();
			AfterInitLevelCoroutine = CoroutineStackManager.Instance.Create();
			
			currentScene = Transform.FindObjectOfType<BaseScene> ();
			if (currentScene == null)
			{
				Debug.LogError("There isn't a BasicScene object in scene\nand it's required to use GameManager");
				Application.Quit();
			}
			else
			{
				_gameManagerObject = currentScene.gameManagerObject;
				CreateInitialPrefabs();
				InitializeScene();
			}
		}
		protected void CreateInitialPrefabs()
		{
			foreach (ScenePrefab prefabData in _gameManagerObject.prefabsSingletons)
			{
				if (prefabData.mode == ScenePrefabMode.ON_INIT && !prefabData.isGameManager)
				{
					CreatePrefab (prefabData);
				}
			}
			foreach (Object prefab in _gameManagerObject.prefabs)
			{
				CreatePrefab (prefab);
			}
		}
		public GameObject CreatePrefab (ScenePrefab prefabData)
		{
			return CreatePrefab (prefabData.prefab);
		}

		public GameObject CreatePrefab (Object prefab)
		{
			if (prefab != null)
			{
				GameObject prefabInstance = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
				prefabInstance.name = prefab.name;
				return prefabInstance;
			}
			else
				return null;
		}

		protected virtual void InitializeScene()
		{
			currentScene.Init();
		}

		public void LoadLevel(string sceneName)
		{
			StopCoroutine ("loadLevelObject");
			StartCoroutine ("loadLevelObject", sceneName);
		}
		protected IEnumerator loadLevelObject(string sceneName)
		{
			_loading = true;
			bool hasOldScene = currentScene != null;
			BaseScene oldScene = currentScene;
				
			if (hasOldScene)
				BeforeUnloadLevel.Invoke(currentScene);
			BeforeUnloadLevelCoroutine.Run();
			while (BeforeUnloadLevelCoroutine.running) yield return new WaitForEndOfFrame();
			
			if (currentScene!=null)
				Destroy (currentScene);
				
			AfterUnloadLevel.Invoke();

			AfterUnloadLevelCoroutine.Run();
			while (AfterUnloadLevelCoroutine.running) yield return new WaitForEndOfFrame();

			#if UNITY_PRO_LICENSE
			AsyncOperation loading = Application.LoadLevelAsync(sceneName);
			yield return loading;
			#else
			Application.LoadLevel (sceneName);
			#endif

			while (hasOldScene && oldScene != null)
			{
				yield return new WaitForEndOfFrame();
			}
			nextScene = null;
			while ( nextScene == null && nextScene == oldScene )
			{
				nextScene = Transform.FindObjectOfType<BaseScene> ();
				yield return new WaitForEndOfFrame();
			}
			currentScene = nextScene;
			nextScene = null;
			currentSceneName = sceneName;
			
			BeforeInitLevel.Invoke(currentScene);

			BeforeInitLevelCoroutine.Run();
			while (BeforeInitLevelCoroutine.running) yield return new WaitForEndOfFrame();
		
			currentScene.Init ();
			
			AfterInitLevel.Invoke(currentScene);

			AfterInitLevelCoroutine.Run();
			while (AfterInitLevelCoroutine.running) yield return new WaitForEndOfFrame();
			_loading = false;
		}
		
		public bool Paused
		{
			get { 
				return _paused; 
				}
		}
		public virtual void Pause(bool paused)
		{
			frameworkPause (paused);
			
			OnPauseEvent.Invoke(_paused);
		}
		protected void frameworkPause(bool paused)
		{
			_paused = paused;
			if (_paused)
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
		}
		public void DoPauseResume()
		{
			Pause (!_paused);
		}
		protected virtual void OnApplicationPause (bool pause)
		{
		}
	}
}