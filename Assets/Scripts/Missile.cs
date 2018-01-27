﻿using System;
using UnityEngine;

public class Missile : MonoBehaviour
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

    public void Update()
    {
        if (Launched)
        {
            //todo: move along the x axis towards the other player's side
        }
    }

    #region MonoBehaviour Messages
    private void OnCollisionEnter(Collision collision)
    {
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