using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using GG18.Missiles;

public class PlayerController : MonoBehaviour {

    #region Variables
    public int id;

    [Header("Missile Settings")]
    [SerializeField] private Transform missileSpawn;
    [SerializeField] private Material missileMaterial;

    private Player player;

    //missile stuff
    private Missile currentMissile;
    private bool isWorm; //quick hack to know if missile has already been swapped
    private float lastMissileTime;
    private float missileChargeTime;

    //consts
    private const float MISSILE_COOLDOWN = 5; //cooldown before you can file another missile
    private const float CHARGE_TIME = 3; //time for missile to be fully charged
    #endregion

    // Use this for initialization
    void Start () {
		player = ReInput.players.GetPlayer(id);
	}
	
	// Update is called once per frame
	void Update () {
        #region Lanes
        float laneDir = player.GetAxis("Lane Horizontal");

        if (player.GetButtonDown("Select Lane"))
        {

        }
        #endregion

        #region Missile
        if (player.GetButtonDown("Spawn Missile"))
        {
            //missile is in cooldown
            if (lastMissileTime < lastMissileTime + MISSILE_COOLDOWN || currentMissile != null) return;

            SpawnMissile("Ping");
        }
        else if (currentMissile != null)
        {
            if (player.GetButton("Spawn Missile"))
            {
                if (missileChargeTime >= CHARGE_TIME && !isWorm)
                {
                    //swap missiles
                    Destroy(currentMissile);
                    SpawnMissile("Worm");
                    isWorm = true;
                }
                else
                {
                    //charge ping into worm
                    missileChargeTime += Time.deltaTime;
                }
            }
            else if (player.GetButtonUp("Spawn Missile"))
            {
                currentMissile.MissileDestroyed += OnMissileDestroyed;

                //swap control maps to missile controls
                player.controllers.maps.SetMapsEnabled(false, "Default");
                player.controllers.maps.SetMapsEnabled(true, "Missile");

                currentMissile.Fire(missileChargeTime);
                missileChargeTime = 0;
            }
        }
        #endregion
    }

    //called when currentMissile has been destroyed
    private void OnMissileDestroyed()
    {
        lastMissileTime = Time.time;

        //swap control maps back to lane controllers
        player.controllers.maps.SetMapsEnabled(false, "Default");
        player.controllers.maps.SetMapsEnabled(true, "Missile");
    }

    //create new missile
    private void SpawnMissile(string name)
    {
        currentMissile = Resources.Load<Missile>(name);
        currentMissile.transform.position = missileSpawn.position;
        currentMissile.Init(player, missileMaterial);
    }
}
