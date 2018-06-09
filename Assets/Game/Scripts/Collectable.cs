using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using com.partho.games.utilities;

namespace com.tip.games.ecorunner
{
	public class Collectable : PickableObject, IPoolGameObject  
	{
		[SerializeField]
		private int _score = 1;

		private static List<Collectable> mActiveCollectables = new List<Collectable>();

		private Player mPullTarget;
		private FloatObject mFloater;
		private float mGravitateSpeed = 15;
		private GameObjectPool mPool;

		public int pScore { get { return _score; } }
		public static ReadOnlyCollection<Collectable> pActiveCollectables 
		{ 
			get { return mActiveCollectables.AsReadOnly(); } 
		}
		public bool pIsBeingPulled 
		{
			get { return (mPullTarget != null); } 
		}

		// Use this for initialization
		public override void Start () 
		{
			base.Start();
			mFloater = GetComponent<FloatObject>();
		}
		
		// Update is called once per frame
		public override void Update () 
		{
			base.Update();
			if(pIsBeingPulled)
			{
				Vector3 pos = transform.position;
				Vector3 dir = mPullTarget.transform.position - pos;
				pos += mGravitateSpeed * Time.deltaTime * dir.normalized;
				transform.position = pos;
			}
		}

		public void Activate()
		{
			if(!mActiveCollectables.Contains(this))
				mActiveCollectables.Add(this);
			if(mFloater != null)
				mFloater.enabled = true;
			if(name == "Collectable4")
				Debug.Log("Float enabled");
		}

		public void Deactivate()
		{
			if(mPool != null)
				mPool.ReleaseObject(gameObject);
			else
				OnDeactivated();
		}

		public void PullTowards(Player player, float speed)
		{
			mPullTarget = player;
			mGravitateSpeed = speed;
			if(mFloater != null)
				mFloater.enabled = false;
			if(name == "Collectable4")
				Debug.Log("Float Disabled");
		}

		protected override void OnObjectPicked(Player player)
		{
			player.OnCollected(this);
			Deactivate();
		}

		#region __IPoolGameobject__Implementation__
		public void OnCreated(GameObjectPool pool)
		{
			mPool = pool;
		}

		public void OnDestroyed()
		{

		}

		public void OnActivated()
		{
			Activate();
		}

		public void OnDeactivated()
		{
			if(mActiveCollectables.Contains(this))
				mActiveCollectables.Remove(this);
			mPullTarget = null;
		}
		#endregion
	}
}