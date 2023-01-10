using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : SceneObject, IEnemy
{
    const string DAMAGE_SOUND_LABEL = "enemyDamaged";
    const string DEATH_SOUND_LABEL = "enemyKilled";
    const string COLLISION_SOUND_LABEL = "enemyCollided";

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

    private Health health;

    void Awake()
    {
        health = new Health(defaultHealthAmount);
    }

    void Start()
    {
        AudioManager.Instance.SetEffect(DAMAGE_SOUND_LABEL, damageSound);
        AudioManager.Instance.SetEffect(DEATH_SOUND_LABEL, deathSound);
        AudioManager.Instance.SetEffect(COLLISION_SOUND_LABEL, collisionSound);
    }

    protected override void Update()
    {
        CheckIfDead();

        base.Update();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player.ToString()))
        {
            GameManager.GetPlayer().DamagePlayer(playerCollisionDamage);
            AudioManager.Instance.PlayEffect(COLLISION_SOUND_LABEL);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Projectile.ToString()))
        {
            health.Damage(collision.gameObject.GetComponent<Projectile>().damage);
            AudioManager.Instance.PlayEffect(DAMAGE_SOUND_LABEL);
        }
    }

    private void CheckIfDead()
    {
        if (IsDead())
        {
            OnDeath?.Invoke();
            AudioManager.Instance.PlayEffect(DEATH_SOUND_LABEL);
            Destroy(gameObject);
        }
    }

    private bool IsDead()
    {
        if (health == null) return true;

        return health.CurrentHealth <= 0;
    }
}
