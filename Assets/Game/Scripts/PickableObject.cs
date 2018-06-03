using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class PickableObject : MonoBehaviour 
	{

		[SerializeField]
		private ParticleSystem _pickedUpEffect;

		public virtual void Start()
		{
			
		}

		public virtual void Update()
		{
			
		}

		public void OnTriggerEnter2D(Collider2D c)
		{
			Player player = c.GetComponent<Player>();
			OnObjectPicked(player);
			if(_pickedUpEffect != null)
				_pickedUpEffect.Play();
			GetComponent<SpriteRenderer>().enabled = false;
		}

		protected virtual void OnObjectPicked(Player player)
		{

		}
	}
}