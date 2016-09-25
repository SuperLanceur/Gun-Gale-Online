using UnityEngine;
using System.Collections;

public class SniperMovement : MonoBehaviour
{
    private GameObject player;
    private Vector3 lookAtVect;
    private NavMeshAgent nav;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        nav.baseOffset = 0;
        nav.height = 0.5f;
    }

    private void Update()
    {
        lookAtVect = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(lookAtVect);
    }
	
}
