using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Projectile : SceneObject, IProjectile
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

    /// <summary>
    /// The fire sound of the projectile
    /// </summary>
    public string fireSoundLabel;

    public GameObject source;

    #endregion Public Properties

    #region Private Members
    private Vector3 m_initialPosition;
    #endregion Private Members

    public List<GameTag> collisionTagList;
    private List<string> tagStringList;

    void Awake()
    {
        // Set initial position
        m_initialPosition = transform.position;
        tagStringList = collisionTagList.Select(t => t.ToString()).ToList();
    }

    void Start()
    {
        if (fireSoundLabel != "")
        {
            AudioManager.Instance.PlayEffect(fireSoundLabel);
        }
    }

    protected override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        CheckRange();

        base.Update();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Hits something. hacky and needs more generic typechecking
        if (collision.gameObject != source && tagStringList.Contains(collision.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }

    /// Destroy projectile if its traveled past its max range
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

    public void SetFireSound(string label)
    {
        this.fireSoundLabel = label;
    }

    public void OnCollision()
    {
        throw new System.NotImplementedException();
    }
}
