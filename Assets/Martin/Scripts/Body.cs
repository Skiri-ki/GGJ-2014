using UnityEngine;
using System.Collections.Generic;
using System;
using GameObjectExtension;

[RequireComponent(typeof(Rigidbody))]
public class Body : BodyPart {
	public AnchorPoint [] anchorPoints;
	public bool	generateAll;
	public bool	generatePartsFromAnchors;
	public override DomainEnum EDomain{get{return DomainEnum.Body;}}

	void Start(){
		if(generateAll){
			GenerateAnchorPoints();
			generatePartsFromAnchors = true;
		}
		if(generatePartsFromAnchors){
			GenerateBodyParts();
		}
	}

	public void GenerateCreature(int parts, int countofBoxes){
		name = "Body_with_"+parts+"_parts";
		switch (parts) {
		case 1:
			DomainEnum [] domains= new DomainEnum[1];
			domains[0] = DomainEnum.Leg;
			GenerateAnchor (RandomSide(),domains);
			anchorPoints[anchorPoints.Length-1].countofBoxes = countofBoxes;
			break;
			
		case 2:
			DomainEnum [] domains2= new DomainEnum[1];
			domains2[0] = DomainEnum.Leg;

			if(UnityEngine.Random.Range(0,2)==1){
				GenerateSymetricalAnchor(RandomAxis(),domains2);
				anchorPoints[anchorPoints.Length-2].countofBoxes = countofBoxes/2;
				anchorPoints[anchorPoints.Length-1].countofBoxes = countofBoxes/2;
			}else{
				int randomCutOf = UnityEngine.Random.Range(1,countofBoxes);
				GenerateAnchor (RandomSide(),domains2);
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf;
				GenerateAnchor (RandomSide(),domains2);
				anchorPoints[anchorPoints.Length-1].countofBoxes = countofBoxes-randomCutOf;
			}

			break;
			
		case 3:
			DomainEnum [] domains3= new DomainEnum[1];
			domains3[0] = DomainEnum.Leg;
			
			int randomCutOf = UnityEngine.Random.Range(1,countofBoxes);

			if(UnityEngine.Random.Range(0,2)==1){
				GenerateSymetricalAnchor(RandomAxis(),domains3);
				anchorPoints[anchorPoints.Length-2].countofBoxes = randomCutOf/2;
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf/2;
			}else{
				int randomCutOf2 = UnityEngine.Random.Range(1,randomCutOf);

				GenerateAnchor (RandomSide(),domains3);
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf2;
				GenerateAnchor (RandomSide(),domains3);
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf-randomCutOf2;
			}
			GenerateAnchor (RandomSide(),domains3);
			anchorPoints[anchorPoints.Length-1].countofBoxes = countofBoxes-randomCutOf;

			break;
			
		case 4:
			DomainEnum [] domains4= new DomainEnum[1];
			domains4[0] = DomainEnum.Leg;
			
			int randomCutOf2 = UnityEngine.Random.Range(1,countofBoxes);

			if(UnityEngine.Random.Range(0,2)==1){
				GenerateSymetricalAnchor(RandomAxis(),domains4);
				anchorPoints[anchorPoints.Length-2].countofBoxes = randomCutOf2/2;
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf2/2;
			}else{
				int randomCutOf3 = UnityEngine.Random.Range(1,randomCutOf2);
				GenerateAnchor (RandomSide(),domains4);
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf3;
				GenerateAnchor (RandomSide(),domains4);
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf2-randomCutOf3;
			}
			if(UnityEngine.Random.Range(0,2)==1){
				GenerateSymetricalAnchor(RandomAxis(),domains4);
				anchorPoints[anchorPoints.Length-2].countofBoxes = (countofBoxes-randomCutOf2)/2;
				anchorPoints[anchorPoints.Length-1].countofBoxes = (countofBoxes-randomCutOf2)/2;
			}else{
				int randomCutOf3 = UnityEngine.Random.Range(1,countofBoxes-randomCutOf2);
				GenerateAnchor (RandomSide(),domains4);
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf3;
				GenerateAnchor (RandomSide(),domains4);
				anchorPoints[anchorPoints.Length-1].countofBoxes = (countofBoxes-randomCutOf2) - randomCutOf3;
			}

			break;
			
		case 5:
			
			DomainEnum [] domains5= new DomainEnum[1];
			domains5[0] = DomainEnum.Leg;
			
			int randomCutOf3 = UnityEngine.Random.Range(1,countofBoxes);
			
			if(UnityEngine.Random.Range(0,2)==1){
				GenerateSymetricalAnchor(RandomAxis(),domains5);
				anchorPoints[anchorPoints.Length-2].countofBoxes = randomCutOf3/2;
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf3/2;
			}else{
				int randomCutOf4 = UnityEngine.Random.Range(1,countofBoxes-randomCutOf3);
				GenerateAnchor (RandomSide(),domains5);
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf4;
				GenerateAnchor (RandomSide(),domains5);
				anchorPoints[anchorPoints.Length-1].countofBoxes = (countofBoxes-randomCutOf3)-randomCutOf4;
			}
			int randomCutOf5 = UnityEngine.Random.Range(1,countofBoxes-randomCutOf3);
			if(UnityEngine.Random.Range(0,2)==1){
				GenerateSymetricalAnchor(RandomAxis(),domains5);
				anchorPoints[anchorPoints.Length-2].countofBoxes = ((countofBoxes-randomCutOf3)-randomCutOf5)/2;
				anchorPoints[anchorPoints.Length-1].countofBoxes = ((countofBoxes-randomCutOf3)-randomCutOf5)/2;
			}else{
				int randomCutOf6 = UnityEngine.Random.Range(1,((countofBoxes-randomCutOf3)-randomCutOf5));
				GenerateAnchor (RandomSide(),domains5);
				anchorPoints[anchorPoints.Length-1].countofBoxes = randomCutOf6;
				GenerateAnchor (RandomSide(),domains5);
				anchorPoints[anchorPoints.Length-1].countofBoxes = ((countofBoxes-randomCutOf3)-randomCutOf5) - randomCutOf6;
			}

			GenerateAnchor (RandomSide(),domains5);
			anchorPoints[anchorPoints.Length-1].countofBoxes = (countofBoxes-randomCutOf5);
			break;

		default:
			break;
		}

		GenerateBodyPartsForCreature();
	}

	private Vector3 RandomSide(){
		return RandomAxis() * ((UnityEngine.Random.Range(0,2)==1)?1:-1) ;
	}

	private Vector3 RandomAxis(){
		Vector3 probability = new Vector3(50,20,30);
		int rand = UnityEngine.Random.Range(0,101);
		if(rand <= probability.x){
			return Vector3.right;
		}else if(rand > probability.x && rand <= probability.y){
			return Vector3.up;
		}else{
			return Vector3.forward;
		}
	}

	public void GenerateAnchorPoints(){
		List<AnchorPoint> anchors = new List<AnchorPoint>();
		if(anchorPoints != null && anchorPoints.Length>0 && anchorPoints[0] != null)
			anchors.AddRange(anchorPoints);

		//Symetrical Anchors, Legs left & right
		int r = UnityEngine.Random.Range(0,5);
		if(r>0){
			DomainEnum[] domains = new DomainEnum[1];
			domains[0] = DomainEnum.Leg;

			GenerateSymetricalAnchor(Vector3.right,domains);
			if(r>=4){
				GenerateSymetricalAnchor(Vector3.right,domains);
			}
		}
		
		r = UnityEngine.Random.Range(0,5);

		if(r>3){ //asymetrical, singular leg on front
			DomainEnum[] domains = new DomainEnum[1];
			domains[0] = DomainEnum.Leg;
			
			GenerateAnchor(Vector3.forward,new Vector3(-0.0f,-0.5f,-0.0f), new Vector3(0.0f,0.5f,0.0f),domains);
		}
		
//		r = UnityEngine.Random.Range(0,5);
//		
//		if(r>2){ //asymetrical, head on front
//			BodyPartDomain[] domains = new BodyPartDomain[1];
//			domains[0] = BodyPartDomain.Head;
//			
//			anchors.Add(GenerateAnchor(Vector3.forward,domains));
//		}

//		anchorPoints = anchors.ToArray();
	}

	private void GenerateSymetricalAnchor(Vector3 mirrorAlongAxis, BodyPart.DomainEnum [] domains){
//		GameObject obj = new GameObject("Sym Anchor for " + domains.ToString());
//		AnchorPoint anchor = obj.AddComponent<AnchorPoint>() as AnchorPoint;
//		obj.transform.parent = transform;
//		obj.transform.localPosition = mirrorAlongAxis * 0.5f;
//		anchor.acceptableParts = domains;
//		List <AnchorPoint> anchors = new List<AnchorPoint>();
//		if(anchorPoints != null && anchorPoints.Length>0 && anchorPoints[0] != null)
//			anchors.AddRange(anchorPoints);

		int seed = UnityEngine.Random.Range(0,2000);
		GenerateAnchor(mirrorAlongAxis, domains, seed);
		GenerateAnchor(-mirrorAlongAxis, domains, seed);
		
		anchorPoints[anchorPoints.Length-2].seed = UnityEngine.Random.Range(1,10000);
		anchorPoints[anchorPoints.Length-1].countofBoxes = anchorPoints[anchorPoints.Length-2].seed;

//		anchorPoints = anchors.ToArray();

//		return anchors;
	}

	private void GenerateAnchor(Vector3 side, BodyPart.DomainEnum [] domains){
		GenerateAnchor(side,domains,UnityEngine.Random.seed);
	}
	private void GenerateAnchor(Vector3 side, Vector3 minFaceAreaExtension, Vector3 maxFaceAreaExtension, BodyPart.DomainEnum [] domains){
		GenerateAnchor(side, minFaceAreaExtension, maxFaceAreaExtension, domains,UnityEngine.Random.seed);
		
	}

	private void GenerateAnchor(Vector3 side, BodyPart.DomainEnum [] domains, int seed){
		GenerateAnchor(side,new Vector3(-0.5f,-0.5f,-0.5f),new Vector3(0.5f,0.5f,0.5f), domains,seed);

	}
	private void GenerateAnchor(Vector3 side, Vector3 minFaceAreaExtension, Vector3 maxFaceAreaExtension, BodyPart.DomainEnum [] domains, int seed){
		UnityEngine.Random.seed = seed;

		
		List <AnchorPoint> anchors = new List<AnchorPoint>();
		if(anchorPoints != null && anchorPoints.Length>0 && anchorPoints[0] != null)
			anchors.AddRange(anchorPoints);

		GameObject obj = new GameObject("Anchor for " + domains[0]);
		AnchorPoint anchor = obj.AddComponent<AnchorPoint>() as AnchorPoint;
		obj.transform.parent = transform;
		obj.transform.localPosition = BodyPart.FindOffset(gameObject, side,null, null) - side * 0.5f;
//		obj.transform.localPosition = side * 0.5f;
		Vector3 abs = new Vector3(Mathf.Abs(side.x),Mathf.Abs(side.y),Mathf.Abs(side.z));
		Vector3 face = Vector3.one - abs;
		face = new Vector3(
			face.x * UnityEngine.Random.Range(minFaceAreaExtension.x,maxFaceAreaExtension.x),
			face.y * UnityEngine.Random.Range(minFaceAreaExtension.y,maxFaceAreaExtension.y),
			face.z * UnityEngine.Random.Range(minFaceAreaExtension.z,maxFaceAreaExtension.z));
		obj.transform.localPosition += face;
		anchor.acceptableParts = domains;

		anchors.Add(anchor);
		anchorPoints = anchors.ToArray();

//		return anchor;
	}

	public void GenerateBodyPartsForCreature(){
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
				foreach (Body.DomainEnum domain in anchor.acceptableParts) {
					
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
					BodyHinge part = (GameObject.Instantiate(anchorTypeBodyPart[i].gameObject, anchor.transform.position,Quaternion.identity) as GameObject).GetComponent<BodyHinge>();
					FillPartWithGraphics(part, anchor);
					BodyPart.ConnectBodyParts(gameObject, part.gameObject);
//					part.transform.parent = anchor.transform;
//					part.ConnectedBody(gameObject.AddComponentIfMissing<Rigidbody>());
					break;
				}
			}
		}
	}

	public void FillPartWithGraphics(BodyHinge part, AnchorPoint anchor){
		GameObject graphics = null;
		if(anchor.seed>0){
			graphics = WorldBuilder.BuildDomain(anchor.countofBoxes,anchor.seed);
		}else
			graphics = WorldBuilder.BuildDomain(anchor.countofBoxes);

		part.GetComponent<BoxCollider>().size = graphics.GetComponent<BoxCollider>().size;
		graphics.transform.position = part.transform.position;
		foreach (Renderer rend in part.GetComponentsInChildren<Renderer>()) {
			rend.enabled =false;
		}
		graphics.transform.parent = part.assignInEditorHinge.transform;

		//add some funn stuff:
		int rand = UnityEngine.Random.Range(0,5);
		if(rand == 3 || rand ==4){
			graphics.AddComponentIfMissing<Rainbow>();
		}
		
		if(rand == 2){
			graphics.AddComponentIfMissing<Musician>();
		}
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
				foreach (Body.DomainEnum domain in anchor.acceptableParts) {
					
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
