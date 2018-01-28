using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG18.Minions;

public class TerminalController : MonoBehaviour {

    public Transform MissileSpawn;

    public int PlayerIndex; //SET ONLY TO 1 or 2 for player 1 and player 2 

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
        if(other.tag == "minion" && other.gameObject.GetComponent<Minion>().PlayerIndex != PlayerIndex)
        {
            if (!laneController.isHacked)
            {
                laneController.HackedBy = PlayerIndex;
                laneController.UpdateHacking();
            }
        }
    }

    //If the minion from opposite team dies, sever connection.
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "minion" && other.gameObject.GetComponent<Minion>().PlayerIndex != PlayerIndex)
        {
            laneController.SeverConnection();
        }
    }
}
