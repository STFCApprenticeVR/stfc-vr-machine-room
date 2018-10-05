using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNewDriveInServer : MonoBehaviour {
	public GameObject serverDriveSlot;
	// needed for collision
	public ServerSlotControl serverSlotRend;

	public ServerLightControl serverLight;

	public static bool newDriveInserted;
	public WinReset winReset;


	// Use this for initialization
	void Start () {
		// Server shell starts with broken drive in
		serverSlotRend = GetComponent<ServerSlotControl>();
		serverSlotRend.setVisible(true);
		serverSlotRend.setBroken ();
		newDriveInserted = false;
	}

	void OnTriggerEnter(Collider other) {
		// on collision
		if (other.name == serverDriveSlot.name) {
			// if collider is drive slot
			if (!serverSlotRend.getVisible()) {
				// if old drive has been ejected

				Destroy (this.gameObject);
				// interactable drive no longer needed, destroy it
				serverSlotRend.setVisible(true);
				serverSlotRend.setNormal ();
				// show cosmetic drive in server
				serverLight.setGreen ();
				// door rotate is fucking cancelled
				newDriveInserted = true;

				// win check
				if (PlacePlugInSocket.plugInSocket) {
					winReset.win ();
				}
			}
		} 
	}

}
