using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class Obstacle : MonoBehaviour 
	{

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
			Debug.Log(c.tag);
			Player player = c.GetComponent<Player>();
			player.OnObstacleHit(this);
		}
	}
}