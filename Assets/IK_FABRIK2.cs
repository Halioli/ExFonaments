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
    }

    void Update()
    {
        // Copy the joints positions to work with
        for (int i = 0; i < joints.Length - 1; i++)
        {
            copy[i] = joints[i].position;

            if (i + 1 < joints.Length)
                distances[i] = Vector3.Distance(joints[i].position, joints[i + 1].position);
        }
        //TODO

        //done = TODO
        if (!done)
        {
            float targetRootDist = Vector3.Distance(copy[0], target.position);

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                for (int i = 0; i < copy.Length - 1; i++)
                {
                    float rDist = Vector3.Magnitude(target.position - copy[i]);
                    float lambda = distances[i] / rDist;

                    copy[i] = (1 - lambda) * copy[i] + lambda * target.position;
                }
            }
            else
            {
                // The target is reachable
                while (Vector3.Distance(copy[copy.Length - 1], target.position) > 0.1f)//(TODO)
                {
                    // STAGE 1: FORWARD REACHING
                    //TODO
                    copy[copy.Length - 1] = target.position;
                    for (int i = 0; i < copy.Length - 1; i++)
                    {
                        float rDist = Vector3.Magnitude(copy[i + 1] - copy[i]);
                        float lambda = distances[i] / rDist;

                        copy[i] = (1 - lambda) * copy[i + 1] + lambda * copy[i];
                    }

                    // STAGE 2: BACKWARD REACHING
                    //TODO
                    Vector3 initPos = copy[0];
                    for (int i = 0; i < copy.Length - 1; i++)
                    {

                    }
                }
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
               //TODO 
            }          
        }
    }

}
