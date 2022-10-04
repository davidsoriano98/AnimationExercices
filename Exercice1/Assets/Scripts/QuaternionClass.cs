using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionClass : MonoBehaviour
{
    public float w;
    public float x;
    public float y;
    public float z;

    public Vector3 axis;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //q1 = q1/sqrt(q1^2)
    QuaternionClass Normalise(QuaternionClass q1) 
    {
        float module = Mathf.Sqrt(q1.w * q1.w + q1.x * q1.x + q1.y * q1.y + q1.z * q1.z);

        q1.w = q1.w / module;
        q1.x = q1.x / module;
        q1.y = q1.y / module;
        q1.z = q1.z / module;

        return q1;    
    }

    //q1 + q2
    QuaternionClass Add(QuaternionClass q1, QuaternionClass q2) 
    {
        QuaternionClass q3 = new QuaternionClass();

        q3.w = q1.w + q2.w;
        q3.x = q1.x + q2.x;
        q3.y = q1.y + q2.y;
        q3.z = q1.z + q2.z;

        return q3;

    }

    //q1-q2
    QuaternionClass Substract(QuaternionClass q1, QuaternionClass q2)
    {
        QuaternionClass q3 = new QuaternionClass();

        q3.w = q1.w - q2.w;
        q3.x = q1.x - q2.x;
        q3.y = q1.y - q2.y;
        q3.z = q1.z - q2.z;

        return q3;

    }

    //q1 * q2
    QuaternionClass Multiply(QuaternionClass q1, QuaternionClass q2)
    {
        QuaternionClass q3 = new QuaternionClass();

        q3.w = q1.w * q2.w + -q1.x * q2.x - q1.y * q2.y - q1.z * q2.z;
        q3.x = q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y;
        q3.y = q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x; ;
        q3.z = q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w;

        return q3;

    }
}
