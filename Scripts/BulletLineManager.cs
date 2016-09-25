using UnityEngine;
using System.Collections;

public class BulletLineManager : MonoBehaviour
{
    
    public float range = 100;

    private LineRenderer bulletLine;

    private void Awake()
    {
        bulletLine = GetComponent<LineRenderer>();
        bulletLine.enabled = true;
        bulletLine.SetPosition(0, transform.position);
        bulletLine.SetPosition(1, transform.position + transform.forward * range);
    }

    private void Update()
    {
        bulletLine.SetPosition(0, transform.position);
    }
	
}
