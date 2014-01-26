using UnityEngine;
using System.Collections;
using GameObjectExtension;

//[RequireComponent(typeof(CharacterhJoint))]
public class Leg : LocomotiveExtremity {
//	public HingehJoint hJoint;
	public bool autoPropel = true;
	protected float swingFrequenzyForwardMovement = 1.0f;//in seconds between swings
	protected float swingFrequenzyBackMovement = 1.0f;//in seconds between swings
	protected float swingForceForwardMovement = 100.0f;
	protected float swingForceBackMovement = 50.0f;

	protected BodyHinge hinge;

	public override DomainEnum EDomain{get{return DomainEnum.Leg;}}

	void Awake(){
		hinge = GetComponent<BodyHinge>();
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
			//			leghJoint.rigidbody.AddRelativeForce(Vector3.forward * swingForceForwardMovement);
			hinge.assignInEditorHinge.useMotor= true;
			hinge.assignInEditorHinge.useLimits = true;
			JointMotor motor = new JointMotor();
			motor.targetVelocity = swingForceForwardMovement;
			motor.force = swingForceForwardMovement;
			hinge.assignInEditorHinge.motor = motor;
//			Debug.Log("for");
	
		}else{
			hinge.assignInEditorHinge.useMotor= true;
			hinge.assignInEditorHinge.useLimits = true;
			JointMotor motor = new JointMotor();
			motor.targetVelocity = -swingForceBackMovement;
			motor.force = swingForceBackMovement;
			hinge.assignInEditorHinge.motor = motor;
//			leghJoint.rigidbody.AddRelativeForce(Vector3.forward * -swingForceBackMovement);
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
