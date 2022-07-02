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
    #endregion Public Members

    #region Private Members
    private Vector3 m_inputFireVector;
    private Vector3 m_inputMoveVector;
    private Rigidbody2D m_rigidbody2D;

    private float m_fireRateTimer;
    #endregion Private Members

    #region Unity Lifecycle Methods
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        m_inputFireVector = new Vector3();
        m_inputMoveVector = new Vector3();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (m_inputFireVector != Vector3.zero)
        {
            if (m_fireRateTimer > 0)
            {
                m_fireRateTimer -= Time.deltaTime;
            }
            // Timer reached 0, reset timer and spawn projectile
            else
            {
                GameObject obj = Instantiate(projectile, transform.position + m_inputFireVector, Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().AddForce(m_inputFireVector * 100);

                m_fireRateTimer = fireRate;
            }
        }
        // Reset fire rate timer
        else
        {
            m_fireRateTimer = fireRate;
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
}
