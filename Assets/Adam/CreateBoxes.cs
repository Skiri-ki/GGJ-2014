using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateBoxes : MonoBehaviour {
	public int side_size = 10;
	private float scale_factor = 1f;
	private Vector3 local_scale;
	private List<GameObject> cubes; 

	// Use this for initialization
	void Start () {
		GameObject cubes_list = new GameObject ();
		cubes_list.name = "cubes_list";
		local_scale = new Vector3 (scale_factor, scale_factor, scale_factor);
		cubes = new List<GameObject> ();
		for (int x = 0; x < side_size; x++) {
			for (int y = 0; y < side_size; y++) {
				for (int z = 0; z < side_size; z++) {
					if (Random.Range(0f,1f) > 0.05f)
						continue;
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					// Destroy(cube.GetComponent<BoxCollider>());
					cube.transform.localScale = local_scale;
					cube.transform.Translate(new Vector3(x,y,z));
					cube.transform.parent = cubes_list.transform;
					cubes.Add(cube);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
