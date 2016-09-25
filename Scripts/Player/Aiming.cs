using UnityEngine;
using System.Collections;

public class Aiming : MonoBehaviour
{
    public Transform bulletPoint;

    
    private Transform pivot;

    private void Awake()
    {
        
        pivot = transform.parent.parent.transform;
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Fire2"))
        {
            transform.position = bulletPoint.position;
            transform.rotation = bulletPoint.rotation;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            transform.position = pivot.position;
            transform.rotation = pivot.rotation;
        }
    }
}
