using UnityEngine;
using System.Collections;

public class ShootingAnimManager : MonoBehaviour
{
    public Animator gunTopAnim;
    public Animator gunAnim;
    public Animator ammoAnim;

    private ParticleSystem gunFireParticle;
    private PlayerShooting pShooting;
    private AudioSource shootSound;

    private void Awake()
    {
        pShooting = GetComponentInParent<PlayerShooting>();
        gunFireParticle = GetComponentInChildren<ParticleSystem>();
        shootSound = GetComponent<AudioSource>();
    }
	
    private void Update()
    {
        if (pShooting.bShooting)
        {
            gunFireParticle.Play();
            shootSound.Play();
            ShootAnimationSetup();
            pShooting.bShooting = false;
        }
    }

    private void ShootAnimationSetup()
    {
        gunAnim.SetTrigger("tShoot");
        gunTopAnim.SetTrigger("tShoot");
        ammoAnim.SetTrigger("tShoot");
    }
}
