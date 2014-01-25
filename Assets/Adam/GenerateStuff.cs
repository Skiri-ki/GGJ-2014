using UnityEngine;
using System.Collections;

public class GenerateStuff : MonoBehaviour {

	void generateWalker() {
		Color color = DomainEditors.getRandColor ();
		GameObject hex1 = HexahedronFiller.FillHexahedron (6, 3, 3, 0.95f, 0.25f, 2);
		DomainEditors.ChangeColor (color, hex1);
		GameObject hex2 = HexahedronFiller.FillHexahedron (5, 4, 5, 1f, 0.8f, 5);
		DomainEditors.ChangeColor (color, hex2);
		GameObject hex3 = HexahedronFiller.FillHexahedron (5, 4, 5, 1f, 0.8f, 5);
		DomainEditors.ChangeColor (color, hex3);
		hex2.transform.Translate(Vector3.left * 20f);
		hex2.AddComponent<Rotator> ();
		hex3.transform.Translate(Vector3.right * 20f);
		hex3.AddComponent<Rotator> ();
		
		BodyPart.ConnectBodyParts<HingeJoint> (hex1, hex2);
		BodyPart.ConnectBodyParts<HingeJoint> (hex1, hex3);
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

	void generateFlyer() {
		Color color = DomainEditors.getRandColor ();
		GameObject hex1 = HexahedronFiller.FillHexahedron (2, 5, 3, 0.95f, 0.25f, 0);
		DomainEditors.ChangeColor (color, hex1);
		GameObject hex2 = HexahedronFiller.FillHexahedron (2, 4, 5, 0.7f, 0.4f, 0);
		DomainEditors.ChangeColor (color, hex2);
		hex2.AddComponent<Flyer> ();
		
		BodyPart.ConnectBodyParts<SpringJoint> (hex1, hex2).transform.Translate(Vector3.up * 10f);
	}

	void generateJuggler() {
		Color color = DomainEditors.getRandColor ();
		GameObject hex1 = HexahedronFiller.FillHexahedron (8, 5, 5, 0.99f, 0.25f, 1);
		DomainEditors.ChangeColor (color, hex1);
		hex1.AddComponent<Juggler> ().transform.Translate(Vector3.right * 15f);
	}

	void generateTwitcher() {
		Color color = DomainEditors.getRandColor ();
		GameObject hex1 = HexahedronFiller.FillHexahedron (6, 3, 3, 0.95f, 0.25f, 2);
		DomainEditors.ChangeColor (color, hex1);
		GameObject hex2 = HexahedronFiller.FillHexahedron (5, 4, 5, 1f, 0.8f, 5);
		DomainEditors.ChangeColor (color, hex2);
		hex2.AddComponent<Twitcher> ();
		
		BodyPart.ConnectBodyParts<HingeJoint> (hex1, hex2).transform.Translate(Vector3.right * 10f);
	}

	void generateRainbow() {
		Color color = DomainEditors.getRandColor ();
		GameObject hex1 = HexahedronFiller.FillHexahedron (6, 3, 3, 0.95f, 0.25f, 2);
		DomainEditors.ChangeColor (color, hex1);
		GameObject hex2 = HexahedronFiller.FillHexahedron (5, 4, 5, 1f, 0.8f, 5);
		DomainEditors.ChangeColor (color, hex2);
		hex2.AddComponent<Rainbow> ();
		
		BodyPart.ConnectBodyParts<HingeJoint> (hex1, hex2).transform.Translate(Vector3.left * 15f);
	}

	// Use this for initialization
	void Start () {
	    generateWalker ();
		generateJumper ();
		generateFlyer ();
		generateJuggler ();
		generateTwitcher ();
		generateRainbow ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
