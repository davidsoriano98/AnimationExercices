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
                    //Buscamos un vector perpendicular a los dos vectores. Será nuestro axis
                    Vector3 axisX = Vector3.Cross(transform.right, target1.transform.right).normalized;

                    //Buscamos un angulo de rotacion -> El angulo tiene que ser negativo
                    float angleX = -Mathf.Acos(Vector3.Dot(transform.right, target1.transform.right)) * Mathf.Rad2Deg;

                    //Aplicamos la rotacion al objeto 1 en world space
                    target1.transform.Rotate(axisX, angleX,Space.World);

                    //Same for Y axis
                    Vector3 axisY = Vector3.Cross(transform.up, target1.transform.up).normalized;

                    //El angulo tiene que ser negativo
                    float angleY = -Mathf.Acos(Vector3.Dot(transform.up, target1.transform.up)) * Mathf.Rad2Deg;

                    target1.transform.Rotate(axisY, angleY,Space.World);

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
