using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using GG18.Missiles;
using GG18.Minions;

public class PlayerController : MonoBehaviour {

    #region Variables
    public int id;

    [Header("Missile Settings")]
    [SerializeField] private Transform missileSpawn;
    [SerializeField] private Material missileMaterial;

    private Player player;

    //minion stuff
    private Minion currentMinion; //the currently spawning minion
    private bool isPayload;
    private float lastMinionTime;
    private float minionChargeTime;

    //missile stuff
    private Missile currentMissile;
    private bool isWorm; //quick hack to know if minions have been swapped
    private float lastMissileTime;
    private float missileChargeTime;

    //consts
    private const float SELECT_COOLDOWN = 0.2f; //cooldown time for lane selection
    private const float MINION_COOLDOWN = 2; //cooldown before you can spawn another minion
    private const float MINION_CHARGE_MAX = 4; //time for missile to be fully charged
    private const float MISSILE_COOLDOWN = 5; //cooldown before you can file another missile
    private const float MISSILE_CHARGE_MAX = 3; //time for missile to be fully charged
    
    //lane select
    private int lane;
    float lastSelectTime = 0;
    private float[] zAxis = { -3.5f, 0, 3.5f };
    #endregion

    // Use this for initialization
    void Start () {
		player = ReInput.players.GetPlayer(id);

        //setup lane selection
        lane = 1;
        transform.position = (player.id == 0) ? new Vector3(-8.5f, .01f, zAxis[lane]) : new Vector3(8.5f, .01f, zAxis[lane]);
    }
	
	// Update is called once per frame
	void Update () {
        #region Lanes
        if (lastSelectTime + SELECT_COOLDOWN < Time.time)
        {
            float laneDir = player.GetAxis("Lane Horizontal");
            lastSelectTime = Time.time;

            //Move lane depending on player input
            if (player.id == 0) //player 1
            {
                if (laneDir < 0)
                    lane++;
                else if (laneDir > 0)
                    lane--;
            }
            else //player 2
            {
                if (laneDir > 0)
                    lane++;
                else if (laneDir < 0)
                    lane--;
            }

            //Set player position in lane 
            transform.position = (player.id == 0) ? new Vector3(-8.5f, .01f, zAxis[Mathf.Abs(lane % 3)]) : new Vector3(8f, .01f, zAxis[Mathf.Abs(lane % 3)]);
        }
        #endregion

        #region Minions
        if (player.GetButtonDown("Spawn Minion"))
        {
            if (lastMinionTime + MINION_COOLDOWN < Time.time || currentMinion == null)
            {
                SpawnMinion("Packet");
            }
        }
        else if (currentMinion != null)
        {
            if (player.GetButton("Spawn Minion"))
            {
                if (minionChargeTime >= MINION_CHARGE_MAX && !isPayload)
                {
                    //swap minions
                    Destroy(currentMinion);
                    SpawnMissile("Payload");
                    isPayload = true;
                }
                else
                {
                    //charge packet into payload
                    minionChargeTime += Time.deltaTime;
                }
            }
            else if (player.GetButtonUp("Spawn Minion"))
            {
                currentMinion.Fire();
                minionChargeTime = 0;
            }
        }
        #endregion

        #region Missiles
        if (player.GetButtonDown("Spawn Missile"))
        {
            if (lastMissileTime + MISSILE_COOLDOWN < Time.time || currentMissile == null)
            {
                //spawn initial missile
                SpawnMissile("Ping");
            }
        }
        else if (currentMissile != null) 
        {
            if (player.GetButton("Spawn Missile"))
            {
                if (missileChargeTime >= MISSILE_CHARGE_MAX && !isWorm)
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

                currentMissile.Fire();
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

    #region Spawn Methods
    private void SpawnMissile(string name)
    {
        currentMissile = Instantiate(Resources.Load<Missile>("Missiles/" + name)) as Missile;
        currentMissile.transform.position = transform.position;
        currentMissile.Init(player, missileMaterial);
    }

    private void SpawnMinion(string name)
    {
        currentMinion = Instantiate(Resources.Load<Minion>("Minions/" + name)) as Minion;
        currentMinion.transform.position = transform.position;
        currentMinion.Init(player);
    }
    #endregion
}
