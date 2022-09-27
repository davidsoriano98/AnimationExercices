using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentScript : MonoBehaviour
{

    public Transform target1;
    public Transform target2;
	public int exercise = 1;


    // Use this for initialization
    void Start ()
    {
       
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
               
            } break;

            case 3:
            {
               
            } break;

            case 4:
            {
                
            } break;
        }
    }
}
