﻿using UnityEngine;
using System.Collections;

public class FinalLeftSide : MonoBehaviour {

    private FinalBossDoorScript bds;
    // Use this for initialization
    void Start()
    {
        bds = GetComponentInParent<FinalBossDoorScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bds.leftbool = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bds.touching = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bds.leftbool = false;
            bds.touching = false;
        }
    }
}
