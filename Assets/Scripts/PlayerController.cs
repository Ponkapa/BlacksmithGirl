using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Standing,
        Walking,
        Dashing,
        Crouching,
        Sliding,
        Crawling,
        Attacking,
        SpecialAttack,
        Parrying,
        ParryMiss,
        Jumping,
        Falling,
        SkidStop,
        Hurt,
        Death,
        ParrySuccess
    }
    public enum Facing
    {
        Left,
        Right
    }
    public static bool NGPlus;
    public AudioClip HitSound;
    public AudioClip ForgeSound;
    public AudioClip FireSound;
    public AudioClip IceSound;
    public AudioClip LightningSound;
    public AudioClip EarthSound;
    public AudioClip LightSound;
    public AudioClip ShadowSound;
    public AudioClip WalkSound;
    public AudioClip RunSound;
    public AudioClip ParrySound;
    public AudioClip JumpSound;
    public AudioClip Woosh;
    public string Jump;
    public string Attack;
    public string SpAttack;
    public string XAxis;
    public string YAxis;
    public string Dash;
    public string Parry;
    public float deadzone;
    public int MaxHealth;
    public int Health;
    public HitboxScript hitbox;
    private Animator animator;
    public GameObject ice;
    public GameObject fire;
    public HitboxScript Earthhitbox;
    public GameObject ShadowHitbox;
    private float AtkDelay;
    private float JumpSpeed;
    private float MaxWalkSpeed;
    private float GroundFriction;
    private float AirFriction;
    private float DashInitial;
    private float DashAccel;
    private float DashMax;
    private float AirMax;
    private float AirAccel;
    private float AtkDuration;
    private float SlideBoost;
    private float ParryDuration;
    private float HurtDelay;
    private float SlidingFriction;
    private float SlideSpeed;
    private float GroundToAirMultiplier;
    private float AirLeaveInitial;
    private float AirTotalMax;
    private string CurrentSp;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    public bool Grounded;
    public bool Standable;
    public State state;
    public Facing facing;
    public static bool hasFire;
    public static bool hasIce;
    public static bool hasLightning;
    public static bool hasEarth;
    public static bool hasLight;
    public static bool hasShadow;
    public static bool gameComplete;
    public static string currentPower;
    public bool Lightninging;
    public bool Iceing;
    public bool talking;
    private bool dead;
    private int j;
    private bool needIce;
    public bool Lighting;
    private int facingCorrector = 1;
    public bool dashed;
    public Vector3 respawnPosition;
    private float yOffsetIce = 0.1f;
    public static GameObject instance;
    public GameObject LightBox;
    public ParticleSystem Rok;
    private int MovingWait;
    private bool movable;
    private int k;
    void Awake()
    {
        if (instance == null)

            //if not, set instance to this
            instance = this.gameObject;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    
    
    // Use this for initialization
    void Start()
    {
        MovingWait = 20;
        respawnPosition = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        state = State.Standing;
        facing = Facing.Right;
        Standable = true;
        deadzone = 0.3f;
        Lighting = false;
        SlideSpeed = 5f;
        AtkDelay = 0.167f;
        JumpSpeed = 24f;
        MaxWalkSpeed = 7f;
        GroundFriction = 0.5f;
        GroundToAirMultiplier = 0.7f;
        AirLeaveInitial = 3f;
        AirTotalMax = 23f;
        AirFriction = 0.05f;
        DashInitial = 9f;
        DashAccel = 0.3f;
        DashMax = 13f;
        AirMax = 8f;
        AirAccel = 0.6f;
        SlideBoost = 13f;
        ParryDuration = 0.4f;
        AtkDuration = 0.2f;
        HurtDelay = 1f;
        SlidingFriction = 0.05f;
    }

    public static void Reset()
    {
        if (!gameComplete || !NGPlus)
        {
            FinalBoss.clearStages = 1;
        }
        hasFire = false;
        hasIce = false;
        hasLight = false;
        hasLightning = false;
        hasEarth = false;
        hasShadow = false;
        currentPower = "";
        gameComplete = false;
        Timer.Frames = 0;
        PlayerController currentPlayer = instance.GetComponent<PlayerController>(); 
        currentPlayer.SetRespawn(new Vector3(-10.5f,0.06f,0f));
        currentPlayer.Respawn();
        if (currentPlayer.facing == Facing.Right)
        {
            currentPlayer.SwitchFacing();
        }
        currentPlayer.movable = false;
        currentPlayer.k = 0;
    }
    public void levelLoader()
    {
        movable = false;
        k = 0;
    }

    void JumpFunc()
    {
    //    AudioSource.PlayClipAtPoint(JumpSound, transform.position);
        animator.SetTrigger("Jump");
        state = State.Jumping;
        Grounded = false;
        rb.velocity = new Vector2(AirLeaveInitial * Input.GetAxis(XAxis) + rb.velocity.x * GroundToAirMultiplier, JumpSpeed);
        bc.size = new Vector2(0.7f, 2f);
        bc.offset = new Vector2(0.1f, 0f);
    }
    public bool PlayerFacingRight()
    {
        if (facing == Facing.Right)
        {
            return true;
        }
        if (facing == Facing.Left)
        {
            return false;
        }
        return true;
    }
    void SlideJumpFunc()
    {
   //     AudioSource.PlayClipAtPoint(JumpSound, transform.position);

        animator.SetTrigger("Jump");
        state = State.Jumping;
        Grounded = false;
        if (PlayerFacingRight())
        {
            rb.velocity = new Vector2(AirLeaveInitial * Input.GetAxis(XAxis) + rb.velocity.x * GroundToAirMultiplier + SlideBoost, JumpSpeed * 3 / 4);
        }
        else if (!PlayerFacingRight())
        {
            rb.velocity = new Vector2(AirLeaveInitial * Input.GetAxis(XAxis) + rb.velocity.x * GroundToAirMultiplier - SlideBoost, JumpSpeed * 3 / 4);
        }
        bc.size = new Vector2(0.7f, 2f);
        bc.offset = new Vector2(0.1f, 0f);
    }
    void TimeNormal()
    {
        Time.timeScale = 1f;
    }

    void TimeSlow()
    {
        Time.timeScale = 0.1f;
    }
    void SlidingFunc()
    {
        animator.SetTrigger("Slide");
        state = State.Sliding;
        bc.size = new Vector2(1.4f, 1f);
        bc.offset = new Vector2(-0.4f, -0.5f);
    }
    void DashFunc()
    {
        needIce = true;
        animator.SetTrigger("Dash");
        state = State.Dashing;
        if (facing == Facing.Right)
        {
            rb.velocity = new Vector2(DashInitial, rb.velocity.y);
        }
        else if (facing == Facing.Left)
        {
            rb.velocity = new Vector2(-DashInitial, rb.velocity.y);
        }
    }
    void CrouchingFunc()
    {
        animator.SetTrigger("Crouch");
        state = State.Crouching;
        bc.size = new Vector2(0.7f, 1f);
        bc.offset = new Vector2(0.1f, -0.5f);
    }
    void Death()
    {
        state = State.Death;
        animator.SetTrigger("Dead");
        dead = true;
    }
    void CrawlingFunc()
    {
        animator.SetTrigger("Crawl");
        state = State.Crawling;
        bc.size = new Vector2(0.7f, 1f);
        bc.offset = new Vector2(0.1f, -0.5f);
    }
    void StandFunc()
    {
        animator.SetTrigger("Stand");
        state = State.Standing;
        bc.size = new Vector2(0.7f, 2f);
        bc.offset = new Vector2(0.1f, 0f);
    }
    void WalkingFunc()
    {
        animator.SetTrigger("Walk");
        state = State.Walking;
    }
    void ParryFunc()
    {
        animator.SetTrigger("Parry");
        state = State.Parrying;
        TerminateCollisions();
    }
    void ParryMissFunc()
    {
        state = State.ParryMiss;
        Invoke("StandFunc", 0.8f);
    }
    void AttackFunc()
    {
        if (!talking)
        {
            animator.SetTrigger("Attack");
            state = State.Attacking;
            Invoke("Swing", AtkDelay);
        }
    }
    void Swing()
    {
        AudioSource.PlayClipAtPoint(Woosh, transform.position);
        if (facing == Facing.Right)
        {
            var newHitbox = Instantiate(hitbox, gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity) as HitboxScript;
            newHitbox.gameObject.transform.parent = transform;
            newHitbox.gameObject.transform.localScale = new Vector2(-1,1);
        }
        else if (facing == Facing.Left)
        {
            var newHitbox = Instantiate(hitbox, gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity) as HitboxScript;
            newHitbox.gameObject.transform.parent = transform;
            newHitbox.gameObject.transform.localScale = new Vector2(-1,1);
        }
        Invoke("TerminateCollisions", AtkDuration);
        Invoke("StandFunc", AtkDuration + 0.01f);
    }
    void TerminateCollisions()
    {
        HitboxScript foundHitbox = GetComponentInChildren<HitboxScript>();
        if (foundHitbox)
        {
            Destroy(foundHitbox.gameObject);
        }
    }
    void FallingFunc()
    {
        animator.SetTrigger("Fall");
        state = State.Falling;
    }
    void SpecialAttackFunc()
    {
        if (!dashed)
        {
            if (currentPower == "Fire")
            {
                state = State.SpecialAttack;
                Invoke("Fire", 0f);
                Invoke("StandFunc", 0.5f);
            }
            else if (currentPower == "Lightning")
            {
                state = State.SpecialAttack;
                Invoke("Lightning", 0f);
                Invoke("StandFunc", 1f);
            }
            else if (currentPower == "Ice")
            {
                state = State.SpecialAttack;
                Invoke("Ice", 0f);
                Invoke("StandFunc", 1f);
            }
            else if (currentPower == "Earth")
            {
                state = State.SpecialAttack;
                Invoke("Earth", 0f);
                Invoke("StandFunc", 1f);
            }
            else if (currentPower == "Light")
            {
                state = State.SpecialAttack;
                dashed = true;
                Invoke("Light", 0f);
                Invoke("ReturnStats", 0.25f);
                Invoke("StandFunc", 0.25f);
            }
            else if (currentPower == "Shadow" && Grounded)
            {
                state = State.SpecialAttack;
                Invoke("Shadow", 0f);
                Invoke("StandFunc", 1f);
            }
        }
    }

    void Fire()
    {
        AudioSource.PlayClipAtPoint(FireSound, transform.position);
        animator.SetTrigger("Fire");
        Invoke("Fireball", 0.25f);
    }
    void Fireball()
    {
        var effect = Instantiate(fire, transform.position + new Vector3(1.1f*facingCorrector,0,0), Quaternion.identity) as GameObject;
        if (facing == Facing.Right)
        {
            effect.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            effect.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Lightning()
    {
        AudioSource.PlayClipAtPoint(LightningSound, transform.position);
        if (!Lightninging)
        {
            if (Iceing)
            {
                ReturnStats();
                animator.SetBool("Ice", false);
                animator.SetBool("Lightning", false);
                Iceing = false;
            }
            Invoke("ReturnStats", 10f);
            animator.SetBool("Lightning", true);
            animator.SetTrigger("SpAtk");
            AtkDelay = AtkDelay * 0.67f;
            AtkDuration = AtkDuration * 0.67f;
            Lightninging = true;
        }
        else
        {
            if (Iceing)
            {
                ReturnStats();
                animator.SetBool("Ice", false);

                animator.SetBool("Lightning", false);
                Iceing = false;
            }
            animator.SetTrigger("SpAtk");
            CancelInvoke("ReturnStats");
            Invoke("ReturnStats", 10f);
        }
    }
    void Ice()
    {
        AudioSource.PlayClipAtPoint(IceSound, transform.position);
        if (!Iceing)
        {
            if (Lightninging)
            {
                ReturnStats();
                animator.SetBool("Lightning", false);
                Lightninging = false;
            }
            animator.SetBool("Ice", true);
            animator.SetTrigger("SpAtk");
            AirTotalMax *= 1.5f;
            GroundFriction *= 0.5f;
            SlidingFriction *= 0.5f;
            DashMax *= 1.5f;
            Iceing = true;
            Invoke("ReturnStats", 10f);
        }
        else
        {
            if (Lightninging)
            {
                ReturnStats();
                animator.SetBool("Lightning", false);
                Lightninging = false;
            }

            animator.SetBool("Ice", true);
            animator.SetTrigger("SpAtk");
            CancelInvoke("ReturnStats");
            Invoke("ReturnStats", 10f);
        }
    }
    void Earth()
    {
        
        animator.SetTrigger("Earth");
        Invoke("EarthHit", 0.75f);
        Invoke("TerminateCollisions", 1f);
        AudioSource.PlayClipAtPoint(Woosh, transform.position);
    }
    void EarthHit()
    {
        AudioSource.PlayClipAtPoint(EarthSound, transform.position);
        if (facing == Facing.Right)
        {
            var newHitbox = Instantiate(Earthhitbox, gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity) as HitboxScript;
            newHitbox.gameObject.transform.parent = transform;
            newHitbox.gameObject.transform.localScale = new Vector2(-1, 1);
            ParticleSystem ps = Instantiate(Rok, transform.position - new Vector3(-2, 0.49f, 0), Quaternion.identity) as ParticleSystem;
            ps.Emit(20);
        }
        else if (facing == Facing.Left)
        {
            var newHitbox = Instantiate(Earthhitbox, gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity) as HitboxScript;
            newHitbox.gameObject.transform.parent = transform;
            newHitbox.gameObject.transform.localScale = new Vector2(-1, 1);
            ParticleSystem ps = Instantiate(Rok, transform.position - new Vector3(2, 0.49f, 0), Quaternion.identity) as ParticleSystem;
            ps.Emit(20);
        }
    }
    void Light()
    {
        AudioSource.PlayClipAtPoint(LightSound, transform.position);
        animator.SetTrigger("LightDash");
        gameObject.layer = 18;
        Lighting = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(20 * facingCorrector, 0);
    }
    void Shadow()
    {
        AudioSource.PlayClipAtPoint(ShadowSound, transform.position);
        animator.SetTrigger("Shadow");
        if (facing == Facing.Right)
        {
            var effect = Instantiate(ShadowHitbox, transform.position - new Vector3(-3.09f, 0.97f, 0), Quaternion.identity) as GameObject;
        }
        else
        {
            var effect = Instantiate(ShadowHitbox, transform.position - new Vector3(3.09f, 0.97f, 0), Quaternion.identity) as GameObject;
        }
    }
    void ReturnStats()
    {
        gameObject.layer = 11;
        Lighting = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.SetBool("Lightning", false);
        animator.SetBool("Ice", false);
        SlideSpeed = 5f;
        AtkDelay = 0.167f;
        JumpSpeed = 24f;
        MaxWalkSpeed = 7f;
        GroundFriction = 0.5f;
        GroundToAirMultiplier = 0.7f;
        AirLeaveInitial = 3f;
        AirTotalMax = 23f;
        AirFriction = 0.05f;
        DashInitial = 9f;
        DashAccel = 0.3f;
        DashMax = 13f;
        AirMax = 8f;
        AirAccel = 0.6f;
        SlideBoost = 13f;
        ParryDuration = 0.4f;
        AtkDuration = 0.2f;
        HurtDelay = 1f;
        SlidingFriction = 0.05f;
        Iceing = false;
        Lightninging = false;
    }




    void SkidStopFunc()
    {
        animator.SetTrigger("SkidStop");
        state = State.SkidStop;
    }

    void ApplyGroundFriction(int times)
    {
        if (rb.velocity.x > 0)
        {
            rb.velocity -= new Vector2(GroundFriction * times, 0);
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else if (rb.velocity.x < 0)
        {
            rb.velocity -= new Vector2(-GroundFriction * times, 0);
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    void ApplyAirFriction(int times)
    {
        if (rb.velocity.x > 0)
        {
            rb.velocity -= new Vector2(AirFriction * times, 0);
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else if (rb.velocity.x < 0)
        {
            rb.velocity -= new Vector2(-AirFriction * times, 0);
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
    void ApplySlidingFriction()
    {

        if (rb.velocity.x > 0)
        {
            rb.velocity -= new Vector2(SlidingFriction, 0);
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else if (rb.velocity.x < 0)
        {
            rb.velocity -= new Vector2(-SlidingFriction, 0);
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
    void ChangeFacing()
    {
        if (Input.GetAxis(XAxis) > 0)
        {
            facingCorrector = 1;
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            facing = Facing.Right;
        }
        else if (Input.GetAxis(XAxis) < 0)
        {
            facingCorrector = -1;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            facing = Facing.Left;
        }
    }
    public void SwitchFacing()
    {
        if (facing == Facing.Right)
        {
            facingCorrector = -1;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            facing = Facing.Left;
        }
        else
        {
            facingCorrector = 1;
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            facing = Facing.Right;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (movable)
        {
            if (Grounded)
            {
                dashed = false;
            }
            switch (state)
            {
                case State.Standing:
                    if (!Standable)
                    {
                        Invoke("CrouchingFunc", 0f);
                    }
                    if (!Grounded)
                    {
                        state = State.Jumping;
                    }
                    else
                    {

                        ChangeFacing();
                        ApplyGroundFriction(1);
                        if (Input.GetButtonDown(Jump))
                        {

                            Invoke("JumpFunc", 0f);
                        }
                        else if (Input.GetButtonDown(Parry))
                        {
                            CancelInvoke("Swing"); CancelInvoke("StandFunc");
                            Invoke("ParryFunc", 0f);
                            Invoke("ParryMissFunc", ParryDuration);
                        }
                        else if (Input.GetButton(Dash))
                        {

                            Invoke("DashFunc", 0f);
                        }
                        else if (Input.GetButtonDown(Attack))
                        {

                            Invoke("AttackFunc", 0f);
                        }
                        else if (Input.GetButtonDown(SpAttack))
                        {
                            Invoke("SpecialAttackFunc", 0f);
                        }
                        else if (Input.GetAxis(YAxis) < -deadzone)
                        {

                            Invoke("CrouchingFunc", 0f);
                        }
                        else if (Mathf.Abs(Input.GetAxis(XAxis)) > deadzone)
                        {

                            Invoke("WalkingFunc", 0f);
                        }
                    }
                    break;
                case State.Walking:
                    if (!Standable)
                    {
                        Invoke("CrouchingFunc", 0f);
                    }
                    if (!Grounded)
                    {
                        state = State.Jumping;
                    }
                    ChangeFacing();
                    if (Input.GetButtonDown(Jump))
                    {

                        Invoke("JumpFunc", 0f);
                    }
                    else if (Input.GetButtonDown(Parry))
                    {
                        CancelInvoke("Swing"); CancelInvoke("StandFunc");
                        Invoke("ParryFunc", 0f);
                        Invoke("ParryMissFunc", ParryDuration);
                    }
                    else if (Input.GetButtonDown(Attack))
                    {

                        Invoke("AttackFunc", 0f);
                    }
                    else if (Input.GetButtonDown(SpAttack))
                    {
                        Invoke("SpecialAttackFunc", 0f);
                    }
                    else if (Input.GetButton(Dash))
                    {

                        Invoke("DashFunc", 0f);
                    }
                    else if (Input.GetAxis(YAxis) < -deadzone)
                    {

                        Invoke("CrouchingFunc", 0f);
                    }
                    else if (Mathf.Abs(Input.GetAxis(XAxis)) < deadzone)
                    {

                        Invoke("StandFunc", 0f);
                    }
                    rb.velocity = new Vector2(Input.GetAxis(XAxis) * MaxWalkSpeed, rb.velocity.y);
                    break;
                case State.Dashing:
                    if (!Standable)
                    {
                        Invoke("CrouchingFunc", 0f);
                    }
                    if (Input.GetButton(Dash))
                    {
                        if (Iceing)
                        {
                            j++;
                            if (needIce)
                            {
                                j = 0;
                                needIce = false;
                            }
                            if (j > 20)
                            {
                                var effect = Instantiate(ice, transform.position - new Vector3(0, 1 + yOffsetIce, 0), Quaternion.identity) as GameObject;
                                if (facing == Facing.Right)
                                {
                                    effect.transform.localScale = new Vector3(-1, 1, 1);
                                }
                                else
                                {
                                    effect.transform.localScale = new Vector3(1, 1, 1);
                                }
                                j = 0;
                                yOffsetIce = -yOffsetIce;
                            }
                        }
                        if (!Grounded)
                        {
                            state = State.Jumping;
                        }
                        ChangeFacing();
                        if (Input.GetButtonDown(Jump))
                        {

                            Invoke("JumpFunc", 0f);
                        }
                        else if (Input.GetButtonDown(Parry))
                        {
                            CancelInvoke("Swing"); CancelInvoke("StandFunc");
                            Invoke("ParryFunc", 0f);
                            Invoke("ParryMissFunc", ParryDuration);
                        }
                        else if (Input.GetButtonDown(Attack))
                        {

                            Invoke("AttackFunc", 0f);
                        }
                        else if (Input.GetButtonDown(SpAttack))
                        {
                            Invoke("SpecialAttackFunc", 0f);
                        }
                        else if (Input.GetAxis(YAxis) < -deadzone)
                        {

                            Invoke("SlidingFunc", 0f);
                        }
                        if (Mathf.Abs(rb.velocity.x) < DashMax)
                        {
                            if (facing == Facing.Right)
                            {
                                rb.velocity += new Vector2(DashAccel, 0);
                            }
                            else if (facing == Facing.Left)
                            {
                                rb.velocity += new Vector2(-DashAccel, 0);
                            }
                        }
                        if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(Input.GetAxis(XAxis)) && Input.GetAxis(XAxis) != 0)
                        {

                            Invoke("DashFunc", 0f);
                        }
                    }
                    else
                    {

                        Invoke("SkidStopFunc", 0f);
                    }
                    break;
                case State.Crouching:
                    ApplyGroundFriction(1);
                    if (Input.GetButtonDown(SpAttack) && currentPower == "Light")
                    {
                        Invoke("SpecialAttackFunc", 0f);
                    }
                    if (Input.GetAxis(YAxis) < -deadzone)
                    {
                        if (!Grounded)
                        {
                            state = State.Jumping;
                        }

                        if (Standable)
                        {
                            if (Input.GetButtonDown(Jump))
                            {

                                Invoke("JumpFunc", 0f);
                            }
                            else if (Input.GetButtonDown(Parry))
                            {
                                CancelInvoke("Swing"); CancelInvoke("StandFunc");
                                Invoke("ParryFunc", 0f);
                                Invoke("ParryMissFunc", ParryDuration);
                            }
                            else if (Input.GetButtonDown(Attack))
                            {
                                Invoke("AttackFunc", 0f);
                            }
                            else if (Input.GetButtonDown(SpAttack))
                            {
                                Invoke("SpecialAttackFunc", 0f);
                            }
                        }
                        if (Mathf.Abs(Input.GetAxis(XAxis)) > deadzone)
                        {

                            Invoke("CrawlingFunc", 0f);
                        }
                    }
                    else
                    {
                        if (Standable)
                        {

                            Invoke("StandFunc", 0f);
                        }
                        else if (Mathf.Abs(Input.GetAxis(XAxis)) > deadzone)
                        {

                            Invoke("CrawlingFunc", 0f);
                        }
                    }
                    break;
                case State.Sliding:
                    if (Input.GetAxis(YAxis) < -deadzone && Mathf.Abs(rb.velocity.x) > SlideSpeed)
                    {
                        ApplySlidingFriction();
                        if (!Grounded)
                        {
                            state = State.Jumping;
                        }
                        if (Standable)
                        {
                            if (Input.GetButtonDown(Jump))
                            {
                                Invoke("SlideJumpFunc", 0f);
                            }
                            else if (Input.GetButtonDown(Parry))
                            {
                                CancelInvoke("Swing"); CancelInvoke("StandFunc");
                                Invoke("ParryFunc", 0f);
                                Invoke("ParryMissFunc", ParryDuration);
                            }
                        }
                    }
                    else
                    {

                        Invoke("CrouchingFunc", 0f);
                    }
                    break;
                case State.Attacking:
                    if (Input.GetButtonDown(Parry))
                    {
                        CancelInvoke("Swing"); CancelInvoke("StandFunc");
                        Invoke("ParryFunc", 0f);
                        Invoke("ParryMissFunc", ParryDuration);
                    }
                    if (Grounded)
                    {
                        ApplyGroundFriction(2);
                    }
                    else
                    {
                        ApplyAirFriction(1);
                    }
                    break;
                case State.SpecialAttack:
                    if (Input.GetButtonDown(Parry))
                    {
                        CancelInvoke("Swing"); CancelInvoke("StandFunc");
                        ReturnStats();
                        Invoke("ParryFunc", 0f);
                        Invoke("ParryMissFunc", ParryDuration);
                    }
                    if (!Lighting)
                    {
                        if (Grounded)
                        {
                            ApplyGroundFriction(2);
                        }
                        else
                        {
                            ApplyAirFriction(1);
                        }
                    }
                    break;
                case State.Parrying:
                    if (Grounded)
                    {
                        ApplyGroundFriction(2);
                    }
                    else
                    {
                        ApplyAirFriction(1);
                    }
                    break;
                case State.ParryMiss:
                    if (Grounded)
                    {
                        ApplyGroundFriction(2);
                    }
                    else
                    {
                        ApplyAirFriction(1);
                    }
                    break;
                case State.Jumping:
                    if (Mathf.Abs(rb.velocity.x) > AirTotalMax)
                    {
                        rb.velocity = new Vector2(AirTotalMax * Mathf.Sign(rb.velocity.x), rb.velocity.y);
                    }
                    if (((Mathf.Abs(rb.velocity.x) < AirMax) && Mathf.Abs(Input.GetAxis(XAxis)) > deadzone) || (Mathf.Sign(Input.GetAxis(XAxis)) != (Mathf.Sign(rb.velocity.x))))
                    {
                        rb.velocity += new Vector2(Input.GetAxis(XAxis) * AirAccel, 0);
                    }
                    else
                    {
                        ApplyAirFriction(1);
                    }
                    if (Input.GetButtonDown(Parry))
                    {
                        CancelInvoke("Swing"); CancelInvoke("StandFunc");
                        Invoke("ParryFunc", 0f);
                        Invoke("ParryMissFunc", ParryDuration);
                    }
                    else if (Input.GetButtonDown(Attack))
                    {

                        Invoke("AttackFunc", 0f);
                    }
                    else if (Input.GetButtonDown(SpAttack))
                    {
                        Invoke("SpecialAttackFunc", 0f);
                    }
                    if (rb.velocity.y < 0)
                    {

                        Invoke("FallingFunc", 0f);
                    }
                    if (Grounded)
                    {

                        Invoke("StandFunc", 0f);
                    }
                    break;
                case State.Falling:
                    if (Mathf.Abs(rb.velocity.x) > AirTotalMax)
                    {
                        rb.velocity = new Vector2(AirTotalMax * Mathf.Sign(rb.velocity.x), rb.velocity.y);
                    }
                    if (((Mathf.Abs(rb.velocity.x) < AirMax) && Mathf.Abs(Input.GetAxis(XAxis)) > deadzone) || (Mathf.Sign(Input.GetAxis(XAxis)) != (Mathf.Sign(rb.velocity.x))))
                    {
                        rb.velocity += new Vector2(Input.GetAxis(XAxis) * AirAccel, 0);
                    }
                    else
                    {
                        ApplyAirFriction(1);
                    }
                    if (Input.GetButtonDown(Parry))
                    {
                        CancelInvoke("Swing"); CancelInvoke("StandFunc");
                        Invoke("ParryFunc", 0f);
                        Invoke("ParryMissFunc", ParryDuration);
                    }
                    else if (Input.GetButtonDown(Attack))
                    {

                        Invoke("AttackFunc", 0f);
                    }
                    else if (Input.GetButtonDown(SpAttack))
                    {
                        Invoke("SpecialAttackFunc", 0f);
                    }
                    if (Grounded)
                    {

                        Invoke("StandFunc", 0f);
                    }

                    break;
                case State.Crawling:
                    if (!Grounded)
                    {
                        state = State.Jumping;
                    }
                    if (Mathf.Abs(Input.GetAxis(XAxis)) < deadzone)
                    {

                        Invoke("CrouchingFunc", 0f);
                    }
                    if (Input.GetAxis(YAxis) > -deadzone && Standable)
                    {

                        Invoke("StandFunc", 0f);
                    }
                    else if (Mathf.Abs(Input.GetAxis(XAxis)) > deadzone)
                    {
                        rb.velocity = new Vector2(Input.GetAxis(XAxis) * MaxWalkSpeed, rb.velocity.y);
                    }
                    else
                    {
                        if (Standable)
                        {
                            if (Input.GetButtonDown(Jump))
                            {

                                Invoke("JumpFunc", 0f);
                            }
                            else if (Input.GetButtonDown(Parry))
                            {
                                CancelInvoke("Swing"); CancelInvoke("StandFunc");
                                Invoke("ParryFunc", 0f);
                                Invoke("ParryMissFunc", ParryDuration);
                            }
                            else if (Input.GetButtonDown(Attack))
                            {

                                Invoke("AttackFunc", 0f);
                            }
                            else if (Input.GetButtonDown(SpAttack))
                            {
                                Invoke("SpecialAttackFunc", 0f);
                            }
                        }
                        rb.velocity = new Vector2(Input.GetAxis(XAxis) * MaxWalkSpeed, rb.velocity.y);
                    }
                    break;
                case State.Hurt:

                    if (Grounded)
                    {
                        ApplyGroundFriction(2);
                    }
                    else
                    {
                        ApplyAirFriction(1);
                    }
                    break;
                case State.Death:
                    if (Grounded)
                    {
                        ApplyGroundFriction(2);
                    }
                    else
                    {
                        ApplyAirFriction(1);
                    }

                    break;
                case State.SkidStop:
                    if (Mathf.Abs(rb.velocity.x) > MaxWalkSpeed)
                    {
                        ApplyGroundFriction(2);
                    }
                    else
                    {
                        ApplyGroundFriction(1);
                    }
                    if (rb.velocity.x == 0f)
                    {
                        Invoke("StandFunc", 0f);
                    }
                    break;
                case State.ParrySuccess:
                    if (Input.GetButtonDown(Attack))
                    {
                        CancelInvoke("StandFunc");
                        Invoke("AttackFunc", 0f);
                    }
                    else if (Input.GetButtonDown(Parry))
                    {
                        CancelInvoke("StandFunc");
                        Invoke("ParryFunc", 0f);
                        Invoke("ParryMissFunc", ParryDuration);
                    }
                    else if (Input.GetButtonDown(SpAttack))
                    {
                        Invoke("SpecialAttackFunc", 0f);
                    }
                    if (!Grounded)
                    {
                        if (Mathf.Abs(rb.velocity.x) > AirTotalMax)
                        {
                            rb.velocity = new Vector2(AirTotalMax * Mathf.Sign(rb.velocity.x), rb.velocity.y);
                        }
                        if (((Mathf.Abs(rb.velocity.x) < AirMax) && Mathf.Abs(Input.GetAxis(XAxis)) > deadzone) || (Mathf.Sign(Input.GetAxis(XAxis)) != (Mathf.Sign(rb.velocity.x))))
                        {
                            rb.velocity += new Vector2(Input.GetAxis(XAxis) * AirAccel, 0);
                        }
                        else
                        {
                            ApplyAirFriction(1);
                        }
                    }
                    else
                    {
                        if (Input.GetButtonDown(Dash))
                        {
                            CancelInvoke("StandFunc");
                            Invoke("DashFunc", 0f);
                        }
                        else if (Input.GetButtonDown(Jump))
                        {
                            CancelInvoke("StandFunc");
                            Invoke("JumpFunc", 0f);
                        }
                        rb.velocity = new Vector2(Input.GetAxis(XAxis) * MaxWalkSpeed, rb.velocity.y);
                    }

                    break;
            }
        }
        else
        {
            k++;
            if (k > MovingWait)
            {
                movable = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!dead)
        {
            if (!Lighting)
            {
                if (other.CompareTag("Hitbox"))
                {
                    CancelInvoke("Swing");
                    CancelInvoke("EarthHit");
                    if (state == State.Parrying)
                    {
                        CancelInvoke("ParryMissFunc");
                        animator.SetTrigger("ParrySucceed");
                        AudioSource.PlayClipAtPoint(ParrySound, transform.position);
                        HitboxScript hitbyhitbox = other.GetComponent<HitboxScript>();
                        if (hitbyhitbox)
                        {
                            Enemy enemy = hitbyhitbox.gameObject.GetComponentInParent<Enemy>();
                            enemy.parried = true;
                            Destroy(other.gameObject);
                            state = State.ParrySuccess;
                            Invoke("TimeSlow", 0f);
                            Invoke("StandFunc", 0.1f);
                            Invoke("TimeNormal", 0.035f);
                        }
                        else
                        {
                            Projectile projectileHit = other.GetComponent<Projectile>();
                            if (projectileHit)
                            {
                                projectileHit.parried = true;
                                state = State.ParrySuccess;
                                projectileHit.gameObject.layer = 17;
                                projectileHit.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileHit.GetComponent<Rigidbody2D>().velocity.x, -projectileHit.GetComponent<Rigidbody2D>().velocity.y);
                                projectileHit.gameObject.AddComponent<HitboxScript>();
                                projectileHit.gameObject.transform.localScale = new Vector3(-projectileHit.gameObject.transform.localScale.x, -projectileHit.transform.localScale.y, projectileHit.transform.localScale.z);
                                projectileHit.GetComponent<HitboxScript>().damage = projectileHit.damage;
                                Invoke("TimeSlow", 0f);
                                Invoke("StandFunc", 0.1f);
                                Invoke("TimeNormal", 0.035f);
                            }
                        }
                    }
                    else
                    {
                        int hitdamage;
                        HitboxScript hitbyhitbox = other.GetComponent<HitboxScript>();
                        Projectile projectileHit = other.GetComponent<Projectile>();
                        if (hitbyhitbox)
                        {
                            Health -= hitbyhitbox.damage;
                            hitdamage = hitbyhitbox.damage;
                        }
                        else if (projectileHit)
                        {
                            Health -= projectileHit.damage;
                            hitdamage = projectileHit.damage;
                        }
                        else
                        {
                            hitdamage = 0;
                        }
                        if (facing == Facing.Right)
                        {
                            rb.velocity += new Vector2(-hitdamage, 10);
                        }
                        else if (facing == Facing.Left)
                        {
                            rb.velocity += new Vector2(hitdamage, 10);
                        }
                        if (Standable)
                        {
                            Debug.Log("StandAfterHit");
                            Invoke("StandFunc", hitdamage / 10);
                        }
                        else
                        {
                            Debug.Log("CrouchAfterHit");
                            Invoke("CrouchingFunc", hitdamage / 10 );
                        }
                            state = State.Hurt;
                        Destroy(other.gameObject);
                        if (Health <= 0)
                        {
                            CancelInvoke();
                            ReturnStats();
                            Invoke("Death", 0f);
                        }
                        else
                        {
                            AudioSource.PlayClipAtPoint(HitSound, transform.position);
                            animator.SetTrigger("Hurt");
                            
                        }
                    }
                }
            }
        }
    }
    public void SetAbility(string ability)
    {
        currentPower = ability;
    }
    public void Respawn()
    {
        transform.position = respawnPosition;
        Health = MaxHealth;
        dead = false;
        rb.velocity = new Vector2(0, 0);
        Invoke("StandFunc",0f);
        TimeNormal();
        Standable = true;
    }
    public void SetRespawn(Vector3 newPos)
    {
        respawnPosition = newPos;
    }
    public void kill()
    {
        CancelInvoke();
        ReturnStats();
        Health = Health - MaxHealth;
        Invoke("Death", 0f);
    }
}