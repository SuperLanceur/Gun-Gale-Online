using UnityEngine;
using System.Collections;

public class AnimatorSetup : MonoBehaviour
{
    private PlayerInput pInput;
    private Animator anim;
    
    
    private float timer;

    private void Awake()
    {
        pInput = GetComponentInParent<PlayerInput>();
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            timer += Time.deltaTime;
            if (Input.GetButtonDown("Horizontal") && timer < 0.3f && timer > 0.01f && pInput.horizontal > 0f)
            {
                
                anim.SetTrigger("tDodgeRight");
                timer = 0f;
            }
            if (Input.GetButtonDown("Horizontal") && timer < 0.3f && timer > 0.01f && pInput.horizontal < 0f)
            {
                anim.SetTrigger("tDodgeLeft");
                timer = 0f;
            }
            
        }

        if (timer > 0.3F)
        {
            timer = 0f;
        }

        if (pInput.horizontal != 0f || pInput.verical != 0f)
        {
            anim.SetBool("bWalking", true);
            anim.SetFloat("Speed", pInput.verical);
            anim.SetFloat("Horizontal", pInput.horizontal);
            if (pInput.bRun)
            {
                anim.SetBool("bRunning", true);
            }
            else
            {
                anim.SetBool("bRunning", false);
            }
        }
        else
        {
            anim.SetBool("bWalking", false);
        }
    }
}
