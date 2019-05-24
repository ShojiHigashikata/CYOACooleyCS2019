using UnityEngine;
using System.Collections;

public class SetSunLight : MonoBehaviour {
    //Initialize variables
	public Renderer lightwall;
	Material sky;
	public Renderer water;
	public Transform stars;
	public Transform worldProbe;

	// Use this for initialization
	void Start () {
        //On start, set sky to the skybox material
		sky = RenderSettings.skybox;

	}
    //Set all unnatural lighting to false by default. 
	bool lighton = false;

	// Update is called once per frame
	void Update () {
        //Make the stars rotate over time
		stars.transform.rotation = transform.rotation;
        
        //Turn on unnatural lights. 
		if (Input.GetKeyDown(KeyCode.T)) {

			lighton = !lighton;
        
		}

        //Changes emissive gamma, effectively making the scene more lit/bright. 
		if (lighton) {
			Color final = Color.white * Mathf.LinearToGammaSpace(5);
			lightwall.material.SetColor("_EmissionColor", final);
			DynamicGI.SetEmissive(lightwall, final);
		} else {
			Color final = Color.white * Mathf.LinearToGammaSpace(0);
			lightwall.material.SetColor("_EmissionColor", final);
			DynamicGI.SetEmissive(lightwall, final);
		}
	    
        //Changes probe position based on main camera's position.
		Vector3 tvec = Camera.main.transform.position;
		worldProbe.transform.position = tvec;

        //Water texture offset is changed over time based on a vector 2(x,y only)
		water.material.mainTextureOffset = new Vector2(Time.time / 100, 0);
		water.material.SetTextureOffset("_DetailAlbedoMap", new Vector2(0, Time.time / 80));

	}
}
