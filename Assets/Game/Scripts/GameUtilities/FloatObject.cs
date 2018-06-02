using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class FloatObject : MonoBehaviour 
	{
		[SerializeField]
		private float _floatDistance = 0.5f;
		[SerializeField]
		private float _floatSpeed = 2;

		private bool mFloatUp = true;
		private float mStartPos = 0;
		private float mCurrPos = 0;
		private float mCurrFloatSpeed = 0;

		// Use this for initialization
		void Start () 
		{
			mStartPos = transform.localPosition.y;
			mCurrPos = mStartPos;
			mCurrFloatSpeed = _floatSpeed;
		}
		
		// Update is called once per frame
		void Update () 
		{
			mCurrPos += ((mFloatUp ? 1 : -1) * (mCurrFloatSpeed * Time.deltaTime));
			Vector3 pos = transform.localPosition;
			pos.y = mCurrPos;
			transform.localPosition = pos;
			if((mFloatUp && (mCurrPos > (mStartPos + _floatDistance)))
				|| (!mFloatUp && (mCurrPos < (mStartPos - _floatDistance))))
			{
				mFloatUp = !mFloatUp;
				mCurrFloatSpeed = _floatSpeed;
			}
		}
	}
}