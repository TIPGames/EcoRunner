using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	/**
	 * Represents a level depth layer
	 * Each layer can contain multiple LevelSections
	 */
	 [System.SerializableAttribute]
	public class LayerInfo
	{

		[SerializeField]
		private string _layerName;
		[SerializeField]
		private Transform _layerRoot;
		[SerializeField]
		[RangeAttribute(0.2f, 1)]
		private float _layerSpeedMultiplier = 1;

		private List<Transform> mInactiveElements = new List<Transform>();
		private List<Transform> mActiveElements = new List<Transform>();
		private bool mIsSpawning = true;
		private Transform[] mLanes;
		private float mSpanX;

		public void SetupLayer(float activeTime, Transform[] lanes, float spanX)
		{
			mLanes = lanes;
			mSpanX = spanX;
			for(int i = mActiveElements.Count - 1; i >= 0; --i)
				RemoveElement(i);
			mInactiveElements.Clear();
			mActiveElements.Clear();
			for(int i = 0; i < _layerRoot.childCount; ++i)
			{
				Transform child = _layerRoot.GetChild(i);
				child.gameObject.SetActive(false);
				mInactiveElements.Add(child);
			}
			GetNextElement(0);
		}

		public void Update(float speed, float screenLimit, float epsilon)
		{
			for(int i = 0; i < mActiveElements.Count; ++i)
			{
				Vector3 pos = mActiveElements[i].position;
				pos.x -= speed * Time.deltaTime * _layerSpeedMultiplier;
				if((i == mActiveElements.Count - 1)
				&& (mActiveElements[i].position.x >= 0 && pos.x <= 0)) //(pos.x < 0 && pos.x >= -epsilon))
					GetNextElement(-(mActiveElements[i].position.x + screenLimit));
				mActiveElements[i].position = pos;
			}
			for(int i = mActiveElements.Count - 1; i >= 0; --i)
			{
				if(mActiveElements[i].position.x < screenLimit)
					RemoveElement(i);
			}
		}

		public void Pushback(float distance) 
		{
			Vector3 pushbackVector = new Vector3(distance, 0, 0);
			for(int i = 0; i < mActiveElements.Count; ++i)
			{
				mActiveElements[i].position += pushbackVector;
			}
		}

		public void StartSpawning()
		{
			mIsSpawning = true;
		}

		public void StopSpawning()
		{
			mIsSpawning = false;
		}

		private void GetNextElement(float startX)
		{
			if(mInactiveElements.Count <= 0 || !mIsSpawning)
				return;

			// Fetch relevant sections
			int prevSectionLevel = 1;
			if(mActiveElements.Count > 0)
			{
				LevelSection prevSection = mActiveElements[mActiveElements.Count - 1].GetComponent<LevelSection>();
				if(prevSection != null)
					prevSectionLevel = prevSection.pEndingPlatformLevel;
			}
			List<Transform> mSearchElements = new List<Transform>(); 
			for(int i = 0; i < mInactiveElements.Count; ++i)
			{
				LevelSection sect = mInactiveElements[i].GetComponent<LevelSection>();
				if(sect == null || sect.pStartingPlatformLevel == prevSectionLevel)
					mSearchElements.Add(mInactiveElements[i]);
			}

			int rnd = Random.Range(0, mSearchElements.Count);
			Transform outTrans = mSearchElements[rnd];
			mActiveElements.Add(outTrans);
			//mInactiveElements.RemoveAt(rnd);
			mInactiveElements.Remove(outTrans);
			outTrans.gameObject.SetActive(true);
			LevelSection section = outTrans.GetComponent<LevelSection>();
			if(section != null)
				section.OnActivated(mLanes, mSpanX);
			outTrans.position = new Vector3(startX, outTrans.position.y, outTrans.position.z);
		}

		private void RemoveElement(int idx)
		{
			if(idx < 0 || idx >= mActiveElements.Count)
				return;
			Transform outTrans = mActiveElements[idx];
			mInactiveElements.Add(outTrans);
			mActiveElements.RemoveAt(idx);
			LevelSection section = outTrans.GetComponent<LevelSection>();
			if(section != null)
				section.OnDeactivated();
			outTrans.gameObject.SetActive(false);
		}

	}
}