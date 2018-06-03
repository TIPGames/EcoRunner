using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class PowerupScoreMultiplier : PowerupBase 
	{
		[SerializeField]
		private int _scoreMultiplier = 2;

#region _IPowerup_Implementation_
		public override void Activate()
		{
			if(!mIsActive)
				mPlayer.pScoreMultiplier *= _scoreMultiplier;
			base.Activate();
		}
		
		public override void Deactivate()
		{
			mPlayer.pScoreMultiplier /= _scoreMultiplier;
			base.Deactivate();
		}

#endregion	//.	_IPowerup_Implementation_
	}
}