using UnityEngine;
using System;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class WorldBuilder : MonoBehaviour {

	public Transform Player;

	public List<Transform> ActiveObjects;

	float[] timers;
	public float InfluenceDistance = 30.0f;
	public float ResetTime = 3.0f;
	// Use this for initialization
	void Start () {
		ActiveObjects = new List<Transform>();

		int count = 0;

		
		for(int i = 0; count < 10000; i++) {

			int blockCount = 0;
			if( i % 530 < 500 )
				blockCount = 10;
			else if (i % 530 < 525)
				blockCount = 200;
			else
				blockCount = 1000;

			int y = Random.Range( 3, Math.Min(10, blockCount / 3));
			int area = blockCount / y;
			int z = Random.Range( 1, (int)Mathf.Sqrt(area));
			int x = area/z;

			Transform newObj = HexahedronFiller.FillHexahedron(x, y, z, 1 /*Random.Range(0.4f, 0.95f)*/, 0.25f, 2).transform;

			if(newObj.childCount != 0)  {
				ResetObject(newObj);
				ActiveObjects.Add(newObj);
				count += newObj.childCount;
				if(Random.value < 0.01f) {
					newObj.gameObject.AddComponent<MusicGenerator>();
					newObj.gameObject.AddComponent<Light>();
				}
			}else {
				Destroy(newObj.gameObject);
			}

		}

		timers = new float[ActiveObjects.Count];
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < ActiveObjects.Count; i++){
			if( Vector3.Distance( ActiveObjects[i].position, Player.position) > InfluenceDistance + ActiveObjects[i].collider.bounds.max.magnitude) {
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
		_obj.collider.enabled = false;

		float scale = _obj.localScale.y * Random.Range(0.3f, 2.0f);
		_obj.localScale = new Vector3(scale, scale, scale);

		float boundsMax = _obj.collider.bounds.max.magnitude/2;
		Vector2 inCirlce = Random.insideUnitCircle.normalized * (InfluenceDistance - 5 + Random.Range(-10, 40));
		inCirlce += inCirlce.normalized * boundsMax;


		_obj.position = Player.position + new Vector3(inCirlce.x, _obj.localScale.y/2 - Player.position.y, inCirlce.y);
		_obj.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
	}

	Color RandomColor() {
		Color col = new Color(Random.value, Random.value, Random.value);
		return col;
	}
}
