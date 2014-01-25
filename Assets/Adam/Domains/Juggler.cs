using UnityEngine;
using System.Collections;

public class Juggler : MonoBehaviour {
	float timer;
	float CHANGE_TIME = 0.1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0f) {
			int chosen_cube = Random.Range(0, transform.childCount);

			timer = CHANGE_TIME;
		}
	}
}
