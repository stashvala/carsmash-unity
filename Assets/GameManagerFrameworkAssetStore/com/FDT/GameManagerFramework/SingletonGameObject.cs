using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace com.FDT.GameManagerFramework
{
	/*
	 *	 Singleton game object to inherit from. 
	 * 
	 *	 This contains the linked functionality for the instantiation capabilities of the GameManager
	 */
	public abstract class SingletonGameObject<T> : MonoBehaviour, ISingleton  where T : MonoBehaviour
	{
		protected static T _instance;
		
		protected static object _lock = new object();
		protected static bool _initialized = false;
		
		public static bool exists
		{
			get
			{
				return _instance != null && !applicationIsQuitting;
			}
		}
		
		public static T Instance
		{
			get
			{
				if (applicationIsQuitting) {
					Debug.LogWarning("[Singleton] Instance '"+ typeof(T) +
					                 "' already destroyed on application quit." +
					                 " Won't create again - returning null.");
					return null;
				}
				
				lock(_lock)
				{
					if (_instance == null)
					{
						Type typeParameterType = typeof(T);
						if (!GameManager.exists && typeParameterType.Name != "GameManager")
						{
							GameManager.Instance.Call();
							Debug.Log( "Invoking GameManager ");
						}
						
						_instance = Transform.FindObjectOfType<T>();
						
						if ( FindObjectsOfType(typeof(T)).Length > 1 )
						{
							Debug.LogError("[Singleton] Something went really wrong " +
							               " - there should never be more than 1 singleton!" +
							               " Reopenning the scene might fix it.");
							return _instance;
						}
						
						if (_instance == null)
						{
							bool createNew = true;
							GameObject singleton = null;
							if (typeParameterType.Name != "GameManager" && GameManager.Instance.gameManagerObject != null)
							{
								int c = GameManager.Instance.gameManagerObject.prefabsSingletons.Count;
								for(int i = 0; i < c; i ++)
								{
									ScenePrefab sp =  GameManager.Instance.gameManagerObject.prefabsSingletons[i];
									if (!sp.isGameManager && sp.prefab.GetType() == typeParameterType && sp.mode == ScenePrefabMode.ON_DEMAND)
									{
										Debug.Log("found");
										singleton = GameManager.Instance.CreatePrefab(sp);
									}
								}
							}
							
							
							if (createNew)
							{
								singleton = new GameObject("(singleton) "+ typeof(T).Name.ToString());
								_instance = singleton.AddComponent<T>();
								singleton.name = "(singleton) "+ typeof(T).Name.ToString();
								
								DontDestroyOnLoad(singleton);
								
								Debug.Log("[Singleton] An instance of " + typeof(T) + 
								          " is needed in the scene, so '" + singleton +
								          "' was created with DontDestroyOnLoad.");
								
								
							}
							if (!_initialized)
							{
								_initialized = true;
								singleton.SendMessage("Initialize");
							}
						}  
						else 
						{
							Debug.Log("[Singleton] Using instance of " + typeof(T) +  " already created: " +
							          _instance.gameObject.name);
							if (!_initialized)
							{
								_initialized = true;
								_instance.SendMessage("Initialize");
							}
						}
					}
					
					return _instance;
				}
			}
		}
		public abstract void Initialize();
		
		protected virtual void Awake()
		{
			if (!_initialized)
			{
				_initialized = true;
				Instance.SendMessage("Initialize");
			}
		}
		protected virtual void Destroyed()
		{
			
		}
		
		protected virtual void OnEnable()
		{
		}
		protected virtual void OnDisable()
		{
		}
		private static bool applicationIsQuitting = false;
		public static bool applicationQuitting { get { return applicationIsQuitting; } }
		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		protected virtual void OnDestroy () 
		{
			applicationIsQuitting = true;
		}
		protected virtual void Update()
		{
		}
		protected virtual void Start()
		{
		}
		public void Call()
		{
			// Does nothing. Made only for waking up the instantiation in case of use of singletons before instances are created
		}
	}
}