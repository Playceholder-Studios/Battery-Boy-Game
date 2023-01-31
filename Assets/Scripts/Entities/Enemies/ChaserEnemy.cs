using UnityEngine;

public class ChaserEnemy : Enemy
{
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

    public bool shouldOnlyMoveWhenAggroed = false;

    private float stopTimer = 0f;

    protected override void Start()
    {
        movementTarget = new Vector3();
        currentSpeed = defaultSpeed;
        
        base.Start();
    }

    protected override void Update()
    {
        if (!base.isMoving) stopTimer -= Time.deltaTime;

        if (stopTimer <= 0)
        {
            stopTimer = Random.Range(0f, maxWaitInterval);
            base.MoveToTarget(movementTarget, currentSpeed, true);
        }

        base.Update();
    }

    protected override void Move()
    {
        if (target != null && isMoving && (!shouldOnlyMoveWhenAggroed || base.isInRange(target.position)))
        {
            body.position = Vector3.MoveTowards(base.transform.position, base.movementTarget, base.currentSpeed * Time.deltaTime);
            base.moveTimer -= Time.deltaTime;
            if (base.moveTimer <= 0)
            {
                base.isMoving = false;
                base.currentSpeed = defaultSpeed;
            }
        } else if (target != null) {
            base.movementTarget = target.position;
        }
    }

    protected override float GetTimeToMove()
    {
        return Random.Range(0f, maxMoveTime);
    }
}
