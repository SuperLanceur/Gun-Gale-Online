using UnityEngine;
using System.Collections;

public class ShinonCrosshairManager : MonoBehaviour
{
    public float expandTime = 2f;
    public float crosshairMaxSize;
    public float crosshairMinSize;
    public Vector3 originalScale;
    public Transform center;
    public Transform raycastPoint;
    public Transform muzzle;
    public Transform shinon;

    private RaycastHit hit;
    private float distance;
    private float rotator;
    private bool increasing;
    private float centerRotation;
    private GameObject player;
    private EnemySightHearing enemySense;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemySense = GetComponentInParent<EnemySightHearing>();
        originalScale = transform.localScale;
        rotator = 0f;
        increasing = true;
    }

    private void Update()
    {
        if (originalScale == Vector3.one * crosshairMinSize)
        {
            increasing = true;
        }
        if (increasing)
        {
            originalScale = Vector3.Lerp(originalScale, Vector3.one * crosshairMaxSize, expandTime * Time.deltaTime);
        }
        if (originalScale == Vector3.one * crosshairMaxSize)
        {
            increasing = false;
        }
        if (!increasing)
        {
            originalScale = Vector3.Lerp(originalScale, Vector3.one * crosshairMinSize, expandTime * Time.deltaTime);
        }


        transform.LookAt(shinon.position);
        transform.Rotate(0f, 180f, 0f);
        transform.position = player.transform.position + Vector3.up* 1.3f;
        //transform.localScale = originalScale * distance;
        centerRotation = Random.Range(0f, 360f);
        center.transform.localRotation = Quaternion.Euler(0f, 0f, centerRotation);
        Vector3 raycastPosition = raycastPoint.localPosition;
        raycastPosition.x = Random.Range(0f, 0.9f);

        raycastPoint.transform.localPosition = raycastPosition;
    }

}
