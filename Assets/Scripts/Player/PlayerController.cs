using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Controls player movement.
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Public Members
    /// <summary>
    /// Fire rate.
    /// </summary>
    [Range(0.0f, 10.0f)]
    public float fireRate;

    /// <summary>
    /// Momement speed.
    /// </summary>
    [Range(0.0f, 100.0f)]
    public float moveSpeed = 80.0f;

    /// <summary>
    /// Movement smoothing factor.
    /// </summary>
    [Range(0.0f, 1.0f)]
    public float smoothFactor = 0.825f;

    public bool hasKey { get; set; }

    public GameObject projectile;

    public GameObject currentSkill;

    public GameObject keyHolder;

    public AudioClip footsteps;

    [InspectorName("Max Player Health")]
    public int playerMaxHealth = 10;

    [InspectorName("Max Player Projectile")]
    public int playerMaxProjectile = 10;

    [HideInInspector]
    public Health playerHealth { get; private set; }

    public delegate void IntDel(int projectileCount);
    public event IntDel OnProjectileUpdate;
    #endregion Public Members

    #region Private Members
    private Vector3 m_inputFireVector;
    private Vector3 m_inputMoveVector;
    private Rigidbody2D m_rigidbody2D;
    private float m_defaultFireRate;
    private float m_fireRateTimer;
    private float m_projectileSize = 1.5f;
    
    /// <summary>
    /// The amount the players health goes down when a projectile is shot
    /// </summary>
    private int m_projectileHealthDamage = 1;
    private ISkill m_currentSkill;
    #endregion Private Members

    #region Unity Lifecycle Methods
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        playerHealth = new Health(playerMaxHealth);
        m_inputFireVector = new Vector3();
        m_inputMoveVector = new Vector3();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_currentSkill = currentSkill.GetComponent<ISkill>();
        m_defaultFireRate = fireRate;
    }

    private void Update()
    {
        if (m_fireRateTimer > 0)
        {
            m_fireRateTimer -= Time.deltaTime;
        }

        if (CanShoot())
        {
            GameObject obj = Instantiate(projectile, transform.position + m_inputFireVector, Quaternion.identity);
            Projectile pj = obj.GetComponent<Projectile>();
            playerHealth.Damage(m_projectileHealthDamage);
            if (OnProjectileUpdate != null)
            {
                OnProjectileUpdate(playerHealth.CurrentHealth);
            }
            pj?.SetDirection(m_inputFireVector);
            pj?.SetSize(m_projectileSize);

            m_fireRateTimer = fireRate;
        }

        if (hasKey)
        {
            keyHolder.SetActive(true);
        }
        Debug.DrawLine(transform.position, transform.position + (m_inputFireVector * 2f), Color.yellow);
    }

    /// <summary>
    /// FixedUpdate is called 50 times per second.
    /// </summary>
    private void FixedUpdate()
    {
        // Apply force and apply smoothing factor
        m_rigidbody2D.AddForce(m_inputMoveVector * moveSpeed);
        m_rigidbody2D.velocity *= smoothFactor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var unlockableWall = collision.gameObject.HasComponent<UnlockableWall>();
        if (hasKey && unlockableWall != null)
        {
            unlockableWall.Unlock();
            keyHolder.SetActive(false);
            hasKey = false;
        }
    }

    #endregion Unity Lifecycle Methods

    #region Unity Input Methods

    /// <summary>
    /// Handles messages from the Player->Fire action.
    /// </summary>
    /// <param name="value"></param>
    private void OnFire(InputValue value)
    {
        m_inputFireVector = value.Get<Vector2>();
    }

    /// <summary>
    /// Handles messages from the Player->Movement action.
    /// </summary>
    /// <param name="value"></param>
    private void OnMovement(InputValue value)
    {
        m_inputMoveVector = value.Get<Vector2>();
        AudioManager.Instance.Play(footsteps);
    }

    /// <summary>
    /// Handles messages from the Player->Skill action.
    /// </summary>
    /// <param name="value"></param>
    private void OnSkill(InputValue value)
    {
        m_currentSkill?.Activate();
    }

    #endregion Unity Input Methods

    private bool CanShoot()
    {
        // m_inputFireVector is updated by OnFire
        return playerHealth.CurrentHealth > 1 + (m_projectileHealthDamage - 1) && m_inputFireVector != Vector3.zero && m_fireRateTimer <= 0;
    }

    public void HealPlayer(int amount)
    {
        playerHealth.Heal(amount);
        if (OnProjectileUpdate != null)
        {
            OnProjectileUpdate(playerHealth.CurrentHealth);
        }
    }

    public void UpdateFireRate(float newFireRate)
    {
        fireRate = newFireRate;
    }

    public void ResetFireRate()
    {
        fireRate = m_defaultFireRate;
    }
}
