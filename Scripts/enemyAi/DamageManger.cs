using UnityEngine;
using System.Collections;

public class DamageManger : MonoBehaviour
{
    public GameObject bullet;

    private EnemyAI enemyHealth;
    private BulletManager bulletManager;
   

    private void Awake()
    {
        enemyHealth = GetComponentInParent<EnemyAI>();
        bulletManager = bullet.GetComponent<BulletManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            
            BulletManager bulletManager = other.GetComponent<BulletManager>();
           
            enemyHealth.TakeDamage(bulletManager.damage);
            bulletManager.bHit = true;
        }
        
    }
	
}
