using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float minDistanceToPlayer = 10f;
    public float waitTime = 3f;

    private EnemySightHearing enemySense;
    private PlayerHealth pHealth;
    private NavMeshAgent nav;
    private int patrolPointIndex;
    private float patrolTimer;
    private float distanceToPlayer;
    private Rigidbody rb;
    private Animator enemyAnim;
    
    private void Awake()
    {
        enemySense = GetComponent<EnemySightHearing>();
        pHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<Animator>();
        patrolTimer = 0f;
        distanceToPlayer = 0f;
    }

    private void Update()
    {
        if (enemySense.position != enemySense.resetPosition && pHealth.health > 0f)
        {
            Chase();           
        }
        else if (enemySense.bHeard && pHealth.health > 0f)
        {

            LookAtPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        nav.speed = patrolSpeed;
        if (nav.destination == enemySense.resetPosition || nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= waitTime)
            {
                patrolPointIndex = Random.Range(0, patrolPoints.Length-1);
                nav.SetDestination(patrolPoints[patrolPointIndex].position);
                patrolTimer = 0f;
            }
        }
        else
        {
            patrolTimer = 0f;
        }
        enemyAnim.SetFloat("Speed", nav.speed);
    }

    private void LookAtPlayer()
    {
        nav.speed = 0f;
        nav.SetDestination(transform.position);
        transform.LookAt(player.transform.position);
    }

    private void Chase()
    {
        nav.speed = chaseSpeed;
        distanceToPlayer = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));
        if (distanceToPlayer <= minDistanceToPlayer)
        {
            LookAtPlayer();
        }
        else
        {
            nav.SetDestination(player.transform.position);
            
        }
    }
	
}
