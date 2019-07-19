# RAL Data Centre VR

The RAL Data Centre VR Experience is a Unity driven VR application to be run through SteamVR, presenting a recreation of the Rutherford Appleton Laboratory R89 Data Centre. The demo runs through a short quest of locating a malfunctioning server and replacing it.
The project is intended for display at recruitment or STEM events to showcase opportunities of work for prospective software engineers. Potential proof of concept for future applications of VR within the science research industry, possibly for use in immersive safety training.

The application makes use of the [VRTK Framework](https://vrtoolkit.readme.io/) to help drive and optimise its interactions.

## User Requirements

Successfully running the VR application requires the following:
+ Standard HTC Vive Headset, Controllers & LightHouses (Functionality for other HMDs is unconfirmed. Only HTC Vive is supported until further notice).
+ Functional installation of [SteamVR software](http://store.steampowered.com/steamvr). (Any errors presented by SteamVR are not specific to the DataCemte application unless it is specifically named. Seek support through Valve or SteamVR community troubleshooting.)
+ Full build of RALDataCentreVR, taken from provided release or built from source files using Unity Studio

Ensure SteamVR is running without error, and start the unity file from the build folder.

## Developer Requirements

Developing for SMRVR requires:
+ Licensed copy of [Unity Studio](https://unity3d.com/unity)
+ IDE for developing C#
+ A HMD is required only for testing of player interactions and perception. Develepment can take place without one.

### Useful Tools
+ [Blender](https://www.blender.org/download/) - Useful for creating 3D models for use in the scene. (Note: Materials should be created and applied in Unity, as materials added before this stage can be unstable and may not carry over to repo or unity project).

