using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
    public int toLevel;
    public Vector3 spawnPos;
    public bool gainPower = false;
    public enum Facing
    {
        Left,
        Right
    }
    public GameObject BlackScreen;
    public Facing facing;
    void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(BlackScreen, transform.position, Quaternion.identity);
        PlayerController player = other.GetComponent<PlayerController>();
        if (player)
        {
            player.respawnPosition = spawnPos;
            player.Respawn();
            player.Standable = true;
            player.talking = false;
            if (facing == Facing.Left && player.facing == PlayerController.Facing.Right)
            {
                player.SwitchFacing();
            }
            else if (facing == Facing.Right && player.facing == PlayerController.Facing.Left)
            {
                player.SwitchFacing();
            }
            player.levelLoader();
            if(Enemy.checkRespawn)
            {
                Enemy.checkRespawn = false;
                gainPower = true;
            }
            Application.LoadLevel(toLevel);
        }
    }
}
