using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class PickableVaccum : PickableObject 
	{
		protected override void OnObjectPicked(Player player)
		{
			player.ActivatePowerup<PowerupVaccum>();
		}
	}
}