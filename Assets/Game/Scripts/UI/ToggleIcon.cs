using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.tip.games.ecorunner
{
	public class ToggleIcon : MonoBehaviour 
	{
		[SerializeField]
		private Sprite _onSprite;
		[SerializeField]
		private Sprite _offSprite;
		[SerializeField]
		private Image _targetImage;
		[SerializeField]
		private bool _defaultOn = true;
		
		// Use this for initialization
		void Start () 
		{
			Reset();
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}

		public void Reset()
		{
			SetState(_defaultOn);
		}

		public void SetState(bool isOn)
		{
			_targetImage.sprite = isOn ? _onSprite : _offSprite;
		}
	}
}