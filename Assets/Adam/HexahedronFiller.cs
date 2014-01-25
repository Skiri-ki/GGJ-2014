using UnityEngine;
using System.Collections;

public class HexahedronFiller : MonoBehaviour {
	static int obj_count = 0;
	private static float scale_factor = 0.95f;
	private static Vector3 local_scale;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static GameObject turnToObject(ref bool[, ,] array1) {
		GameObject holder = new GameObject ("hexahedron_" + obj_count++.ToString ());
		local_scale = new Vector3 (scale_factor, scale_factor, scale_factor);

		for (int x = 0; x < array1.GetLength(0); x++) {
			for (int y = 0; y < array1.GetLength(1); y++) {
				for (int z = 0; z < array1.GetLength(2); z++) {
					if (!array1[x,y,z])
						continue;

					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					// Destroy(cube.GetComponent<BoxCollider>());
					cube.transform.Translate(new Vector3(x,y,z));
					cube.transform.localScale = local_scale;
					cube.transform.parent = holder.transform;
				}
			}
		}

		return holder;
	}

	static void initiate(ref bool[, ,] array1, float seed_prob ) {
		for (int x = 0; x < array1.GetLength(0); x++) {
			for (int y = 0; y < array1.GetLength(1); y++) {
				for (int z = 0; z < array1.GetLength(2); z++) {
					array1[x,y,z] = Random.Range(0f, 1f) <= seed_prob;
				}
			}		
		}
	}

	public static GameObject FillHexahedron(int x, int y, int z, float seed_prob) {
		bool[, ,] array1 = new bool[x, y, z];
		initiate (ref array1, seed_prob);

		return turnToObject (ref array1);
	}
}
