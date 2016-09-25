using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public Rigidbody bulletInstance;
    public GameObject bullet;
    public float bulletSpeed = 30f;
    public Transform bulletPoint;
    public AudioClip shootAudio;
    public float range = 100f;
    public GameObject player;

    private Animator anim;
    private bool bShooting;
    private RaycastHit hit;
    private int layerMask;
    private LineRenderer shootLine;
    private WeaponSetting weaponSetting;
    private BulletManager bulletManager;
    private int environmentLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bShooting = false;
        layerMask = LayerMask.GetMask("Player");
        shootLine = GetComponent<LineRenderer>();
        bulletManager = bullet.GetComponent<BulletManager>();
        environmentLayer = LayerMask.GetMask("Environment");
    }

    private void Update()
    {
        float shot = anim.GetFloat("Shot");

        if (shot >= 0.5f && !bShooting)
        {
            Shoot();
        }
        if (shot < 0.5)
        {
            bShooting = false;
            //shootLine.enabled = false;
        }
    }

    private void Shoot()
    {
        bShooting = true;
        weaponSetting = GetComponentInChildren<WeaponSetting>();
        bulletSpeed = weaponSetting.bulletSpeed;
        bulletManager.damage = weaponSetting.maxDamage;

        Vector3 vecToPlayer = (player.transform.position + Vector3.up*1.5f) - bulletPoint.position;
        Quaternion lookAtPlayer = Quaternion.LookRotation(vecToPlayer);
        bulletPoint.rotation = lookAtPlayer;
        shootLine.enabled = true;
        shootLine.SetPosition(0, bulletPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(bulletPoint.position, bulletPoint.forward, out hit, range, environmentLayer))
        {
            shootLine.SetPosition(1, hit.point);
        }
        else
        {
            shootLine.SetPosition(1, bulletPoint.position + bulletPoint.forward * range);
        }
        
        
        Rigidbody bullet = Instantiate(bulletInstance, bulletPoint.position, bulletPoint.rotation) as Rigidbody;
        bullet.velocity = bulletSpeed * bulletPoint.forward;
        AudioSource.PlayClipAtPoint(shootAudio, bulletPoint.position);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        float aimWeight = anim.GetFloat("AimWeight");
        anim.SetIKPosition(AvatarIKGoal.RightHand, player.transform.position + Vector3.up * 1.5f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);

    }

}
