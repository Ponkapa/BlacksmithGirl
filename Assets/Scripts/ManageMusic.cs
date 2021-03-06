﻿using UnityEngine;
using System.Collections;

public class ManageMusic : MonoBehaviour {
    public static GameObject instance;
    public AudioClip finalMusic;
    public AudioClip[] audioClips;
    private AudioSource audiosource;
    void Awake()
    {
        if (instance == null)

            //if not, set instance to this
            instance = this.gameObject;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
	void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void FinalBossFight()
    {
        audiosource.clip = finalMusic;
        audiosource.Play();
    }
    void OnLevelWasLoaded(int level)
    {
        if (audiosource && level != 10)
        {
            audiosource.clip = audioClips[level];
            audiosource.Play();
        }
    }

}
