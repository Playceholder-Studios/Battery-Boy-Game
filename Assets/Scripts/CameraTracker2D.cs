using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker2D : MonoBehaviour
{
    [SerializeField]
    protected bool isCameraTrap;

    [SerializeField]
    protected Vector2 cameraTrapSize = new Vector2(3, 3);

    private Bounds cameraTrapBounds;

    [SerializeField]
    protected Transform trackableObject;

    // Start is called before the first frame update
    void Start()
    {
        cameraTrapBounds = new Bounds(trackableObject.position, cameraTrapSize);
        transform.position = new Vector3(cameraTrapBounds.center.x, cameraTrapBounds.center.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraTrap)
        {
            Vector3 delta = Vector3.zero;

            if (trackableObject.position.x > cameraTrapBounds.max.x)
            {
                delta += new Vector3(trackableObject.position.x - cameraTrapBounds.max.x, 0);
            }
            else if (trackableObject.position.x < cameraTrapBounds.min.x)
            {
                delta -= new Vector3(cameraTrapBounds.min.x - trackableObject.position.x, 0);
            }

            if (trackableObject.position.y > cameraTrapBounds.max.y)
            {
                delta += new Vector3(0, trackableObject.position.y - cameraTrapBounds.max.y);
            }
            else if (trackableObject.position.y < cameraTrapBounds.min.y)
            {
                delta -= new Vector3(0, cameraTrapBounds.min.y - trackableObject.position.y);
            }

            cameraTrapBounds.center = cameraTrapBounds.center + delta;
        }
        else
        {
            cameraTrapBounds.center = trackableObject.position;
        }

        transform.position = new Vector3(cameraTrapBounds.center.x, cameraTrapBounds.center.y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        if (isCameraTrap)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(cameraTrapBounds.center, cameraTrapSize);
        }
    }
}
