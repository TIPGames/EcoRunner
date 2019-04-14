using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class PlatformObstacle : Obstacle 
	{

		// Use this for initialization
		void Start () 
		{
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}

		public override void OnTriggerEnter2D(Collider2D c)
		{
			Player player = c.GetComponent<Player>();
			player.OnPlatformObstacleHit(this);
		}
	}
}