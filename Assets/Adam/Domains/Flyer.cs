using UnityEngine;
using System.Collections;

public class Flyer : MonoBehaviour {
	float HEIGHT = 30f;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float req_height = transform.localScale.y * HEIGHT;
		if (transform.position.y < req_height) 
			rigidbody.AddForce(new Vector3 (0f, (req_height - transform.position.y) * transform.localScale.y, 0f));
	}
}
