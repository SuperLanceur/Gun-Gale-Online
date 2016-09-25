using UnityEngine;
using System.Collections;

public class ZombieAnimation : MonoBehaviour
{
    public AudioClip screamAudio;
    public AudioClip hitAudio;
    public AudioClip attackAudio;

    private SphereCollider sphereColider;
    private ZombieController zombieController;
    private Animator anim;
    private EnemyAI enemyAI;
    private int screamCount;
    private int hashAttack = Animator.StringToHash("Base Layer.Attack");
    private bool bAttacking;

    private void Awake()
    {
        zombieController = GetComponent<ZombieController>();
        anim = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        sphereColider = GetComponentInChildren<SphereCollider>();
        anim.SetBool("bChase", false);
        screamCount = 0;
        bAttacking = false;
        sphereColider.enabled = false;
    }

    private void Update()
    {
        if (zombieController.bAttack)
        {
            anim.SetTrigger("tAttack");
           if (anim.GetCurrentAnimatorStateInfo(0).nameHash == hashAttack)
            {
                
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f && !bAttacking )
                {
                    sphereColider.enabled = true;
                    AudioSource.PlayClipAtPoint(attackAudio, transform.position);
                    bAttacking = true;
                }
                else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.3f && bAttacking)
                {
                    sphereColider.enabled = false;
                    bAttacking = false;
                }
            }
            
        }
        else if (zombieController.bScream)
        {
            if (zombieController.bScream && screamCount == 0)
            {
                screamCount += 1;
                anim.SetTrigger("tScream");
                AudioSource.PlayClipAtPoint(screamAudio, transform.position);
            }
        }
        else if (!zombieController.bChase)
        {
            sphereColider.enabled = false;
            screamCount = 0;
            anim.SetBool("bChase", false);
        }

        if (zombieController.bChase)
        {
            
            anim.SetBool("bChase", true);
        }

        

        if (enemyAI.health <= 0f)
        {
            anim.SetTrigger("tDead");
        }

        if (zombieController.bHit)
        {
            anim.SetTrigger("tHit");
            AudioSource.PlayClipAtPoint(hitAudio, transform.position);
            zombieController.bHit = false;
        }
    }
}
