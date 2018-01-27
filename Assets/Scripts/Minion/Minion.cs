using System;
using UnityEngine;

namespace GG18.Minions {
	public abstract class Minion : MonoBehaviour {

		// Set only to 1 and 2 for player 1 and player 2
		public int PlayerIndex;

		void Start () {
		}

		void Init () {
		}

		void Fire () {}
		
		void Update ()
		{
		}

        #region MonoBehaviour Messages
        private void OnCollisionEnter(Collision collision)
        {
        }

        private void OnDestroy()
        {
        }
        #endregion
	}
}