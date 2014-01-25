using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterJoint))]
public class Leg : LocomotiveExtremity {
	public CharacterJoint legJoint;
	public bool autoPropel = true;
	public float swingFrequenzyForwardMovement = 1.0f;//in seconds between swings
	public float swingFrequenzyBackMovement = 1.0f;//in seconds between swings
	protected float swingForceForwardMovement = 10.0f;
	protected float swingForceBackMovement = 5.0f;

	public override BodyPartDomain Domain{get{return BodyPartDomain.Leg;}}
	
	public override void ConnectedBody(Rigidbody body){
		legJoint.connectedBody = body;
	}

	
	void FixedUpdate(){
		PropelForward();
	}

	/// <summary>
	/// Adds forces so the leg can propel it's connected rigidbody forward.
	/// Call from withing FixedUpdate as long as it should move forward.
	/// adjust swingFrequenzy and swingForce for desired effect.
	/// </summary>
	public virtual void PropelForward(){
		if(Time.time%(swingFrequenzyForwardMovement+swingFrequenzyBackMovement)<swingFrequenzyForwardMovement)
		{
			legJoint.rigidbody.AddRelativeForce(Vector3.forward * swingForceForwardMovement);
//			Debug.Log("for");
	
		}else{
			legJoint.rigidbody.AddRelativeForce(Vector3.forward * -swingForceBackMovement);
//			Debug.Log("Back");
			
		}
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
