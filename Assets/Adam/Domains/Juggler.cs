using UnityEngine;
using System.Collections;

public class Juggler : Domain {
	float timer;
	float CHANGE_TIME;
	
	public override DomainEnum EDomain{get{return DomainEnum.Visuals;}}

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
