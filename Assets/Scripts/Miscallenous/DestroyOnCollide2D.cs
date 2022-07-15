using UnityEngine;

/// <summary>
/// Simple script to add to a game object
/// to destroy when collision occurs.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DestroyOnCollide2D : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
       Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
