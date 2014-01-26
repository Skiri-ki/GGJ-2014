using UnityEngine;
using System.Collections;

public class Musician : Domain {
	
	public override DomainEnum EDomain{get{return DomainEnum.Audio;}}
	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MusicGenerator>();
		gameObject.AddComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
