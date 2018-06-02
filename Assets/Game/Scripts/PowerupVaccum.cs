using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class PowerupVaccum : PowerupBase 
	{


#region _IPowerup_Implementation_
		public override void Activate()
		{
			mIsActive = true;
			mActivatedTime = Time.realtimeSinceStartup;
		}
		
		public override void Deactivate()
		{

		}

#endregion	//.	_IPowerup_Implementation_
	}
}