using UnityEngine;
using UnityEngine.InputSystem;

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
    public float fireRate = 5.0f;

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

    public GameObject projectile;

    public GameObject currentSkill;

    #endregion Public Members

    #region Private Members
    [SerializeField]
    private GameObject pauseMenu;
    private Vector3 m_inputFireVector;
    private Vector3 m_inputMoveVector;
    private Rigidbody2D m_rigidbody2D;
    private float m_fireRateTimer;
    private float m_projectileSize = 1.5f;
    private PauseMenu m_pauseMenu;
    private ISkill m_currentSkill;
    #endregion Private Members

    #region Unity Lifecycle Methods
    private void OnValidate()
    {
        // Todo: Maybe change this check to be an attribute instead on in OnValidate
        if (!currentSkill.HasComponent<ISkill>())
        {
            currentSkill = null;
        }
    }

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        m_inputFireVector = new Vector3();
        m_inputMoveVector = new Vector3();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_pauseMenu = pauseMenu.GetComponent<PauseMenu>();
        m_currentSkill = currentSkill.GetComponent<ISkill>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (m_fireRateTimer > 0)
        {
            m_fireRateTimer -= Time.deltaTime;
        }

        if (m_inputFireVector != Vector3.zero)
        {
            // Timer reached 0, reset timer and spawn projectile
            if (m_fireRateTimer <= 0)
            {
                GameObject obj = Instantiate(projectile, transform.position + m_inputFireVector, Quaternion.identity);
                Projectile pj = obj.GetComponent<Projectile>();
                pj?.SetDirection(m_inputFireVector);
                pj?.SetSize(m_projectileSize);


                m_fireRateTimer = fireRate;
            }
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

    #endregion Unity Lifecycle Methods

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
    }

    /// <summary>
    /// Handles messages from the Player->Skill action.
    /// </summary>
    /// <param name="value"></param>
    private void OnSkill(InputValue value)
    {
        m_currentSkill?.Activate();
    }

    private void OnPause(InputValue pause)
    {
        m_pauseMenu?.TogglePause();
    }
}
