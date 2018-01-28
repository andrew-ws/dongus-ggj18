﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GG18.Missiles
{
    /// <summary>
    /// Moveable missile class
    /// </summary>
    public class Worm : Missile
    {
        protected override void Update()
        {
            base.Update();
            if (launched)
            {
                //control missile movement
                float axis = player.GetAxis("Missile Horizontal");
                transform.Translate(new Vector3(0, 0, axis * speed) * Time.deltaTime);
            }
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            GameObject otherGO = collision.gameObject;
            if (otherGO.tag == "terminal")
            {
                TerminalController.StunAll(player.id);
            }
        }
    }
}