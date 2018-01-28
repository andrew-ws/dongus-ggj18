using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;              //Static instance of AudioManager which allows it to be accessed by any other script.

    private SfxrSynth synth;
    private AudioSource HackingAudioSource;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;
        //If instance already exists and it's not this destroy since there can only be one
        else if (instance != this)
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start () {
        synth = new SfxrSynth();

        HackingAudioSource = gameObject.AddComponent<AudioSource>();
        HackingAudioSource.clip = Resources.Load<AudioClip>("HackSuccess");
        
    }

    public void SuccessfulHackSound()
    {
        //successful hack sound
        HackingAudioSource.Play();
    }

    public void MinionSpawnSound()
    {
        //Minion Spawn
        synth.parameters.SetSettingsString("0,.5,.343,.0537,,.2399,.3,.3721,.001,.3032,,,,,,,,,,,.229,,.652,-.031,,1,,,,,,");
        synth.CacheSound();
        //call when play
        synth.Play();
    }

    public void MissileSpawnSound()
    {
        //Missile Spawn
        synth.parameters.SetSettingsString("0,.5,.343,.275,,.2399,.3,.127,,.404,.026,.076,.047,.036,,,,,,,.201,.205,.652,-.217,-.473,1,,,,,,");
        synth.CacheSound();
        //call when play
        synth.Play();
    }

    public void MissileHitSound()
    {
        //Missile hit
        synth.parameters.SetSettingsString("3,.304,.06,.443,.128,.632,.253,.224,,-.255,,.301,,,,,.0323,,-.0199,,,-.0281,.102,.267,-.193,1,,,,-.0248,,");
        synth.CacheSound();
        //call when play
        synth.Play();
    }

    public void ActiveHackingSound()
    {
        //Hacking
        synth.parameters.SetSettingsString("1,.034,.694,.842,.168,1,.637,.2607,,.3446,,,,,,,,,,,,,.517,,,1,,,,,,");
        synth.CacheSound();
        //call when play
        synth.Play();
    }

    public void MinionKillSound()
    {
        //Minion kill
        synth.parameters.SetSettingsString("3,.143,.535,.764,.909,.995,.871,.861,.3278,-.314,-.916,.282,.266,.027,.42,.046,-.16,.143,-.257,.945,.6505,-.4465,.781,.0402,-.1868,.489,-.56,,.047,.843,1,-.327");
        synth.CacheSound();
        //call when play
        synth.Play();
    }
    
    public void SelectLaneSound()
    {
        //Select lane
        synth.parameters.SetSettingsString("0,.263,,.0729,.3909,.21,.3,.5154,,.0055,.0121,.031,,,.0152,,.3846,.6558,,,,.555,,.62,-.973,.9563,,.0476,,,.0281,.0359");
        synth.CacheSound();
        //call when play
        synth.Play();
    }
}
