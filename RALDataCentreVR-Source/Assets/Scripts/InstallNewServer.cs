using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class InstallNewServer : MonoBehaviour
{
    // Resize Vars
    [Range(0.1f, 2f)]
    public float NewScale;
    private Vector3 origScale;

    public GameObject newServer;
    // Snap Zone event blocks any rescaling, so a fake server is used for its final position
    public GameObject fakeNewServer;
    private bool alreadySized = false;

    // Used to check if data centre has power for win condition
    public Light globalLight;
    
    public InstructionsController instructionsController;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure server is correct initial sizeS
        origScale = newServer.transform.localScale;

        // Assign object grapped and snapped events to appropriate functions
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(serverGrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectSnappedToDropZone += new InteractableObjectEventHandler(serverInstalled);
    }

    void serverGrabbed(object sender, InteractableObjectEventArgs e) {
        // Shrink the server when grabbed as actual size is unwieldy
        resizeServer();
    }

    void resizeServer() {
        // If not already resized, rescale uniformly
        if (!alreadySized) {
            origScale.Set(NewScale, NewScale, NewScale);
            this.transform.localScale = origScale;
            alreadySized = true;
        }
    }

    void serverInstalled(object sender, InteractableObjectEventArgs e) {
        // Enable fake server
        fakeNewServer.SetActive(true);

        // If global light is on, data centre has power
        if (globalLight.intensity > 0) {
            // Show final success message
            instructionsController.disableAll();
            instructionsController.serverInstalled.SetActive(true);
            // Restart game after 10 seconds
            Invoke("restart", 10f);
        }
    }

    void restart() {
        // Reloading the scene resets the state of all entities
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
