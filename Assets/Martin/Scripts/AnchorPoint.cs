using UnityEngine;
using System.Collections;
//using System;

//[Serializable]
public class AnchorPoint : MonoBehaviour  {
//	public Transform anchorPoint;
	public BodyPart.DomainEnum [] acceptableParts;
	public int countofBoxes;
	public int seed;
	public string AnchorType{
		get{
			string type = "";
			foreach (BodyPart.DomainEnum domain in acceptableParts) {
				type += domain.ToString();
			}
			return type;
		}
	}
}
