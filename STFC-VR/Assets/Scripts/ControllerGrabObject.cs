using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {
	private SteamVR_TrackedObject trackedObj;
	private GameObject collidingObject;
	private GameObject objectInHand;

	public GameObject serverButton;
	// still needed for collision
	public ServerLightControl serverLight;

	public GameObject serverDrive;
	public ServerSlotControl serverSlotRend;

	public WinReset winReset;


	public GameObject brokenDrive;
	// interactions too small for class to be worth it

	void Start() {
		// brokendrive only physically appears once ejected
		brokenDrive.SetActive (false);
	}

	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	private void SetCollidingObject(Collider col) {
		if (collidingObject || !col.GetComponent<Rigidbody> ()) {
			return;
		}

		collidingObject = col.gameObject;
	}

	public void OnTriggerEnter(Collider other) {
		SetCollidingObject (other);
	}

	public void OnTriggerStay(Collider other) {
		SetCollidingObject (other);
	}

	public void OnTriggerExit(Collider other) {
		if (!collidingObject) {
			return;
		}

		collidingObject = null;
	}
		
	private FixedJoint AddFixedJoint() {
		FixedJoint fj = gameObject.AddComponent<FixedJoint> ();
		fj.breakForce = 999999;
		fj.breakTorque = 999999;
		return fj;
	}

	private void GrabObject() {
		objectInHand = collidingObject;
		collidingObject = null;

		var joint = AddFixedJoint ();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody> ();
	}
		

	private void ReleaseObject() {
		if (GetComponent<FixedJoint> ()) {
			GetComponent<FixedJoint> ().connectedBody = null;
			Destroy (GetComponent<FixedJoint> ());

			// do not carry over velocity
			// stops users from throwing objects out of playspace
		}

		objectInHand = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (Controller.GetHairTriggerDown ()) {
			if (collidingObject) {
				
				if (collidingObject.name == serverButton.name) {
					// if pressing server button
					if (brokenDrive.activeSelf == false) {
						// if button has not been pressed before
						serverSlotRend.setVisible(false);
						serverLight.setBlue ();

						// put interactive broken drive in scene
						brokenDrive.SetActive (true);
						Rigidbody brokenDriveRigid = brokenDrive.GetComponent<Rigidbody> ();
						brokenDriveRigid.velocity = new Vector3 (-1, 0, 0);
						// fetch rigid and give slight forward velocity as it falls
					}

				} else {
					GrabObject ();
				}
			}
		}

		if (Controller.GetHairTriggerUp ()) {
			if (objectInHand) {
				ReleaseObject ();
			}
		}

		if (Controller.GetPress (SteamVR_Controller.ButtonMask.ApplicationMenu)) {
			winReset.reset ();
		}
	}
}
