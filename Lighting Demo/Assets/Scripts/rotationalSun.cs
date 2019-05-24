using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationalSun : MonoBehaviour {

    public int timeRotating = 20;
    private int VectorX = 0;
    private int VectorY = 0;
    private int VectorZ = 0;

    public Vector3 goober;

	// Use this for initialization
	void Start () {
		goober.Set(VectorX, VectorY, VectorZ);
    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(goober, Vector3.forward, timeRotating * Time.deltaTime);
    }
}
