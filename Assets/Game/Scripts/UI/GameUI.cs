using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.tip.games.ecorunner.ui;

namespace com.tip.games.ecorunner
{
	public class GameUI : MonoBehaviour 
	{
		public delegate void StartGameDelegate();
		
		[SerializeField]
		private UiMainMenu _gameStartScreen;
		[SerializeField]
		private UiEndGame _gameEndScreen;
		[SerializeField]
		private LivesUI _livesUi;
		[SerializeField]
		private UiScore _collectablesScore;
		[SerializeField]
		private Text _gameTimeText;

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
			_livesUi.SetLives(numLives);
		}

		public void SetScore(int score)
		{
			_collectablesScore.UpdateScore(score);
		}

		public void ShowStartGameScreen(StartGameDelegate onStartGame)
		{
			_gameStartScreen.Show(
				() => onStartGame(), 
				() => onStartGame());
		}

		public void ShowEndGameScreen(StartGameDelegate onStartGame)
		{
			_gameEndScreen.Show( 
				() => onStartGame(), 
				() => onStartGame());
		}

		public void SetGameTime(float seconds)
		{
			if(_gameTimeText != null)
			{
				int mins = (int)((int)seconds / 60);
				int secs = (int)seconds % 60;
				_gameTimeText.text = mins.ToString("00") + ":" + secs.ToString("00");
			}
		}

	}
}