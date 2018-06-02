using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class Collectable : MonoBehaviour 
	{
		[SerializeField]
		private int _score = 1;

		public int pScore { get { return _score; } }

		// Use this for initialization
		void Start () 
		{
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}

		public void OnTriggerEnter2D(Collider2D c)
		{
			Player player = c.GetComponent<Player>();
			player.OnCollected(this);
	//		gameObject.SetActive(false);
		}
	}
}