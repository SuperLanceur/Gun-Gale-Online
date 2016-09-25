using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    public GameObject player;
    private Quaternion uiRotation;

    private void Start()
    {
        if (player != null)
            uiRotation = player.transform.rotation;
    }

    private void Update()
    {
        if (player != null)
            uiRotation = player.transform.rotation;
        transform.rotation = uiRotation;
    }
}
