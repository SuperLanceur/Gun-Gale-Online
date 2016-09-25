using UnityEngine;
using System.Collections;

public class EnemySightHearing : MonoBehaviour
{
    public GameObject player;
    public float maxSightDistance = 50f;
    public float sightMaxAngle = 120f;
    public float timeTolost = 7f;
    public float maxHearing = 40f;
    public Transform raycastPoint;
    public bool bInSight;
    public bool bHeard;
    public Vector3 resetPosition;
    public Vector3 position;

    private SphereCollider sphereCollider;
    private float lostTimer;
    private float heardLookingTimer;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyAI enemyHealth;
    private Vector3 toPlayerVec;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        resetPosition = new Vector3(1000f, 1000f, 1000f);
        position = new Vector3(1000f, 1000f, 1000f);
        sphereCollider = GetComponent<SphereCollider>();
        nav = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyAI>();
        bInSight = false;
        anim.SetBool("bInSight", bInSight);
        lostTimer = 0f;
        heardLookingTimer = 0f;
        
    }

    private void Update()
    {
        //Calculate the distance between a player and an enemy 
        toPlayerVec = player.transform.position - transform.position;
        if (toPlayerVec.magnitude <= maxSightDistance)
        {
            TriggerSense();
        }
            


        if (!bInSight && position != resetPosition)
        {
            lostTimer += Time.deltaTime;
            if (lostTimer >= timeTolost)
            {
                position = resetPosition;
                bHeard = false;
                nav.destination = resetPosition;
                lostTimer = 0f;
            }
        }
        if (bHeard && !bInSight)
        {
            heardLookingTimer += Time.deltaTime;
            if (heardLookingTimer >= timeTolost)
            {
                bHeard = false;
                nav.destination = resetPosition;
                heardLookingTimer = 0f;
            }
        }
    }

    private void TriggerSense()
    {
        float angle = Mathf.Abs(Vector3.Angle(transform.forward, toPlayerVec));
        if (angle <= sightMaxAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(raycastPoint.position, toPlayerVec, out hit, maxSightDistance))
            {
                if (hit.collider.gameObject == player)
                {
                    bInSight = true;

                    position = player.transform.position;
                    bHeard = false;
                }
                else
                {
                    bInSight = false;

                }
            }
            anim.SetBool("bInSight", true);
        }
        else
        {
            anim.SetBool("bInSight", false);
        }

        if (Input.GetButton("Fire1"))
        {
            bHeard = true;
        }
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
           
            Vector3 toPlayerVec = player.transform.position - transform.position;
            float angle = Mathf.Abs(Vector3.Angle(transform.forward, toPlayerVec));
            if (angle <= sightMaxAngle* 0.5f )
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, toPlayerVec, out hit, sphereCollider.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        bInSight = true;
                        
                        position = player.transform.position;
                        bHeard = false;
                    }
                    else
                    {
                        bInSight = false;
                        
                    }
                }
                anim.SetBool("bInSight", true);
            }
            else
            {
                anim.SetBool("bInSight", false);
            }
            
            if (Input.GetButton("Fire1"))
            {
                bHeard = true;
            }
        }
    }
	*/
}
