using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GG18.Missiles
{
    /// <summary>
    /// Moveable missile class
    /// </summary>
    public class Worm : Missile
    {
        public override void Update()
        {
            if (launched)
            {
                //control missile movement
                float axis = player.GetAxis("Missile Horizontal");
                transform.Translate(new Vector3(0, 0, axis * speed) * Time.deltaTime);
            }
        }
    }
}