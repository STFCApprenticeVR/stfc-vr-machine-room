using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLightControl : MonoBehaviour {
	private Renderer rend;

	public GameObject rowLight;
	private Renderer rowLightRend;

	public Material lightRed;
	public Material lightBlue;
	public Material lightGreen;

	// Use this for initialization
	void Start () {
		// server contains broken drive at start
		rend = this.GetComponent<Renderer>();
		rowLightRend = rowLight.GetComponent<Renderer> ();
		setRed ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setRed() {
		rend.material = lightRed;
		rowLightRend.material = lightRed;
	}

	public void setBlue() {
		rend.material = lightBlue;
		rowLightRend.material = lightBlue;
	}

	public void setGreen() {
		rend.material = lightGreen;
		rowLightRend.material = lightGreen;
	}
}
