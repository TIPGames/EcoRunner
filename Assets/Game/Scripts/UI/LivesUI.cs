using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class LivesUI : MonoBehaviour 
	{
		[SerializeField]
		private ToggleIcon[] _lifeIcons;

		// Use this for initialization
		void Start () 
		{
			
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}

		public void SetLives(int numLives)
		{
			for(int i = 0; i < _lifeIcons.Length; ++i)
			{
				_lifeIcons[i].SetState(i < numLives);
			}
		}
	}
}