using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraTracker2D : MonoBehaviour
{
    #region Protected Members
    /// <summary>
    /// Toggles whether the camera tracker follows from the center of the 
    /// <see cref="trackableObject"/> or from the center of the camera trap with the bounds defined by <see cref="cameraTrapSize"/>
    /// </summary>
    [SerializeField]
    protected bool isCameraTrap;

    /// <summary>
    /// Sets the size of the camera trap to be the size of the camera viewport when enabled.
    /// </summary>
    [SerializeField]
    protected bool isSizeOfViewPort;

    /// <summary>
    /// The area where the <see cref="trackableObject"/> can move before the camera
    /// starts following.
    /// </summary>
    [SerializeField]
    protected Vector2 cameraTrapSize = new Vector2(3, 3);

    /// <summary>
    /// The object that is going to be followed by the camera tracker.
    /// </summary>
    [SerializeField]
    protected Transform trackableObject;

    /// <summary>
    /// Approximately the time it will take the camera to reach the target in seconds when following.
    /// The smaller the value, the faster the camera will reach the target.
    /// </summary>
    [SerializeField]
    protected float followSpeed = 0.125f;
    
    #endregion

    private Bounds cameraTrapBounds;
    private Camera cam;
    
    /// <summary>
    /// Used to keep track of how fast the camera follow is moving at a particular frame
    /// It is also needed by the implemention of Unity's Vector3.SmoothDamp
    /// </summary>
    private Vector3 velocity = Vector3.zero;

    public static CameraTracker2D Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        if (isSizeOfViewPort)
        {
            cameraTrapSize = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize) * 2;
        }
        cameraTrapBounds = new Bounds(trackableObject.position, cameraTrapSize);
        transform.position = new Vector3(cameraTrapBounds.center.x, cameraTrapBounds.center.y, transform.position.z);
    }

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
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(cameraTrapBounds.center.x, cameraTrapBounds.center.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSpeed);
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
