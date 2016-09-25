using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    public float mouseX;
    public float mouseY;
    public float horizontal;
    public float verical;
    public bool fire1;
    public bool bRun;
    public bool fire3;
    

    private void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        horizontal = Input.GetAxis("Horizontal");
        verical = Input.GetAxis("Vertical");
        fire1 = Input.GetButton("Fire1");
        bRun = Input.GetButton("Run");
        fire3 = Input.GetButton("Fire3");
    }


}
