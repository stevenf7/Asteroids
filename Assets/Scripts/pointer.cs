using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class pointer : MonoBehaviour
{

    public float m_Distance = 10.0f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0;
    public LayerMask m_InteractableMask = 0;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null; //
    public float throwForce = 30f;
    float nukethrow = 20f;
    public GameObject GrenadePrefab;
    public GameObject NukePrefab;

    [SerializeField]
    public int missle_available = 5;

    public int missle_launched = 0;
   
    // could be different based on left, right, or head controller
    private Transform m_CurrentOrigin = null;
    private GameObject m_CurrentObject = null;

    void Start()
    {
        SetLineColor();
        
    }
    private void Awake()
    {
        playerEvents.OnControllerSource += UpdateOrigin;
        playerEvents.OnTouchpadDown += ProcessTouchpadDown;
        playerEvents.OnTriggerDown += ProcessTriggerDown;
        playerEvents.OnBackButtonDown += ProcessBackButton;
        playerEvents.OnTouchpadTouchDown += ProcessTouchpadTouchDown;
    }

    private void OnDestroy()
    {
        playerEvents.OnControllerSource -= UpdateOrigin;
        playerEvents.OnTouchpadDown -= ProcessTouchpadDown;
        playerEvents.OnTriggerDown -= ProcessTriggerDown;
        playerEvents.OnBackButtonDown -= ProcessBackButton;
        playerEvents.OnTouchpadTouchDown -= ProcessTouchpadTouchDown;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        //set origin for the pointer
        m_CurrentOrigin = controllerObject.transform;
        //set visibility of line renderer, is it visible?
        if (controller == OVRInput.Controller.Touchpad)
        {
            m_LineRenderer.enabled = false;
        }
        else
        {
            m_LineRenderer.enabled = true;
        }

    }

    private void ProcessTouchpadTouchDown()
    {
        if (!m_CurrentObject)
        {
            return;
        }
        Debug.Log("ProcessTouchpadDown");
        Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
        interactable.Destruction();
    }

    private void ProcessBackButton()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
            SceneManager.LoadScene("Menu");
    }
    private void ProcessTouchpadDown()
    {
        //spawn game object?
        if (FindObjectOfType<SuperWeapon>().nukes_available > 0) 
        { 
            ThrowNuke();
            FindObjectOfType<SuperWeapon>().nukes_available--;
        }
        
    }

    private void ProcessTriggerDown()
    { 

        
        if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "endGame"||SceneManager.GetActiveScene().name == "endGame_Local")
        {
            ProcessTouchpadTouchDown();
            return;
        }
        if (SceneManager.GetActiveScene().name == "Hammer")
        {
            ThrowHammer();
            return;
        }
        if (SceneManager.GetActiveScene().name == "Help" || SceneManager.GetActiveScene().name =="Credit")
        {
            ProcessBackButton();
            return;
        }
        ThrowGrenade();


    }
    private void ThrowNuke()
    {
        GameObject nuke = Instantiate(NukePrefab, m_CurrentOrigin.position, m_CurrentOrigin.rotation * Quaternion.Euler(90, Random.Range(-1, 1), Random.Range(-1, 1)));

        Rigidbody rb = nuke.GetComponent<Rigidbody>();
        
        rb.AddForce(m_CurrentOrigin.forward * nukethrow, ForceMode.VelocityChange);
    }
    private void ThrowGrenade()
    {
        if (missle_launched >= missle_available)
        {
            return;
        }
        GameObject grenade = Instantiate(GrenadePrefab, m_CurrentOrigin.position, m_CurrentOrigin.rotation * Quaternion.Euler(90, 90, 90) );

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(m_CurrentOrigin.forward * throwForce,ForceMode.VelocityChange );
        missle_launched++;
    }

    private void ThrowHammer()
    {
        if (missle_launched >= missle_available)
        {
            return;
        }
        GameObject hammer = Instantiate(GrenadePrefab, m_CurrentOrigin.position, m_CurrentOrigin.rotation * Quaternion.Euler(0, 90, 90));

        Rigidbody rb = hammer.GetComponent<Rigidbody>();
        rb.AddForce(m_CurrentOrigin.forward * throwForce*3, ForceMode.VelocityChange);
        missle_launched++;
    }


    private void SetLineColor()
    {
        if (!m_LineRenderer)
        {
            return;
        }
        Color endColor = Color.cyan;
        //a to make it transparent
        endColor.a = 0.0f;

        //set the end color to transparent
        m_LineRenderer.endColor = endColor;
    }

    private Vector3 UpdateLine()
    {
        //create ray
        RaycastHit hit = CreateRaycast(m_EverythingMask);


        //create default end - so if we don't hit anything, we give it a default target
        Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);


        //check hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }

        //set position
        m_LineRenderer.SetPosition(0, m_CurrentOrigin.position); // starting point at handle
        m_LineRenderer.SetPosition(1, endPosition); //ending point at the hit point

        return endPosition;

    }

    private GameObject UpdatePointerStatus()
    {
        //create ray
        RaycastHit hit = CreateRaycast(m_InteractableMask);
        
        //check hit
        if (hit.collider)
        {
            return hit.collider.gameObject;
        }
        //return
        return null;

    }

    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);

        return hit;
    }
    // Update is called once per frame
    private void Update()
    {
        Vector3 hitPoint = UpdateLine();

        m_CurrentObject = UpdatePointerStatus();

        if(OnPointerUpdate != null)
        {
            OnPointerUpdate(hitPoint, m_CurrentObject);
        }


    }
}
