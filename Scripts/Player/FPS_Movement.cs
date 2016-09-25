using UnityEngine;
using System.Collections;

public class FPS_Movement : MonoBehaviour
{
    public float runSpeed = 0.01f;
    public float walkSpeed = 100f;
    public float aimSpeed = 0.01f;
    public float idle = 0f;
    public float currentSpeed;
    public Camera fpsCam;
    public float aimFOV = 20f;
    public float regFOV = 60f;
    public float zoomSpeed = 10f;
    public float aimFOVMin = 40f;
    public float aimFOVMax = 5f;
    public GameObject[] objectsToDisable;
    public Canvas scopeCanvas;
    public Animator animBody;

    private float horizontal;
    private float vertical;
    private float mouseWheelInput;
    private PlayerInput pInput;
    private Transform cameraHolder;
    private Rigidbody rb;
    private bool bZoom;
    private CameraController camController;
    private CrosshairManager crosshairManager;
    private float originalCrosshairMaxSize;
    private float originalCrosshairMinSize;
    
    Vector3 movement;
   

    private void Awake()
    {
        
        pInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        crosshairManager = GetComponentInChildren<CrosshairManager>();
        camController = GetComponentInChildren<CameraController>();
        currentSpeed = idle;
        scopeCanvas.enabled = false;

        originalCrosshairMaxSize = crosshairManager.crosshairMaxSize;
        originalCrosshairMinSize = crosshairManager.crosshairMinSize;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            bZoom = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            bZoom = false;
        }
        Aiming();
        /*
        if (bZoom)
        {
            aimFOV += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            fpsCam.fieldOfView = aimFOV;
        }
        if (!bZoom)
            */
        
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Movement();
    }

    

    private void Movement()
    {
        currentSpeed = walkSpeed;


        movement.Set(horizontal, 0, vertical);
        if (pInput.bRun)
        {
            movement = movement.normalized * currentSpeed * 2f* Time.deltaTime;
        }
        else
        {
            movement = movement.normalized * currentSpeed * Time.deltaTime;
        }
        
        transform.Translate(movement.x, 0f, movement.z);
        if (horizontal != 0f || vertical != 0f)
        {
            animBody.SetFloat("Speed", 0.7f);
        }
        else
        {
            animBody.SetFloat("Speed", 0f);
        }
    }

    private void Aiming()
    {
        if (bZoom)
        {
            scopeCanvas.enabled = true;
            crosshairManager.crosshairMaxSize = 0.03f;
            crosshairManager.crosshairMinSize = 0.01f;
            DisableObjectsForAiming();
            aimFOV += (Input.GetAxis("Mouse ScrollWheel")*-1) * zoomSpeed;
            aimFOV = Mathf.Min(aimFOV, aimFOVMin);
            aimFOV = Mathf.Max(aimFOV, aimFOVMax);
            fpsCam.fieldOfView = aimFOV;
            camController.sensitivity = 1f;
        }
        else
        {
            crosshairManager.crosshairMaxSize = originalCrosshairMaxSize;
            crosshairManager.crosshairMinSize = originalCrosshairMinSize;
            scopeCanvas.enabled = false;
            fpsCam.fieldOfView = regFOV;
            EnableObjectsForAiming();
            camController.sensitivity = 5f;
            
        }

        
        
    }

    private void DisableObjectsForAiming()
    {
        for (int i = 0; i < objectsToDisable.Length; i ++)
        {
            objectsToDisable[i].SetActive(false);
        }
    }
    private void EnableObjectsForAiming()
    {
        for (int i = 0; i < objectsToDisable.Length; i++)
        {
            objectsToDisable[i].SetActive(true);
        }
    }
}
