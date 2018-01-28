using System;
using UnityEngine;
using Rewired;

namespace GG18.Missiles
{
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class Missile : MonoBehaviour
    {
        [SerializeField] protected float dmg;
        [SerializeField] protected float speed;

        public bool launched { get; private set; }
        public Action MissileDestroyed;

        public Player player {get; protected set;}

        private AudioManager audioManager;


        public void Init(Player player, Material mat)
        {
            this.player = player;
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
            audioManager = gameController.GetComponent<AudioManager>();

            //hack to get this working
            //apply material to all
            foreach (var item in GetComponentsInChildren<MeshRenderer>())
            {
                item.material = mat;
            }
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
        protected virtual void OnCollisionEnter(Collision collision)
        {
            GameObject otherGO = collision.gameObject;
            if (otherGO.tag == "minion") //minion collision
            {
                Minions.Minion otherMinion = otherGO.GetComponent<Minions.Minion>();
                if (otherMinion.player != player) {
                    otherMinion.TakeDamage(dmg);
                    Destroy(gameObject);
                    audioManager.MissileHitSound();
                }
            }
            else if (otherGO.tag == "terminal")
            {
                TerminalController otherTerminal = otherGO.GetComponent<TerminalController>();
                if (otherTerminal.player != player)
                {
                    audioManager.MissileHitSound();
                    Destroy(gameObject);
                }
            }
            else if (otherGO.tag == "missile") //missile collision
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("nothing happened");
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