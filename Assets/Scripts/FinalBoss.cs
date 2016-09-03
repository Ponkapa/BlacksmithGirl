using UnityEngine;
using System.Collections;

public class FinalBoss : MonoBehaviour {
    public bool dead;
    public int health;
    public int MaxHealth;
    public GameObject target;
    public bool hit;
    public static int clearStages = 1;
    public Vector3 projectiles;
    public Projectile sphere;
    public Projectile beam;
    private Animator self;
    public Animator mover;
    private bool Firing;
    public bool dying;
    public bool DontRespawn;
    public static bool FinalStage;
    // Use this for initialization

    void Start () {
        MaxHealth = 100 + 25 * clearStages;
        if (FinalStage)
        {
            MaxHealth *= 2;
        }
        health = MaxHealth;
        target = FindObjectOfType<PlayerController>().gameObject;
        self = GetComponent<Animator>();
        mover = gameObject.transform.parent.GetComponent<Animator>();
    }
	void Sphere()
    {
        self.SetTrigger("Fire");
        mover.enabled = false;
        Invoke("FireSphere", 0.5f);
        Invoke("MoveAgain", 0.6f);
    }
    void MoveAgain()
    {
        self.SetTrigger("Idle");
        mover.enabled = true;
        Firing = false;
    }
    void Beam()
    {
        self.SetTrigger("Fire");
        mover.enabled = false;
        Invoke("FireBeam", 0.1f);
        Invoke("FireBeam", 0.2f);
        Invoke("FireBeam", 0.3f);
        Invoke("FireBeam", 0.4f);
        Invoke("FireBeam", 0.5f);
        Invoke("MoveAgain", 0.6f);
    }
    void FireSphere()
    {
        var newProjectile = Instantiate(sphere, gameObject.transform.position + projectiles, Quaternion.identity) as Projectile;
        newProjectile.damage = 5 +5*((clearStages-1)/2);
        newProjectile.projectileSpeed = 5 + clearStages;
    }
    void FireBeam()
    {
        var newProjectile = Instantiate(beam, gameObject.transform.position + projectiles, Quaternion.identity) as Projectile;
        newProjectile.damage = 5 + 5 * ((clearStages - 1) / 2);
        newProjectile.projectileSpeed = 5 + clearStages;
    }
    void emitParticles()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Emit(100);
    }
    void Death()
    {
        if (FinalStage)
        {
            self.SetTrigger("Death");
            Invoke("Die", 5f);
            dead = true;
            if (gameObject.GetComponentInChildren<ParticleSystem>())
            {
                Invoke("emitParticles", 0f);
                Invoke("emitParticles", 0.5f);
                Invoke("emitParticles", 1f);
                Invoke("emitParticles", 1.5f);
                Invoke("emitParticles", 2f);
                Invoke("emitParticles", 2.5f);
                Invoke("emitParticles", 3f);
                Invoke("emitParticles", 3.5f);
            }
        }
        else
        {
            self.SetTrigger("Idle");
            mover.SetTrigger("Dead");
            dead = true;
            Invoke("Die", 1f);
        }
    }
    void Die()
    {
        if (FinalStage)
        {
            PlayerController.gameComplete = true;
        }
        Destroy(gameObject.transform.parent.gameObject);
    }
	// Update is called once per frame
	void Update () {
        if (DontRespawn && Enemy.checkRespawn && !dying)
        {
            dying = true;
            Invoke("Death", 0f);
        }
        if (!dead)
        {
            if (!Firing)
            {
                if (Random.Range(0, 100) == 1)
                {
                    Invoke("Sphere", 0f);
                    Firing = true;
                }
            }
            if (!Firing && FinalStage)
            {
                if (Random.Range(0,100) == 1)
                {
                    Invoke("Beam", 0f);
                    Firing = true;
                }
            }
        }
	}

    void NotHit()
    {
        hit = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            if (!dead)
            {
                HitboxScript hitbyhitbox = other.GetComponent<HitboxScript>();
                if (hitbyhitbox)
                {
                    health = health - hitbyhitbox.damage;
                }
                if (health <= 0)
                {
                    CancelInvoke();
                    Invoke("Death", 0f);
                }
                if (hitbyhitbox)
                {
                    foreach (Collider2D col in hitbyhitbox.GetComponents<Collider2D>())
                    {
                        Destroy(col);
                    }
                }
            }
        }
        else if (other.CompareTag("EarthAoE") && !hit)
        {
            if (!dead)
            {
                HitboxScript hitbyhitbox = other.GetComponent<HitboxScript>();
                health = health - hitbyhitbox.damage;
                
                Invoke("NotHit", 1f);
                if (health <= 0)
                {
                    CancelInvoke();
                    Invoke("Death", 0f);
                }
            }
        }
    }
}
