using UnityEngine;

/// <summary>
/// Moves a GameObject's transform within a random interval
/// </summary>
public class RandomMovement : MonoBehaviour
{
    /// <summary>
    /// The maximum amount of time this game object should wait
    /// before moving towards the target.
    /// </summary>
    [Range(0.5f, 6.0f)]
    public float maxWaitInterval;

    public float rangeOfMovement = 1.0f;

    private bool isMoving = false;
    private float timeLeft = 0.0f;
    private float followSpeed = 0.075f;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private float smallNumber = 0.001f;

    void Update()
    {
        if (!isMoving) timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            timeLeft = Random.Range(0f, maxWaitInterval);

            targetPosition = new Vector3(transform.position.x + Random.Range(-rangeOfMovement, rangeOfMovement), transform.position.y + Random.Range(-rangeOfMovement, rangeOfMovement), transform.position.z);
            isMoving = true;
        }

        if (Vector3.Distance(transform.position, targetPosition) < smallNumber && isMoving)
        {
            transform.position = targetPosition;
            isMoving = false;
        }


        if (isMoving) transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSpeed);

    }
}
