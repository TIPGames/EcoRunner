using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class LevelManager : MonoBehaviour 
	{
		[System.SerializableAttribute]
		public class LevelWaveInfo
		{
			[SerializeField]
			private LayerInfo[] _layers;
			[TooltipAttribute("Number of sections shown before the next wave is spawned")]
			[SerializeField]
			private int _numSectionsShown;
		}

		[SerializeField]
		private float _epsilon = 0.25f;
		[SerializeField]
		private float _minRunSpeed = 2;
		[SerializeField]
		private LayerInfo[] _layers;
		[SerializeField]
		private LevelWaveInfo[] _waves;
		[SerializeField]
		private float _screenLimit = -20;
		[SerializeField]
		private Transform[] _laneCenterMarkers;
		[SerializeField]
		private float _laneSpanX = 10f;

		private float mCurrXVelocity;
		private bool mIsRunning = true;

		// private LevelWaveInfo mCurrentWave;
		// private float mCurrentWaveTime = 0;

		// Use this for initialization
		void Start () 
		{
			mCurrXVelocity = _minRunSpeed;
			for(int i = 0; i < _layers.Length; ++i)
				_layers[i].SetupLayer(0, _laneCenterMarkers, _laneSpanX);
			// mCurrentWave = SelectNextWave();
			StopRunning();
		}
		
		// Update is called once per frame
		void Update () 
		{
			UpdateMovement();
		}
		public void StopRunning()
		{
			mIsRunning = false;
		}
		
		public void ResumeRunning()
		{
			mIsRunning = true;
		}

		public void Reset()
		{
			for(int i = 0; i < _layers.Length; ++i)
				_layers[i].SetupLayer(0, _laneCenterMarkers, _laneSpanX);
		}

		public void Pushback(float distance) 
		{
			for(int i = 0; i < _layers.Length; ++i)
				_layers[i].Pushback(distance);
		}

		private void UpdateMovement()
		{
			if(!mIsRunning)
				return;
			for(int i = 0; i < _layers.Length; ++i)
				_layers[i].Update(mCurrXVelocity, _screenLimit, _epsilon);
		}
		
		private LevelWaveInfo SelectNextWave()
		{
			if(_waves == null || _waves.Length <= 0)
				return null;
			return _waves[Random.Range(0, _waves.Length)];
		}
		
		private void OnDrawGizmos()
		{
			if(_laneCenterMarkers != null)
			{
				Gizmos.color = Color.magenta;
				for(int i = 0; i < _laneCenterMarkers.Length; ++i)
				{
					Gizmos.DrawWireCube(_laneCenterMarkers[i].position, new Vector3(0.25f, 0.25f, 0.25f));
					Gizmos.DrawLine(_laneCenterMarkers[i].position - new Vector3(_laneSpanX, 0, 0), 
						_laneCenterMarkers[i].position + new Vector3(_laneSpanX, 0, 0));
				}
			}
		}
	}
}