using UnityEngine;
using System.Collections;
using com.FDT.EditorTools;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		Helper class to maintain the same width in pixels for the application across every device.
	 * 
	 * 		To use this, it needs a camera instance and that camera will be the one to scale the orthogonal size to match the requeriments
	 */
	[ExecuteInEditMode, DisallowMultipleComponent]
	public class CameraWidthControl2D : CustomBase 
	{
		public float pixelsToUnits = 100;
		public float targetwidth = 640;
		[RedNull]
		public Camera target;
	
		protected override void OnEnable ()
		{
			base.OnEnable ();
			target = GetComponent<Camera> ();
		}
		protected virtual void LateUpdate ()
		{
			if (target != null)
			{
				int height = Mathf.RoundToInt(targetwidth / (float)Screen.width * Screen.height);
				target.orthographicSize = height / pixelsToUnits / 2;
			}
		}
	}
}