using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    #region Public Members
    /// <summary>
    /// The damage of the projectile
    /// </summary>
    public int damage;

    /// <summary>
    /// The direction of the projectile
    /// </summary>
    public Vector2 direction;

    /// <summary>
    /// The range of the projectile before destructing
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
    #endregion Public Properties

    #region Private Members
    private Vector3 m_initialPosition;
    #endregion Private Members

    void Awake()
    {
        // Set initial position
        m_initialPosition = transform.position;
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        CheckRange();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Enemy.ToString()))
        {
            Destroy(gameObject);
        }
    }

    private void CheckRange()
    {
        if (Vector3.Distance(transform.position, m_initialPosition) >= range)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void SetSize(float size)
    {
        gameObject.transform.localScale = Vector3.one * size;
    }

    public void OnCollision()
    {
        throw new System.NotImplementedException();
    }
}
