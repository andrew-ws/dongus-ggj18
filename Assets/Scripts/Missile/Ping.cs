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
        protected override void OnTriggerEnter(Collider collider)
        {
            base.OnTriggerEnter(collider);

            GameObject otherGO = collider.gameObject;
            if (otherGO.tag == "terminal")
            {
                TerminalController terminal = otherGO.GetComponent<TerminalController>();
                if (terminal)
                    terminal.Stun();
            }
        }
    }
}