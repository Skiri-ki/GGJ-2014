using UnityEngine;
using System.Collections;

public class Tail : BodyPart {
	
	public override BodyPartDomain Domain{get{return BodyPartDomain.Tail;}}

//	Transform anker;
	public TailPart basePart;
	public TailPart tip;
	public float swingFrequenzy = 2.0f ;//in seconds between swings
	public float swingForce = 1.0f;

	void FixedUpdate(){
		basePart.rigidbody.AddRelativeForce(Vector3.right * swingForce * (Time.time%(swingFrequenzy*2)>swingFrequenzy?1:-1));
	}

	public override void ConnectedBody(Rigidbody body){
		basePart.GetComponent<Joint>().connectedBody = body;
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
