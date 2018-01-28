using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour {

    const float HackTime = 5.0f;

    float hackTimeElapsed;

    public bool isHacked; //terminal will not set hackedby if already hacked
    bool isHacking;

    public Player HackedBy;

    public GameObject TerminalP1;
    public GameObject TerminalP2;

    private GameObject gameController;
    private AudioManager audioManager; 

    // Use this for initialization
    void Start () {

        isHacked = false;
        isHacking = false;
        hackTimeElapsed = 0;
        gameController = GameObject.FindGameObjectWithTag("GameController");
        audioManager = gameController.GetComponent<AudioManager>();
    }


    // Update is called once per frame
    void Update () {

        if (!isHacked)
        {
            //If opponent minion collides with terminal
            if (isHacking)
            {
                UpdateHacking();
            }
        }
    }

    //process of hacking. call by terminal after minion collides
    public void UpdateHacking()
    {
        //if this is called by terminal
        if (!isHacking)
        {
            isHacking = true;
            audioManager.ActiveHackingSound();
        }
        hackTimeElapsed += Time.deltaTime;

        if (hackTimeElapsed >= HackTime)
        {
            isHacked = true;
            audioManager.SuccessfulHackSound();
            if (HackedBy.id == 0)
            {
                TerminalP2.GetComponent<TerminalController>().screenLight = TerminalP1.GetComponent<TerminalController>().screenLight;
                gameController.GetComponent<GameManager>().PlayerOneTally++;
            }
            else
            {
                TerminalP1.GetComponent<TerminalController>().screenLight = TerminalP2.GetComponent<TerminalController>().screenLight;
                gameController.GetComponent<GameManager>().PlayerTwoTally++;
            }
        }
    }

    //call this to stop hacking if minion dies 
    public void SeverConnection()
    {
        isHacking = false;
        hackTimeElapsed = 0;
        audioManager.Stop(); //stop active hacking sound
    }
}
