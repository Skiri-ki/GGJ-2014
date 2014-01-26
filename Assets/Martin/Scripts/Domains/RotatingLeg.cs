using UnityEngine;
using System.Collections;

public class RotatingLeg : Leg {


	void Start(){
		AnchorPoint anchor = transform.parent.GetComponent<AnchorPoint>();
		if(anchor && anchor.seed>0){
			UnityEngine.Random.seed = anchor.seed;
		}
		JointMotor motor = new JointMotor();
		motor.targetVelocity = UnityEngine.Random.Range(-1.0f,1.0f);
		motor.force = motor.targetVelocity;
		hinge.assignInEditorHinge.motor = motor;
		hinge.assignInEditorHinge.useMotor = true;

	}

	public override void PropelForward(){
//		joint.rigidbody.AddRelativeTorque(Vector3.forward * -swingForceForwardMovement);
	}
}
