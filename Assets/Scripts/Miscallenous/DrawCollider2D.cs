using UnityEngine;

public class DrawCollider2D : MonoBehaviour
{
    public Color colliderGreen;

    private void Reset()
    {
        colliderGreen = new Color(145 / 255f, 244 / 255f, 139 / 255f, 1.0f);
    }

    private void OnDrawGizmos()
    {
        var colliders = GetComponents<Collider2D>();
        Gizmos.color = colliderGreen;
        foreach (var col in colliders)
        {
            if (col is BoxCollider2D)
                Gizmos.DrawWireCube(col.transform.position + new Vector3(col.offset.x, col.offset.y, 0), col.bounds.size);
            else if (col is CircleCollider2D circleCollider)
                Gizmos.DrawWireSphere(col.transform.position + new Vector3(col.offset.x, col.offset.y, 0), circleCollider.radius);
        }
    }
}
