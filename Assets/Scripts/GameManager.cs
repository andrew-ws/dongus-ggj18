using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private AudioSource musicSource;
    public int PlayerOneTally;
    public int PlayerTwoTally;

    public Text p1ScoreText;
    public Text p2ScoreText;

	void Start ()
    {
        PlayerOneTally = 0;
        PlayerTwoTally = 0;
        //we don't need to play the song here since we can use the
        //'Play On Awake' property for the audio source. (nikki)
        musicSource = GetComponent<AudioSource>();
	}

    private void Update()
    {
        p1ScoreText.text = "Terminals Hacked: " + PlayerOneTally;
        p2ScoreText.text = "Terminals Hacked: " + PlayerTwoTally;


        //If one player has 2 lanes then game over. 
        if (PlayerOneTally == 2 || PlayerTwoTally == 2)
        {
            //TODO: Trigger game over
        }
    }
}
