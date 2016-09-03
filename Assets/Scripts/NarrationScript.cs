using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NarrationScript : MonoBehaviour {
    public bool talking;
    public string Speech;
    public string[] Narratives;
    public bool beginTalking;
    private int endTalkTime = 240;
    private int maxChars;
    private int charDelay;
    private int i;
    private int LineBreak;
    private string randText;
    public AudioClip[] voices;
    private AudioSource audioSource;
    public Text textMesh;
    public static bool AlreadyStarted;
    public static bool Town2;
    public static bool notTown2;
    public static bool notSeenEnd;
    public static int currentNarrative = 0;
    private int j = 0;
    private bool FindBreak;
    private bool BreakOut;
    private int charsFound;
    // Use this for initialization
    void Start () {
        textMesh = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        Speech = "";
    }
	
	// Update is called once per frame
	void Update () {
        if (!AlreadyStarted)
        {
            audioSource.clip = voices[0];
            beginTalking = true;
            Speech = "";
            charDelay = 3;
            AlreadyStarted = true;
        }
        if (PlayerController.gameComplete && !notSeenEnd)
        {
            charDelay = 3;
            beginTalking = true;
            currentNarrative = 2;
            notSeenEnd = true;
        }
        if (Town2 && !notTown2)
        {
            charDelay = 3;
            beginTalking = true;
            currentNarrative = 1;
            notTown2 = true;
        }
        if (beginTalking)
        {
            i++;
            if (!BreakOut && j < Narratives[currentNarrative].Length)
            {
                if (charsFound > 100)
                {
                    FindBreak = true;
                }
                if (i > charDelay)
                {
                    Speech += Narratives[currentNarrative][j];
                    j++;
                    charsFound++;
                    i = 0;
                    audioSource.Play();
                }
                if (FindBreak && (Narratives[currentNarrative][j].CompareTo('.') == 0 || Narratives[currentNarrative][j].CompareTo('!') == 0 || Narratives[currentNarrative][j].CompareTo('?') == 0))
                {
                    if (Narratives[currentNarrative][j+1].CompareTo(' ') == 0)
                    {
                        Speech += Narratives[currentNarrative][j];
                        j++;
                        i = 0;
                        audioSource.Play();
                        BreakOut = true;
                        charsFound = 0;
                    }
                }
            }
            else
            {
                if (i > endTalkTime)
                {
                    Speech = "";
                    if (j >= Narratives[currentNarrative].Length - 1)
                    {
                        beginTalking = false;
                        j = 0;
                        currentNarrative++;
                    }
                    BreakOut = false;
                    FindBreak = false;
                }
            }
        }


       textMesh.text = Speech;

    }
    public static void Reset()
    {
        AlreadyStarted = false;
        Town2 = false;
        notTown2 = false;
        notSeenEnd = false;
        currentNarrative = 0;
    }
    void OnLevelWasLoaded(int level)
    {
        if (level == 3)
        {
            Town2 = true;
        }
    }
}
