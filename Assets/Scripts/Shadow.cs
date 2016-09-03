using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {
    private int i;
	// Use this for initialization
	void Start () {
        i = 0;
        Invoke("Ded",1f);
	}
	
    void Ded()
    {
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
        i++;
        if (i < 28)
        {
        Vector3 temp = gameObject.transform.localScale;
        temp.x += 1;
        gameObject.transform.localScale = temp;
        }
	}
}
