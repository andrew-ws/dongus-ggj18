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
                float axis = player.GetAxis("Missile Horizontal");
                if (axis > 0)
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                else if(axis < 0) 
                {
                    transform.Translate(Vector3.back * speed * Time.deltaTime);
                }
            }
        }
    }
}