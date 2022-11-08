using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iksolver : MonoBehaviour
{

	// Array to hold all the joints
	// index 0 - root
	// index END - End Effector
	[SerializeField]
	GameObject[] joints;

	// The target for the IK system
	[SerializeField]
	GameObject targ;


	// Array of angles to rotate by (for each joint), as well as sin and cos values
	[SerializeField]
	float[] _theta, _sin, _cos;

	// To check if the target is reached at any point
	bool _done = false;

	// To store the position of the target
	private Vector3 tpos;

	// Max number of tries before the system gives up (Maybe 10 is too high?)
	[SerializeField]
	private int _mtries = 10;
	// The number of tries the system is at now
	[SerializeField]
	private int _tries = 0;

	// the range within which the target will be assumed to be reached
	readonly float _epsilon = 0.1f;

	//EndEffector
	Transform _endEffector;

	// Initializing the variables
	void Start()
	{
		_theta = new float[joints.Length];
		_sin = new float[joints.Length];
		_cos = new float[joints.Length];
		tpos = targ.transform.position;
		_endEffector = joints[3].transform;
	}

	// Running the solver - all the joints are iterated through once every frame
	void Update()
	{
		// if the target hasn't been reached
		if (!_done)
		{
			// if the Max number of tries hasn't been reached
			if (_tries <= _mtries)
			{
				// starting from the second last joint (the last being the end effector)
				// going back up to the root
				for (int i = joints.Length - 2; i >= 0; i--)
				{
					// The vector from the ith joint to the end effector
					Vector3 r1 = _endEffector.position - joints[i].transform.position;

					// The vector from the ith joint to the target
					Vector3 r2 = tpos - joints[i].transform.position;

					// to avoid dividing by tiny numbers
					if (r1.magnitude * r2.magnitude <= 0.001f)
					{
						// cos ? sin? 
						_cos[i] = 1;
						_sin[i] = 0;
					}
					else
					{
						// find the components using dot and cross product
						_cos[i] = Vector3.Dot(r1, r2) / (r1.magnitude * r2.magnitude);
						_sin[i] = Vector3.Cross(r1, r2).magnitude / (r1.magnitude * r2.magnitude);

					}

					// The axis of rotation 
					Vector3 axis = Vector3.Cross(r1, r2);

					// find the angle between r1 and r2 (and clamp values if needed avoid errors)
					_theta[i] = Mathf.Acos(_cos[i]);

					//Optional. correct angles if needed, depending on angles invert angle if sin component is negative
					if (_sin[i] < 0)
					{
						_theta[i] *= -1;
					}

					// obtain an angle value between -pi and pi, and then convert to degrees
					_theta[i] = ((float)SimpleAngle(_theta[i])) * Mathf.Rad2Deg;

					// rotate the ith joint along the axis by theta degrees in the world space.
					joints[i].transform.Rotate(axis, _theta[i], Space.World);

				}

				// increment tries
				_tries++;
			}
		}

		// find the difference in the positions of the end effector and the target
		float targetMagnitud = (tpos - _endEffector.position).magnitude;

		// if target is within reach (within epsilon) then the process is done
		if (Mathf.Abs(targetMagnitud) < _epsilon)
		{
			_done = true;
		}
		// if it isn't, then the process should be repeated
		else
		{
			_done = false;
		}

		// the target has moved, reset tries to 0 and change tpos
		if (targ.transform.position != tpos)
		{
			_tries = 0;
			tpos = targ.transform.position;
		}




	}


	// function to convert an angle to its simplest form (between -pi to pi radians)
	double SimpleAngle(double theta)
	{
		//Make sure it's int
		return theta - Mathf.PI * (int)(theta / Mathf.PI);
	}
}
