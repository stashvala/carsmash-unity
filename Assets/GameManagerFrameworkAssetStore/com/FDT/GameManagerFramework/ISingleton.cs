using UnityEngine;
using System.Collections;

namespace com.FDT.GameManagerFramework
{
	/*
	 *	 ISingleton interface.
	 * 
	 *	 Currently this is only used by the editor to discard easily the prefabs that are not a singleton
	 */
	public interface ISingleton 
	{
		void Initialize();
	}
}