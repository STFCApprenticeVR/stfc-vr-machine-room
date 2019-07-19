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
        snapZone.transform.SetParent(eye.transform);
        snapZone.transform.localPosition = new Vector3(0f, 0.1f, 0.05f);
        doors = GameObject.FindGameObjectsWithTag("HPDDoor");
        GetComponent<VRTK_InteractableObject>().InteractableObjectSnappedToDropZone += new InteractableObjectEventHandler(enableDoors);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUnsnappedFromDropZone += new InteractableObjectEventHandler(disableDoorsCall);

        disableDoors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void enableDoors(object sender, InteractableObjectEventArgs e) {
        this.transform.SetParent(eye.transform);
        this.GetComponent<Rigidbody>().useGravity = false;
        foreach (GameObject door in doors) {
            HingeJoint hinge = door.GetComponent<HingeJoint>();
            JointLimits limits = hinge.limits;
            limits.min = -90;
            limits.max = 90;
            hinge.limits = limits;
        }
    }

    void disableDoorsCall(object sender, InteractableObjectEventArgs e) {
        disableDoors();
    }
    void disableDoors() {
        this.GetComponent<Rigidbody>().useGravity = true;
        foreach (GameObject door in doors) {
            HingeJoint hinge = door.GetComponent<HingeJoint>();
            JointLimits limits = hinge.limits;
            limits.min = 0;
            limits.max = 0;
            hinge.limits = limits;
        }
    }
}
