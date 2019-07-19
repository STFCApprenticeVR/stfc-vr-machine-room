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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableAll() {
        GameObject[] instructions = GameObject.FindGameObjectsWithTag("Instructions");
        foreach (GameObject inst in instructions) {
            inst.SetActive(false);
        }
    }
}
