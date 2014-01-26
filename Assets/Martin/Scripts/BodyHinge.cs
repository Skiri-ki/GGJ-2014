using UnityEngine;
using System.Collections;
using GameObjectExtension;
//
public class BodyHinge : BodyJoint {
//	protected Joint joint;
	protected HingeJoint hJoint;
	public override Joint BJoint{
		get{
			return hJoint as Joint;
		}
		set{
			joint = value as Joint;
			hJoint = value as HingeJoint;
		}
	}
	public virtual HingeJoint HJoint{
		get{
			return hJoint;
		}
		set{
			hJoint = value;
			joint = hJoint;
		}
	}
	
	public override void ConnectedBody(Rigidbody body){
		Init(); //safety measure
		joint.connectedBody = body;
	}
	
	public override BodyPartDomain BodyDomain{get{return BodyPartDomain.BodyJoint;}}
	
	// Use this for initialization
	public new void Init () {
		Debug.Log("Init 1: "+ joint);
		if(joint == null)
			joint = transform.GetComponent<HingeJoint>();
		Debug.Log("Init 2: "+ joint);

		if(joint == null)
			joint = transform.GetComponentInChildren<HingeJoint>();
		
		Debug.Log("Init 3: "+ joint);
		if(joint == null)
			joint = gameObject.AddComponentIfMissing<HingeJoint>();
		Debug.Log("Init 4: "+ joint);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
