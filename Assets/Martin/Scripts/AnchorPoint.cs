using UnityEngine;
using System.Collections;
//using System;

//[Serializable]
public class AnchorPoint : MonoBehaviour  {
//	public Transform anchorPoint;
	public BodyPart.BodyPartDomain [] acceptableParts;
	public string AnchorType{
		get{
			string type = "";
			foreach (BodyPart.BodyPartDomain domain in acceptableParts) {
				type += domain.ToString();
			}
			return type;
		}
	}
}
