using UnityEngine;

public class CamBoundObject : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    float camspeed = 5f;

    private void LateUpdate()
    {
        Vector3 camPoint = target.position + offset;
        if (target != null)
            transform.position = Vector3.Slerp(target.position, camPoint, camspeed);
        else
            transform.position = offset;
    }
}
