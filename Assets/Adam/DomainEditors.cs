using UnityEngine;
using System.Collections;

public class DomainEditors : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static Color getRandColor( ) {
		System.Func<float> rand_col = () => Random.Range(0f,1f);
		return new Color(rand_col(), rand_col(), rand_col());
	}

	public static void ChangeColor(Color new_color, GameObject domain) {
		Material new_material = new Material (Shader.Find ("Diffuse"));
		new_material.color = new_color;
		foreach (Transform child in domain.transform) {
			child.renderer.sharedMaterial = new_material;
		}
	}
}
