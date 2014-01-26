using UnityEngine;
using System.Collections;

public class PaintGun : Domain {
	float timer;
	float CHANGE_TIME;

	public override DomainEnum EDomain{get{return DomainEnum.Visuals;}}

	// Use this for initialization
	void Start () {
		CHANGE_TIME = Random.Range(0.5f, 1f);
		timer = CHANGE_TIME;
		name += "PaintGun";
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision coll) {
//		timer -= Time.deltaTime;
//		if (timer < 0f) {
		DomainEditors.ChangeColor(DomainEditors.getRandColor(), this.gameObject);
		DomainEditors.ChangeColor(this.gameObject.GetComponentInChildren<Renderer>().material.color, coll.gameObject);
			
//			timer = CHANGE_TIME;
//		}
	}
}
