using UnityEngine;
using System.Collections;

namespace com.FDT.GameManagerFramework
{
	/*
	 *	 CustomBase
	 * 
	 *	 Main Monobehaviour of Game Manager Framework with override-ready Unity events.
	 */
	public class CustomBase : MonoBehaviour
	{
		protected GameManager gameManager;
		protected GameManagerObject gmObject;

		protected virtual void Awake() 
		{

			if (Application.isPlaying)
			{
				if (!GameManager.exists && !GameManager.applicationQuitting)
					GameManager.Instance.Call ();
				if (!GameManager.applicationQuitting)
				{
					gameManager = GameManager.Instance;
					gmObject = gameManager != null ? gameManager.gameManagerObject : null;
				}
			}
		}
		protected virtual void Start()
		{
		
		}
		protected virtual void OnDisable()
		{
		
		}
		protected virtual void OnEnable()
		{

		}
		protected virtual void OnDestroy()
		{
		}
	}
}
