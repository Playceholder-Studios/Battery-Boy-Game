using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.eulerAngles = new Vector3(0, 0, angle);
        Debug.Log(angle);
        //transform.localEulerAngles = new Vector3(0, 0, angle);
        //transform.Rotate(Vector3.forward, angle);
        //transform.localEulerAngles = new Vector3(0, 0, angle);
        //transform.Rotate(Vector3.forward, angle);
    }
}
