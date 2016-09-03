using UnityEngine;
using System.Collections;

public class FinalBossScript : MonoBehaviour {
    private FinalBoss enemy;
    private PlayerController player;
    public Vector3 RespawnPositionFinalBoss;
    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<FinalBoss>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.dead)
        {
            Enemy.checkRespawn = true;
            player.SetRespawn(RespawnPositionFinalBoss);
        }
    }
}
