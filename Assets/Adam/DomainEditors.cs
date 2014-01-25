using UnityEngine;
using UnityEditor;
using System.Collections;

public class DomainEditors : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void ChangeColor(Color new_color, GameObject domain) {
		Material new_material = new Material (Shader.Find ("Diffuse"));
		new_material.color = new_color;
		foreach (Transform child in domain.transform) {
			child.renderer.sharedMaterial = new_material;
		}
	}
}
