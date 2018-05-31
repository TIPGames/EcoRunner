using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
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

		public void SetupLayer()
		{
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

		private void GetNextElement(float startX)
		{
			if(mInactiveElements.Count <= 0)
				return;
			int rnd = Random.Range(0, mInactiveElements.Count);
			Transform outTrans = mInactiveElements[rnd];
			mActiveElements.Add(outTrans);
			mInactiveElements.RemoveAt(rnd);
			outTrans.gameObject.SetActive(true);
			outTrans.position = new Vector3(startX, outTrans.position.y, outTrans.position.z);
		}

		private void RemoveElement(int idx)
		{
			if(idx < 0 || idx >= mActiveElements.Count)
				return;
			Transform outTrans = mActiveElements[idx];
			mInactiveElements.Add(outTrans);
			mActiveElements.RemoveAt(idx);
			outTrans.gameObject.SetActive(false);
		}

	}

	[SerializeField]
	private float _epsilon = 0.25f;
	[SerializeField]
	private float _minRunSpeed = 2;
	[SerializeField]
	private float _maxRunSpeed = 8;
	[SerializeField]
	private LayerInfo[] _layers;
	[SerializeField]
	private float _screenLimit = -20;

	private float mCurrXVelocity;
	// Use this for initialization
	void Start () 
	{
		mCurrXVelocity = _minRunSpeed;
		for(int i = 0; i < _layers.Length; ++i)
			_layers[i].SetupLayer();

	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateMovement();
	}
	private void UpdateMovement()
	{
		for(int i = 0; i < _layers.Length; ++i)
			_layers[i].Update(mCurrXVelocity, _screenLimit, _epsilon);
	}
}
