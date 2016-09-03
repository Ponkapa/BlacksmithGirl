using UnityEngine;
using System.Collections;

public class ParallaxBirdy : MonoBehaviour {
    public float RatioX;
    public Vector3 Offset;
    private PlayerController player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(RatioX * player.gameObject.transform.position.x + Offset.x, gameObject.transform.position.y + Offset.y, gameObject.transform.position.z);
	}
}
