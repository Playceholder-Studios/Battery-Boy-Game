using UnityEngine;

public class LightBullet : PlayerConsumable, IProjectile, IConsumable
{
    /// <summary>
    /// The damage of the projectile
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// The direction of the projectile
    /// </summary>
    public Vector2 direction;

    /// <summary>
    /// The range of the projectile before it stops moving
    /// </summary>
    public float range;

    /// <summary>
    /// The size of the projectile
    /// </summary>
    public float size;

    /// <summary>
    /// The speed of the projectile
    /// </summary>
    public float speed;

    /// <summary>
    /// The amount the projectile heals the player when picked up
    /// </summary>
    public int HealAmount = 1;

    /// <summary>
    /// How long this projectile lives
    /// before it disappears
    /// </summary>
    public float timeToLiveInSeconds = 1f;

    private float internalTimer = 0f;

    private Vector3 m_initialPosition;

    private bool hasStopped = false;

    private void Start()
    {
        m_initialPosition = transform.position;
    }

    private void Update()
    {
        if (!hasStopped)
        {
            transform.Translate(direction * speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_initialPosition) >= range)
            {
                speed = 0;
                hasStopped = true;
            }
        }
        else
        {
            internalTimer += Time.deltaTime;
        }

        if (internalTimer >= timeToLiveInSeconds)
        {
            Destroy(gameObject);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public override void Consume()
    {
        GameManager.GetPlayer().HealPlayer(HealAmount);
        Destroy(gameObject);
    }

    public void OnCollision()
    {
        throw new System.NotImplementedException();
    }
}
