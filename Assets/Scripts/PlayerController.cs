using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls player movement.
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Public Members
    /// <summary>
    /// Momement speed.
    /// </summary>
    [Range(0.0f, 100.0f)]
    public float moveSpeed = 1.5f;

    /// <summary>
    /// Movement smoothing factor.
    /// </summary>
    [Range(0.0f, 1.0f)]
    public float smoothTime = 0.25f;
    #endregion Public Members

    #region Private Members
    private Vector3 m_inputVector;
    private Vector3 m_velocity;
    private BoxCollider2D m_boxCollider2D;
    #endregion Private Members

    #region Unity Lifecycle Methods
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        m_inputVector = new Vector3();
        m_velocity = new Vector3();
        m_boxCollider2D = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Calculate target position
        Vector3 targetPos = transform.position + (m_inputVector * moveSpeed);

        // Calculate ray length according to input direction
        float rayLength = Vector3.Scale(m_boxCollider2D.bounds.extents, m_inputVector).magnitude;

        // Cast ray in the velocity direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_velocity, rayLength);
        Debug.DrawRay(transform.position, m_velocity.normalized * rayLength, Color.yellow);
        if (hit.collider != null)
        {
            // TODO: Figure this shit out
        }

        // Update position
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref m_velocity, smoothTime);
    }
    #endregion Unity Lifecycle Methods

    /// <summary>
    /// Handles messages from the Player->Movement action.
    /// </summary>
    /// <param name="value"></param>
    private void OnMovement(InputValue value)
    {
        Vector2 vector = value.Get<Vector2>();
        m_inputVector.x = vector.x;
        m_inputVector.y = vector.y;
    }
}
