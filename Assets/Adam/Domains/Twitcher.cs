using UnityEngine;
using System.Collections;

public class Twitcher : MonoBehaviour {
	HingeJoint joint;
	float MIN_FORCE = 250f, MAX_FORCE = 1000f;
	float MIN_VELOCITY = -250f, MAX_VELOCITY = 250f;

	float timer;
	float CHANGE_TIME;
	
	// Use this for initialization
	void Start () {
		CHANGE_TIME = Random.Range(0.25f, 1f);
		timer = CHANGE_TIME;

		joint = GetComponent<HingeJoint> ();
		joint.useMotor = true;
		JointMotor motor = joint.motor;
		motor.force = Random.Range (MIN_FORCE, MAX_FORCE) * 
			transform.localScale.x * transform.localScale.y * transform.localScale.z;;
		motor.targetVelocity = Random.Range (MIN_VELOCITY, MAX_VELOCITY);
		joint.motor = motor;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0f) {
			JointMotor motor = joint.motor;
			motor.targetVelocity = Random.Range (MIN_VELOCITY, MAX_VELOCITY);
			joint.motor = motor;

			timer = CHANGE_TIME;
		}
	}
}
