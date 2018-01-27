using System;
using UnityEngine;
using Rewired;

namespace GG18.Missiles
{
    public abstract class Missile : MonoBehaviour
    {
		// Set only to 1 and 2 for player 1 and player 2
		public int PlayerIndex;

        [SerializeField] protected float speed;

        protected bool launched;
        public Action MissileDestroyed;

        protected Player player;

        public void Init(Player player, Material mat)
        {
            this.player = player;

            //apply player's missile material to the model
            GetComponent<MeshRenderer>().material = mat;
        }

        public void Fire(float chargeTime)
        {
            //todo: calculate power and launch
            launched = true;
        }

        public virtual void Update()
        {

        }

        #region MonoBehaviour Messages
        private void OnCollisionEnter(Collision collision)
        {
            //todo: check if collision is with opposite team
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (MissileDestroyed != null)
            {
                MissileDestroyed();
            }
        }
        #endregion
    }
}