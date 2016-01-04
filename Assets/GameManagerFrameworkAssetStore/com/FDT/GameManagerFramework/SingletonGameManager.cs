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
	public abstract class SingletonGameManager : MonoBehaviour, ISingleton
	{
		protected static GameManager _instance;
		
		protected static object _lock = new object();
		protected static bool _initialized = false;
		
		public static bool exists
		{
			get
			{
				return _instance != null && !applicationIsQuitting;
			}
		}

		protected static bool _sceneInstance = true;

		public static bool isSceneInstance
		{
			get
			{
				return _sceneInstance;
			}
		}
		public static GameManager Instance
		{
			get
			{
				if (applicationIsQuitting) {
					Debug.LogWarning("[Singleton] Instance '"+ typeof(GameManager) +
					                 "' already destroyed on application quit." +
					                 " Won't create again - returning null.");
					return null;
				}
				
				lock(_lock)
				{
					if (_instance == null)
					{
						if (!GameManager.exists)
						{
							_sceneInstance = false;
							GameManager gm = Transform.FindObjectOfType<GameManager>();
							if (gm != null)
							{
								_instance = gm as GameManager;
								return _instance;
							}
							BaseScene bscene = Transform.FindObjectOfType<BaseScene>();
							if (bscene != null && bscene.gameManagerObject != null)
							{
								int c = bscene.gameManagerObject.prefabsSingletons.Count;
								for(int i = 0; i < c; i ++)
								{
									ScenePrefab sp = bscene.gameManagerObject.prefabsSingletons[i];
									if (sp.prefab != null)
									{
										GameManager tprefab = (sp.prefab as GameObject).GetComponent<GameManager>();
										if (tprefab != null)
										{
											Debug.Log("GameManager prefab found.");
											GameObject singleton = Instantiate(sp.prefab) as GameObject;
											_instance = singleton.GetComponent<GameManager>();
											_instance.gameObject.name = "(singleton) "+ typeof(GameManager).Name.ToString();
											
											DontDestroyOnLoad(_instance.gameObject);
											if (!_initialized)
											{
												_initialized = true;
												DontDestroyOnLoad(_instance);
												_instance.SendMessage("Initialize");
											}
											return _instance;
										}
									}
								}
							}
						}

						_instance = Transform.FindObjectOfType<GameManager>();
						
						if ( FindObjectsOfType(typeof(GameManager)).Length > 1 )
						{
							Debug.LogError("[Singleton] Something went really wrong " +
							               " - there should never be more than 1 singleton!" +
							               " Reopenning the scene might fix it.");
							return _instance;
						}
						
						if (_instance == null)
						{
							GameObject singleton = null;


							singleton = new GameObject("(singleton) "+ typeof(GameManager).Name.ToString());
							_instance = singleton.AddComponent<GameManager>();
							singleton.name = "(singleton) "+ typeof(GameManager).Name.ToString();
							
							DontDestroyOnLoad(singleton);
							
							Debug.Log("[Singleton] An instance of " + typeof(GameManager) + 
							          " is needed in the scene, so '" + singleton +
							          "' was created with DontDestroyOnLoad.");

							if (!_initialized)
							{
								_initialized = true;
								DontDestroyOnLoad(singleton);
								singleton.SendMessage("Initialize");
							}
						}  
						else 
						{
							Debug.Log("[Singleton] Using instance of " + typeof(GameManager) +  " already created: " +
							          _instance.gameObject.name);
							if (!_initialized)
							{
								_initialized = true;
								DontDestroyOnLoad(_instance);
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
				GameManager o = Transform.FindObjectOfType<GameManager>();
				if ( o == null)
				{
					_initialized = true;
					DontDestroyOnLoad(Instance);
					Instance.SendMessage("Initialize");
				}
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