using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFade : MonoBehaviour
{
    public Light globalLight;
    [Range(0f, 1f)]
    public float lightBrightness;
    [Range(0f, 1f)]
    public float darkBrightness;

    public Material facadeLitTexture;
    public Material facadeUnlitTexture;
    public Material facadeErrorTexture;

    public Material indicatorOn;
    public Material indicatorOff;
    public Material indicatorError;

    private GameObject[] lights;
    private GameObject[] serverFacades;
    private GameObject[] serverIndicators;
    private GameObject[] emergencyLights;
    private GameObject[] realLights;

    private bool error = false;
    // Start is called before the first frame update
    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");
        serverFacades = GameObject.FindGameObjectsWithTag("ServerFront");
        serverIndicators = GameObject.FindGameObjectsWithTag("Indicator");
        emergencyLights = GameObject.FindGameObjectsWithTag("EmergencyLight");
        realLights = GameObject.FindGameObjectsWithTag("RealLight");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lightsOn() {
        if (!error) {
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
