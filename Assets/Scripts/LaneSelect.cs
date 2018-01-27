using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class LaneSelect : MonoBehaviour {

    public int id;

    private Player player;

    float[] zAxis = { -3.5f, 0, 3.5f };
    int lane;  

	// Use this for initialization
	void Start () {
        player = ReInput.players.GetPlayer(id);
        lane = 1;
        transform.position = (id == 0) ? new Vector3(-10f, .01f, zAxis[lane]) : new Vector3(10f, .01f, zAxis[lane]);
    }

    // Update is called once per frame
    void Update () {

        float laneDir = player.GetAxis("Lane Horizontal");

        if (laneDir > 0)
        {
            lane++;
            lane%= 3;
            Debug.Log(lane);

        }
        else if (laneDir < 0)
        {
            lane--;

            lane%= 3;
            Debug.Log(lane);

        }
        else
        {
            return;
        }


        transform.position = (id == 0) ? new Vector3(-8.5f, .01f, zAxis[Mathf.Abs(lane)]) : new Vector3(8f, .01f, zAxis[lane]);

    }
}
