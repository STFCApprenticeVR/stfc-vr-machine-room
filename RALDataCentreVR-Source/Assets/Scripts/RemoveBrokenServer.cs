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

    public LightFade lightFade;

    public GameObject flashlight;

    public GameObject snapDrop;

    public InstructionsController instructionsController;


    // Start is called before the first frame update
    void Start()
    {
        sparksBurst.SetActive(false);
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(serverGrabbed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void serverGrabbed(object sender, InteractableObjectEventArgs e) {
        if (!alreadySized) {
            sparksBurst.SetActive(true);
            disableSparks();

            resizeServer();

            lightFade.lightsOff();
            flashlight.SetActive(true);

            snapDrop.SetActive(true);

            instructionsController.disableAll();
            instructionsController.postGrabBroken.SetActive(true);
            instructionsController.prePressButton.SetActive(true);
        }

    }

    void resizeServer() {
        if (!alreadySized) {
            Vector3 origScale = this.transform.localScale;
            this.transform.localScale = new Vector3(origScale.x * NewScale, origScale.y * NewScale, origScale.z * NewScale);
            alreadySized = true;
        }
    }

    void disableSparks() {
        sparks.SetActive(false);
    }
}
