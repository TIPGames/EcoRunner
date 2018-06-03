using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class CollectablesSection : LevelSection 
	{
		private List<Vector3> mOriginalPositions = new List<Vector3>();
		private Collectable[] mCollectables;

		public override void Start()
		{
			mCollectables = GetComponentsInChildren<Collectable>();
			for(int i = 0; i < mCollectables.Length; ++i)
				mOriginalPositions.Add(mCollectables[i].transform.localPosition);
		}

		public override void OnActivated()
		{
			for(int i = 0; i < mCollectables.Length; ++i)
			{
				mCollectables[i].Activate();
				mCollectables[i].gameObject.SetActive(true);
				mCollectables[i].enabled = true;
				mCollectables[i].GetComponent<SpriteRenderer>().enabled = true;
				mCollectables[i].transform.localPosition = mOriginalPositions[i];
				BlinkObject blink = mCollectables[i].GetComponent<BlinkObject>();
				if(blink != null)
					blink.enabled = true;
			}
		}

		public override void OnDeactivated()
		{
			for(int i = 0; i < mCollectables.Length; ++i)
			{
				mCollectables[i].Deactivate();
			}
		}
	}
}