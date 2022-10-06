using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angleConstraints : MonoBehaviour
{
    public bool active;

    [Range(0.0f, 180.0f)]
    public float maxAngle;

    [Range(0.0f, 180.0f)]
    public float minAngle;

    public Transform parent;
    public Transform child;

    float angle;
    Vector3 axis;

    void Start()
    {
    }

    void LateUpdate()
    {
        if (active)
        {
            //solve your exercise here
            transform.localRotation.ToAngleAxis(out angle, out axis);

            angle = Mathf.Clamp(angle, 0, maxAngle);
            transform.localRotation = Quaternion.AngleAxis(angle, axis);
        }
    }

    //add auxiliary functions, if needed, below
    Quaternion GetTwistRot(Quaternion quaternion)
    {
        Quaternion newQuat = new Quaternion(0, quaternion.y, 0, quaternion.w); // depends on axis

        quaternion.y = newQuat.y * 1 / (Mathf.Sqrt(Mathf.Pow(newQuat.w, 2) + Mathf.Pow(newQuat.y, 2)));
        quaternion.w = newQuat.w * 1 / (Mathf.Sqrt(Mathf.Pow(newQuat.w, 2) + Mathf.Pow(newQuat.y, 2)));

        return quaternion;
    }

    Quaternion GetSwingRot(Quaternion quaternion)
    {
        Quaternion swingQuat = Quaternion.Inverse(GetTwistRot(quaternion)) * quaternion;

        return swingQuat;
    }
}
