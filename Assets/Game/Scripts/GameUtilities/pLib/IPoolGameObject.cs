using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.partho.games.utilities
{
	public interface IPoolGameObject 
	{
		void OnCreated(GameObjectPool pool);
		void OnDestroyed();
		void OnActivated();
		void OnDeactivated();
	}
}