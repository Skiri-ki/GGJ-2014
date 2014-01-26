using UnityEngine;
using System.Collections;

public class ConnectorTest : MonoBehaviour {

	public GameObject objA;
	public GameObject objB;

	void Start () {
		BodyPart.ConnectBodyParts(objA,objB);
	}
}
