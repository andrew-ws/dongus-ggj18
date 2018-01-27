using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour {

    const float HackTime = 5.0f;

    float hackTimeElapsed;

    public bool isHacked; //terminal will not set hackedby if already hacked
    bool isHacking;

    public int HackedBy;

    // Use this for initialization
    void Start () {

        isHacked = false;
        isHacking = false;
        hackTimeElapsed = 0;
    }


    // Update is called once per frame
    void Update () {
        
        if (!isHacked)
            //If opponent minion collides with terminal
            if (isHacking)
            {
                Transmitting();
            }

    }

    //process of hacking. call by terminal after minion collides
    public void Transmitting()
    {
        //if this is called by terminal
        if (!isHacking)
            isHacking = true;

        hackTimeElapsed += Time.deltaTime;

        if (hackTimeElapsed >= HackTime)
            isHacked = true;
    }

    //call this to stop hacking if minion dies 
    public void SeverConnection()
    {
        isHacking = false;
        hackTimeElapsed = 0;
    }
}
