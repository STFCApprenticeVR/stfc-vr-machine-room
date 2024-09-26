using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    public GameObject preGrabBroken;
    public GameObject postGrabBroken;
    public GameObject prePressButton;
    public GameObject rightButton;
    public GameObject wrongButton;
    public GameObject serverInstalled;
    public GameObject storage;

    public void disableAll() {
        // Fetch all instructional text objects
        GameObject[] instructions = GameObject.FindGameObjectsWithTag("Instructions");
        foreach (GameObject inst in instructions) {
            // Disable them
            inst.SetActive(false);
        }
    }
}
