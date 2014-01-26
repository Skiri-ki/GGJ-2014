using UnityEngine;
using System.Collections;

public abstract  class Domain : MonoBehaviour {
	public enum DomainEnum{
		Body,
		LocomotiveExtremity,
		Arm,
		Leg,
		Tail,
		Head,
		BodyJoint,
		Visuals,
		Audio
		//...
	}
	public abstract DomainEnum EDomain{get;}

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
