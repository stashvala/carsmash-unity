using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.FDT.GameManagerFramework
{
	/*
	 *	 ScenePlug
	 * 
	 *	 Used to plug a game object in the scene system during the life of the specific scene
	 * 
	 *	 It will be destroyed when the scene is unloaded
	 * 
	 *	 Inherit from this to use this functionality
	 */
	public abstract class ScenePlug : CustomBase 
	{
	
		protected BaseScene GetCurrentScene()
		{
			return gameManager.currentScene;
		}
		
		protected bool initialized { get { return currentScene != null; } }
		protected bool _paused = false;
		
		[SerializeField] protected BaseScene currentScene;

		protected override void Awake ()
		{
			base.Awake ();
		}
		protected override void Start ()
		{
			base.Start ();
			if (gameManager.currentScene != null)
			{
				getScene();
			}
			else
			{
				gameManager.BeforeInitLevel.AddListener(OnSceneInit);
			}
		}
		protected void getScene()
		{
			currentScene = gameManager.currentScene;
			if (currentScene.initialized)
				OnSceneInit(currentScene);
			else
				gameManager.BeforeInitLevel.AddListener(OnSceneInit);
		}		
		
		
		protected virtual void Update ()
		{
			if (initialized && !_paused)
				UnpausedUpdate();
			if (initialized)
				SceneActiveUpdate();
		}
		public void SceneUnload(BaseScene scene)
		{
			OnSceneUnload ();
			gameManager.BeforeUnloadLevel.RemoveListener(SceneUnload);
			currentScene = null;
		}
		protected virtual void OnSceneUnload()
		{
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
				gameManager.BeforeInitLevel.RemoveListener( OnSceneInit);
				gameManager.BeforeUnloadLevel.RemoveListener( SceneUnload);
			}
			if (initialized)
				SceneActiveDestroy ();
			base.OnDestroy ();
		}
		protected virtual void OnSceneInit (BaseScene scene)
		{
			currentScene = scene;
			gameManager.BeforeUnloadLevel.AddListener( SceneUnload);
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
	}
}
