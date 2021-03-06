using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    /// <summary>
    /// The amount of damage this enemy deals to the player when it collides with it.
    /// </summary>
    public int playerCollisionDamage = 1;

    public int defaultHealthAmount = 1;

    private Health health;

    void Awake()
    {
        health = new Health(defaultHealthAmount);
    }

    void Update()
    {
        CheckIfDead();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player))
        {
            collision.gameObject.GetComponent<PlayerController>()?.playerHealth.Damage(playerCollisionDamage);
            health.Damage(playerCollisionDamage);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Projectile))
        {
            health.Damage(collision.gameObject.GetComponent<Projectile>().damage);
        }
    }

    private void CheckIfDead()
    {
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    private bool IsDead()
    {
        if (health == null) return true;

        return health.CurrentHealth <= 0;
    }
}
