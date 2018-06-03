using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class PowerupVaccum : PowerupBase 
	{
		[SerializeField]
		private float _gravityRange;
		[SerializeField]
		private float _gravitySpeed = 50;

		public override void Update()
		{
			if(mIsActive)
			{
				ReadOnlyCollection<Collectable> activeCollectables = Collectable.pActiveCollectables;
				for(int i = 0; i < activeCollectables.Count; ++i)
				{
					if(!activeCollectables[i].pIsBeingPulled && 
						Vector3.Distance(transform.position, activeCollectables[i].transform.position) <= _gravityRange)
					{
						activeCollectables[i].PullTowards(GetComponent<Player>(), _gravitySpeed);
					}
				}
			}
		}

#region _IPowerup_Implementation_
		public override void Activate()
		{
			base.Activate();
		}
		
		public override void Deactivate()
		{
			base.Deactivate();
		}

#endregion	//.	_IPowerup_Implementation_

		public void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, _gravityRange);
		}
	}
}