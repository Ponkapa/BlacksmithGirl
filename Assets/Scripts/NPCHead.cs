using UnityEngine;
using System.Collections;

public class NPCHead : MonoBehaviour {
    public int gender;
    public SpriteRenderer sr;
    public Sprite[] sprites;
	// Use this for initialization
	void Start () {

        sr = GetComponent<SpriteRenderer>();
        gender = GetComponentInParent<NPC>().getGender();
        sr.sortingOrder = GetComponentInParent<NPC>().gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
        if (gender == 0)
        {
            sprites = Resources.LoadAll<Sprite>("female/heads");
        }
        else
        {
            sprites = Resources.LoadAll<Sprite>("male/head");
        }

        sr.sprite = sprites[Random.Range(0, sprites.Length)];
	}
}
