﻿using UnityEngine;
using System;
using System.Collections.Generic;
using GameObjectExtension;

using Random = UnityEngine.Random;

public class WorldBuilder : MonoBehaviour {

	public Transform Player;
//	public GameObject cube;
	public static WorldBuilder builder;
	void Awake(){
		builder = this;
	}

	public List<Transform> ActiveObjects;

	float[] timers;
	public float InfluenceDistance = 30.0f;
	public float ResetTime = 3.0f;
	// Use this for initialization
	void Start () {
		ActiveObjects = new List<Transform>();

		int count = 0;

		
		for(int i = 0; count < 15000; i++) {

			int blockCount = 0;
			if( i % 60 < 30 )
				blockCount = 10;
			else if (i % 60 < 40)
				blockCount = 200;
			else
				blockCount = 1000;

			bool isDomain = false;
			Transform newObj = null;
			if(Random.value < 90.0f/(blockCount))
				newObj=BuildCreature(blockCount).transform;
			else {
				newObj = BuildDomain(blockCount, Random.Range(0, 100000)).transform;
				isDomain = true;
			}
				
			count += blockCount;
			if(newObj.childCount != 0)  {
				ResetObject(newObj);
				ActiveObjects.Add(newObj);
				if(isDomain) {
					float scale = newObj.localScale.y * Random.Range(0.3f, 4.0f);
					newObj.localScale = new Vector3(scale, scale, scale);
					newObj.position = new Vector3(newObj.position.x, 4, newObj.position.z);
				}

				if(Random.value < 0.1f) {
					newObj.gameObject.AddComponent<MusicGenerator>();
					newObj.gameObject.AddComponent<Light>();
				}
			}else {
				Destroy(newObj.gameObject);
			}

		}

		timers = new float[ActiveObjects.Count];

		StartCoroutine(ResetDelay());
	}

	private GameObject BuildEntity(int blockCount){
//		int numberOfSubparts = Random.Range(1,10);

		List<int> blockCountForPart = new List<int>();

		for (int i = 0; 0 < blockCount && i <= 5; i++) {
			blockCountForPart.Add(Random.Range(1,blockCount));
			blockCount -=blockCountForPart[i];
		}
		if(blockCount>0){
			blockCountForPart.Add(blockCount);
		}
		List<GameObject> objects = new List<GameObject>();

		foreach (int count in blockCountForPart) {
			objects.Add(BuildDomain(count));
		}

		return GenerateCreature.GenerateFromParts(objects).gameObject;
	}

	private GameObject BuildCreature(int blockCount){
		int numberOfSubparts = UnityEngine.Random.Range(1,6);
		Body body = null;

		if(numberOfSubparts==1){
			body = BuildDomain(blockCount).AddComponent<Body>();
		}else{
			body = BuildDomain(blockCount/(numberOfSubparts)).AddComponent<Body>();
		}
		
		body.GenerateCreature(numberOfSubparts-1,blockCount/(numberOfSubparts));

		return body.gameObject;
	}

	public static GameObject BuildDomain(int blockCount){
		return BuildDomain(blockCount, UnityEngine.Random.seed);
	}
	public static GameObject BuildDomain(int blockCount, int seed){
		UnityEngine.Random.seed = seed;
		int y = (int)Math.Pow(blockCount, 0.33f);// UnityEngine.Random.Range( 3, Math.Min(10, blockCount / 3));
		y= Mathf.Max(y,2);
		/*int area = blockCount / y;
		int z = UnityEngine.Random.Range( 2, (int)Mathf.Sqrt(area));
		int x = area/z;*/
		int z = (int)Math.Pow(blockCount, 0.33f);
		int x = (int)Math.Pow(blockCount, 0.33f);
		x= Mathf.Max(x,2);
		z= Mathf.Max(z,2);

		int nos = Mathf.Max(0, (int) Mathf.Floor(Mathf.Log (blockCount)));
		float spacing = Random.Range (0.9f, 1f);
		spacing = spacing < 0.95f ? spacing : 1f;
		
		GameObject generatedObj = HexahedronFiller.FillHexahedron(x, y, z, spacing, Random.Range(0.25f, 0.5f), nos);

		return generatedObj;
	}

	
	Queue <Transform> resetQueue = new Queue<Transform>();

	// Update is called once per frame
	void Update () {
		for(int i = 0; i < ActiveObjects.Count; i++){
			float dot = Vector3.Dot( ActiveObjects[i].position - Player.position, Player.forward);
			dot = Mathf.Clamp(dot, 0.3f, 1);
			if( Vector3.Distance( ActiveObjects[i].position, Player.position)  > InfluenceDistance * dot + ActiveObjects[i].collider.bounds.max.magnitude) {
				if(timers[i] == -1)
					timers[i] = ResetTime;
				else if(timers[i] >= 0) {
					timers[i] -= Time.deltaTime;
					if(timers[i] <= 0) {
						timers[i] = -1;
						resetQueue.Enqueue(ActiveObjects[i]);
//						ResetObject(ActiveObjects[i]);
					}
				}
			} else {
				timers[i] = -1;
			}
		}
	}

	System.Collections.IEnumerator ResetDelay(){
		while(true){
			if(resetQueue.Count >0){
				Transform trans = resetQueue.Dequeue();
				bool setKinematicBack = false;
				if(trans.rigidbody && !trans.rigidbody.isKinematic){
					trans.rigidbody.isKinematic = true;
					ResetObject(trans);
					setKinematicBack=true;
				}else
					ResetObject(trans);

				yield return new WaitForSeconds(0.1f);

				if(setKinematicBack)
					trans.rigidbody.isKinematic = false;

			}
			yield return new WaitForSeconds(0.3f);
		}
	}

	void ResetObject(Transform _obj) {
		_obj.collider.enabled = false;

		float boundsMax = _obj.collider.bounds.max.magnitude/2;

		Vector2 inCirlce = Random.insideUnitCircle.normalized * (InfluenceDistance - 5 + Random.Range(-10, 40));
		inCirlce += inCirlce.normalized * boundsMax;


		_obj.position = Player.position + new Vector3(inCirlce.x, -Player.position.y + (!_obj.rigidbody ? 5 : 0), inCirlce.y);
		if(!_obj.rigidbody)
			_obj.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
	}

	Color RandomColor() {
		Color col = new Color(Random.value, Random.value, Random.value);
		return col;
	}
}
