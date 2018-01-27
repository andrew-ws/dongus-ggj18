using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {

    public int id;
    private Player player;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(id);
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 axis = player.GetAxis2D("Horizontal", "Vertical");

        if (player.GetButtonDown("Select"))
        {

        }

        if (player.GetButtonDown("Rocket"))
        {

        }
    }
}
