﻿using UnityEngine;
using System.Collections;

public class RightSide : MonoBehaviour {
    private BossDoorScript bds;
    // Use this for initialization
    void Start()
    {
        bds = GetComponentInParent<BossDoorScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bds.rightbool = true;
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
            bds.rightbool = false;
        }
    }
}
