﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables;
using VRTK.Controllables.PhysicsBased;

public class DeleteButtonPressed : MonoBehaviour
{
    // Start is called before the first frame update
    bool alreadyPressed = false;
    public LightFade lightFade;
    public InstructionsController instructionsController;
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
            lightFade.lightsError();

            instructionsController.disableAll();
            instructionsController.wrongButton.SetActive(true);

        }
    }
}
