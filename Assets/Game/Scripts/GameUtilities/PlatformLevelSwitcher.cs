using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tip.games.ecorunner
{
	[RequireComponent(typeof(BoxCollider2D))]
    public class PlatformLevelSwitcher : MonoBehaviour
    {

        [SerializeField]
        private int _rightCollisionPlatformLevel = 1;
        [SerializeField]
        private int _leftCollisionPlatformLevel = 1;

        private BoxCollider2D _switchPlatformCollider;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void OnTriggerEnter2D(Collider2D c)
		{
			Player player = c.GetComponent<Player>();
			Vector3 pRight = player.transform.right;
            Vector3 obsRight = transform.right;
            if(Vector3.Dot(pRight, obsRight) >= 0)
                player.OnSwitchPlatform(_leftCollisionPlatformLevel);
            else
                player.OnSwitchPlatform(_rightCollisionPlatformLevel);
		}
    }
}