using UnityEngine;
using System.Collections;

public class Rainbow : Domain {
	float timer;
	float CHANGE_TIME;

	public override DomainEnum EDomain{get{return DomainEnum.Visuals;}}

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
