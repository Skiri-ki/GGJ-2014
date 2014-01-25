using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BodyPartPrefabCollection : MonoBehaviour {
//	public TypedBodyPartPrefabCollection [] bodyPartCollections;
	public static BodyPartPrefabCollection COLLECTION;
	
	public TypedBodyPartExample[] examples;

	public BodyPart [] prefabs;

	void Awake(){
		COLLECTION = this;
	}

	public BodyPart GetExampleFor(BodyPart.BodyPartDomain domain){
		IEnumerable<TypedBodyPartExample> partQuerry =
			from example in examples
				where example.domain == domain
				select example;

		List<BodyPart> foundExamples = new List<BodyPart>(1);
		foreach (TypedBodyPartExample foundExample in partQuerry) {
			foundExamples.Add(foundExample.example);
		}
		return foundExamples[0];
	}

	public List<T> GetPrefabs<T>(T example) where T : BodyPart{
		IEnumerable<BodyPart> partQuerry =
			from prefab in prefabs
				where prefab.GetType() == example.GetType()
				select prefab;

		List<T> foundPrefabs = new List<T>();
		foreach (T foundPrefab in partQuerry) {
			foundPrefabs.Add(foundPrefab);
		}

		return foundPrefabs;
	}
}
