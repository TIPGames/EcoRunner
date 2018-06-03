using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public abstract class PowerupBase: MonoBehaviour 
	{
		[SerializeField]
		private float _activeTime = 5;
		[SerializeField]
		private ParticleSystem _effect;

		protected Player mPlayer;
		protected bool mIsActive = false;
		protected float mActivatedTime = 0;

		public virtual void Start()
		{
			mPlayer = GetComponent<Player>();
		}

		public virtual void Update()
		{
			if(mIsActive && _activeTime > 0 && (Time.realtimeSinceStartup - mActivatedTime) >= _activeTime)
				Deactivate();
		}

		public virtual void Activate()
		{
			mIsActive = true;
			if(_effect != null)
				_effect.Play();
			mActivatedTime = Time.realtimeSinceStartup;
		}

		public virtual void Deactivate()
		{
			mIsActive = false;
			if(_effect != null)
				_effect.Stop();
		}
	}
}