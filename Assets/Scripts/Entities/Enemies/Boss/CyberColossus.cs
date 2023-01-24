using UnityEngine;

public class CyberColossus : Enemy
{
    public GameObject[] MovementTarget;
    private PlayerController player;
    private int currentTargetIndex;
    private int movementTargetCount;

    // To handle lerping
    private float timeLerpElapsed;
    private Vector3 startingPosition;
    public float timeToLerpInSeconds = 10;

    protected override void Start()
    {
        player = GameManager.GetPlayer();
        ResetTargetMovement();
        movementTargetCount = MovementTarget.Length;
        startingPosition = transform.position;
    }

    protected override void Move()
    {
        transform.position = Vector3.Lerp(startingPosition, MovementTarget[currentTargetIndex].transform.position, timeLerpElapsed / timeToLerpInSeconds);
        timeLerpElapsed += Time.deltaTime;
        CheckIfTargetReached();
    }

    private void ResetTargetMovement()
    {
        currentTargetIndex = 0;
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
