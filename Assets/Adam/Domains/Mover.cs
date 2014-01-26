using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	float MIN_FORCE = 1f, MAX_FORCE = 10f;
	Vector3 direction;
	
	// Use this for initialization
	void Start () {
		System.Func<float> rand_for = () => Random.Range(MIN_FORCE,MAX_FORCE) * 
			transform.localScale.x * transform.localScale.y * transform.localScale.z;
		direction = new Vector3 (rand_for(), rand_for(), rand_for());
	}
	
	// Update is called once per frame
	void Update () {
		this.rigidbody.AddForce (direction * Time.deltaTime);
	}
}