using System;
using UnityEngine;

namespace GG18.Minions {
	public abstract class Minion : MonoBehaviour {

		void Start () {
		}

		void Init () {}

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