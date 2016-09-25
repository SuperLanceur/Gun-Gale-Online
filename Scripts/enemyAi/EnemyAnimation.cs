using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour
{
    public float ignoreAngle = 5f;
    public float speedDampTime = 0.1f;
    public float rotateDampTime = 0.7f;
    public float angleResponseTime = 0.6f;
    public GameObject player;
    public float speed = 2f;

    private Animator enemyAnim;
    private NavMeshAgent nav;
    private PlayerHealth pHealth;
    private EnemySightHearing enemySight;
   

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        
        pHealth = player.GetComponent<PlayerHealth>();
        enemySight = GetComponent<EnemySightHearing>();


        


        enemyAnim.SetLayerWeight(1, 1f);
        enemyAnim.SetLayerWeight(2, 1f);

        ignoreAngle *= Mathf.Deg2Rad;
    }

    private void Update()
    {
        SetMovement();
    }
    /*
    private void OnAnimatorMove()
    {
        nav.velocity = enemyAnim.deltaPosition / Time.deltaTime;
        transform.rotation = enemyAnim.rootRotation;
    }
    */
    private void SetMovement()
    {
        float speed;

        float angle;
        if (enemySight.bInSight)

        {
            speed = 0f;
            Vector3 directionToPlayer = player.transform.position - transform.position;
            angle = CalcurateAngle(transform.position, directionToPlayer, transform.up);
        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = CalcurateAngle(transform.position, nav.desiredVelocity, transform.up);

            if (Mathf.Abs(angle) < ignoreAngle)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0f;
            }
        }

        float angularSpeed = angle / angleResponseTime;
        enemyAnim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
        //enemyAnim.SetFloat("AngularSpeed", angularSpeed, rotateDampTime, Time.deltaTime);

    }

    private float CalcurateAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if (toVector == Vector3.zero)
            return 0f;

        Vector3 normal = Vector3.Cross(fromVector, toVector);
        float angleToTarget = Vector3.Angle(fromVector, toVector);
        angleToTarget *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angleToTarget *= Mathf.Deg2Rad;
        return angleToTarget;
    }
}
