using Rewired;
using System;
using UnityEngine;

namespace GG18.Minions {

    //at this point we could make a superclass to combine minions and missiles but time.
	public abstract class Minion : MonoBehaviour
    {
        [SerializeField] protected float speed;

        protected bool launched;

        // Set only to 1 and 2 for player 1 and player 2
        public int PlayerIndex; //the minion's player allegiance 

		public void Init(Player player)
        {
            PlayerIndex = player.id;
		}

		public virtual void Fire()
        {
            launched = true;
        }

        #region MonoBehaviour Messages
        private void OnCollisionEnter(Collision collision)
        {

        }
        #endregion
	}
}