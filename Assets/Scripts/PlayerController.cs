using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {

    #region Variables
    public int id;

    [Header("Missile Settings")]
    [SerializeField] private Transform missileSpawn;
    [SerializeField] private Material missileMaterial;

    private Player player;
    private Missile currentMissile;
    private float lastMissileTime; //todo: should be based on time AFTER missile explodes
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

            //create new missile
            currentMissile = Resources.Load<Missile>("Missile");
            currentMissile.transform.position = missileSpawn.position;
            currentMissile.Init(missileMaterial);
            currentMissile.MissileDestroyed += OnMissileDestroyed;
        }
        else if (currentMissile != null)
        {
            if (player.GetButton("Spawn Missile"))
            {
                //charge missile
                missileChargeTime += Time.deltaTime;

                //todo: scale missile asset based on time held down and increase power level if applicable
            }
            else if (player.GetButtonUp("Spawn Missile"))
            {
                //todo: swap
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
        //todo: swap controls back to standard lane controls
    }
}
