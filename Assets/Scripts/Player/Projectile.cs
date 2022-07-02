using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    #region Public Members
    /// <summary>
    /// The damage of the projectile
    /// </summary>
    public float damage;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollision()
    {
        throw new System.NotImplementedException();
    }
}
