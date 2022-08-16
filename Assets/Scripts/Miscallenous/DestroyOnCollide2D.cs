using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to add to a game object to destroy
/// when a collision occurs.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DestroyOnCollide2D : MonoBehaviour
{
    /// <summary>
    /// Whether the game object is destroyed from any collision.
    /// </summary>
    public bool destroyOnAllCollision = true;

    /// <summary>
    /// A list of game object tags that this game object will be destroyed by when colliding.
    /// </summary>
    public List<GameTag> collisionTagList;
    private List<string> tagStringList;

    private void OnValidate()
    {
        if (collisionTagList.Count > 0)
        {
            destroyOnAllCollision = false;
        }
        else
        {
            destroyOnAllCollision = true;
        }
    }

    private void Start()
    {
        tagStringList = collisionTagList.Select(t => t.ToString()).ToList();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyOnAllCollision || tagStringList.Contains(collision.gameObject.tag)) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnAllCollision || tagStringList.Contains(collision.gameObject.tag)) {
            Destroy(gameObject);
        }
    }
}
