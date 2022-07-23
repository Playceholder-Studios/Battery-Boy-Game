using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this class to a game object to visualize
/// bounds. Control the size by adjusting the extents
/// of the Bounds.
/// </summary>
public class BoundsDrawer : MonoBehaviour
{
    public bool useObjectCenter;

    public Color color = new Color(1,1,1);

    public Bounds bounds;

    private Vector3 centerToDraw;

    void OnDrawGizmos()
    {
        if (useObjectCenter)
        {
            centerToDraw = transform.position;
        }
        else
        {
            centerToDraw = bounds.center;
        }

        Gizmos.color = color;
        Gizmos.DrawWireCube(centerToDraw, bounds.extents);
    }
}
