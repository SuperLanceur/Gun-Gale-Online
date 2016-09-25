using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float dyingTime = 10f;
    public Slider UISlider;
    public Image UISliderImage;
    public GameObject bullet;
    public Animator anim;
    public SkinnedMeshRenderer[] skinnedMesh;
    public ParticleSystem deathEffect;
    public AudioClip deathAudio;
    public float materialChangeSpeed = 10f;
    public RetryMenu retryMenu;
    public GameObject gameController;
    public float turnDownMusicSpeed = 2f;
    public Image bloodEffect;
    public Image damageUI;
    public Color startColor;
    public Color endColor;
    public float colorFadeSpeed = 10f;
    public AudioClip playerHitAudio;

    private BulletManager bulletManager;
    private FPS_Movement fps_Movement;
    private PlayerShooting playerShooting;
    private Color minColor = Color.red;
    private bool bDead;
    private bool bDying;
    private float deadTimer = 0f;
    private int count = 0;
    private float previousHealth;
    private float damageSpeed = 60f;
    private AudioSource backgroundMusic;
    private Material newM;
    private bool bDeathAudioPlay;
    private Color originalBloodColor;
    private Color originalRedUIColor;

    private void Awake()
    {
        bDead = false;
        bDying = false;
        bDeathAudioPlay = false;
        previousHealth = health;
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("bDead", bDead);
        deadTimer = 0f;
        UISlider.value = health;
        bulletManager = bullet.GetComponent<BulletManager>();
        fps_Movement = GetComponent<FPS_Movement>();
        playerShooting = GetComponent<PlayerShooting>();
        backgroundMusic = gameController.GetComponent<AudioSource>();
        if (skinnedMesh[0].enabled == false)
        {
            for (int i = 0; i < skinnedMesh.Length; i++)
            {
                skinnedMesh[i].enabled = true;
            }
        }

        UnableDamageUI();
        originalBloodColor = bloodEffect.color;
        originalRedUIColor = damageUI.color;
    }

    private void Update()
    {
        if (bDead)
        {
            fps_Movement.enabled = false;
            playerShooting.enabled = false;
            deadTimer += Time.deltaTime;

            if (!bDying)
            {
                for (int i = 0; i < skinnedMesh.Length; i++)
                {
                    skinnedMesh[i].material.Lerp(skinnedMesh[i].material, newM, materialChangeSpeed * Time.deltaTime);
                }
                bDying = true;
                deathEffect.transform.parent = null;
                deathEffect.Play();
                //Destroy(deathEffect.gameObject, deathEffect.duration);
            }

            
            if (bDead && deadTimer >= dyingTime)
            {
                
                //Destroy(gameObject);
                for (int i = 0; i < skinnedMesh.Length; i++)
                {
                    skinnedMesh[i].enabled = false;
                }

                backgroundMusic.volume = Mathf.Lerp(backgroundMusic.volume, 0f, turnDownMusicSpeed * Time.deltaTime);
                if (deadTimer >= dyingTime + 3f)
                {
                    backgroundMusic.Stop();
                    RetryCanvas();
                    bDead = false;
                    dyingTime = 0f;
                }
            }
        }

        if (previousHealth != health)
        {
            damageUIEffect();
        }
    }

    public void TakeDamage(float damage)
    {
        //previousHealth = health;
        health -= damage;
        SetUISlider();
        AudioSource.PlayClipAtPoint(playerHitAudio, transform.position);
        if (health <= 0f && !bDead)
        {
            bDead = true;
            //anim.SetLayerWeight(1, 0f);
            //anim.SetLayerWeight(2, 0f);
            anim.SetBool("bDead", true);
            if (!bDeathAudioPlay)
            {
                AudioSource.PlayClipAtPoint(deathAudio, transform.position);
                bDeathAudioPlay = true;
            }

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {

            BulletManager bulletManager = other.GetComponent<BulletManager>();
            TakeDamage(bulletManager.damage);
            bulletManager.bHit = true;
        }

    }
    */
    private void RetryCanvas()
    {
        retryMenu.Pause();
        retryMenu.bGameOver = true;
    }

    private void damageUIEffect()
    {
        EnableDamageUI();
        //bloodEffect.color = startColor;
        bloodEffect.color = Color.Lerp(bloodEffect.color, endColor, colorFadeSpeed * Time.deltaTime);
        //damageUI.color = new Color(255, 0, 0, 60);
        damageUI.color = Color.Lerp(damageUI.color, endColor, colorFadeSpeed* Time.deltaTime);
        if (damageUI.color == endColor)
        {
            previousHealth = health;
            bloodEffect.color = originalBloodColor;
            damageUI.color = originalRedUIColor;
            UnableDamageUI();
        }
    }

    private void EnableDamageUI()
    {
        
        bloodEffect.enabled = true;
        damageUI.enabled = true;
    }

    private void UnableDamageUI()
    {
        //bloodEffect.color = new Color(255, 0, 0, 255);
        //damageUI.color = new Color(255, 0, 0, 45);
        bloodEffect.enabled = false;
        damageUI.enabled = false;
    }
}
