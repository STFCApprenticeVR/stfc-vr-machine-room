using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPointer : MonoBehaviour {
	private SteamVR_TrackedObject trackedObj;

	public GameObject laserPrefab;
	private GameObject laser;
	private Transform laserTransform;
	private Vector3 hitPoint;

	public Transform cameraRigTransform; 
	public GameObject teleportReticlePrefab;
	private GameObject reticle;
	private Transform teleportReticleTransform; 
	public Transform headTransform; 
	public Vector3 teleportReticleOffset; 
	public LayerMask teleportMask; 
	private bool shouldTeleport; 

	private float dashTime = 0.2f;


	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	private void ShowLaser(RaycastHit hit) {
		laser.SetActive (true);
		laserTransform.position = Vector3.Lerp (trackedObj.transform.position, hitPoint, .5f);
		laserTransform.LookAt (hitPoint);
		laserTransform.localScale = new Vector3 (laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
	}
	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	private IEnumerator Teleport() {
		yield return new WaitForSeconds(0.1f);

		shouldTeleport = false;
		reticle.SetActive (false);
		Vector3 startPoint = cameraRigTransform.position;
		Vector3 difference = cameraRigTransform.position - headTransform.position;
		difference.y = 0;
		hitPoint.y = 0;

		float elapsed = 0f;

		while (elapsed < dashTime) {
			elapsed += Time.deltaTime;
			float elapsedPct = elapsed / dashTime;

			cameraRigTransform.position = Vector3.Lerp (startPoint, hitPoint, elapsedPct);
			yield return null;
		}
		//cameraRigTransform.position = hitPoint;
	}

	// Use this for initialization
	void Start () {
		laser = Instantiate (laserPrefab);
		laserTransform = laser.transform;

		reticle = Instantiate (teleportReticlePrefab);
		teleportReticleTransform = reticle.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Controller.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
			RaycastHit hit;
			Physics.Raycast (trackedObj.transform.position, transform.forward,out hit, 10);
			if (Physics.Raycast (trackedObj.transform.position, transform.forward, out hit, 10, teleportMask)) {
				ShowLaser (hit);
				reticle.SetActive (true);
				hitPoint = hit.transform.gameObject.transform.position;

				teleportReticleTransform.position = hitPoint + teleportReticleOffset;
				shouldTeleport = true;
			}
		} else {
			laser.SetActive (false);
			reticle.SetActive (false);
		}

		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport) {
			StartCoroutine(Teleport());
		}
	}
}
