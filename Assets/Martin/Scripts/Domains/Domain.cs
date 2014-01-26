using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GameObjectExtension;

public abstract  class Domain : MonoBehaviour {
	public enum DomainEnum{
		Body, //special case
		Leg, //special case
		BodyJoint, //special case
		BodyAttachment,
		BodyAttachmentSpecialMovement, 
		Visuals,
		Audio,
		//...
	}

	public static Component  AddComponentTypeFor(GameObject obj, DomainEnum domain){
		switch (domain) {
		case DomainEnum.Visuals:
			int randomVisual = UnityEngine.Random.Range(0,4);
			switch (randomVisual) {
			case 0:
				return obj.AddComponentIfMissing<Rainbow>();
			case 1:
				return obj.AddComponentIfMissing<PaintGun>();
			default:
				return obj.AddComponentIfMissing<Transform>();
			}
		case DomainEnum.Audio:
			
			int randomAudio = UnityEngine.Random.Range(0,4);
			switch (randomAudio) {
			case 0:
				return obj.AddComponentIfMissing<Musician>();
			default:
				return obj.AddComponentIfMissing<Transform>();
			}

		case DomainEnum.BodyAttachment:
			int randomBodyAttachment = UnityEngine.Random.Range(0,4);
			switch (randomBodyAttachment) {
//			case 0:
//				return obj.AddComponentIfMissing<Musician>();
			default:
				return obj.AddComponentIfMissing<Transform>();
			}
			
		case DomainEnum.BodyAttachmentSpecialMovement:
			
			int randomBodyAttachmentSpecialMovement = UnityEngine.Random.Range(0,8);
			switch (randomBodyAttachmentSpecialMovement) {
			case 0:
				return obj.AddComponentIfMissing<Hover>();
			default:
				return obj.AddComponentIfMissing<Transform>();
			}

		default:
			return obj.AddComponentIfMissing<Transform>();
		}
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
