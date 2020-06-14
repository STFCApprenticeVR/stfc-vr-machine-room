using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables;
using VRTK.Controllables.PhysicsBased;
using UnityEngine.SceneManagement;

public class ResetButtonPressed : MonoBehaviour
{
    public GameObject fakeNewServer;
    public GameObject alternateWinText;

    //Button is only pressable once
    bool alreadyPressed = false;
       
    public LightsController lightsController;
    public InstructionsController instructionsController;

    // Start is called before the first frame update
    void Start()
    {
        // Call buttonpushed function when button is fully pushed
        GetComponent<VRTK_PhysicsPusher>().MaxLimitReached += new ControllableEventHandler(buttonPushed);
    }

    void buttonPushed(object sender, ControllableEventArgs e) {
        if (!alreadyPressed) {
            // Turn lights back on
            alreadyPressed = true;
            lightsController.lightsOn();

            // If new server is already installed
            if (fakeNewServer.activeSelf) {
                // Show game win text
                alternateWinText.SetActive(true);
                // Restart game after 10 seconds
                Invoke("restart", 10f);
            } else {
                // Show instructions to retreive new server
                instructionsController.disableAll();
                instructionsController.rightButton.SetActive(true);
                instructionsController.storage.SetActive(true);
            }
        }
    }

    void restart() {
        // Reloading the scene resets state of all objects
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
