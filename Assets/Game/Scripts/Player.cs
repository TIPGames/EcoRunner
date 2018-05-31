using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class Player : MonoBehaviour 
	{
		[SerializeField]
		private float _groundHeight = 1;
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

		private float mCurrYVelocity;
		private int mJumpCount = 0;
		private int mCurrentLives = 0;
		private int mCollectablesPicked = 0;
		private bool mIsGameOn = false;
	
		public int pLives { get { return mCurrentLives; } }
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
			mCurrentLives = _lives;
			mCurrYVelocity = 0;
			mCollectablesPicked = 0;
			_gameUi.SetLives(pLives);
			_gameUi.SetScore(mCollectablesPicked);
			_levelManager.Reset();
			_levelManager.ResumeRunning();
		}

		public void OnObstacleHit(Obstacle obstacle)
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

		public void OnCollected(Collectable collectable)
		{
			mCollectablesPicked++;
			_gameUi.SetScore(mCollectablesPicked);
			BlinkObject blink = collectable.GetComponent<BlinkObject>();
			blink.StartBlink(true);
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
			if(pos.y <= _groundHeight)
			{
				pos.y = _groundHeight;
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
			_gameUi.ShowEndGameScreen(StartNewGame);
		}

		public void OnDrawGizmos()
		{
		}
	}
}