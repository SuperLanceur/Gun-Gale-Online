using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    //public float turnSpeed = 1.5f;
    //public float turnSmoothing = 0.1f;
    //public float tiltMax = 75f;
    //public float tiltMin = 45f;

    public float sensitivity = 5f;
    public float smoothing = 2f;
    private Vector2 mouseLook;
    private Vector2 smoothV;


   // private float smoothX = 0f;
    //private float smoothY = 0f;
    //private float smoothXVelocity = 0f;
    //private float smoothYVelocity = 0f;
    //private float lookAngle;
   // private float tiltAngle;
    private Transform cam;
    private Transform pivot;
    private Rigidbody rb;
    private PlayerInput pInput;
    GameObject character;


    private void Start()
    {
        cam = Camera.main.transform;
        pivot = cam.parent.parent.transform;
        rb = GetComponentInParent<Rigidbody>();
        pInput = GetComponentInParent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
        character = this.transform.parent.gameObject;
        //lookAngle = 180f;
    }

    private void FixedUpdate()
    {
        manageCameraRotation();
    }

    private void manageCameraRotation()
    {
        float inputX =  pInput.mouseX;
        float inputY =  pInput.mouseY;

        Vector2 md = new Vector2(inputX, inputY);
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
        if (mouseLook.y > 90)
        {
            mouseLook.y = 90;
        }
        if (mouseLook.y < -90)
        {
            mouseLook.y = -90;
        }
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        pivot.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        

        

        //lookAngle += smoothX * turnSpeed;
        //tiltAngle -= smoothY * turnSpeed;
        //tiltAngle = Mathf.Clamp(tiltAngle, tiltMin, tiltMax);

        //transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);
        //pivot.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);

    }

}
