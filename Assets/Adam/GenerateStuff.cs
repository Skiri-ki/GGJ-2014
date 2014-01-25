using UnityEngine;
using System.Collections;

public class GenerateStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject hex1 = HexahedronFiller.FillHexahedron (6, 3, 9, 0.95f, 0.25f, 3);
		GameObject hex2 = HexahedronFiller.FillHexahedron (5, 5, 5, 1f, 0.2f, 3);
		BodyPart.ConnectBodyParts<HingeJoint> (hex1, hex2);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
