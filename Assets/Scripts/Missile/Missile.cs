using System;
using UnityEngine;

namespace GG18.Missiles
{
    public abstract class Missile : MonoBehaviour
    {
        public bool Launched { get; private set; }
        public Action MissileDestroyed;

        public void Init(Material mat)
        {
            //apply player's missile material to the model
            GetComponent<MeshRenderer>().material = mat;
        }

        public void Fire(float chargeTime)
        {
            //todo: calculate power and launch
            Launched = true;
        }

        public virtual void Update()
        {
            if (Launched)
            {
                //todo: move along the x axis towards the other player's side
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