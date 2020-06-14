using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    // Vars for brightness of global light
    public Light globalLight;
    [Range(0f, 1f)]
    public float lightBrightness;
    [Range(0f, 1f)]
    public float darkBrightness;

    private GameObject[] serverFacades;
    // Textures for powered, unpowered, and broken server facades
    public Material facadeLitTexture;
    public Material facadeUnlitTexture;
    public Material facadeErrorTexture;

    private GameObject[] serverIndicators;
    // Materials for powered, unpowered and broken indicator lights on the interactable server rack
    public Material indicatorOn;
    public Material indicatorOff;
    public Material indicatorError;

    // Objects for ceiling light meshes
    private GameObject[] lights;
    private GameObject[] emergencyLights;
    // Objects for ceiling light Light components
    private GameObject[] realLights;

    private bool error = false;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch all objects via tags
        lights = GameObject.FindGameObjectsWithTag("Light");
        serverFacades = GameObject.FindGameObjectsWithTag("ServerFront");
        serverIndicators = GameObject.FindGameObjectsWithTag("Indicator");
        emergencyLights = GameObject.FindGameObjectsWithTag("EmergencyLight");
        realLights = GameObject.FindGameObjectsWithTag("RealLight");
    }

    public void lightsOn() {
        // If data centre is not broken
        if (!error) {
            // Set global light to bright
            globalLight.intensity = lightBrightness;

            // make the fake lights black
            foreach (GameObject light in lights) {
                light.transform.GetComponent<Renderer>().material.color = Color.white;
            }
            // set server facades to lit
            foreach (GameObject facade in serverFacades) {
                facade.GetComponent<Renderer>().material = facadeLitTexture;
            }
            // turn on indicator lights
            foreach(GameObject indicator in serverIndicators) {
                indicator.GetComponent<Renderer>().material = indicatorOn;
            }
            // Turn off emergency lights
            foreach(GameObject emerLight in emergencyLights) {
                emerLight.GetComponent<Renderer>().material = indicatorOff;
            }
            // Turn on spotlights
            foreach(GameObject realLight in realLights) {
                realLight.SetActive(true);
            }
        }
    }

    public void lightsOff() {
        if (!error) {
            globalLight.intensity = darkBrightness;
            // make the fake lights black
            foreach (GameObject light in lights) {
                light.transform.GetComponent<Renderer>().material.color = Color.black;
            }
            // set server facades to unlit
            foreach (GameObject facade in serverFacades) {
                facade.GetComponent<Renderer>().material = facadeUnlitTexture;
            }

            // turn off indicator lights
            foreach(GameObject indicator in serverIndicators) {
                indicator.GetComponent<Renderer>().material = indicatorOff;
            }

            // Turn off emergency lights
            foreach(GameObject emerLight in emergencyLights) {
                emerLight.GetComponent<Renderer>().material = indicatorError;
            }
            // Turn off spotlights
            foreach(GameObject realLight in realLights) {
                realLight.SetActive(false);
            }
        }
    }

    public void lightsError() {
        error = true;
        globalLight.intensity = darkBrightness;
        // make the fake lights black
        foreach (GameObject light in lights) {
            light.transform.GetComponent<Renderer>().material.color = Color.red;
        }
        // set server facades to error
        foreach (GameObject facade in serverFacades) {
            facade.GetComponent<Renderer>().material = facadeErrorTexture;
        }

        // turn indicator lights to red
        foreach(GameObject indicator in serverIndicators) {
            indicator.GetComponent<Renderer>().material = indicatorError;
        }
        // Turn off emergency lights
            foreach(GameObject emerLight in emergencyLights) {
                emerLight.GetComponent<Renderer>().material = indicatorOff;
        }
        // Turn off spotlights
            foreach(GameObject realLight in realLights) {
                realLight.SetActive(false);
        }
    }

 }
