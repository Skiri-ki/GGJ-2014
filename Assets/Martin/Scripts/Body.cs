using UnityEngine;
using System.Collections.Generic;
using System;

public class Body : BodyPart {
	public AnchorPoint [] anchorPoints;
	public bool	generateAll;
	public bool	generatePartsFromAnchors;
	public override BodyPartDomain BodyDomain{get{return BodyPartDomain.Body;}}

	void Start(){
		if(generateAll){
			GenerateAnchorPoints();
			generatePartsFromAnchors = true;
		}
		if(generatePartsFromAnchors){
			GenerateBodyParts();
		}
	}

	public void GenerateAnchorPoints(){
		List<AnchorPoint> anchors = new List<AnchorPoint>();
		if(anchorPoints != null && anchorPoints.Length>0 && anchorPoints[0] != null)
			anchors.AddRange(anchorPoints);

		//Symetrical Anchors, Legs left & right
		int r = UnityEngine.Random.Range(0,5);
		if(r>0){
			BodyPartDomain[] domains = new BodyPartDomain[1];
			domains[0] = BodyPartDomain.Leg;

			anchors.AddRange(GenerateSymetricalAnchor(Vector3.right,domains));
			if(r>=4){
				anchors.AddRange(GenerateSymetricalAnchor(Vector3.right,domains));
			}
		}
		
		r = UnityEngine.Random.Range(0,5);

		if(r>3){ //asymetrical, singular leg on front
			BodyPartDomain[] domains = new BodyPartDomain[1];
			domains[0] = BodyPartDomain.Leg;
			
			anchors.Add(GenerateAnchor(Vector3.forward,new Vector3(-0.0f,-0.5f,-0.0f), new Vector3(0.0f,0.5f,0.0f),domains));
		}
		
//		r = UnityEngine.Random.Range(0,5);
//		
//		if(r>2){ //asymetrical, head on front
//			BodyPartDomain[] domains = new BodyPartDomain[1];
//			domains[0] = BodyPartDomain.Head;
//			
//			anchors.Add(GenerateAnchor(Vector3.forward,domains));
//		}

		anchorPoints = anchors.ToArray();
	}

	private List<AnchorPoint> GenerateSymetricalAnchor(Vector3 mirrorAlongAxis, BodyPart.BodyPartDomain [] domains){
//		GameObject obj = new GameObject("Sym Anchor for " + domains.ToString());
//		AnchorPoint anchor = obj.AddComponent<AnchorPoint>() as AnchorPoint;
//		obj.transform.parent = transform;
//		obj.transform.localPosition = mirrorAlongAxis * 0.5f;
//		anchor.acceptableParts = domains;
		List <AnchorPoint> anchors = new List<AnchorPoint>();
		int seed = UnityEngine.Random.Range(0,2000);
		anchors.Add(GenerateAnchor(mirrorAlongAxis, domains, seed));
		anchors.Add(GenerateAnchor(-mirrorAlongAxis, domains, seed));
		return anchors;
	}

	private AnchorPoint GenerateAnchor(Vector3 side, BodyPart.BodyPartDomain [] domains){
		return GenerateAnchor(side,domains,UnityEngine.Random.seed);
	}
	private AnchorPoint GenerateAnchor(Vector3 side, Vector3 minFaceAreaExtension, Vector3 maxFaceAreaExtension, BodyPart.BodyPartDomain [] domains){
		return GenerateAnchor(side, minFaceAreaExtension, maxFaceAreaExtension, domains,UnityEngine.Random.seed);
		
	}

	private AnchorPoint GenerateAnchor(Vector3 side, BodyPart.BodyPartDomain [] domains, int seed){
		return GenerateAnchor(side,new Vector3(-0.5f,-0.5f,-0.5f),new Vector3(0.5f,0.5f,0.5f), domains,seed);

	}
	private AnchorPoint GenerateAnchor(Vector3 side, Vector3 minFaceAreaExtension, Vector3 maxFaceAreaExtension, BodyPart.BodyPartDomain [] domains, int seed){
		UnityEngine.Random.seed = seed;

		GameObject obj = new GameObject("Anchor for " + domains[0]);
		AnchorPoint anchor = obj.AddComponent<AnchorPoint>() as AnchorPoint;
		obj.transform.parent = transform;
		obj.transform.localPosition = side * 0.5f;
		Vector3 abs = new Vector3(Mathf.Abs(side.x),Mathf.Abs(side.y),Mathf.Abs(side.z));
		Vector3 face = Vector3.one - abs;
		face = new Vector3(
			face.x * UnityEngine.Random.Range(minFaceAreaExtension.x,maxFaceAreaExtension.x),
			face.y * UnityEngine.Random.Range(minFaceAreaExtension.y,maxFaceAreaExtension.y),
			face.z * UnityEngine.Random.Range(minFaceAreaExtension.z,maxFaceAreaExtension.z));
		obj.transform.localPosition += face;
		anchor.acceptableParts = domains;
		return anchor;
	}

	public void GenerateBodyParts(){
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
					
					possiblePartsForAnchor.AddRange( BodyPartPrefabCollection.COLLECTION.GetPrefabs(domain));
				}
				if(possiblePartsForAnchor.Count>0){
					int r = UnityEngine.Random.Range(0, possiblePartsForAnchor.Count);
					anchorTypeBodyPart.Add(possiblePartsForAnchor[r]);
				}else{
					Debug.LogError("No Prefabs found for AnchorType: " + anchorTypes[anchorTypes.Count-1]);
					anchorTypes.RemoveAt(anchorTypes.Count-1); //there are no prefabs for this type
				}

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
