using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	HingeJoint joint;
	float MIN_FORCE = 50f, MAX_FORCE = 500f;
	float MIN_VELOCITY = -100f, MAX_VELOCITY = 100f;

	// Use this for initialization
	void Start () {
		joint = GetComponent<HingeJoint> ();
		joint.useMotor = true;
		JointMotor motor = joint.motor;
		motor.force = Random.Range (MIN_FORCE, MAX_FORCE);
		motor.targetVelocity = Random.Range (MIN_VELOCITY, MAX_VELOCITY);
		joint.motor = motor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
