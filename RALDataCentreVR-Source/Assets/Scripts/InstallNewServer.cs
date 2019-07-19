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
    public GameObject newServer;
    public GameObject fakeNewServer;
    public Light globalLight;
    private bool alreadySized = false;
    private Vector3 origScale;

    public InstructionsController instructionsController;
    // Start is called before the first frame update
    void Start()
    {
        origScale = newServer.transform.localScale;
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(serverGrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectSnappedToDropZone += new InteractableObjectEventHandler(serverInstalled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void serverGrabbed(object sender, InteractableObjectEventArgs e) {
        resizeServer();
    }

    void resizeServer() {
        if (!alreadySized) {
            origScale.Set(NewScale, NewScale, NewScale);
            this.transform.localScale = origScale;
            alreadySized = true;
        }
    }

    void serverInstalled(object sender, InteractableObjectEventArgs e) {
        fakeNewServer.SetActive(true);
        if (globalLight.intensity > 0) {
            instructionsController.disableAll();
            instructionsController.serverInstalled.SetActive(true);
            Invoke("restart", 10f);
        }
    }

    void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
