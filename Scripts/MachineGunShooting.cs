using UnityEngine;
using System.Collections;

public class MachineGunShooting : MonoBehaviour
{
    public Rigidbody bulletInstance;
    public GameObject bullet;
    public float bulletSpeed = 30f;
    public LineRenderer bulletLineInstance;
    public AudioClip shootAudio;
    public float range = 100f;
    public Transform muzzle; 
    public float shootInterval = 3.5f;

    private GameObject player;
    private EnemySightHearing enemySense;
    private LineRenderer shootLine;
    private WeaponSetting weaponSetting;
    private BulletManager bulletManager;
    private bool bShooting;
    private float timer;
    private ShinonCrosshairManager crosshairManager;

    private void Awake()
    {
        enemySense = GetComponent<EnemySightHearing>();
        player = GameObject.FindGameObjectWithTag("Player");
        shootLine = GetComponent<LineRenderer>();
        bulletManager = bullet.GetComponent<BulletManager>();
        crosshairManager = GetComponentInChildren<ShinonCrosshairManager>();
        bShooting = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (enemySense.bInSight && timer > shootInterval)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        timer = 0f;
        bShooting = true;
        weaponSetting = GetComponentInChildren<WeaponSetting>();
        bulletSpeed = weaponSetting.bulletSpeed;
        bulletManager.damage = weaponSetting.maxDamage;

        muzzle.LookAt(crosshairManager.raycastPoint);

        //LineRenderer bulletLine = Instantiate(bulletLineInstance, muzzle.position, muzzle.rotation) as LineRenderer;
        //bulletLine.SetPosition(0, muzzle.position);
        //bulletLine.SetPosition(1, muzzle.position + muzzle.forward * range);
        //shootLine.enabled = true;
        //shootLine.SetPosition(0, muzzle.position);
        //shootLine.SetPosition(1, muzzle.position + muzzle.forward * range);

        Rigidbody bullet = Instantiate(bulletInstance, muzzle.position, muzzle.rotation) as Rigidbody;
        bullet.velocity = bulletSpeed * muzzle.forward;
        AudioSource.PlayClipAtPoint(shootAudio, muzzle.position);
    }

}
