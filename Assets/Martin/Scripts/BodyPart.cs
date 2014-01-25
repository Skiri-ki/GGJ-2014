﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

//[RequireComponent(typeof(Rigidbody))]
public abstract class BodyPart : Domain {
//	public virtual AnchorPoint [] anchorPoints;

	public enum BodyPartDomain{
		Body,
		LocomotiveExtremity,
		Arm,
		Leg,
		Tail,
		Head
		//...
	}
	public abstract BodyPartDomain BodyDomain{get;}

	/// <summary>
	/// Connects the body parts.
	/// </summary>
	/// <returns>The parent body part.</returns>
	/// <param name="objA">Object a.</param>
	/// <param name="objB">Object b.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static GameObject ConnectBodyParts<T>(GameObject objA, GameObject objB) where T : Joint{
		
//		BodyPart partA = objA.GetComponent<BodyPart>();
//		if(partA == null)
//			partA = objA.AddComponent<BodyPart>() as BodyPart;
//
//		BodyPart partB = objB.GetComponent<BodyPart>();
//		if(partB == null)
//			partB =  objB.AddComponent<BodyPart>() as BodyPart;

		//
		Vector3 distBA = objA.transform.position - objB.transform.position;

		Vector3 side = Vector3.right * Mathf.Sign(distBA.x);
		bool changedSideToY = false;
		if(Mathf.Abs(distBA.y) > Mathf.Abs(distBA.x)){
			side = Vector3.up * Mathf.Sign(distBA.y);
			changedSideToY = true;
		}
		if( (!changedSideToY && Mathf.Abs(distBA.z) > Mathf.Abs(distBA.x)) || 
		    (changedSideToY && Mathf.Abs(distBA.z) > Mathf.Abs(distBA.y))){
			side = Vector3.forward * Mathf.Sign(distBA.z);
		}

		Rigidbody rigidA = objA.rigidbody;
		if(rigidA == null){
			rigidA = objA.AddComponent<Rigidbody>();
		}
		
		Rigidbody rigidB = objB.rigidbody;
		if(rigidB == null){
			rigidB = objB.AddComponent<Rigidbody>();
		}

		Vector3 offsetToA; 
		if(objA.collider != null)
			offsetToA = new Vector3(objA.collider.bounds.extents.x * -side.x, 
			                        objA.collider.bounds.extents.y * -side.y,
			                        objA.collider.bounds.extents.z * -side.z);
		else if(objA.renderer != null)
			offsetToA = new Vector3(objA.renderer.bounds.extents.x * -side.x, 
			                        objA.renderer.bounds.extents.y * -side.y,
			                        objA.renderer.bounds.extents.z * -side.z);
		else{
			offsetToA = rigidA.ClosestPointOnBounds(rigidB.transform.position) - rigidA.transform.position;
			offsetToA = new Vector3(offsetToA.x * Mathf.Abs( side.x),
			                        offsetToA.y * Mathf.Abs( side.y),
			                        offsetToA.z * Mathf.Abs( side.z));
			Debug.LogError("objA " + objA.name + " has neither renderer nor collider attached");
		}

		Vector3 offsetToB; 
		
		if(objB.collider != null)
			offsetToB = new Vector3(objB.collider.bounds.extents.x * side.x, 
			                        objB.collider.bounds.extents.y * side.y,
			                        objB.collider.bounds.extents.z * side.z);
		else if(objA.renderer != null)
			offsetToB = new Vector3(objB.renderer.bounds.extents.x * side.x, 
			                        objB.renderer.bounds.extents.y * side.y,
			                        objB.renderer.bounds.extents.z * side.z);
		else{
			offsetToB = rigidB.ClosestPointOnBounds(rigidA.transform.position) - rigidB.transform.position ;
			offsetToB = new Vector3(offsetToB.x * Mathf.Abs( side.x),
			                        offsetToB.y * Mathf.Abs( side.y),
			                        offsetToB.z * Mathf.Abs( side.z));
			Debug.LogError("objA " + objA.name + " has neither renderer nor collider attached");
		}

		objB.transform.position = objA.transform.position + offsetToA - offsetToB;

		Joint joint = objB.AddComponent<T>() as Joint;
		joint.anchor = side *0.5f;
		joint.axis = side;

		
		joint.connectedBody = rigidA;


		objB.transform.parent = objA.transform;
		return objA;
	}

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
