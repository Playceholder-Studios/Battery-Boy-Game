using System;
using UnityEngine;

/// <summary>
/// Attach to a game object to be able to
/// subscribe to the <c>OnTriggerEnter</c> action
/// whenever this game object's <c>OnTriggerEnter2D</c> is called.
/// </summary>
public class OnTriggerEnter2DBroadcaster : MonoBehaviour
{
    public Action<Collider2D> OnTriggerEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEntered?.Invoke(collision);
    }
}
