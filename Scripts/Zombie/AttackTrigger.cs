using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour
{
    private GameObject player;
    
    private PlayerHealth pHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pHealth = player.GetComponent<PlayerHealth>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pHealth.TakeDamage(10f);
        }
    }
	
}
