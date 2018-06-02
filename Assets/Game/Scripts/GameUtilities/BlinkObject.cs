using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	public class BlinkObject : MonoBehaviour 
	{

		[SerializeField]
		private float _blinkDuration = 2;
		[SerializeField]
		private float _blinkPeriod = 0.5f;

		private float mBlinkStartTime;
		private bool mIsBlinking = false;
		private bool mIsVisibleAfterBlink = true;

		public float pBlinkDuration { get { return _blinkDuration; } }

		public void Update()
		{
			if(mIsBlinking)
			{

			}
		}

		public void StartBlink(bool visibleAfterBlink)
		{
			mBlinkStartTime = Time.realtimeSinceStartup;
			if(!mIsBlinking)
			{
				mIsBlinking = true;
				mIsVisibleAfterBlink = visibleAfterBlink;
				StartCoroutine(BlinkProc());
			}
		}

		private IEnumerator BlinkProc()
		{
			bool currVisibleState = true;
			SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>(true);
			while(true)
			{
				currVisibleState = !currVisibleState;
				for(int i = 0; i < renderers.Length; ++i)
					renderers[i].enabled = currVisibleState;
				yield return new WaitForSeconds(_blinkPeriod);
				if(_blinkDuration <= (Time.realtimeSinceStartup - mBlinkStartTime))
					break;
			}
			for(int i = 0; i < renderers.Length; ++i)
				renderers[i].enabled = mIsVisibleAfterBlink;
			mIsBlinking = false;
		}

		[ContextMenu("Test Blink")]
		private void TestBlink()
		{
			StartBlink(true);
		}
	}
}