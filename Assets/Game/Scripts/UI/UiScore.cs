using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.tip.games.ecorunner.ui
{
	public class UiScore : MonoBehaviour 
	{
		[SerializeField]
		private Text _scoreText;

		public void UpdateScore(float score)
		{
			_scoreText.text = score.ToString();
		}

		public void UpdateScore(int score)
		{
			_scoreText.text = score.ToString();
		}
	}
}