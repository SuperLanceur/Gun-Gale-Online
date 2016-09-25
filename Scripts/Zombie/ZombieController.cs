using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour
{
    public float chaseSpeed = 2f;
    public float walkSpeed = 1f;
    public float chaseRange = 10f;
    public float attackRange = 1f;  
    public bool bDead;
    public bool bChase;
    public bool bAttack;
    public bool bScream;
    public bool bHit;

    private GameObject player;
    private Vector3 vecToPlayer;
    private NavMeshAgent nav;
    private Animator anim;
    private int hashScream = Animator.StringToHash("Base Layer.Scream");

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.speed = walkSpeed;
        bDead = false;
        bChase = false;
        bAttack = false;
        bScream = false;
        bHit = false;
        hashScream = Animator.StringToHash("Base Layer.Scream");
        
    }

    private void Update()
    {
        vecToPlayer = player.transform.position - transform.position;
        if (vecToPlayer.magnitude < attackRange)
        {
            nav.speed = 0f;
            bAttack = true;
        }
        else if (vecToPlayer.magnitude < chaseRange && vecToPlayer.magnitude > attackRange && !bScream)
        {
            bAttack = false;
            
            //bChase = false;
            bScream = true;
            nav.speed = 0f;
            //bChase = true;
            //nav.speed = chaseSpeed;
        }
        else if (vecToPlayer.magnitude < chaseRange && vecToPlayer.magnitude > attackRange && anim.GetCurrentAnimatorStateInfo(0).nameHash == hashScream)
        {
            
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95  /*!anim.IsInTransition(0)*/)
            {
                bChase = true;
                nav.speed = chaseSpeed;
            }
        }
        else if (vecToPlayer.magnitude < chaseRange && vecToPlayer.magnitude > attackRange && bChase)
        {
            bAttack = false;
            nav.speed = chaseSpeed;
        }
        else if (vecToPlayer.magnitude > chaseRange)
        {
            bAttack = false;
            bChase = false;
            bScream = false;
            nav.speed = walkSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Bullet")
        {
            bHit = true;
        }
    }
}
