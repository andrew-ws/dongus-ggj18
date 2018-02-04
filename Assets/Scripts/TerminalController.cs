using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG18.Minions;
using System;
using System.Linq;

public class TerminalController : MonoBehaviour {

    public Transform MissileSpawn;

    [SerializeField] private int id;
    public Player player;

    public GameObject Lane;
    public LaneController laneController;

    public Light screenLight;

    [SerializeField] private float stunTime = 1;

    public bool IsStunned { get; private set; }

	// Use this for initialization
	void Start () {
        player = ReInput.players.GetPlayer(id);
        laneController = Lane.GetComponent<LaneController>();	
	}

    #region MonoBehaviour Messages
    //If a minion from the opposite team collides with the terminal, start hacking.
    //And change "HackedBy" to the opponent 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "minion" && other.gameObject.GetComponent<Minion>().player != player)
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
        if (other.gameObject.tag == "minion" && other.gameObject.GetComponent<Minion>().player != player)
        {
            laneController.SeverConnection();
        }
    }
    #endregion

    #region Stun Methods
    //stun terminal, resetting the stun if already stunned
    public void Stun()
    {
        if (IsStunned) StopCoroutine("StunRoutine");
        StartCoroutine(StunRoutine());
    }

    public IEnumerator StunRoutine()
    {
        IsStunned = true;
        yield return new WaitForSeconds(stunTime);
        IsStunned = false;
    }

    //stuns all terminals of a certain side
    public static void StunAll(int stunnedBy)
    {
        int targetId = stunnedBy == 0 ? 1 : 0; //get id of who to hack based on who the terminal stunned by
        //not optimal but was quicker to type
        TerminalController[] terminals = (TerminalController[])FindObjectsOfType<TerminalController>().Where(n => n.id == targetId);

        foreach (TerminalController t in terminals)
        {
            t.Stun();
        }
    }
    #endregion
}
