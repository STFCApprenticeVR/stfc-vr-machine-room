using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class ControllerController : MonoBehaviour
{
    // Vars for hold to restart
    private VRTK_HeadsetFade headsetFade;
    // Length in seconds button must be held to restart
    public float timeToRestart;
    // Boolean check is used to differentiate headset fade complete events from a full restart, or a fade back in
    private bool restarting;

    // Vars for tooltips
    public VRTK_ControllerTooltips toolTips;
    public GameObject toolTipObject;
    private bool toolTipsActive = true;

    // Start is called before the first frame update
    void Start()
    {
        // Assign vive grip button to toggle tooltips
        GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(toggleTooltips);

        // Set up initial state for hold to restart
        restarting = false;
        headsetFade = GetComponent<VRTK_HeadsetFade>();
        // Assign menu button to hold to restart
        GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(startRestartFade);
        GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(cancelRestartFade);

        // Call restart function if headset finishes fading
        GetComponent<VRTK_HeadsetFade>().HeadsetFadeComplete += new HeadsetFadeEventHandler(restart);
    }

    void startRestartFade(object sender, ControllerInteractionEventArgs e) {
        // Start the headset fade event
        headsetFade.Fade(Color.black, timeToRestart);
        restarting = true;
    }

    void cancelRestartFade(object sender, ControllerInteractionEventArgs e) {
        // If headset is fading to black, start fading back in
        if (headsetFade.IsTransitioning()) {
            headsetFade.Unfade(3f);
        }
        restarting = false;
    }

    void restart(object sender, HeadsetFadeEventArgs e) {
        // If headset was fading to black
        if (restarting) {
            restarting = false;
            // Start fading back in
            headsetFade.Unfade(3f);
            // Reloading scene resets state of all objects
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void toggleTooltips(object sender, ControllerInteractionEventArgs e) {
        // Set active state of tooltips object to the opposite of current state
        toolTips.ToggleTips(!toolTipsActive);
        toolTipsActive = !toolTipsActive;
    }
}