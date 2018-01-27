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
    [SerializeField] private const float MISSILE_COOLDOWN = 5; //cooldown before you can file another missile
    [SerializeField] private const float CHARGE_TIME = 3; //time for missile to be fully charged
    [SerializeField] private const float inputCooldown = .2f; //input cooldown 

    //time elapsed for input cooldown
    float timeElapsed = 0;

    //lane select
    float[] zAxis = { -3.5f, 0, 3.5f };
    int lane;
    #endregion

    // Use this for initialization
    void Start () {
		player = ReInput.players.GetPlayer(id);
        lane = 1;
        transform.position = (player.id == 0) ? new Vector3(-8.5f, .01f, zAxis[lane]) : new Vector3(8.5f, .01f, zAxis[lane]);
    }
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime; //add time to elapsed

        #region Lanes
        if (timeElapsed >= inputCooldown)
        {
            timeElapsed = 0;
            //Move lane depending on player input
            float laneDir = player.GetAxis("Lane Horizontal");
            if (player.id == 0)
            {
                if (laneDir < 0)
                {
                    lane++;
                }
                else if (laneDir > 0)
                {
                    lane--;
                }
            }
            else
            {
                if (laneDir > 0)
                {
                    lane++;
                }
                else if (laneDir < 0)
                {
                    lane--;
                }
            }
            //Set player position in lane 
            transform.position = (player.id == 0) ? new Vector3(-8.5f, .01f, zAxis[Mathf.Abs(lane % 3)]) : new Vector3(8f, .01f, zAxis[Mathf.Abs(lane % 3)]);
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
        currentMissile = Resources.Load<Missile>("Missiles/" + name);
        currentMissile.transform.position = transform.position;
        currentMissile.Init(player, missileMaterial);
    }
}
