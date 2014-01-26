using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameObjectExtension;

public class GenerateCreature : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static Body GenerateFromParts(List<GameObject> parts){
		int bodyIndex = Random.Range(0,parts.Count);
		GameObject bodyObj = parts[bodyIndex];
		parts.RemoveAt(bodyIndex);

		List<BodyHinge> bodyParts = new List<BodyHinge>();

		while(parts.Count>0){
			int partIndex = Random.Range(0,parts.Count);
			GameObject partObj = parts[partIndex];
			parts.RemoveAt(partIndex);
			BodyPart.ConnectBodyParts<HingeJoint>(bodyObj,partObj);
			
			bodyParts.Add(partObj.GetComponent<BodyHinge>());

		}

		foreach (BodyHinge part in bodyParts) {
			
		}

		return bodyObj.AddComponentIfMissing<Body>();
	}
}
