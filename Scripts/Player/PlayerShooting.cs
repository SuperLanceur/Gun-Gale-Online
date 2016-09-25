using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{

    public Rigidbody bulletInstance;
    public Transform bulletPoint;
    public AudioClip shootAudio;
    public AudioClip outOfAmmoAudio;
    public float shootInterval = 0.15f;
    public GameObject bullet;
    public bool bShooting;

    private float bulletSpeed = 30f;
    private CrosshairManager crosshairManager;
    private RaycastHit hit;
    private float timer;
    private ChangeWeapon changeWeapon;
    private WeaponSetting weaponSetting;
    private BulletManager bulletManager;
    

    private void Awake()
    {
        crosshairManager = GetComponentInChildren<CrosshairManager>();
        changeWeapon = GetComponent<ChangeWeapon>();
        bulletManager = bullet.GetComponent<BulletManager>();
        weaponSetting = changeWeapon.guns[changeWeapon.arrayIndex].GetComponent<WeaponSetting>();
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        weaponSetting = changeWeapon.guns[changeWeapon.arrayIndex].GetComponent<WeaponSetting>();
        if (Input.GetButton("Fire1") && timer >= shootInterval && weaponSetting.bulletNum > 0)
        {
            Shoot();
        }
        
        if (Input.GetButton("Fire1") && timer >= shootInterval && weaponSetting.bulletNum <= 0)
        {
            timer = 0f;
            AudioSource.PlayClipAtPoint(outOfAmmoAudio, bulletPoint.position);
        }
    }

    private void Shoot()
    {
        bShooting = true;

        bulletSpeed = weaponSetting.bulletSpeed;
        bulletManager.damage = weaponSetting.maxDamage;
        weaponSetting.bulletNum -= 1;
        changeWeapon.bulletNumUI.text = weaponSetting.bulletNum.ToString();

        bulletPoint.transform.LookAt(crosshairManager.raycastPoint);
        timer = 0f;
        Rigidbody bullet = Instantiate(bulletInstance, bulletPoint.position, bulletPoint.rotation) as Rigidbody;
        bullet.velocity = bulletSpeed * bulletPoint.forward;
        AudioSource.PlayClipAtPoint(shootAudio, bulletPoint.position);
    }
}
