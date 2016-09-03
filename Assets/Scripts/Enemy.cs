using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Standing,
        Moving,
        NotMoving,
        Parried
    }
    public enum Facing
    {
        Left,
        Right
    }
    private Animator animator;
    public State state;
    public Facing facing;
    public AudioClip laughClip;
    public int stagger;
    public int staggermax;
    public int damage;
    public int health;
    public HitboxScript hitbox;
    public bool lunge;
    public bool aggro;
    public bool movable;
    public bool attacking;
    public bool parried;
    public bool damageable;
    public bool projectileAttack;
    public Projectile projectile;
    private GameObject target;
    public float standtime;
    public float attacktime;
    public float hitboxtime;
    public float hitboxduration;
    public float parriedtime;
    public bool needParry;
    public float deathtime;
    public float staggertime;
    public float Friction;
    public float Acceleration;
    public float MaxSpeed;
    public float xDisplacement;
    public Vector3 projectileSpawn;
    public bool TwoAttacks;
    private float facingcorrector;
    public float ProjectileDistance;
    public int staggerRecoverRate;
    public bool dead;
    private int beginRecover = 50;
    private int recoveryTime;
    private int recovery;
    private Rigidbody2D rb;
    public GameObject rootObject;
    public float projectileFireTime;
    public float projectileResolveTime;
    public int MaxHealth;
    public bool hit= false;
    public static bool checkRespawn;
    public bool DontRespawn;
    private bool dying = false;
    // Use this for initialization
    void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        aggro = false;
        attacking = false;
        dead = false;
        rootObject = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        hitbox.damage = damage;
        MaxHealth = health;
    }
    public static bool BossDead()
    {
        return checkRespawn;
    }
    public static void setCheckRespawn(bool killed)
    {
        checkRespawn = killed;
    }
    void Idle()
    {
        animator.SetTrigger("Idle");
        state = State.Idle;
    }
    void Move()
    {
        animator.SetTrigger("Move");
        state = State.Moving;
    }
    void NotHit()
    {
        hit = false;
    }
    void Attack()
    {
        CancelInvoke("Stand");
        animator.ResetTrigger("ImmStand");
        state = State.NotMoving;
        if (lunge)
        {
            Invoke("Lunge", hitboxtime);
        }
        if (projectileAttack)
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > ProjectileDistance)
            {

                animator.SetTrigger("Projectile");
                Invoke("FireProjectile", projectileFireTime);
                Invoke("Stand", projectileResolveTime);
            }
            else
            {
                int rand = Random.Range(1, 3);
                if (TwoAttacks)
                {
                    if (rand == 1)
                    {
                        animator.SetTrigger("Attack");
                    }
                    else if (rand == 2)
                    {
                        animator.SetTrigger("Attack2");
                    }
                }
                else
                {
                    animator.SetTrigger("Attack");
                }
                Invoke("Hitbox", hitboxtime);
                Invoke("TerminateCollisions", hitboxtime + hitboxduration);
                Invoke("Stand", attacktime);
            }
        }
        else
        {
            int rand = Random.Range(1, 3);
            if (TwoAttacks)
            {
                if (rand == 1)
                {
                    animator.SetTrigger("Attack");
                }
                else if (rand == 2)
                {
                    animator.SetTrigger("Attack2");
                }
            }
            else
            {
                animator.SetTrigger("Attack");
            }
            Invoke("Hitbox", hitboxtime);
            Invoke("TerminateCollisions", hitboxtime + hitboxduration);
            Invoke("Stand", attacktime);
        }
    }
    void Lunge()
    {
        rb.velocity += new Vector2(15*facingcorrector, 0);
    }
    void Aggroed()
    {
        animator.SetTrigger("Stand");
        state = State.NotMoving;
        Invoke("Stand", standtime);
    }
    void Death()
    {
        foreach (Collider2D col in GetComponents<Collider2D>())
        {
            Destroy(col);
        }
        rb.isKinematic = true;
        animator.SetTrigger("Death");
        state = State.NotMoving;
        dead = true;
        Invoke("Die", deathtime);
    }
    void ShadowDeath()
    {
        foreach (Collider2D col in GetComponents<Collider2D>())
        {
            Destroy(col);
        }
        animator.SetTrigger("Death");
        state = State.NotMoving;
        dead = true;
        Invoke("Die", deathtime);
    }
    void Die()
    {
        Destroy(rootObject);
    }
    void Parried()
    {
        animator.SetTrigger("Parried");
        state = State.Parried;
        Invoke("Stand", parriedtime);
    }
    void Stagger()
    {
        if (laughClip)
        {
            AudioSource.PlayClipAtPoint(laughClip, transform.position);
        }
        animator.SetTrigger("Hurt");
        state = State.NotMoving;
    }
    void Stand()
    {
        if (!aggro)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            animator.SetTrigger("ImmStand");
        }
        state = State.Standing;
        hit = false;
    }
    void TerminateCollisions()
    {
        HitboxScript foundHitbox = GetComponentInChildren<HitboxScript>();
        if (foundHitbox)
        {
            Destroy(foundHitbox.gameObject);
        }
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

    void ChangeFacing()
    {
        if (target.transform.position.x < gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facing = Facing.Left;
            facingcorrector = -1;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facing = Facing.Right;
            facingcorrector = 1;
        }
    }
    void Hitbox()
    {
        if (facing == Facing.Right)
        {
            var newHitbox = Instantiate(hitbox, gameObject.transform.position + new Vector3(xDisplacement, 0, 0), Quaternion.identity) as HitboxScript;
            newHitbox.gameObject.transform.parent = gameObject.transform;
        }
        else if (facing == Facing.Left)
        {
            var newHitbox = Instantiate(hitbox, gameObject.transform.position + new Vector3(-xDisplacement, 0, 0), Quaternion.identity) as HitboxScript;
            newHitbox.gameObject.transform.parent = gameObject.transform;
        }
    }
    void FireProjectile()
    {
        Vector3 projectiles = projectileSpawn;
        if (facing == Facing.Right)
        {
            projectiles.x = -projectiles.x;
            var newProjectile = Instantiate(projectile, gameObject.transform.position + projectiles, Quaternion.identity) as Projectile;
        }
        else if (facing == Facing.Left)
        {
            var newProjectile = Instantiate(projectile, gameObject.transform.position + projectiles, Quaternion.identity) as Projectile;
            //  newProjectile.transform.LookAt(target.gameObject.transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (DontRespawn && checkRespawn && !dying)
        {
            dying = true;
            Invoke("Death", 0f);
        }
        if (!dead)
        {
            if (parried)
            {
                CancelInvoke();
                Invoke("Parried", 0f);
                TerminateCollisions();
                parried = false;
            }
            switch (state)
            {
                case State.Standing:
                    if (needParry)
                    {
                        damageable = false;
                    }
                    ApplyFriction(1);
                    ChangeFacing();
                    if (attacking)
                    {
                        Invoke("Attack", 0f);
                    }
                    else if (aggro && movable)
                    {
                        Invoke("Move", 0f);
                    }
                    else if (!aggro)
                    {
                        Invoke("Idle", 0f);
                    }
                    break;
                case State.Idle:
                    ApplyFriction(1);
                    if (aggro)
                    {
                        Invoke("Aggroed", 0f);
                    }
                    break;
                case State.Moving:
                    if (Mathf.Sign(rb.velocity.x) != facingcorrector)
                    {
                        ApplyFriction(1);
                    }
                    ChangeFacing();
                    if (attacking)
                    {
                        Invoke("Attack", 0f);
                    }
                    else if (!aggro)
                    {
                        Invoke("Stand", 0f);
                    }

                    if (Mathf.Abs(rb.velocity.x) < MaxSpeed)
                    {
                        rb.velocity += new Vector2(facingcorrector * Acceleration, 0);
                    }
                    break;
                case State.NotMoving:
                    ApplyFriction(1);
                    break;
                case State.Parried:
                    ApplyFriction(1);
                    if (needParry)
                    {
                        damageable = true;
                    }
                    break;
            }
            if (recoveryTime > beginRecover)
            {
                if (stagger < staggermax)
                {
                    recovery++;
                    if (recovery > staggerRecoverRate)
                    {
                        stagger++;
                        recovery = 0;
                    }
                }
            }
            else
            {
                recoveryTime++;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            if (!dead)
            {
                HitboxScript hitbyhitbox = other.GetComponent<HitboxScript>();

                if (damageable)
                {
                    health = health - hitbyhitbox.damage;
                }
                if (damageable)
                {
                    if (target.transform.position.x < gameObject.transform.position.x)
                    {
                        rb.velocity += new Vector2(hitbyhitbox.damage, 0.1f);
                    }
                    else if (target.transform.position.x > gameObject.transform.position.x)
                    {
                        rb.velocity += new Vector2(-hitbyhitbox.damage, 0.1f);
                    }

                    ParticleSystem particler = GetComponentInChildren<ParticleSystem>();
                    particler.Emit(hitbyhitbox.damage * 5);
                }
                recoveryTime = 0;
                stagger -= hitbyhitbox.damage * 2;
                Destroy(hitbyhitbox.gameObject);
                if (state != State.Parried && stagger < 0)
                {
                    Debug.Log("Staggered");
                    CancelInvoke();
                    TerminateCollisions();
                    stagger = staggermax;
                    Invoke("Stagger", 0f);
                    Invoke("Stand", staggertime);
                }
                if (health <= 0)
                {
                    CancelInvoke();
                    Invoke("Death", 0f);
                }
            }
        }
        else if (other.CompareTag("EarthAoE") && !hit)
        {
            if (!dead)
            {
                HitboxScript hitbyhitbox = other.GetComponent<HitboxScript>();
                if (damageable)
                {
                    health = health - hitbyhitbox.damage;
                }
                if (damageable)
                {
                    if (target.transform.position.x < gameObject.transform.position.x)
                    {
                        rb.velocity += new Vector2(hitbyhitbox.damage, 0.1f);
                    }
                    else if (target.transform.position.x > gameObject.transform.position.x)
                    {
                        rb.velocity += new Vector2(-hitbyhitbox.damage, 0.1f);
                    }

                    ParticleSystem particler = GetComponentInChildren<ParticleSystem>();
                    particler.Emit(hitbyhitbox.damage * 5);
                }
                recoveryTime = 0;
                stagger -= hitbyhitbox.damage * 2;
                hit = true;
                Invoke("NotHit", 1f);
                if (state != State.Parried && stagger < 0)
                {

                    CancelInvoke();
                    TerminateCollisions();
                    stagger = staggermax;
                    Invoke("Stagger", 0f);
                    Invoke("Stand", staggertime);
                }
                if (health <= 0)
                {
                    CancelInvoke();
                    Invoke("Death", 0f);
                }
            }
        }
        else if(other.CompareTag("Shadow"))
        {
            rb.velocity = new Vector2(0, -10);
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "BehindTerrain";
            CancelInvoke();
            Invoke("ShadowDeath", 0f);
        }
    }
    public void kill()
    {
        CancelInvoke();
        Invoke("Death", 0f);
    }
}


