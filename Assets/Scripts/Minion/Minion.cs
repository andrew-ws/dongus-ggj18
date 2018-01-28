using Rewired;
using System;
using UnityEngine;

namespace GG18.Minions {
    //at this point we could make a superclass to combine minions and missiles but time.
	public abstract class Minion : MonoBehaviour
    {

        [SerializeField] protected float speed;

        protected bool launched;

        public Player player {get; protected set;}
		private bool halt = false;
		[SerializeField] protected float hp;
		[SerializeField] protected float dps;

        private AudioManager audioManager;

        public void Init(Player player)
        {
            this.player = player;
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
            audioManager = gameController.GetComponent<AudioManager>();
            audioManager.MinionSpawnSound();
        }

        public void Fire()
        {
            //todo: calculate power and launch
            launched = true;
        }

        public virtual void Update()
        {
            if (launched && !halt)
            {
                //move towards opposite terminal
                transform.Translate((player.id == 0 ? Vector3.right : Vector3.left) * speed * Time.deltaTime);
            }
			halt = false;
        }

		public void TakeDamage(float dam) {
			hp -= dam;
			if (dam <= 0) {
				Destroy(gameObject);
			}
        }

        #region MonoBehaviour Messages
        private void OnCollisionEnter(Collision collision)
        {
			GameObject otherGO = collision.gameObject;
			if (otherGO.tag == "minion") {
				Minion otherMinion = otherGO.GetComponent<Minion>();
				if (otherMinion.player == player) {
					// lol this is totally gonna fuck up on very long frame delays but w/e
					float xDiff = otherGO.transform.position.x - transform.position.x;
					bool amIInBack = (player.id == 0) ? xDiff > 0 : xDiff < 0;
					if (amIInBack) halt = true;
				}
				else {
					halt = true;
					otherMinion.TakeDamage(dps * Time.deltaTime);
				}
			}
        }

        private void OnDestroy()
        {
            audioManager.MinionKillSound();
        }
        #endregion
	}
}