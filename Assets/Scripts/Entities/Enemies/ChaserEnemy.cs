using UnityEngine;

public class ChaserEnemy : Enemy
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

    public bool shouldChasePlayer = false;

    public bool shouldOnlyMoveWhenAggroed = false;

    public CircleRangeChecker aggroRange;

    private float stopTimer = 0f;

    protected override void Start()
    {
        if (shouldChasePlayer && target == null)
        {
            target = GameObject.FindGameObjectWithTag(GameTag.Player.ToString()).transform;
        }
        currentTarget = target.position;
        currentSpeed = defaultSpeed;
        
        base.Start();
    }

    protected override void Update()
    {
        if (!base.isMoving) stopTimer -= Time.deltaTime;

        if (stopTimer <= 0)
        {
            stopTimer = Random.Range(0f, maxWaitInterval);
            base.MoveToTarget(currentTarget, currentSpeed, true);
        }

        base.Update();
    }

    protected override void Move()
    {
        if (isMoving && (!shouldOnlyMoveWhenAggroed || aggroRange.IsInRange))
        {
            transform.position = Vector3.MoveTowards(base.transform.position, base.currentTarget, base.currentSpeed * Time.deltaTime);
            base.moveTimer -= Time.deltaTime;
            if (base.moveTimer <= 0)
            {
                base.isMoving = false;
                base.currentTarget = target.position;
                base.currentSpeed = defaultSpeed;
            }
        }
    }

    protected override float GetTimeToMove()
    {
        return Random.Range(0f, maxMoveTime);
    }
}
