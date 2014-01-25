using UnityEngine;
using System.Collections;

public class Flyer : MonoBehaviour {
	float HEIGHT = 1f;
	float factor = 1f;

	// Use this for initialization
	void Start () {
		HEIGHT = Random.Range (5f, 30f);
		factor = Random.Range (1f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		float req_height = transform.localScale.y * HEIGHT;
		if (transform.position.y < req_height) 
			rigidbody.AddForce(new Vector3 (0f, (req_height - transform.position.y) * transform.localScale.y * factor, 0f));
	}
}
