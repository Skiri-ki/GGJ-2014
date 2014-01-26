using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BodyPartPrefabCollection : MonoBehaviour {
//	public TypedBodyPartPrefabCollection [] bodyPartCollections;
	public static BodyPartPrefabCollection COLLECTION;


	public BodyPart [] prefabs;

	void Awake(){
		COLLECTION = this;
	}

	public List<BodyPart> GetPrefabs(BodyPart.DomainEnum domain){
		IEnumerable<BodyPart> partQuerry =
			from prefab in prefabs
				where prefab.EDomain == domain
				select prefab;

		List<BodyPart> foundPrefabs = new List<BodyPart>();
		foreach (BodyPart foundPrefab in partQuerry) {
			foundPrefabs.Add(foundPrefab);
		}
		return foundPrefabs;
	}
}
