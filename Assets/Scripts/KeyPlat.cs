using UnityEngine;
using System.Collections;

public class KeyPlat : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    public GameObject object5;
    public GameObject object6;
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!object1 && !object2 && !object3 && !object4 && !object5 && !object6)
        {
            animator.SetTrigger("Clear");
        }
    }
}
