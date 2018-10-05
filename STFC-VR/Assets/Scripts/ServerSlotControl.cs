using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSlotControl : MonoBehaviour {
	public GameObject serverSlotMesh;
	private Renderer rend;

	public Material broken;
	public Material normal;

	// Use this for initialization
	void Start () {
		// server contains broken drive at start
		rend = serverSlotMesh.GetComponent<Renderer>();
		rend.material = broken;
	}

	public void setBroken() {
		rend.material = broken;
	}

	public void setNormal() {
		rend.material = normal;
	}

	public void setVisible (bool visible) {
		rend.enabled = visible;
	}

	public bool getVisible() {
		return rend.enabled;
	}
}
