using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public float health = 100f;
    public float dyingTime = 10f;
    public Slider UISlider;
    public Image UISliderImage;
    public ParticleSystem deathEffect;
    public AudioClip deathAudio;
    public float materialChangeSpeed = 10f;

    private Color minColor = Color.red;
    private bool bDead;
    private Animator anim;
    private float deadTimer = 0f;
    private int count = 0;
    private float previousHealth;
    private float damageSpeed = 60f;
    private NavMeshAgent nav;
    private SkinnedMeshRenderer[] skinnedMesh;
    private Material newM;

    private void Awake()
    {
        bDead = false;
        anim = GetComponent<Animator>();
        anim.SetBool("bDead", false);
        deadTimer = 0f;
        UISlider.value = health;
        nav = GetComponent<NavMeshAgent>();
        skinnedMesh = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        if (bDead)
        {
            for (int i = 0; i < skinnedMesh.Length; i++)
            {
                skinnedMesh[i].material.Lerp(skinnedMesh[i].material, newM, materialChangeSpeed * Time.deltaTime);
            }
            deadTimer += Time.deltaTime;
            if (bDead && deadTimer >= (dyingTime-0.4))
            {
                deathEffect.transform.parent = null;
                deathEffect.Play();

                Destroy(deathEffect.gameObject, deathEffect.duration);
                
            }
            if (bDead && deadTimer >= dyingTime)
            {
                
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        previousHealth = health;
        health -= damage;
        SetUISlider();
        if (health <= 0f && !bDead)
        {
            bDead = true;
            if (anim.layerCount > 1)
            {
                anim.SetLayerWeight(1, 0f);
                anim.SetLayerWeight(2, 0f);
            }
            anim.SetTrigger("bDead");

            AudioSource.PlayClipAtPoint(deathAudio, transform.position);

            Color deathColor = new Color(0, 227, 233, 100);
            newM = new Material(Shader.Find("Transparent/Diffuse"));
            newM.color = deathColor;
            

        }
    }

    private void SetUISlider()
    {
        UISlider.value = Mathf.Lerp(UISlider.value, health, damageSpeed * Time.deltaTime);
        if (health <= 25f)
        {
            UISliderImage.color = minColor;
        }
    }

    /*
    private void Dying()
    {
        deadTimer += Time.deltaTime;
        bDead = true;
        nav.Stop();
        anim.SetBool("bDead", bDead);
        if (count == 0)
        {
            count = 1;
            anim.SetBool("bDying", true);

        }
        else
        {
            anim.SetBool("bDying", false);
        }
    }

    private void AfterDead()
    {
        Destroy(gameObject);
    }
    */
}
