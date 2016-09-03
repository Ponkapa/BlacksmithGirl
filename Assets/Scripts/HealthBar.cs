using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    public PlayerController player;
    public RectTransform RightBar;
    public RectTransform LeftBar;
    public RectTransform MiddleBar;
    public RectTransform Respawn;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        float currentFrac = (float) player.Health / (float)player.MaxHealth;
        float lowFrac = (float)player.Health / 5f;
        if (player.Health >= player.MaxHealth)
        {
            RightBar.transform.localScale = new Vector3(1, 1, 1);
            LeftBar.transform.localScale = new Vector3(1, 1, 1);
            MiddleBar.transform.localScale = new Vector3(1, 1, 1);
            Respawn.gameObject.SetActive(false);
        }
        if (player.Health < player.MaxHealth)
        {
            RightBar.transform.localScale = new Vector3(0, 1, 1);
            LeftBar.transform.localScale = new Vector3(1, 1, 1);
            MiddleBar.transform.localScale = new Vector3(currentFrac, 1, 1);
        }
        if (player.Health < 5)
        {
            RightBar.transform.localScale = new Vector3(0, 1, 1);
            LeftBar.transform.localScale = new Vector3(lowFrac, 1, 1);
            MiddleBar.transform.localScale = new Vector3(0, 1, 1);
        }
        if (player.Health <= 0)
        {
            RightBar.transform.localScale = new Vector3(0, 1, 1);
            LeftBar.transform.localScale = new Vector3(0, 1, 1);
            MiddleBar.transform.localScale = new Vector3(0, 1, 1);
            Respawn.gameObject.SetActive(true);
        }
    }
}
