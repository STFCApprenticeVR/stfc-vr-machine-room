using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables;
using VRTK.Controllables.PhysicsBased;
using UnityEngine.SceneManagement;

public class ResetButtonPressed : MonoBehaviour
{
    // Start is called before the first frame update
    bool alreadyPressed = false;
    public LightFade lightFade;

    public InstructionsController instructionsController;

    public GameObject fakeNewServer;
    public GameObject alternateWinText;
    void Start()
    {
        GetComponent<VRTK_PhysicsPusher>().MaxLimitReached += new ControllableEventHandler(buttonPushed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buttonPushed(object sender, ControllableEventArgs e) {
        if (!alreadyPressed) {
            alreadyPressed = true;
            lightFade.lightsOn();

            if (fakeNewServer.activeSelf) {
                alternateWinText.SetActive(true);
                Invoke("restart", 10f);
            } else {
                instructionsController.disableAll();
                instructionsController.rightButton.SetActive(true);
                instructionsController.storage.SetActive(true);
            }
        }
    }

    void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
