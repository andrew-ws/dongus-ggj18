using System;
using UnityEngine;
using Rewired;

namespace GG18.Missiles
{
    public abstract class Missile : MonoBehaviour
    {
        [SerializeField] protected float speed;

        protected bool launched;
        public Action MissileDestroyed;

        public Player player {get; protected set;}

        public float dmg;

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
            if (launched)
            {
                //move towards opposite terminal
                transform.Translate(player.id == 0 ? Vector3.right : Vector3.left * speed * Time.deltaTime);
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