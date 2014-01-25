using UnityEngine;
using System.Collections.Generic;
using System;

public class Body : BodyPart {
	public AnchorPoint [] anchorPoints;
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
	void Start(){
		List<string> anchorTypes = new List<string>();
		List<BodyPart> anchorTypeBodyPart = new List<BodyPart>();

		foreach (AnchorPoint anchor in anchorPoints){

			bool notYetInList = true;
			for (int i = 0; i < anchorTypes.Count; i++) {
				if(anchor.AnchorType == anchorTypes[i]){
					notYetInList=false;
					break;
				}
			}
			if(notYetInList){
				anchorTypes.Add(anchor.AnchorType);

				List<BodyPart> possiblePartsForAnchor = new List<BodyPart>();
				foreach (Body.BodyPartDomain domain in anchor.acceptableParts) {
					
					BodyPart examplePartToImplicateProperType = BodyPartPrefabCollection.COLLECTION.GetExampleFor(domain);
					
					possiblePartsForAnchor.AddRange( BodyPartPrefabCollection.COLLECTION.GetPrefabs(examplePartToImplicateProperType));
				}
				int r = UnityEngine.Random.Range(0, possiblePartsForAnchor.Count);
				anchorTypeBodyPart.Add(possiblePartsForAnchor[r]);

			}
		}

		foreach (AnchorPoint anchor in anchorPoints) {
			for (int i = 0; i < anchorTypes.Count; i++) {
				if(anchor.AnchorType == anchorTypes[i]){
					BodyPart part = (GameObject.Instantiate(anchorTypeBodyPart[i].gameObject, anchor.transform.position,Quaternion.identity) as GameObject).GetComponent<BodyPart>();
					part.transform.parent = anchor.transform;
					part.ConnectedBody(rigidbody);
					break;
				}
			}
		}
	}
}
