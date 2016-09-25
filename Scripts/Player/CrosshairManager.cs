using UnityEngine;
using System.Collections;

public class CrosshairManager : MonoBehaviour
{
    public Camera cam;
    public Transform bulletPoint;
    public float expandTime = 2f;
    public float crosshairMaxSize;
    public float crosshairMinSize;
    public Vector3 originalScale;
    public Transform center;
    public Transform raycastPoint;
    public HeartRateManager heartRateManager;

    private RaycastHit hit;
    private float distance;
    private float rotator;
    private bool increasing;
    private float centerRotation;
    private float heartRate;

    private float maxSize;
    private float minSize;
    private float expandSpeed;

    private void Start()
    {
        originalScale = transform.localScale;
        rotator = 0f;
        increasing = true;
        expandSpeed = expandTime;
        maxSize = crosshairMaxSize;
        minSize = crosshairMinSize;
        heartRate = float.Parse(heartRateManager.heartRate.text);
    }

    private void Update()
    {
        //heartRate = float.Parse(heartRateManager.heartRate.text);
        //maxSize =  crosshairMaxSize * (heartRate / 75f);
        //minSize =  crosshairMinSize * (heartRate / 75f);
        //expandSpeed =  expandTime * (heartRate / 75);
        if (originalScale == Vector3.one * minSize)
        {
            heartRate = float.Parse(heartRateManager.heartRate.text);
            maxSize = crosshairMaxSize * (heartRate / 75f);
            expandSpeed = expandTime * (heartRate / 75);
            increasing = true;
        }
        if (increasing)
        {
            originalScale = Vector3.Lerp(originalScale, Vector3.one * maxSize, expandSpeed * Time.deltaTime);
        }
        if (originalScale == Vector3.one * maxSize)
        {
            heartRate = float.Parse(heartRateManager.heartRate.text);
            minSize = crosshairMinSize * (heartRate / 75f);
            expandSpeed = expandTime * (heartRate / 75);
            increasing = false;
        }
        if(!increasing)
        {
            originalScale = Vector3.Lerp(originalScale, Vector3.one * minSize, expandSpeed * Time.deltaTime);
        }
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            distance = hit.distance;
        }
        else
        {
            distance = 100f;
            //distance *= 0.95f;
        }

        transform.LookAt(cam.transform.position);
        transform.Rotate(0f, 180f, 0f);
        transform.position = cam.transform.position + cam.transform.forward * distance;
        transform.localScale = originalScale * distance;
        centerRotation = Random.Range(0f, 360f);
        center.transform.localRotation = Quaternion.Euler(0f, 0f, centerRotation);
        Vector3 raycastPosition = raycastPoint.localPosition;
        raycastPosition.x = Random.Range(0f, 0.5f);

        raycastPoint.transform.localPosition = raycastPosition;
    }
	
}
