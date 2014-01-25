using UnityEngine;
using System.Collections;

public class RotatingLeg : Leg {

	
	public override void PropelForward(){
		legJoint.rigidbody.AddRelativeTorque(Vector3.forward * -swingForceForwardMovement);
	}
}
