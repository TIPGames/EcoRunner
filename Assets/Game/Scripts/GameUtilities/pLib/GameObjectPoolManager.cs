using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.partho.games.utilities
{
	public class GameObjectPoolManager : MonoBehaviour 
	{
		[SerializeField]
		private GameObjectPool[] _pools;

		public GameObjectPool GetPool(string poolName)
		{
			return System.Array.Find<GameObjectPool>(_pools, 
			                                                        (p) => p.pName == poolName);

		}
	}
}