using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class LevelManager : MonoBehaviour 
	{
		[SerializeField]
		private float _epsilon = 0.25f;
		[SerializeField]
		private float _minRunSpeed = 2;
		[SerializeField]
		private float _maxRunSpeed = 8;
		[SerializeField]
		private LayerInfo[] _layers;
		[SerializeField]
		private float _screenLimit = -20;

		private float mCurrXVelocity;
		private bool mIsRunning = true;

		// Use this for initialization
		void Start () 
		{
			mCurrXVelocity = _minRunSpeed;
			for(int i = 0; i < _layers.Length; ++i)
				_layers[i].SetupLayer();
			StopRunning();
		}
		
		// Update is called once per frame
		void Update () 
		{
			UpdateMovement();
		}
		private void UpdateMovement()
		{
			if(!mIsRunning)
				return;
			for(int i = 0; i < _layers.Length; ++i)
				_layers[i].Update(mCurrXVelocity, _screenLimit, _epsilon);
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
				_layers[i].SetupLayer();
		}

	}
}