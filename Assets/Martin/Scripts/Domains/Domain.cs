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

	public static Component AddComponentTypeFor(GameObject obj, DomainEnum domain){
		return AddComponentTypeFor(obj, domain,0);
	}
	public static Component AddComponentTypeFor(GameObject obj, DomainEnum domain, int probabilityMalus){
		switch (domain) {
		case DomainEnum.Visuals:
			int randomVisual = UnityEngine.Random.Range(0,4+probabilityMalus);
			switch (randomVisual) {
			case 0:
				obj.name += "Rainbow";
				return obj.AddComponentIfMissing<Rainbow>();
			case 1:
				obj.name += "PaintGun";
				return obj.AddComponentIfMissing<PaintGun>();
			default:
				return obj.AddComponentIfMissing<Transform>();
			}
		case DomainEnum.Audio:
			
			int randomAudio = UnityEngine.Random.Range(0,4+probabilityMalus);
			switch (randomAudio) {
			case 0:
				obj.name += "Musician";
				return obj.AddComponentIfMissing<Musician>();
			default:
				return obj.AddComponentIfMissing<Transform>();
			}

		case DomainEnum.BodyAttachment:
			int randomBodyAttachment = UnityEngine.Random.Range(0,4+probabilityMalus);
			switch (randomBodyAttachment) {
//			case 0:
//				return obj.AddComponentIfMissing<Musician>();
			default:
				return obj.AddComponentIfMissing<Transform>();
			}
			
		case DomainEnum.BodyAttachmentSpecialMovement:
			
			int randomBodyAttachmentSpecialMovement = UnityEngine.Random.Range(0,8+probabilityMalus);
			switch (randomBodyAttachmentSpecialMovement) {
			case 0:
				obj.name += "Hover";
				return obj.AddComponentIfMissing<Hover>();
			case 1:
				obj.name += "Jumper";
				return obj.AddComponentIfMissing<Jumper>();
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
