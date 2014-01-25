using UnityEngine;
using System.Collections;
using GameObjectExtension;

public class BodyJoint : BodyPart {
	protected Joint joint;
	public virtual Joint BJoint{
		get{
//			if(!(joint is Joint))
//				Debug.LogError("value is not a Joint");
			return joint as Joint;
		}
		set{
//			if(!(value is Joint))
//				Debug.LogError("value is not a Joint");
			joint = value as Joint;
		}
	}

	public override void ConnectedBody(Rigidbody body){
		Init(); //safety measure
		joint.connectedBody = body;
	}
	
	public override BodyPartDomain BodyDomain{get{return BodyPartDomain.BodyJoint;}}

	// Use this for initialization
	public void Init () {
		Debug.Log("JInit 1: "+ ((joint!=null)?joint.GetType().ToString():"null"));
		if(joint == null)
			BJoint = transform.GetComponent<Joint>();
		Debug.Log("JInit 2: "+ ((joint!=null)?joint.GetType().ToString():"null"));

		if(joint == null)
			BJoint = transform.GetComponentInChildren<Joint>();
		Debug.Log("JInit 3: "+ ((joint!=null)?joint.GetType().ToString():"null"));
		
		if(joint == null){
			BJoint = gameObject.AddComponentIfMissing<HingeJoint>() as Joint;
		}
		Debug.Log("JInit 4: "+ ((joint!=null)?joint.GetType().ToString():"null"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
