using UnityEngine;
using System.Collections;

public class Head : BodyPart {
	public CharacterJoint headOrNeckJoint	;

	public override BodyPartDomain Domain{get{return BodyPartDomain.Head;}}
	
	public override void ConnectedBody(Rigidbody body){
		headOrNeckJoint.connectedBody = body;
	}
}
