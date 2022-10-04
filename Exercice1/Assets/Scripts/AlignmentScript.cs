using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentScript : MonoBehaviour
{

    public Transform target1;
    public Transform target2;
    public Transform head;   
	public int exercise = 1;

    //Exercice 2
    private float angle;
    private float angleTEMP = 0;
    private Vector3 axis;

    //Exercice 3
    private Quaternion camOffset;
    private Quaternion headOffset;

    Quaternion Conjugate(Quaternion q1)
    {
        Quaternion q2;
        q2.x = -q1.x;
        q2.y = -q1.y;
        q2.z = -q1.z;
        q2.w = q1.w;

        return q2;
    }

    //Use this for initialization
    void Start ()
    {
        //Exercice 2
        //Save rotation in angle & axis
        transform.rotation.ToAngleAxis(out angle, out axis);

        //Exercice 3
        //Diferencia de quaternions entre camara y objeto
        camOffset = Camera.main.transform.rotation * Conjugate(transform.rotation);

        headOffset = head.rotation * Conjugate(transform.rotation);
       
    }

    void Update()
    {
        switch(exercise)
        {
            case 1:
            {
                    //Find the offset angles between target1 and tracker.
                    //Use an “angle axis approach” to find explicitly the angle offsets,
                    //then rotate using Rotate method. 


                    //Take care of the X rotation

                    //Search a perpendicular vector between the two vectors. It will be our axis
                    Vector3 axisX = Vector3.Cross(transform.right, target1.transform.right).normalized;

                    //Find the angle to rotate -> The angle needs to be negative.
                    float angleX = -Mathf.Acos(Vector3.Dot(transform.right, target1.transform.right)) * Mathf.Rad2Deg;
                    
                    //Make sure the angle is not an "imposible" angle. Near to 0 appear lots of vectors and the rotation explotes 
                    if(Mathf.Abs(angleX) > 0.01)
                    {
                        //Apply the X rotation in world space.
                        target1.transform.Rotate(axisX, angleX, Space.World);
                    }



                    //Take care of the Y rotation

                    //Search a perpendicular vector between the two vectors. It will be our axis
                    Vector3 axisY = Vector3.Cross(transform.up, target1.transform.up).normalized;

                    //Find the angle to rotate -> The angle needs to be negative.
                    float angleY = -Mathf.Acos(Vector3.Dot(transform.up, target1.transform.up)) * Mathf.Rad2Deg;

                    //Make sure the angle is not an "imposible" angle. Near to 0 appear lots of vectors and the rotation explotes 
                    if (Mathf.Abs(angleY) > 0.01)
                    {
                        //Apply the Y rotation in world space.
                        target1.transform.Rotate(axisY, angleY, Space.World);
                    }

                } break;

          

            case 2:
            {

                    //Make target1 align with tracker.
                    //•Use one line of code(use the quaternion that corresponds to the offset rotation).
                    //Then, make it align with tracker, but slowly in time.
                    //•Use method Quaternion.AngleAxis
                    //•Use method Transform.Rotate

                    //Use the variables save on the start to make the rotation. 
                    //target1.rotation = Quaternion.AngleAxis(angle, axis); //-> AngleAxis build a quaternion.

                    //Make a smooth transition
                    if (angleTEMP < angle)
                    {
                        target1.rotation = Quaternion.AngleAxis(angleTEMP, axis);
                        angleTEMP += 0.1f;
                    }
                }
                break;

            case 3:
            {
                   //Aplica la rotacion del objeto y luego aplicas offset para moverlo
                    Camera.main.transform.rotation = camOffset * transform.rotation;

                    head.rotation = transform.rotation * headOffset;

            } break;

            case 4:
            {
                
            } break;
        }
    }
}
