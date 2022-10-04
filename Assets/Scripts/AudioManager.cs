using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool pitchRandom;
    public Sound[] sounds;

    [HideInInspector]
    public int currentLevel;
    [HideInInspector]
    public int currentSubLevel = 1;
    [HideInInspector]
    public int characterSelected;
    [HideInInspector]
    public int curPower;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);



        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }

        currentLevel = 1;
        currentSubLevel = 1;
        characterSelected = 0;
        curPower = 0;
    }

    private void Start()
    {


        FindObjectOfType<AudioManager>().Play("MainMusic");

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("sound: " + name + " not found");
            return;
        }

        s.source.volume = s.volume;

        if (pitchRandom == true)
        {
            s.source.pitch = UnityEngine.Random.Range(.6f, 1.1f);

        }
        else if (pitchRandom == false)
        {
            s.source.pitch = s.pitch;
        }

        pitchRandom = false; //reset
        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }


        s.source.Stop();
    }



}
