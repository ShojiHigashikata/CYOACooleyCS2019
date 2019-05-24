using UnityEngine;
using System.Collections;

public class LightWall : MonoBehaviour {

	Renderer renderer;
	Material mat;

	// Use this for initialization
	void Start () 
	{
	    //Grabs renderer and sets the emission material to it. 
		renderer = GetComponent<Renderer>();
		mat = renderer.material;
		mat.SetColor("_EmissionColor", Color.black);

	}
	
	// Update is called once per frame
	void Update () 
	{
	    //On key press, change emit color to the one set. 
		if (Input.GetKeyDown(KeyCode.L))
		{
			Color final = Color.white * Mathf.LinearToGammaSpace(4);
			renderer.material.SetColor("_EmissionColor", final);
			DynamicGI.SetEmissive(renderer, final);
		}

	}
}
