using UnityEngine;

public class GameManager : MonoBehaviour {

    private AudioSource musicSource;

	void Start ()
    {
        //we don't need to play the song here since we can use the
        //'Play On Awake' property for the audio source. (nikki)
        musicSource = GetComponent<AudioSource>();
	}
}
