using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.partho.games.utilities;

namespace com.tip.games.ecorunner
{
	public class CollectablesSection : LevelSection 
	{
		[System.SerializableAttribute]
		public class CollectableInfo
		{
			[SerializeField]
			private GameObjectPool _collectablePool;
			[SerializeField]
			private float _weight = 1;
			[SerializeField]
			private int _minCount = 1;
			[SerializeField]
			private int _maxCount = 4;

			public int pMinCount { get { return _minCount; } }
			public int pMaxCount { get { return _maxCount; } }
			public GameObjectPool pCollectablePool { get { return _collectablePool; } } 
		}

		[SerializeField]
		private CollectableInfo[] _collectables;
		[SerializeField]
		private float _collectablesDistance = 0.2f;

		private List<Vector3> mOriginalPositions = new List<Vector3>();
		private List<Collectable> mCollectables = new List<Collectable>();

		public override void Start()
		{
			// mCollectables = GetComponentsInChildren<Collectable>();
			// for(int i = 0; i < mCollectables.Length; ++i)
			// 	mOriginalPositions.Add(mCollectables[i].transform.localPosition);
		}

		public override void OnActivated(Transform[] lanes, float spanX)
		{
			// for(int i = 0; i < mCollectables.Length; ++i)
			// {
			// 	mCollectables[i].Activate();
			// 	mCollectables[i].gameObject.SetActive(true);
			// 	mCollectables[i].enabled = true;
			// 	mCollectables[i].GetComponent<SpriteRenderer>().enabled = true;
			// 	mCollectables[i].transform.localPosition = mOriginalPositions[i];
			// 	BlinkObject blink = mCollectables[i].GetComponent<BlinkObject>();
			// 	if(blink != null)
			// 		blink.enabled = true;
			// }
			SpawnCollectables(lanes, spanX);
		}

		public override void OnDeactivated()
		{
			for(int i = 0; i < mCollectables.Count; ++i)
			{
				mCollectables[i].Deactivate();
			}
			mCollectables.Clear();
		}

		private void SpawnCollectables(Transform[] lanes, float spanX)
		{
			if(_collectables == null || _collectables.Length <= 0)
				return;
			for(int i = 0; i < mCollectables.Count; ++i)
				mCollectables[i].Deactivate();
			mCollectables.Clear();
			float currPosX = -spanX / 2;
			float endPosX = spanX / 2;
			int minSkip = 5;
			int maxSkip = 10;

			while(currPosX < endPosX)
			{
				bool skip = (Random.Range(0, 2) == 0);
				if(skip)
				{
					currPosX += _collectablesDistance * Random.Range(minSkip, maxSkip + 1);
					continue;
				}
				Transform lane = lanes[Random.Range(0, lanes.Length)];
				CollectableInfo selectedCollectable = _collectables[Random.Range(0, _collectables.Length)];
				int numSpawns = Random.Range(selectedCollectable.pMinCount, selectedCollectable.pMaxCount + 1);
				for(int i = 0; i < numSpawns; ++i)
				{
					GameObject coll = selectedCollectable.pCollectablePool.GetObject();
					mCollectables.Add(coll.GetComponent<Collectable>());
					Vector3 newPos = lane.localPosition + new Vector3(currPosX, 0, 0);
					coll.transform.parent = transform;
					coll.transform.localPosition = newPos;
					currPosX += _collectablesDistance;
					if(currPosX >= endPosX)
						break;
				}
			}

		}

	}
}