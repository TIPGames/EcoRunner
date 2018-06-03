using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class PowerupSection : LevelSection 
	{
		private List<Vector3> mOriginalPositions = new List<Vector3>();
		private PickableObject[] mPowerups;

		public override void Start()
		{
			mPowerups = GetComponentsInChildren<PickableObject>();
			for(int i = 0; i < mPowerups.Length; ++i)
				mOriginalPositions.Add(mPowerups[i].transform.localPosition);
		}

		public override void OnActivated()
		{
			for(int i = 0; i < mPowerups.Length; ++i)
			{
				mPowerups[i].gameObject.SetActive(true);
				mPowerups[i].enabled = true;
				mPowerups[i].GetComponent<SpriteRenderer>().enabled = true;
				mPowerups[i].transform.localPosition = mOriginalPositions[i];
				BlinkObject blink = mPowerups[i].GetComponent<BlinkObject>();
				if(blink != null)
					blink.enabled = true;
			}
		}

		public override void OnDeactivated()
		{
		}
	}
}