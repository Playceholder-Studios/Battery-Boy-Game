using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    /// <summary>
    /// The target to move towards
    /// </summary>
    public Transform target;
    
    /// <summary>
    /// The maximum amount of time this game object should wait
    /// before moving towards the target.
    /// </summary>
    [Range(0.5f, 6.0f)]
    public float maxWaitInterval;
    
    /// <summary>
    /// The maximum amount of time this game object could move
    /// when moving towards the target.
    /// </summary>
    [Range(0.5f, 2.0f)]
    public float maxMoveTime;
    
    public float defaultSpeed = 1.0f;

    public bool isMoving = false;
    private float timeLeft = 0f;
    private float timeToMove = 0f;
    private Vector3 currentTarget;
    private float currentSpeed;

    private void Start()
    {
        currentTarget = target.position;
        currentSpeed = defaultSpeed;
    }

    void Update()
    {
        if (!isMoving) timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            timeLeft = Random.Range(0f, maxWaitInterval);
            timeToMove = GetTimeToMove();
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, currentSpeed * Time.deltaTime);
            timeToMove -= Time.deltaTime;
            if (timeToMove < 0)
            {
                isMoving = false;
                currentTarget = target.position;
                currentSpeed = defaultSpeed;
            }
        }

    }

    public void MoveToTarget(Vector3 target, float speed, bool resetTimeToMove = false)
    {
        if (resetTimeToMove) timeToMove = GetTimeToMove();
        isMoving = true;
        currentTarget = target;
        currentSpeed = speed;
    }

    private float GetTimeToMove()
    {
        return Random.Range(0f, maxMoveTime);
    }
}
