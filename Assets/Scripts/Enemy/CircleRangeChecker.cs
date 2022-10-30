using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CircleRangeChecker : MonoBehaviour
{
    public bool IsInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsInRange = false;
    }
}
