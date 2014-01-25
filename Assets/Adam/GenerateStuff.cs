using UnityEngine;
using System.Collections;

public class GenerateStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Color color = DomainEditors.getRandColor ();
		GameObject hex1 = HexahedronFiller.FillHexahedron (6, 3, 6, 0.95f, 0.25f, 2);
		DomainEditors.ChangeColor (color, hex1);
		GameObject hex2 = HexahedronFiller.FillHexahedron (5, 5, 5, 1f, 0.6f, 3);
		DomainEditors.ChangeColor (color, hex2);
		hex2.AddComponent<Rotator> ();

		BodyPart.ConnectBodyParts<HingeJoint> (hex1, hex2);

	}
	
	// Update is called once per frame
	void Update () {

	}
}
