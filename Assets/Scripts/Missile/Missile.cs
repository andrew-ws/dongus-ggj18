using System;
using UnityEngine;
using Rewired;

namespace GG18.Missiles
{
    public abstract class Missile : MonoBehaviour
    {
        [SerializeField] protected float power;
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

        public virtual void Fire()
        {
            launched = true;
        }

        protected virtual void Update()
        {
            if (launched)
            {
                //move towards opposite terminal
                transform.Translate(player.id == 0 ? Vector3.right : Vector3.left * speed * Time.deltaTime);
            }
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