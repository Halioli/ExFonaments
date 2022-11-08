using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IK_FABRIK2 : MonoBehaviour
{
    public Transform[] joints;
    public Transform target;

    private Vector3[] copy;
    private float[] distances;
    private bool done;

    void Start()
    {
        distances = new float[joints.Length - 1];
        copy = new Vector3[joints.Length];


        for (int i = 0; i < joints.Length - 1; i++)
        {
            copy[i] = joints[i].position;

            if (i + 1 < joints.Length)
                distances[i] = Vector3.Distance(joints[i].position, joints[i + 1].position);
        }
    }

    void Update()
    {
        // Copy the joints positions to work with
        for (int i = 0; i < joints.Length - 1; i++)
        {
            copy[i] = joints[i].position;
        }
        //TODO

        //done = TODO
        done = false;

        if (!done)
        {
            float targetRootDist = Vector3.Distance(copy[0], target.position);

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                for (int i = 1; i < copy.Length - 1; i++)
                {
                    float rDist = Vector3.Magnitude(target.position - copy[i]);
                    float lambda = distances[i] / rDist;

                    copy[i] = (1 - lambda) * copy[i] + lambda * target.position;
                }

                done = true;
            }
            else
            {
                // The target is reachable
                while (Vector3.Distance(copy[copy.Length - 1], target.position) > 0.1f)//(TODO)
                {
                    // STAGE 1: FORWARD REACHING
                    //TODO
                    copy[copy.Length - 1] = target.position;
                    for (int i = copy.Length - 2; i >= 0; i--)
                    {
                        float rDist = Vector3.Magnitude(copy[i + 1] - copy[i]);
                        float lambda = distances[i] / rDist;

                        copy[i] = (1 - lambda) * copy[i + 1] + lambda * copy[i];
                    }

                    // STAGE 2: BACKWARD REACHING
                    //TODO
                    copy[0] = joints[0].position;
                    for (int i = 1; i < copy.Length - 1; i++)
                    {
                        float rDist = Vector3.Magnitude(copy[i - 1] - copy[i]);
                        float lambda = distances[i - 1] / rDist;

                        copy[i] = (1 - lambda) * copy[i - 1] + lambda * copy[i];
                    }
                }

                done = true;
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
                //TODO 
                Vector3 cross = Vector3.Cross(Vector3.Normalize(joints[i + 1].position - joints[i].position), Vector3.Normalize(copy[i + 1] - copy[i]));
                float dot = Vector3.Dot(Vector3.Normalize(joints[i + 1].position - joints[i].position), Vector3.Normalize(copy[i + 1] - copy[i]));

                joints[i].Rotate(cross, Mathf.Acos(dot) * Mathf.Rad2Deg, Space.World);
                //joints[i].position = copy[i];
            }     
        }
    }


    private void OnDrawGizmos()
    {
        if (copy == null)
            return;

        Gizmos.color = Color.red;
        for (int i = 0; i < copy.Length - 1; i++)
        {
            Gizmos.DrawSphere(copy[i], 0.2f);
        }
    }
}
