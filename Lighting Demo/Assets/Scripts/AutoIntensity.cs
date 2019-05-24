using UnityEngine;
using System.Collections;

public class AutoIntensity : MonoBehaviour {
    //Initialize all variables
	public Gradient nightDayColor;

	public float maxIntensity = 3f;
	public float minIntensity = 0f;
	public float minPoint = -0.2f;

	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbientPoint = -0.2f;


	public Gradient nightDayFogColor;
	public AnimationCurve fogDensityCurve;
	public float fogScale = 1f;

	public float dayAtmosphereThickness = 0.4f;
	public float nightAtmosphereThickness = 0.87f;

	public Vector3 dayRotateSpeed;
	public Vector3 nightRotateSpeed;

	float skySpeed = 1;

	Light mainLight;
	Skybox sky;
	Material skyMat;

	void Start () {
        //At program startup, get the primary light source and material and set them 
		mainLight = GetComponent<Light>();
		skyMat = RenderSettings.skybox;

	}

	void Update () {
	    //Edits the skybox's intensity over time. 
		float tRange = 1 - minPoint;
		float dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
		float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

		mainLight.intensity = i;

        //Edits the skybox's ambient intensity over time
		tRange = 1 - minAmbientPoint;
		dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
		i = ((maxAmbient - minAmbient) * dot) + minAmbient;
		RenderSettings.ambientIntensity = i;

        //Edits ambient color 
		mainLight.color = nightDayColor.Evaluate(dot);
		RenderSettings.ambientLight = mainLight.color;

        //Edits fog color and density
		RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
		RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

        //Edits intensity based on "atmosphere thickness"
		i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
		skyMat.SetFloat ("_AtmosphereThickness", i);

        //Rotates around based on real-life timer
		if (dot > 0) 
			transform.Rotate (dayRotateSpeed * Time.deltaTime * skySpeed);
		else
			transform.Rotate (nightRotateSpeed * Time.deltaTime * skySpeed);

        //Write debugging tools below
		if (Input.GetKeyDown (KeyCode.Q)) skySpeed *= 0.5f;
		if (Input.GetKeyDown (KeyCode.E)) skySpeed *= 2f;


	}
}
