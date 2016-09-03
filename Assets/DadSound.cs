using UnityEngine;
using System.Collections;

public class DadSound : MonoBehaviour {
    public AudioClip[] HammerSounds;
    private AudioSource audioSource;
    private Animator animator;
    public  bool Hammering;
    private int i = 0;
    // Use this for initialization
	void Start () {
        Hammering = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audioSource.clip = HammerSounds[Random.Range(0, HammerSounds.Length)];
	}

	// Update is called once per frame
	void Update () {
        if (i > 62)
        {
            if (Random.Range(0, 3) == 1)
            {
                Hammering = !Hammering;
                if (Hammering)
                {
                    animator.SetTrigger("Hammer");
                }
                else
                {
                    animator.SetTrigger("Idle");
                }
            }
            if (Hammering)
            {
                audioSource.pitch = Random.Range(0.8f, 1f);
                audioSource.clip = HammerSounds[Random.Range(0, HammerSounds.Length)];
                audioSource.Play();
            }
            i = 0;
        }
        else
        {
            i++;
        }

	}
}
