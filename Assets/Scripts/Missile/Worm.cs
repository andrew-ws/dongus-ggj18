using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GG18.Missiles
{
    public class Worm : Missile
    {
        public override void Update()
        {
            base.Update();  

            if (Launched)
            {
                //todo: allow movement between lanes
            }
        }
    }
}