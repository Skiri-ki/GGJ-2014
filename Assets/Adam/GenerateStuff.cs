using UnityEngine;
using System.Collections;

public class GenerateStuff : MonoBehaviour {

	void generateWalker() {
		Color color = DomainEditors.getRandColor ();
		GameObject hex1 = HexahedronFiller.FillHexahedron (6, 3, 3, 0.95f, 0.25f, 2);
		DomainEditors.ChangeColor (color, hex1);
		GameObject hex2 = HexahedronFiller.FillHexahedron (5, 4, 5, 1f, 0.8f, 5);
		DomainEditors.ChangeColor (color, hex2);
		hex2.AddComponent<Rotator> ();
		
		BodyPart.ConnectBodyParts<HingeJoint> (hex1, hex2).transform.Translate(Vector3.right * 10f);
	}

	void generateJumper() {
		Color color = DomainEditors.getRandColor ();
		GameObject hex1 = HexahedronFiller.FillHexahedron (2, 5, 3, 0.95f, 0.25f, 2);
		DomainEditors.ChangeColor (color, hex1);
		GameObject hex2 = HexahedronFiller.FillHexahedron (2, 4, 5, 0.7f, 0.4f, 3);
		DomainEditors.ChangeColor (color, hex2);
		hex2.AddComponent<Jumper> ();
		
		BodyPart.ConnectBodyParts<SpringJoint> (hex1, hex2).transform.Translate(Vector3.left * 10f);
	}

	// Use this for initialization
	void Start () {
		generateWalker ();
		generateJumper ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
