using UnityEngine;
using System.Collections;

public class Juggler : MonoBehaviour {
	float timer;
	float CHANGE_TIME;

	// Use this for initialization
	void Start () {
		CHANGE_TIME = Random.Range(0.1f, 0.5f);
		timer = CHANGE_TIME;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0f) {
			int chosen_cube = Random.Range(0, transform.childCount);

			transform.GetChild(chosen_cube).gameObject.SetActive(!transform.GetChild(chosen_cube).gameObject.activeSelf);

			timer = CHANGE_TIME;
		}
	}
}
