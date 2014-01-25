using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterJoint))]
public class Leg : LocomotiveExtremity {
	public CharacterJoint legJoint;

	public override void ConnectedBody(Rigidbody body){
		legJoint.connectedBody = body;
	}

//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
