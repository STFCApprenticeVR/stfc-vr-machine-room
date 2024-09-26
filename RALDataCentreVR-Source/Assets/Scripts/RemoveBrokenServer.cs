using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RemoveBrokenServer : MonoBehaviour
{
    // Resize Vars
    [Range(0.1f, 2f)]
    public float NewScale ;
    private bool alreadySized = false;
    // Particle Vars
    public GameObject sparks;
    public GameObject sparksBurst;

    public GameObject flashlight;

    // Snap zone for the replacement server
    public GameObject snapDrop;

    public LightsController lightsController;
    public InstructionsController instructionsController;


    // Start is called before the first frame update
    void Start()
    {
        // Start with spark burst off
        sparksBurst.SetActive(false);
        // Assign serverGrabbed function to the event generated when the server is grabbed
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(serverGrabbed);
    }

    void serverGrabbed(object sender, InteractableObjectEventArgs e) {
        if (!alreadySized) {
            // Trigger the burst of sparks, and disable the ongoing sparking system
            sparksBurst.SetActive(true);
            disableSparks();

            resizeServer();

            // Use lightscontroller to disable all lights
            lightsController.lightsOff();
            // Enable the flashlight
            flashlight.SetActive(true);
            // Enable the snap zone for the replacement server
            snapDrop.SetActive(true);

            instructionsController.disableAll();
            instructionsController.postGrabBroken.SetActive(true);
            instructionsController.prePressButton.SetActive(true);
        }

    }

    void resizeServer() {
        // If not already resized
        if (!alreadySized) {
            // Uniformly rescale to make object more manageable
            Vector3 origScale = this.transform.localScale;
            this.transform.localScale = new Vector3(origScale.x * NewScale, origScale.y * NewScale, origScale.z * NewScale);
            alreadySized = true;
        }
    }

    void disableSparks() {
        sparks.SetActive(false);
    }
}
