using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GG18.Missiles
{
    /// <summary>
    /// Non-moveable missile class
    /// </summary>
    public class Ping : Missile
    {
        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            GameObject otherGO = collision.gameObject;
            if (otherGO.tag == "terminal")
            {
                TerminalController terminal = otherGO.GetComponent<TerminalController>();
                if (terminal)
                    terminal.Stun();
            }
        }
    }
}