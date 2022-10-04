using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentScript : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform target1;
    public Transform target2;
    public Transform headTransform;
	public int exercise = 1;

    float angle;
    Vector3 axis;
    Quaternion offset;
    Quaternion headOffset;

    // Use this for initialization
    void Start ()
    {
        // Exercise 3
        //offset = target1.rotation * Quaternion.Inverse(transform.rotation); // ex3 p1
        offset = cameraTransform.rotation * Quaternion.Inverse(transform.rotation); // ex3 p2
        headOffset = headTransform.rotation * Quaternion.Inverse(cameraTransform.rotation); // ex3 p3
    }

    void Update()
    {
        switch(exercise)
        {
            case 1:
            {
                    FirstExercise();
            } break;

            case 2:
            {
                    SecondExercise();
            } break;

            case 3:
            {
                    ThirdExercise();               
            } break;

            case 4:
            {
                
            } break;
        }
    }

    private void FirstExercise()
    {
        // Find the offset angles between target1 and tracker.
        // Use an “angle axis approach” to find explicitly
        // the angle offsets, then rotate using Rotate method.

        Vector3 axisX = Vector3.Cross(transform.right, target1.right).normalized;
        float angleX = -Mathf.Acos(Vector3.Dot(transform.right, target1.right)) * Mathf.Rad2Deg;
        if (Mathf.Abs(angleX) > 0.01f)
            target1.Rotate(axisX, angleX, Space.World);

        Vector3 axisY = Vector3.Cross(transform.up, target1.up).normalized;
        float angleY = -Mathf.Acos(Vector3.Dot(transform.up, target1.up)) * Mathf.Rad2Deg;
        if (Mathf.Abs(angleY) > 0.01f)
            target1.Rotate(axisY, angleY, Space.World);
    }

    private void SecondExercise()
    {
        // Make target1 align with tracker.
        //   • Use one line of code (use the quaternion that corresponds to the offset rotation)
        // Then, make it align with tracker, but slowly in time.
        //   • Use method Quaternion.AngleAxis
        //   • Use method Transform.Rotate

        offset = transform.rotation * Quaternion.Inverse(target1.rotation);
        offset.ToAngleAxis(out angle, out axis);

        if (Mathf.Abs(angle) > 0.01f)
        {
            target1.Rotate(axis, Mathf.Clamp(angle, -0.1f, 0.1f), Space.World);
        }
    }

    private void ThirdExercise()
    {
        // 1.
        // Make target1 follow tracker local rotations, if tracker rotates on the X axis,
        // target should rotate on it's own X axis.

        //target1.rotation = offset * transform.rotation;

        // 2.
        // Imagine "tracker" is an HMD tracker, and rotate the camera while keeping the offset.

        cameraTransform.rotation = offset * transform.rotation;

        // 3.
        // Now we want to apply it also tho the robot's head. But be careful!
        // The robot's head axis doesn't match the camera axis, we need to rotate
        // the head on the camera axis, not it's own.

        headTransform.rotation = cameraTransform.rotation * headOffset;
    }
}
