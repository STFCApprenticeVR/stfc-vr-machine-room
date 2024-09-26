using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FlashLightGrab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(flashlightGrabbed);
    }

    void flashlightGrabbed (object sender, InteractableObjectEventArgs e) {
        this.GetComponent<Rigidbody>().isKinematic = true;
    }
}
