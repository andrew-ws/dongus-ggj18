using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG18.Minions;

public class TerminalController : MonoBehaviour {

    public Transform MissileSpawn;

    public Player player;

    public GameObject Lane;
    public LaneController laneController;

	// Use this for initialization
	void Start () {
        laneController = Lane.GetComponent<LaneController>();	
	}
	
    //If a minion from the opposite team collides with the terminal, start hacking.
    //And change "HackedBy" to the opponent 
    private void OnTriggerEnter(Collider other)
    {
        //TODO: switch to player object
        if(other.tag == "minion" && other.gameObject.GetComponent<Minion>().player != player)
        {
            if (!laneController.isHacked)
            {
                laneController.HackedBy = player;
                laneController.UpdateHacking();
            }
        }
    }

    //If the minion from opposite team dies, sever connection.
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "minion" && other.gameObject.GetComponent<Minion>().player != player)
        {
            laneController.SeverConnection();
        }
    }
}
