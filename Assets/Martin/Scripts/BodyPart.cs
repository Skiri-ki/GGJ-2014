using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

//[RequireComponent(typeof(Rigidbody))]
public abstract class BodyPart : Function {
	public enum BodyPartDomain{
		Body,
		LocomotiveExtremity,
		Arm,
		Leg,
		Tail,
		Head
		//...
	}
	public abstract BodyPartDomain Domain{get;}
//	public BodyPartDomain domain = BodyPartDomain.BodyPart;

//	public abstract void ConnectedBody(BodyPart body);

	public virtual void ConnectedBody(Rigidbody body){
		GetComponent<Joint>().connectedBody = body.rigidbody;
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
