using UnityEngine;
using System.Collections;

public class Talking : MonoBehaviour {
    public string Attack;
    public bool talking;
    public string Speech;
    public bool beginTalking;
    private int endTalkTime = 30;
    private int maxChars;
    private int charDelay;
    private int i;
    private int LineBreak;
    private string randText;
    public AudioClip[] voices;
    private AudioSource audioSource;
    public TextMesh textMesh;
	// Use this for initialization
	void Start () {
        textMesh = GetComponentInChildren<TextMesh>();
        Speech = "";
        randText = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.7f, 0.9f);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown(Attack) && talking)
        {
            audioSource.clip = voices[Random.Range(0, voices.Length)];
            beginTalking = true;
            Speech = "";
            maxChars = Random.Range(10, 20);
            charDelay = Random.Range(6, 10);
            LineBreak = maxChars / 2 - 1 + Random.Range(0, 3);
        }
        if (beginTalking)
        {
            i++;
            if (Speech.Length < maxChars)
            {
                if (i > charDelay)
                {
                    if (Speech.Length == LineBreak)
                    {
                        Speech += "\n";
                    }
                    Speech += randText[Random.Range(0, randText.Length)];
                    i = 0;
                    audioSource.Play();
                }
            }
            else
            {
                if (i > endTalkTime)
                {
                    Speech = "";
                    beginTalking = false;
                }
            }
        }
        textMesh.text = Speech;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            talking = true;
            other.GetComponent<PlayerController>().talking = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            talking = false;
            other.GetComponent<PlayerController>().talking = false;
        }
    }
}
