using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HexahedronFiller))]
public class HexagonFillerInspector : Editor {
	int x = 5, z = 5, y = 5;
	float seed_prob = 0.1f, scale_factor = 0.9f;
	int MIN_CUBES = 1, MAX_CUBES = 100;
	int gol_steps = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnInspectorGUI() {
		x = (int) EditorGUILayout.Slider("X:", x, MIN_CUBES, MAX_CUBES);
		y = (int) EditorGUILayout.Slider("Y:", y, MIN_CUBES, MAX_CUBES);
		z = (int) EditorGUILayout.Slider("Z:", z, MIN_CUBES, MAX_CUBES);
		scale_factor = EditorGUILayout.Slider("Scale:", scale_factor, 0f, 1f);
		seed_prob = EditorGUILayout.Slider("Prob:", seed_prob, 0f, 1f);
		gol_steps = (int) EditorGUILayout.Slider("Iters:", gol_steps, 0, 50);
		GameObject hexagon = null;
		if (GUILayout.Button ("CreateObject")) {
			hexagon = HexahedronFiller.FillHexahedron (x, y, z, scale_factor, seed_prob, gol_steps);
			System.Func<float> rand_col = () => Random.Range(0f,1f);
			DomainEditors.ChangeColor(new Color(rand_col(), rand_col(), rand_col()), hexagon);
		}
	}
}
