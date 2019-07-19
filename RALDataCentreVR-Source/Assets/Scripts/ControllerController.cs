using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class ControllerController : MonoBehaviour
{
    private VRTK_HeadsetFade headsetFade;
    public float timeToRestart;
    private bool restarting;
    public VRTK_ControllerTooltips toolTips;
    public GameObject toolTipObject;
    private bool toolTipsActive = true;

    // Start is called before the first frame update
    void Start()
    {
        restarting = false;
        headsetFade = GetComponent<VRTK_HeadsetFade>();

        GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(startRestartFade);
        GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(cancelRestartFade);
        GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(toggleTooltips);
        GetComponent<VRTK_HeadsetFade>().HeadsetFadeComplete += new HeadsetFadeEventHandler(restart);

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startRestartFade(object sender, ControllerInteractionEventArgs e) {
        headsetFade.Fade(Color.black, timeToRestart);
        restarting = true;
    }

    void cancelRestartFade(object sender, ControllerInteractionEventArgs e) {
        if (headsetFade.IsTransitioning()) {
            headsetFade.Unfade(3f);
        }
        restarting = false;
    }

    void restart(object sender, HeadsetFadeEventArgs e) {
        if (restarting) {
            restarting = false;
            headsetFade.Unfade(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void toggleTooltips(object sender, ControllerInteractionEventArgs e) {
        toolTips.ToggleTips(!toolTipsActive);
        toolTipsActive = !toolTipsActive;
    }
}