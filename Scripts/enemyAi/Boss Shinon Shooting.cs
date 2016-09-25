using UnityEngine;
using System.Collections;

public class BossShinonShooting : MonoBehaviour
{
    public Rigidbody bulletInstance;
    public GameObject bullet;
    public float bulletSpeed = 30f;
    public AudioClip shootAudio;
    public float range = 100f;
    public Transform muzzle;
    public GameObject player;
    public float shootInterval = 3.5f;

    

    private EnemySightHearing enemySense;
    private LineRenderer shootLine;
    private WeaponSetting weaponSetting;
    private BulletManager bulletManager;
    private bool bShooting;
    private float timer;


    private void Awake()
    {
        enemySense = GetComponent<EnemySightHearing>();
        shootLine = GetComponent<LineRenderer>();
        bulletManager = bullet.GetComponent<BulletManager>();
        bShooting = false;
    }

    private void Update()
    {
        Vector3 vecToPlayer = (player.transform.position + Vector3.up * 1.5f) - muzzle.position;
        Quaternion lookAtPlayer = Quaternion.LookRotation(vecToPlayer);
        muzzle.rotation = lookAtPlayer;
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

        Vector3 vecToPlayer = (player.transform.position + Vector3.up * 1.5f) - muzzle.position;
        Quaternion lookAtPlayer = Quaternion.LookRotation(vecToPlayer);
        muzzle.rotation = lookAtPlayer;
        shootLine.enabled = true;
        shootLine.SetPosition(0, muzzle.position);
        shootLine.SetPosition(1, muzzle.position + muzzle.forward * range);

        Rigidbody bullet = Instantiate(bulletInstance, muzzle.position, muzzle.rotation) as Rigidbody;
        bullet.velocity = bulletSpeed * muzzle.forward;
        AudioSource.PlayClipAtPoint(shootAudio, muzzle.position);
    }

}
