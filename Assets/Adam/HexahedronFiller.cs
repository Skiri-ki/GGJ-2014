﻿using UnityEngine;

using System.Collections;

public class HexahedronFiller : MonoBehaviour {
	static int obj_count = 0; // Used for naming of new objects

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Based on the boolean array create a new object
	static GameObject turnToObject(GameObject holder, bool[, ,] cube_present, float scale_factor) {
		Vector3 local_scale = new Vector3 (scale_factor, scale_factor, scale_factor);

		for (int x = 0; x < cube_present.GetLength(0); x++) {
			for (int y = 0; y < cube_present.GetLength(1); y++) {
				for (int z = 0; z < cube_present.GetLength(2); z++) {
					if (!cube_present[x,y,z])
						continue;

					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//					GameObject cube = GameObject.Instantiate(WorldBuilder.builder.cube) as GameObject;
					// Destroy(cube.GetComponent<BoxCollider>());
					cube.transform.Translate(new Vector3(x + 0.5f,y + 0.5f, z + 0.5f));
					cube.transform.localScale = local_scale;
					cube.transform.parent = holder.transform;
				}
			}
		}
		holder.collider.enabled = true;
			

		GameObject center = new GameObject("Center" + holder.name);
		center.transform.position = holder.collider.bounds.center;
//		center.transform.localScale = holder.collider.bounds.extents *2;
		Transform [] childs = holder.GetComponentsInChildren<Transform>();
		for (int i = 0; i < childs.Length; i++) {

			if(childs[i] == holder.transform)
				continue;
			childs[i].parent = center.transform;
		}
		center.AddComponent<BoxCollider>();
		center.GetComponent<BoxCollider>().size = holder.GetComponent<BoxCollider>().size;
//		center.GetComponent<BoxCollider>().center = holder.GetComponent<BoxCollider>().center;
		center.collider.enabled=false;
		GameObject.Destroy(holder);
		return center;
	}

	// Number saying how far I am from the center in the given dimension [0,0.33]
	static float getFactor(int pos, int length) {
		return Mathf.Abs((length / 2f) - pos) / (length * 1.5f);
	}

	// Assigns the values to the matrix - each cube is spawned with the seed_prob probability
	static void initiate(bool[, ,] cube_present, float seed_prob ) {
		for (int x = 0; x < cube_present.GetLength(0); x++) {
			float fact_x = getFactor(x, cube_present.GetLength(0));
			for (int y = 0; y < cube_present.GetLength(1); y++) {
				float fact_y = getFactor(y, cube_present.GetLength(1));
				for (int z = 0; z < cube_present.GetLength(2); z++) {
					float fact_z = getFactor(z, cube_present.GetLength(2));
					cube_present[x,y,z] = Random.Range(0f, 1f) * (fact_x + fact_y + fact_z) <= seed_prob ;
				}
			}		
		}
	}

	static int getNeigbour(int val, int length, bool increase) {
		if (increase) {
			return val < (length - 1) ? val + 1 : 0;
		} else {
			return val > 0 ? val - 1 : (length - 1);
		}
	}

	static int getNeighbourCount(bool[, ,] cube_present, int x, int y, int z) {
		int result = 0;

		result += cube_present[getNeigbour(x, cube_present.GetLength(0), true),y,z] ? 1 : 0;
		result += cube_present[getNeigbour(x, cube_present.GetLength(0), false),y,z] ? 1 : 0;
		result += cube_present[x, getNeigbour(y, cube_present.GetLength(1), true),z] ? 1 : 0;
		result += cube_present[x, getNeigbour(y, cube_present.GetLength(1), false),z] ? 1 : 0;
		result += cube_present[x, y, getNeigbour(z, cube_present.GetLength(2), true)] ? 1 : 0;
		result += cube_present[x, y, getNeigbour(z, cube_present.GetLength(2), false)] ? 1 : 0;

		return result;
	}

	static bool hasNeighbour(int val, int length, bool increase) {
		if (increase) {
			return val < (length - 1);
		} else {
			return val > 0;
		}
	}

	static int getExactCount(bool[, ,] cube_present, int x, int y, int z) {
		int result = 0;
		
		result += cube_present[getNeigbour(x, cube_present.GetLength(0), true),y,z] & hasNeighbour(x, cube_present.GetLength(0), true) ? 1 :  0;
		result += cube_present[getNeigbour(x, cube_present.GetLength(0), false),y,z] & hasNeighbour(x, cube_present.GetLength(0), false) ? 1 :  0;
		result += cube_present[x, getNeigbour(y, cube_present.GetLength(1), true),z] & hasNeighbour(y, cube_present.GetLength(1), true) ? 1 :  0;
		result += cube_present[x, getNeigbour(y, cube_present.GetLength(1), false),z] & hasNeighbour(y, cube_present.GetLength(1), false) ? 1 :  0;
		result += cube_present[x, y, getNeigbour(z, cube_present.GetLength(2), true)] & hasNeighbour(z, cube_present.GetLength(2), true) ? 1 :  0;
		result += cube_present[x, y, getNeigbour(z, cube_present.GetLength(2), false)] & hasNeighbour(z, cube_present.GetLength(2), false) ? 1 :  0;
		
		return result;
	}

	// Conduct one step of the GameOfLife simulation
	static void iterate(bool[, ,] cube_present) {
		bool [, ,] old_cube = cube_present;
		for (int x = 0; x < cube_present.GetLength(0); x++) {
			for (int y = 0; y < cube_present.GetLength(1); y++) {
				for (int z = 0; z < cube_present.GetLength(2); z++) {
					int present_count = getNeighbourCount(old_cube, x, y, z);
					if (present_count == 4) {
						cube_present[x,y,z] = true;
					} else if (present_count == 0 || present_count == 1 || present_count == 2 || present_count == 6) {
						cube_present[x,y,z] = false;
					}
				}
			}		
		}
	}

	// Conduct one step of the GameOfLife simulation
	static void clean(bool[, ,] cube_present) {
		bool [, ,] old_cube = cube_present;
		for (int x = 0; x < cube_present.GetLength(0); x++) {
			for (int y = 0; y < cube_present.GetLength(1); y++) {
				for (int z = 0; z < cube_present.GetLength(2); z++) {
					int present_count = getExactCount(old_cube, x, y, z);
					if (present_count == 6 || present_count == 0) 
						cube_present[x,y,z] = false;
				}
			}		
		}
	}

	// Creates a hexagon-filling (possibly) voxel object
	// x,y,z are side-lenghts
	// scale_factor scales the individual voxels
	// seed_prob gives chance of the seed ocurring
	// gol_steps gives nuber of gol iterations
	public static GameObject FillHexahedron(int x, int y, int z, float scale_factor, float seed_prob, int gol_steps) {
		bool[, ,] cube_present = new bool[x, y, z];
		initiate (cube_present, seed_prob);
		for (int i = 0; i < gol_steps; i++) {
			iterate (cube_present);
		}
		clean (cube_present);
		GameObject holder = new GameObject ("hexahedron_" + obj_count++.ToString ());
		BoxCollider collider = holder.AddComponent<BoxCollider> ();
		collider.center = new Vector3 (x  / 2f, y / 2f, z / 2f);
		collider.size = new Vector3 (x, y, z);
		collider.enabled = false;
		return turnToObject (holder, cube_present, scale_factor);
	}
}
