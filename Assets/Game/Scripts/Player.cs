using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Code for player */
namespace com.tip.games.ecorunner
{
	public class Player : MonoBehaviour 
	{
		[SerializeField]
		private float[] _groundLevelHeights = {1, 50, 100};
		[SerializeField]
		private int _startingGroundLevel = 0;
		[SerializeField]
		private float _jumpSpeed = 4;
		[SerializeField]
		private float _gravity = 1;
		[SerializeField]
		private int _maxJumps = 2;
		[SerializeField]
		private LevelManager _levelManager;
		[SerializeField]
		private int _lives = 3;
		[SerializeField]
		private GameUI _gameUi;
		[SerializeField]
		private float _obstaclePushbackDistance = 0.5f;
		[SerializeField]
		private float _platformPushbackDistance = 0.5f;
		[SerializeField]
		private Transform _startMarker;

		[Header("Debug Variables")]
		[SerializeField]
		[Tooltip("Debug Variable. Do not set to true in final build.")]
		private bool _godMode = false;

		private float mCurrYVelocity;
		private int mCurrGroundLevel;
		private int mJumpCount = 0;
		private int mCurrentLives = 0;
		private int mCollectablesPicked = 0;
		private int mCollectablesScore = 0;
		private float mScoreMultiplier = 1;
		private bool mIsGameOn = false;
		private float mRunStartTime = 0;
	
		public int pLives { get { return mCurrentLives; } }
		public float pScoreMultiplier 
		{
			get { return mScoreMultiplier; }
			set { mScoreMultiplier = value; }
		}
		// Use this for initialization
		void Start () 
		{
			_gameUi.ShowStartGameScreen(StartNewGame);
		}
		
		// Update is called once per frame
		void Update () 
		{
			if(!mIsGameOn)
				return;
			_gameUi.SetGameTime(Time.realtimeSinceStartup - mRunStartTime);
			if(Input.GetMouseButtonDown(0) && mJumpCount < _maxJumps)
			{
				mCurrYVelocity = _jumpSpeed;
				mJumpCount++;
			}
			UpdateMovement();
		}

		public void StartNewGame()
		{
			mIsGameOn = true;
			mCurrGroundLevel = _startingGroundLevel;
			mCurrentLives = _lives;
			mCurrYVelocity = 0;
			mCollectablesPicked = 0;
			mCollectablesScore = 0;
			_gameUi.SetLives(pLives);
			_gameUi.SetScore(mCollectablesPicked);
			DeactivatePowerUps();
			mScoreMultiplier = 1;
			_levelManager.Reset();
			_levelManager.ResumeRunning();
			mRunStartTime = Time.realtimeSinceStartup;
			if(_startMarker != null)
			{
				transform.position = _startMarker.position;
			}
		}

		public void OnObstacleHit(Obstacle obstacle)
		{
#if UNITY_EDITOR
			if(_godMode)
				return;
#endif		//.	UNITY_EDITOR
			OnHit();
			Pushback(_obstaclePushbackDistance);
		}

		public void OnPlatformObstacleHit(Obstacle obstacle)
		{
#if UNITY_EDITOR
			if(_godMode)
				return;			
#endif		//.	UNITY_EDITOR
			OnObstacleHit(obstacle);
			Pushback(_platformPushbackDistance);
		}

		public void OnCollected(Collectable collectable)
		{
			mCollectablesPicked++;
			mCollectablesScore += (int)(collectable.pScore * pScoreMultiplier);
			_gameUi.SetScore(mCollectablesScore);
		}
	
		public void OnSwitchPlatform(int newLevel) 
		{
			mCurrGroundLevel = newLevel;
		}

		public void ActivatePowerup<T>() where T: PowerupBase
		{
			T powerUp = GetComponent<T>();
			powerUp.Activate();
		}

		public void DeactivatePowerUps()
		{
			PowerupBase[] powerUps = GetComponents<PowerupBase>();
			for(int i = 0; i < powerUps.Length; ++i)
				powerUps[i].Deactivate();
		}

		private void OnHit() 
		{
			mCurrentLives--;
			if(pLives <= 0)
			{
				DoGameOver();
				return;
			}
			_gameUi.SetLives(pLives);
			BlinkObject blink = GetComponent<BlinkObject>();
			blink.StartBlink(true);
			_levelManager.StopRunning();
			StartCoroutine(DelayedResume(blink.pBlinkDuration));
		}

		private void Pushback(float distance) 
		{
			//transform.position -= new Vector3(distance, 0, 0);
			_levelManager.Pushback(distance);
		}

		private IEnumerator DelayedResume(float timeDelay)
		{
			yield return new WaitForSeconds(timeDelay);
			if(mIsGameOn)
				_levelManager.ResumeRunning();
		}

		private void UpdateMovement()
		{
			Vector3 pos = transform.position;
			pos += new Vector3( 0, mCurrYVelocity * Time.deltaTime, 0 );
			if(pos.y <= _groundLevelHeights[mCurrGroundLevel])
			{
				pos.y = _groundLevelHeights[mCurrGroundLevel];
				mCurrYVelocity = 0;
				mJumpCount = 0;
			}
			transform.position = pos;
			mCurrYVelocity -= _gravity * _gravity * Time.deltaTime;
		}

		private void DoGameOver()
		{
			_levelManager.StopRunning();
			mIsGameOn = false;
			DeactivatePowerUps();
			_gameUi.ShowEndGameScreen(StartNewGame);
		}

		public void OnDrawGizmos()
		{
		}
	}
}