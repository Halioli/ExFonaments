using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentScript : MonoBehaviour
{

    public Transform target1;
    public Transform target2;
	public int exercise = 1;

    // Exercise 2
    float angle2;
    Vector3 axis2;
    float tempAngle = 0f;
	
    // Use this for initialization
    void Start ()
    {
        // Exercise 2
        transform.rotation.ToAngleAxis(out angle2, out axis2);
        angle2 = transform.rotation.w;
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

        if (tempAngle <= angle2)
            tempAngle += 0.1f;

        target1.rotation = Quaternion.AngleAxis(tempAngle, axis2);
    }
}
