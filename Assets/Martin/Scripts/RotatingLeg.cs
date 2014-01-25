using UnityEngine;
using System.Collections;

public class RotatingLeg : Leg {

	
	public override void PropelForward(){
		joint.rigidbody.AddRelativeTorque(Vector3.forward * -swingForceForwardMovement);
	}
}
