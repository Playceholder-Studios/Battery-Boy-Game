using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
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
