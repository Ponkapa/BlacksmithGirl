using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public static int Frames;
    public Text text;
    public static bool isEnabled;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.gameComplete)
        {
            Frames++;
        }
        string tempString;
        tempString = "Frames: " + Frames.ToString();
        text.text = tempString;
    }
    public static void Enabler()
    {
        isEnabled = !isEnabled;
    }
}
