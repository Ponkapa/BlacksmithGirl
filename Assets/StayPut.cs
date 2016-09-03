using UnityEngine;
using System.Collections;

public class StayPut : MonoBehaviour {
    private PlayerController player;
    void Start()
    {
        player = GetComponent<PlayerController>();
    }
    void OnLevelWasLoaded(int level)
    {
        if (player)
        {
            player.Respawn();
        }
    }
}
