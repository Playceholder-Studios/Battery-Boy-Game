using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : SceneObject, IEnemy
{
    const string DAMAGE_SOUND_LABEL = "enemyDamaged";
    const string DEATH_SOUND_LABEL = "enemyKilled";
    const string COLLISION_SOUND_LABEL = "enemyCollided";

    const float DEFAULT_MOVE_TIME = 1f;

    /// <summary>
    /// The amount of damage this enemy deals to the player when it collides with it.
    /// </summary>
    public int playerCollisionDamage = 1;

    public int defaultHealthAmount = 1;

    [field: SerializeField]
    public Action OnDeath;

    public UnityEvent EnemyDied;

    public AudioClip damageSound;
    public AudioClip deathSound;
    public AudioClip collisionSound;

    public PlayerConsumable[] drops;

    protected Health health;
    public float aggroRange;
    public Transform target;

    [NonSerialized]
    public bool isMoving = false;
    protected float moveTimer = 0f;
    public Vector3 movementTarget;
    protected float currentSpeed;

    private Rigidbody2D body;

    void Awake()
    {
        health = new Health(defaultHealthAmount);
    }

    protected virtual void Start()
    {
        AudioManager.Instance.SetEffect(DAMAGE_SOUND_LABEL, damageSound);
        AudioManager.Instance.SetEffect(DEATH_SOUND_LABEL, deathSound);
        AudioManager.Instance.SetEffect(COLLISION_SOUND_LABEL, collisionSound);

        body = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        Move();
        
        CheckIfDead();

        base.Update();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player.ToString()))
        {
            AudioManager.Instance.PlayEffect(COLLISION_SOUND_LABEL);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player.ToString()))
        {
            GameManager.GetPlayer().DamagePlayer(playerCollisionDamage, DamageType.Enemy);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Projectile.ToString()))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile.source.CompareTag(GameTag.Player.ToString())) 
            {
                health.Damage(projectile.damage);
                AudioManager.Instance.PlayEffect(DAMAGE_SOUND_LABEL);
            }
        }
    }

    protected virtual void Move()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, movementTarget, currentSpeed * Time.deltaTime);
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0)
            {
                isMoving = false;
                movementTarget = new Vector3();
                currentSpeed = 0f;
            }
        }
    }

    public virtual void MoveToTarget(Vector3 target, float speed, bool resetMoveTimer = false)
    {
        if (resetMoveTimer) moveTimer = GetTimeToMove();
        isMoving = true;
        movementTarget = target;
        currentSpeed = speed;
    }

    protected virtual float GetTimeToMove()
    {
        return DEFAULT_MOVE_TIME;
    }

    private void CheckIfDead()
    {
        if (IsDead())
        {
            OnDeath?.Invoke();
            AudioManager.Instance.PlayEffect(DEATH_SOUND_LABEL);
            GenerateDrops();
            Destroy(gameObject);
        }
    }

    private void GenerateDrops()
    {
        foreach (PlayerConsumable drop in drops)
        {
            drop.Spawn(transform.position);
        }
    }

    private bool IsDead()
    {
        if (health == null) return true;
        return health.CurrentHealth <= 0;
    }

    protected bool isInRange(Vector3 target) {
        return (target - transform.position).magnitude <= aggroRange;
    }
}
