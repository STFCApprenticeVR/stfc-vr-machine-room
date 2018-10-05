using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlugInSocket : MonoBehaviour {
	public GameObject socket;
	private Vector3 socketPosition;
	private Vector3 plugEndPosition;
	private Rigidbody plugBody;
	// needed for collision
	public static bool plugInSocket;

	public WinReset winReset;

	// Use this for initialization
	void Start () {
		socketPosition = socket.transform.position;
		plugEndPosition = socketPosition;
		plugEndPosition.z += 0.1f;

		plugBody = this.GetComponent<Rigidbody> ();
		plugInSocket = false;
	}

	void OnTriggerEnter(Collider other) {
		// on collision
		if (other.name == socket.name) {
			plugBody.useGravity = false;
			plugBody.isKinematic = true;
			this.transform.position = plugEndPosition;
			this.transform.eulerAngles = new Vector3 (0, 0, 0);

			plugInSocket = true;

			if (PlaceNewDriveInServer.newDriveInserted) {
				winReset.win ();
			}
				
		} 
	}

}
