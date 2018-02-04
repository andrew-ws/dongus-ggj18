using Rewired;
using System;
using UnityEngine;

namespace GG18.Minions {
    //at this point we could make a superclass to combine minions and missiles but time.
	public abstract class Minion : MonoBehaviour
    {

        [SerializeField] protected float speed;

        protected bool launched = false;
        protected Minion haltFor = null;
        public bool hacking = false;

        public Player player {get; protected set;}
		[SerializeField] protected float hp;
		[SerializeField] protected float dps;

        private AudioManager audioManager;

        public void Init(Player player, Material mat)
        {
            this.player = player;
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
            audioManager = gameController.GetComponent<AudioManager>();
            audioManager.MinionSpawnSound();

            //apply material to all
            foreach (var item in GetComponentsInChildren<MeshRenderer>())
            {
                item.material = mat;
            }
        }

        public void Fire()
        {
            //todo: calculate power and launch
            launched = true;
        }

        public virtual void Update()
        {
            if (launched && !haltFor && !hacking)
            {
                //move towards opposite terminal
                transform.Translate((player.id == 0 ? Vector3.right : Vector3.left) * speed * Time.deltaTime);
            }
        }

		public void TakeDamage(float dam) {
			hp -= dam;
			if (hp <= 0) {
				Destroy(gameObject);
			}
        }

        #region MonoBehaviour Messages
        private void OnTriggerEnter(Collider collider)
        {
			GameObject otherGO = collider.gameObject;
        }

        private void OnTriggerStay(Collider collider)
        {
			GameObject otherGO = collider.gameObject;
			if (otherGO.tag == "minion") {
				Minion otherMinion = otherGO.GetComponent<Minion>();
				if (otherMinion.player == player) {
					// lol this is totally gonna fuck up on very long frame delays but w/e
					float xDiff = otherGO.transform.position.x - transform.position.x;
					bool amIInBack = (player.id == 0) ? xDiff > 0 : xDiff < 0;
					if (amIInBack) {
                        haltFor = otherMinion;
                    }
				}
				else {
					haltFor = otherMinion;
					otherMinion.TakeDamage(dps * Time.deltaTime);
				}
			}
        }

        private void OnTriggerExit(Collider collider) {
            if (collider.gameObject.tag == "minion") {
                haltFor = null;
            }
        }

        private void OnDestroy()
        {
            audioManager.MinionKillSound();
        }
        #endregion
	}
}