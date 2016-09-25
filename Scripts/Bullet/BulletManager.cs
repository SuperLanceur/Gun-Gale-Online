using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    public float damage = 10f;
    public ParticleSystem[] hitParticles;
    public float destroyTime = 3f;
    public bool bHit;
    

    private PlayerHealth playerHealth;
    private EnemyAI enemyHealth;
    
    private void Start()
    {
        
        Destroy(gameObject, destroyTime);

    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        /*
        bloodP.transform.parent = null;
        bloodP.Play();
        Destroy(bloodP.gameObject, bloodP.duration);
        */

        if (other.gameObject.tag == "Player" )
        {
            
            hitParticles[0].transform.parent = null;
            hitParticles[0].Play();
            Destroy(hitParticles[0].gameObject, hitParticles[0].duration);
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            
            hitParticles[0].transform.parent = null;
            hitParticles[0].Play();
            Destroy(hitParticles[0].gameObject, hitParticles[0].duration);
            enemyHealth = other.GetComponent<EnemyAI>();
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        { 
            hitParticles[1].transform.parent = null;
            hitParticles[1].Play();
            Destroy(hitParticles[1].gameObject, hitParticles[1].duration);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    
    
    /*
    private void Hit()
    { 
        
        bloodP.transform.parent = null;
        bloodP.Play();
        Destroy(bloodP.gameObject, bloodP.duration);
        
        Destroy(gameObject);
    }
	*/
}
