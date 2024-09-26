using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables;
using VRTK.Controllables.PhysicsBased;

public class DeleteButtonPressed : MonoBehaviour
{
    public LightsController lightsController;
    public InstructionsController instructionsController;

    // Button is only pressable once
    private bool alreadyPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Call buttonPushed function when button is fully pushed
        GetComponent<VRTK_PhysicsPusher>().MaxLimitReached += new ControllableEventHandler(buttonPushed);
    }

    void buttonPushed(object sender, ControllableEventArgs e) {
        // If not already pressed
        if (!alreadyPressed) {
            alreadyPressed = true;

            // Set all lights to red
            lightsController.lightsError();

            // Show text for delete button pressed
            instructionsController.disableAll();
            instructionsController.wrongButton.SetActive(true);
            // Win condition no longer possible
        }
    }
}
