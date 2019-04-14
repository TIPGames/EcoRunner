using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class LevelSection : MonoBehaviour 
	{
		[SerializeField]
		protected int _startingPlatformLevel = 1;
		[SerializeField]
		protected int _endingPlatformLevel = 1;

		public int pStartingPlatformLevel { get { return _startingPlatformLevel; } }
		public int pEndingPlatformLevel { get { return _endingPlatformLevel; } }

		// Use this for initialization
		public virtual void Start () {
			
		}
		
		// Update is called once per frame
		public virtual void Update () {
			
		}

		public virtual void OnActivated(Transform[] lanes, float spanX)
		{
		
		}

		public virtual void OnDeactivated()
		{

		}
	}
}