using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
    public SpriteRenderer sr;
    public Sprite[] sprites;
    private Rigidbody2D rb;
    private float Friction = 0.5f;
    private float Accel = 0.4f;
    private float MaxSpeed = 2f;
    private int changetime;
    private int i, j = 0;
    private int currentFrame;
    public int gender;
    public string costumenumber;
    public enum State
    {
        Walking,
        Standing
    }
    public enum Facing
    {
        Left,
        Right
    }
    public Facing facing;
    private int sign = 1;
    public State state;
    public bool CanBeDeleted = false;
    // Use this for initialization
    void Awake()
    {
        gender = Random.Range(0, 2);
        costumenumber = Random.Range(1, 7).ToString();
        currentFrame = 0;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
        if (gender == 0)
        {
            sprites = Resources.LoadAll<Sprite>("female/dresses/dress" + costumenumber);
        }
        else
        {
            sprites = Resources.LoadAll<Sprite>("male/body/costume" + costumenumber);
        }
        sr.sprite = sprites[0];
    }
    void Start () {
        
    }
    void ApplyFriction(int times)
    {
        if (rb.velocity.x > 0)
        {
            rb.velocity -= new Vector2(Friction * times, 0);
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else if (rb.velocity.x < 0)
        {
            rb.velocity -= new Vector2(-Friction * times, 0);
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
    void ResetVelocity()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    public void SwitchFacing()
    {
        ResetVelocity();
        if (facing == Facing.Left)
        {
            facing = Facing.Right;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            facing = Facing.Left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void SwitchMoving()
    {
        ResetVelocity();
        if (state == State.Walking)
        {
            state = State.Standing;
            sign = 1;
            currentFrame = 7;
        }
        else if (state == State.Standing)
        {
            state = State.Walking;
            currentFrame = 0;
        }
    }
    
    void Move()
    {
        if (Mathf.Abs(rb.velocity.x) >= MaxSpeed)
        {

        }
        else
        {
            if (facing == Facing.Right)
            {
                rb.velocity += new Vector2(Accel, 0);
            }
            else
            {
                rb.velocity -= new Vector2(Accel, 0);
            }
        }
    }
	// Update is called once per frame
	void Update () {
        
        if (j > 100)
        {
            CanBeDeleted = true;
        }
        else
        {
            j++;
        }
        bool isTalking = GetComponentInChildren<Talking>().beginTalking;
        if (isTalking)
        {
            CanBeDeleted = false;
        }
        if (isTalking && state == State.Walking)
        {
            state = State.Standing;
            sign = 1;
            currentFrame = 7;
        }
        if (Random.Range(0,100) == 1 && !isTalking)
        {
            SwitchFacing();
        }
        if (Random.Range(0,500) == 1 && !isTalking)
        {
            SwitchMoving();
        }
        i++;
        switch (state)
        {
            case State.Walking:
                if (i > 15)
                {
                    i = 0;
                    currentFrame++;
                    if (currentFrame > 6)
                    {
                        currentFrame = 1;
                    }
                    sr.sprite = sprites[currentFrame];
                }
                Move();
                break;

            case State.Standing:
                ApplyFriction(1);
                if (i > 40)
                {
                    i = 0;
                    currentFrame += sign;
                    if (currentFrame > sprites.Length-2 || currentFrame < 8)
                    {
                        sign = -sign;
                    }
                    sr.sprite = sprites[currentFrame];
                }

                break;
        }
	}
    public int getGender()
    {
        return gender;
    }
    
}
