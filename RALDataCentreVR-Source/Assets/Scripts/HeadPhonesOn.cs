using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HeadPhonesOn : MonoBehaviour
{
    private GameObject[] doors;
    public GameObject eye;
    public GameObject snapZone;

    // Start is called before the first frame update
    void Start()
    {
        // Set snap zone parent to in game eye position
        snapZone.transform.SetParent(eye.transform);
        // Move snap zone to slightly above the head
        snapZone.transform.localPosition = new Vector3(0f, 0.1f, 0.05f);

        // Fetch all HPD doors
        doors = GameObject.FindGameObjectsWithTag("HPDDoor");

        // Unlock doors when headphones are put on
        GetComponent<VRTK_InteractableObject>().InteractableObjectSnappedToDropZone += new InteractableObjectEventHandler(enableDoors);
        // Lock doors when headphones are removed
        GetComponent<VRTK_InteractableObject>().InteractableObjectUnsnappedFromDropZone += new InteractableObjectEventHandler(disableDoorsCall);

        // Start with doors locked
        disableDoors();
    }

    void enableDoors(object sender, InteractableObjectEventArgs e) {
        // Lock headphones in place on head
        this.transform.SetParent(eye.transform);
        this.GetComponent<Rigidbody>().useGravity = false;

        // For all hpd doors
        foreach (GameObject door in doors) {
            // Fetch hinge joint component
            HingeJoint hinge = door.GetComponent<HingeJoint>();
            // Set joint limits to 180 degree arc
            JointLimits limits = hinge.limits;
            limits.min = -90;
            limits.max = 90;
            hinge.limits = limits;
        }
    }

    // Encapsulating this function allows disable doors to be called outside of an event
    void disableDoorsCall (object sender, InteractableObjectEventArgs eventArgs) {
        disableDoors();
    }

    void disableDoors() {
        // Reenable gravity for headphones
        this.GetComponent<Rigidbody>().useGravity = true;

        // For all hpd doors
        foreach (GameObject door in doors) {
            // Fetch hinge joint component
            HingeJoint hinge = door.GetComponent<HingeJoint>();
            // Set angle limits to zero, preventing any movement
            JointLimits limits = hinge.limits;
            limits.min = 0;
            limits.max = 0;
            hinge.limits = limits;
        }
    }
}
