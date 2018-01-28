using System;
using UnityEngine;
using Rewired;

namespace GG18.Missiles
{
    public abstract class Missile : MonoBehaviour
    {
        [SerializeField] protected float dmg;
        [SerializeField] protected float speed;

        protected bool launched;
        public Action MissileDestroyed;

        public Player player {get; protected set;}

        public void Init(Player player)
        {
            this.player = player;
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
                transform.Translate((player.id == 0 ? Vector3.right : Vector3.left) * speed * Time.deltaTime);
            }
        }

        #region MonoBehaviour Messages
        private void OnCollisionEnter(Collision collision)
        {
            GameObject otherGO = collision.gameObject;
            if (otherGO.tag == "minion") {
                Minions.Minion otherMinion = otherGO.GetComponent<Minions.Minion>();
                if (otherMinion.player != player) {
                    otherMinion.TakeDamage(dmg);
                    Destroy(gameObject);
                }
            }
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