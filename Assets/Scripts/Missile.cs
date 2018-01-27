using System;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Action MissileDestroyed;

    public void Init(Material mat)
    {
        //apply player's missile material to the model
        GetComponent<MeshRenderer>().material = mat;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        if (MissileDestroyed != null)
        {
            MissileDestroyed();
        }
    }
}