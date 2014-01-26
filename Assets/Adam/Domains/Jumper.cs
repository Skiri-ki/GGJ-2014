using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {
	float delay;
	float MIN_DELAY = 1f, MAX_DELAY = 5f;
	float timer;

	float MIN_FORCE = 1f, MAX_FORCE = 10f;

	// Use this for initialization
	void Start () {
		delay = Random.Range (MIN_DELAY, MAX_DELAY);
		timer = delay;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0f) {
			System.Func<float> rand_for = () => Random.Range(MIN_FORCE,MAX_FORCE) * 
				transform.localScale.x * transform.localScale.y * transform.localScale.z;
			rigidbody.AddForce(new Vector3 (rand_for(), Mathf.Abs(rand_for()) * 3f, rand_for()));

			timer = delay;
		}
	}
}
