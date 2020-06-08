using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerEvents : MonoBehaviour
{
    #region Events
    public static UnityAction OnTouchpadUp = null;
    public static UnityAction OnTouchpadDown = null;
    public static UnityAction OnTriggerUp = null;
    public static UnityAction OnTriggerDown = null;
    public static UnityAction OnBackButtonUp = null;
    public static UnityAction OnBackButtonDown = null;
    public static UnityAction OnTouchpadTouchDown = null;
    public static UnityAction OnTouchpadTouchUp = null;


    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;
    #endregion


    #region Anchors

    public GameObject m_LeftAnchor;

    public GameObject m_RightAnchor;

    public GameObject m_HeadAnchor;

    #endregion

    //The controller as key, corresponding game object as dictionary


    #region Input
    private Dictionary<OVRInput.Controller, GameObject> m_ControllerSets = null;
    private OVRInput.Controller m_InputSource = OVRInput.Controller.None;
    private OVRInput.Controller m_Controller = OVRInput.Controller.None;
    private bool m_InputActive = true;
    #endregion

    //Subscribe to HMDMounted event
    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerFound;

        m_ControllerSets = CreateControllerSets();
    }

    //Unscribe to HMDMounted event
    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerFound;

    }


    //A event to tell only when a new value is connected
    private void CheckForController()
    {
        Debug.Log("Checking for Controllers");
        OVRInput.Controller controllerCheck = m_Controller;
        
        //right remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
        {
            Debug.Log(OVRInput.Controller.RTrackedRemote + "Found");
            controllerCheck = OVRInput.Controller.RTrackedRemote;
        }


        //left remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
        {
            Debug.Log(OVRInput.Controller.LTrackedRemote + "Found");
            controllerCheck = OVRInput.Controller.LTrackedRemote;
        }
        //neither controller connected controller, headset
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)&&
            !OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
        {
            Debug.Log(OVRInput.Controller.Touchpad + "Found");
            controllerCheck = OVRInput.Controller.Touchpad;
        }

        //update 
        m_Controller = UpdateSource(controllerCheck, m_Controller);
    }

    // Update is called once per frame
    private void Update()
    {
        //check for active input
        if (!m_InputActive)
        {
            return;
        }
        //check if controller exist
        CheckForController();
        
        //check for input source
        CheckInputSource();

        //check for actual input
        Input();
    }

    private void CheckInputSource()
    {
        //update
        m_InputSource = UpdateSource(OVRInput.GetActiveController(), m_InputSource);
        Debug.Log("InputSource " + m_InputSource);
    }

    private void Input()
    {
        if(OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("GetDown");
            Debug.Log("OnTouchpadDown status: " + OnTouchpadDown);
            if(OnTouchpadDown != null)
            {
                OnTouchpadDown();
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            Debug.Log("GetUp");
            Debug.Log("OnTouchpadUp status: " + OnTouchpadUp);
            if (OnTouchpadUp != null)
            {
                OnTouchpadUp();
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("GetDown");
            Debug.Log("On trigger button status: " + OnTriggerDown);
            if (OnTriggerDown != null)
            {
                OnTriggerDown();
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("GetUp");
            Debug.Log("On trigger status: " + OnTriggerUp);
            if (OnTriggerUp != null)
            {
                OnTriggerUp();
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            Debug.Log("GetDown");
            Debug.Log("On trigger button status: " + OnBackButtonDown);
            if (OnBackButtonDown != null)
            {
                OnBackButtonDown();
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.Back))
        {
            Debug.Log("GetUp");
            Debug.Log("On trigger status: " + OnBackButtonUp);
            if (OnBackButtonUp != null)
            {
                OnBackButtonUp();
            }
        }

        if (OVRInput.GetDown(OVRInput.Touch.One))
        {
            if (OnTouchpadTouchDown != null)
            {
                OnTouchpadTouchDown();
            }
        }

        if (OVRInput.GetUp(OVRInput.Touch.One))
        {
            if (OnTouchpadTouchUp  != null)
            {
                OnTouchpadTouchUp();
            }
        }


    }

    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        Debug.Log("Update Source");
        //check if values are the same, return
        if(check == previous)
        {
            return previous;
        }

        Debug.Log("Check Controller Object");
        //If different, Get controller object, 
        GameObject controllerObject = null;
        m_ControllerSets.TryGetValue(check, out controllerObject);
        Debug.Log("Controller Value: " + controllerObject);
        //if no controller, set to head
        if(controllerObject == null)
        {
            controllerObject = m_HeadAnchor;
        }
        //send out event
        if (OnControllerSource != null)
        {
            OnControllerSource(check, controllerObject);
            Debug.Log("Send out event: " + check);
        }
        return check;
    }

    private void PlayerFound()
    {
        m_InputActive = true;
    }

    private void PlayerLost()
    {
        m_InputActive = false;
    }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            { OVRInput.Controller.LTrackedRemote, m_LeftAnchor }, 
            { OVRInput.Controller.RTrackedRemote, m_RightAnchor},
            { OVRInput.Controller.Touchpad, m_HeadAnchor}
        };

        return newSets;
    }
}
