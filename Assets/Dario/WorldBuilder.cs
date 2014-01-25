using UnityEngine;
using System;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class WorldBuilder : MonoBehaviour {

	public Transform Player;
	public MusicGenerator MusicGen;

	public List<Transform> ActiveObjects;

	float[] timers;
	public float InfluenceDistance = 30.0f;
	public float ResetTime = 3.0f;
	// Use this for initialization
	void Start () {
		ActiveObjects = new List<Transform>();

		int count = 530;

		
		for(int i = 0; i < count; i++) {

			int blockCount = 0;
			if( i < 500 )
				blockCount = 10;
			else if (i < 525)
				blockCount = 200;
			else
				blockCount = 1000;

			int y = Random.Range(3, 10);
			int x = Random.Range(3, 10);
			int z = blockCount/(x * y);

			Transform newObj = HexahedronFiller.FillHexahedron(x, y, z, 1 /*Random.Range(0.4f, 0.95f)*/, 0.25f, 2).transform;
			ResetObject(newObj);
			ActiveObjects.Add(newObj);
		}

		timers = new float[ActiveObjects.Count];
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < ActiveObjects.Count; i++){
			if( Vector3.Distance( ActiveObjects[i].position, Player.position) > InfluenceDistance) {
				if(timers[i] == -1)
					timers[i] = ResetTime;
				else if(timers[i] >= 0) {
					timers[i] -= Time.deltaTime;
					if(timers[i] <= 0) {
						timers[i] = -1;
						ResetObject(ActiveObjects[i]);
					}
				}
			} else {
				timers[i] = -1;
			}
		}
	}

	void ResetObject(Transform _obj) {
		Vector2 inCirlce = Random.insideUnitCircle.normalized * (InfluenceDistance - 5);
		float scale = _obj.localScale.y * Random.Range(0.3f, 2.0f);
		_obj.localScale = new Vector3(scale, scale, scale);
		_obj.position = Player.position + new Vector3(inCirlce.x, _obj.localScale.y/2 - Player.position.y, inCirlce.y);
		_obj.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
	}

	Color RandomColor() {
		Color col = new Color(Random.value, Random.value, Random.value);
		return col;
	}
}
