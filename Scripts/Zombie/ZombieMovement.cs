using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour
{
    private GameObject player;

    private NavMeshAgent nav;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        
    }
	
    private void Update()
    {
        nav.SetDestination(player.transform.position);
    }
}
