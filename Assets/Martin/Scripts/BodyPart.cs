using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameObjectExtension;

//[RequireComponent(typeof(Rigidbody))]
public abstract class BodyPart : Domain {
//	public virtual AnchorPoint [] anchorPoints;

	public enum BodyPartDomain{
		Body,
		LocomotiveExtremity,
		Arm,
		Leg,
		Tail,
		Head,
		BodyJoint
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
		Body body = objA.AddComponentIfMissing<Body>();
		BodyJoint jointPart;
		if(typeof(T) == typeof(HingeJoint)) 
			jointPart = objB.AddComponentIfMissing<BodyHinge>();
		else
			jointPart = objB.AddComponentIfMissing<BodyJoint>();

		Debug.Log(jointPart);

		//first calculate the Distance
		Vector3 posA = GetPosition(objA);
		Vector3 posB = GetPosition(objB);

		Vector3 distBA = posA - posB;

		//next find out which side 

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

		//now lets add some rigidbodys in case we need them for ClosestPointOnBounds and because we need them later anyway

		Rigidbody rigidA = objA.AddComponentIfMissing<Rigidbody>();
		
		Rigidbody rigidB = objB.AddComponentIfMissing<Rigidbody>();

		//find out where their surface points are

		Vector3 offsetToA = FindOffset(objA, side, rigidA, rigidB); 
		Vector3 offsetToB= FindOffset(objB, side, rigidB, rigidA);  

		//calculate and set necessary position, this needs to hapeen before adding a joint
		if(objA.collider != null && objB.collider != null){
			bool previousCollStateA = objA.collider.enabled;
			bool previousCollStateB = objB.collider.enabled;
			objA.collider.enabled=true;
			objB.collider.enabled=true;
			{
				objB.transform.position = 
					objA.collider.bounds.center - new Vector3(objA.collider.bounds.extents.x * side.x,
					                                          objA.collider.bounds.extents.y * side.y,
					                                          objA.collider.bounds.extents.z * side.z)
						- new Vector3(objB.collider.bounds.extents.x * Mathf.Sign(side.x)*(1 - Mathf.Abs(side.x)),
						              objB.collider.bounds.extents.y * Mathf.Sign(side.y)*(1 - Mathf.Abs(side.y)),
						              objB.collider.bounds.extents.z * Mathf.Sign(side.z)*(1 - Mathf.Abs(side.z)));
			}
			objA.collider.enabled=previousCollStateA;
			objB.collider.enabled=previousCollStateB;
		}else
			objB.transform.position = objA.transform.position + offsetToA + offsetToB;

		//add the joint
		jointPart.Init(); //init the Joint, adds a joint if not allready present
		Joint joint = jointPart.BJoint;

		if(objB.collider){
			bool previousCollState = objB.collider.enabled;
			objB.collider.enabled=true;
			{
				GameObject temp = new GameObject("PositionHelper");
				temp.transform.position = 
					objB.collider.bounds.center + new Vector3(objB.collider.bounds.extents.x * side.x,
					                                          objB.collider.bounds.extents.y * side.y,
					                                          objB.collider.bounds.extents.z * side.z);
				temp.transform.parent = objB.transform;
				
				joint.anchor = temp.transform.localPosition;
				GameObject.Destroy(temp);
			}
			objB.collider.enabled=previousCollState;
		}else
			joint.anchor = side * 0.5f;
		joint.axis = -side;
		//connect the joint with it's parent and parent it

		joint.connectedBody = rigidA;
		objB.transform.parent = objA.transform;


		return objA;
	}

	private static Vector3 GetPosition(GameObject obj){
		Vector3 pos = obj.transform.position;
		if(obj.collider != null){
			bool previousCollState = obj.collider.enabled;
			obj.collider.enabled=true;

			pos = obj.collider.bounds.center;

			obj.collider.enabled=previousCollState;
		}
		else if(obj.renderer != null){
			pos = obj.renderer.bounds.center;
		}
		return pos;
	}

	private static Vector3 FindOffset(GameObject obj, Vector3 side, Rigidbody rigidTo, Rigidbody rigidFrom){
		Vector3 offset; 
		if(obj.collider != null){
			bool previousCollState = obj.collider.enabled;
			obj.collider.enabled=true;

			offset = new Vector3(obj.collider.bounds.extents.x * -side.x, 
			                     obj.collider.bounds.extents.y * -side.y,
			                     obj.collider.bounds.extents.z * -side.z);

			obj.collider.enabled=previousCollState;
			
		}
		else if(obj.renderer != null){
			offset = new Vector3(obj.renderer.bounds.extents.x * -side.x,
			                     obj.renderer.bounds.extents.y * -side.y,
			                     obj.renderer.bounds.extents.z * -side.z);
		}
		else{
			offset = rigidTo.ClosestPointOnBounds(rigidFrom.transform.position) - rigidTo.transform.position;
			offset = new Vector3(offset.x * Mathf.Abs( side.x),
			                        offset.y * Mathf.Abs( side.y),
			                        offset.z * Mathf.Abs( side.z));
			Debug.LogError(obj.name + " has neither renderer nor collider attached");
		}
		return offset;
	}


//
//	public static T AddComponentIfMissing<T>(this GameObject obj)where T : Component{
//		T comp= obj.GetComponent<T>();
//		if(comp == null)
//			comp = obj.AddComponent<T>();
//		return comp;
//	}

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
