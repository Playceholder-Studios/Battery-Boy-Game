using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Controls player movement.
/// </summary>
public class PlayerController : SceneObject
{
    const string FOOTSTEPS_SOUND_LABEL = "footsteps";
    const string FIRE_SOUND_LABEL = "fireProjectile";
    const string DAMAGE_SOUND_LABEL = "playerDamaged";

    /// <summary>
    /// Fire rate.
    /// </summary>
    [Range(0.0f, 10.0f)]
    public float fireRate;

    [Range(0.0f, 10.0f)]
    public float invulnWindow;

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

    public int defaultProjectileSelfDamage = 1;

    public bool hasKey { get; set; }

    public GameObject projectile;
    public GameObject currentSkill;
    public GameObject keyHolder;

    public AudioClip footstepsSound;
    public AudioClip fireSound;
    public AudioClip damageSound;

    public IInteractable currentInteractable;

    [InspectorName("Max Player Health")]
    public int playerMaxHealth = 10;

    [InspectorName("Max Player Projectile")]
    public int playerMaxProjectile = 10;

    [HideInInspector]
    public Health playerHealth { get; private set; }

    public delegate void IntDel(int projectileCount);
    public event IntDel OnProjectileUpdate;

    private Vector3 m_inputFireVector;
    private Vector3 m_inputMoveVector;
    private Rigidbody2D m_rigidbody2D;
    private float m_defaultFireRate;
    private float m_fireRateTimer;
    private float m_projectileSize = 1.5f;

    private float iframes = 0;
    private float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public SpriteRenderer spriteRenderer;

    private bool moving = false;
    
    /// <summary>
    /// The amount the players health goes down when a projectile is shot
    /// </summary>
    public int projectileHealthDamage;
    private ISkill m_currentSkill;

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

        AudioManager.Instance.SetEffect(FOOTSTEPS_SOUND_LABEL, footstepsSound, true);
        AudioManager.Instance.SetEffect(FIRE_SOUND_LABEL, fireSound);
        AudioManager.Instance.SetEffect(DAMAGE_SOUND_LABEL, damageSound);

        ResetProjectileSelfDamage();
    }

    protected override void Update()
    {
        // Damage taken
        if (iframes > 0)
        {
            SpriteBlinkingEffect(true);
            iframes -= Time.deltaTime;
        }
        else 
        {
            SpriteBlinkingEffect(false);
        }

        // Fire projectile
        if (m_fireRateTimer > 0)
        {
            m_fireRateTimer -= Time.deltaTime;
        }

        if (CanShoot())
        {
            Vector3 projectileSpawnLocation = transform.position + m_inputFireVector * 2;
            GameObject obj = Instantiate(projectile, projectileSpawnLocation, Quaternion.identity);
            Projectile pj = obj.GetComponent<Projectile>();
            DamagePlayer(projectileHealthDamage, DamageType.PlayerSelf);
            pj?.SetDirection(m_inputFireVector);
            pj?.SetSize(m_projectileSize);
            pj?.SetFireSound(FIRE_SOUND_LABEL);     
            pj.source = this.gameObject;

            m_fireRateTimer = fireRate;
        }

        if (hasKey != keyHolder.activeSelf)
        {
            keyHolder.SetActive(hasKey);
        }
        Debug.DrawLine(transform.position, transform.position + (m_inputFireVector * 2f), Color.yellow);

        base.Update();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Projectile.ToString()))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (!projectile.source.CompareTag(GameTag.Player.ToString())) 
            {
                DamagePlayer(projectile.damage, DamageType.Enemy);
            }
        }
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
        if (m_inputMoveVector == Vector3.zero)
        {
            moving = false;
            AudioManager.Instance.StopEffect(FOOTSTEPS_SOUND_LABEL);
        } else {
            if (!moving) 
            {
                moving = true;
                AudioManager.Instance.PlayEffect(FOOTSTEPS_SOUND_LABEL);
            }
        }
    }

    /// <summary>
    /// Handles messages from the Player->Skill action.
    /// </summary>
    /// <param name="value"></param>
    private void OnSkill(InputValue value)
    {
        m_currentSkill?.Activate();
    }

    private void OnInteract(InputValue value)
    {
        currentInteractable?.Interact();
    }

    private bool CanShoot()
    {
        // m_inputFireVector is updated by OnFire
        return playerHealth.CurrentHealth > 1 + (projectileHealthDamage - 1) && m_inputFireVector != Vector3.zero && m_fireRateTimer <= 0;
    }

    private void SpriteBlinkingEffect(bool enable)
    {
        if(!enable)
        {
            spriteRenderer.enabled = true;
        } 
        else 
        {
            spriteBlinkingTimer += Time.deltaTime;
            if(spriteBlinkingTimer >= spriteBlinkingMiniDuration)
            {
                spriteBlinkingTimer = 0.0f;
                if (spriteRenderer.enabled == true) {
                    spriteRenderer.enabled = false;
                } else {
                    spriteRenderer.enabled = true;
                }
            }
        }
    }

    public void DamagePlayer(int amount, DamageType dmgType)
    {
        if (iframes <= 0 || dmgType == DamageType.PlayerSelf)
        {
            if (dmgType != DamageType.PlayerSelf)
            {
                iframes = invulnWindow;
                AudioManager.Instance.PlayEffect(DAMAGE_SOUND_LABEL);
            }
            playerHealth.Damage(amount);
            if (OnProjectileUpdate != null)
            {
                OnProjectileUpdate(playerHealth.CurrentHealth);
            }
        }
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

    public void ResetProjectileSelfDamage()
    {
        projectileHealthDamage = defaultProjectileSelfDamage;
    }
}
