using System;
using UnityEngine;
public abstract class SceneObject : MonoBehaviour
{
    public const float MAX_Y = 10000;
    public const float MIN_Y = -10000;

    protected virtual void Update() {
        float minPos = CameraTracker2D.Instance.transform.position.z;
        float newZ = (transform.position.y - MIN_Y)/(MAX_Y - MIN_Y) + minPos;
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }
}