using UnityEngine;
using System.Collections;

public class Rainbow : MonoBehaviour {
	float timer;
	float CHANGE_TIME;

	// Use this for initialization
	void Start () {
		CHANGE_TIME = Random.Range(1f, 10f);
		timer = CHANGE_TIME;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0f) {
			DomainEditors.ChangeColor(DomainEditors.getRandColor(), this.gameObject);
			
			timer = CHANGE_TIME;
		}
	}
}
