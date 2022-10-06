using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuaternion
{
    float x, y, z, w;

    // == CONSTRUCTORS
    public MyQuaternion(float xVal, float yVal, float zVal, float wVal)
    {
        x = xVal;
        y = yVal;
        z = zVal;
        w = wVal;
    }
    public MyQuaternion(float yaw, float pitch, float roll)
    {
        x = EulerToQuaternion(yaw, pitch, roll).x;
        y = EulerToQuaternion(yaw, pitch, roll).y;
        z = EulerToQuaternion(yaw, pitch, roll).z;
        w = EulerToQuaternion(yaw, pitch, roll).w;
    }
    public MyQuaternion(Vector3 angleAxis, float angle = 0.0f)
    {
        // NEED TO NORMALIZE A THINGY

        float s = Mathf.Sin(angle / 2);
     
        x = angleAxis.x * s;
        y = angleAxis.y * s;
        z = angleAxis.z * s;
        w = Mathf.Cos(angle / 2);
    }
    // ==

    ~MyQuaternion()
    {

    }

    // == EULER TO QUATERNION
    public Vector4 EulerToQuaternion(float yaw, float pitch, float roll)
    {
        Vector4 quaternion = new Vector4();

        float cosYaw = Mathf.Cos(yaw * 0.5f);
        float sinYaw = Mathf.Sin(yaw * 0.5f);
        float cosPitch = Mathf.Cos(pitch * 0.5f);
        float sinPitch = Mathf.Sin(pitch * 0.5f);
        float cosRoll = Mathf.Cos(roll * 0.5f);
        float sinRoll = Mathf.Sin(roll * 0.5f);

        quaternion.w = cosRoll * cosPitch * cosYaw + sinRoll * sinPitch * sinYaw;
        quaternion.x = sinRoll * cosPitch * cosYaw - cosRoll * sinPitch * sinYaw;
        quaternion.y = cosRoll * sinPitch * cosYaw + sinRoll * cosPitch * sinYaw;
        quaternion.z = cosRoll * cosPitch * sinYaw - sinRoll * sinPitch * cosYaw;

        return quaternion;
    }

    // == NORMALIZE
    public MyQuaternion Normalize(MyQuaternion myQuaternion)
    {
        Vector4 vector = new Vector4(myQuaternion.x, myQuaternion.y, myQuaternion.z, myQuaternion.w);

        vector *= 1 / Mathf.Sqrt((Mathf.Pow(myQuaternion.x, 2) + Mathf.Pow(myQuaternion.y, 2) + Mathf.Pow(myQuaternion.z, 2) + Mathf.Pow(myQuaternion.w, 2)));

        myQuaternion.x = vector.x;
        myQuaternion.y = vector.y;
        myQuaternion.z = vector.z;
        myQuaternion.w = vector.w;

        return myQuaternion;
    }

    // == CONJUGATE
    public MyQuaternion Conjugate(MyQuaternion myQuaternion)
    {
        //q? = q0 ? iq1 ? jq2 ? kq3
        myQuaternion.x = -myQuaternion.x;
        myQuaternion.y = -myQuaternion.y;
        myQuaternion.z = -myQuaternion.z;

        return myQuaternion;
    }
    public Vector4 Conjugate(Vector4 myVec4)
    {
        //q? = q0 ? iq1 ? jq2 ? kq3
        myVec4.x = -myVec4.x;
        myVec4.y = -myVec4.y;
        myVec4.z = -myVec4.z;

        return myVec4;
    }
    // ==

    // == INVERSE
    public MyQuaternion Inverse(MyQuaternion myQuaternion)
    {
        Vector4 vector = new Vector4(myQuaternion.x, myQuaternion.y, myQuaternion.z, myQuaternion.w);

        // q^-1 = q' / qw^2 + qx^2 + qy^2 + qz^2
        vector = Conjugate(vector) / (Mathf.Pow(vector.w, 2) + Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));

        myQuaternion.x = vector.x;
        myQuaternion.y = vector.y;
        myQuaternion.z = vector.z;
        myQuaternion.w = vector.w;

        return myQuaternion;
    }

    // == MULTIPLY
    public Vector4 Multiply(Vector4 quat1, Vector4 quat2)
    {
        Vector4 result = new Vector4();

        //a* e -b * f - c * g - d * h
        //+ i(b * e + a * f + c * h - d * g)
        //+ j(a * g - b * h + c * e + d * f)
        //+ k(a * h + b * g - c * f + d * e)


        // z1 * z2 = a*e - b*f - c*g - d*h + i (b*e + a*f + c*h - d*g) + j (a*g - b*h + c*e + d*f) + k (a*h + b*g - c*f + d*e)

        return result;
    }

    // == TO ANGLE AXIS
    public Vector3 ToAngleAxis()
    {
        // NOT DONE
        Vector3 result = new Vector3();

        return result;
    }
}
