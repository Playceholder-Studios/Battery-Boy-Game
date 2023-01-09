using UnityEngine;

public class CyberColossus : Enemy
{
    public GameObject[] MovementTarget;
    private PlayerController player;
    private Transform currentTarget;
    private int currentTargetIndex;
    private int movementTargetCount;

    // To handle lerping
    private float timeLerpElapsed;
    private Vector3 startingPosition;
    public float timeToLerpInSeconds = 10;

    private void Start()
    {
        player = GameManager.GetPlayer();
        ResetTargetMovement();
        movementTargetCount = MovementTarget.Length;
        startingPosition = transform.position;
    }

    protected override void Update()
    {
        transform.position = LerpToTarget(MovementTarget[currentTargetIndex].transform.position);
        CheckIfTargetReached();

        base.Update();
    }

    private void ResetTargetMovement()
    {
        currentTargetIndex = 0;
    }

    private Vector3 LerpToTarget(Vector3 target)
    {
        Vector3 currentPosition = Vector3.Lerp(startingPosition, target, timeLerpElapsed / timeToLerpInSeconds);
        timeLerpElapsed += Time.deltaTime;
        return currentPosition;
    }

    private void CheckIfTargetReached()
    {
        float distSqrToTarget = (MovementTarget[currentTargetIndex].transform.position - transform.position).sqrMagnitude;
        if (Mathf.Approximately(distSqrToTarget, 0))
        {
            currentTargetIndex += 1;
            currentTargetIndex %= movementTargetCount;
            timeLerpElapsed = 0;
            startingPosition = transform.position;
        }
    }
}
