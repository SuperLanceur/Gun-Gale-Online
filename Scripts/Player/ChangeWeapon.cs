using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour {

    public GameObject[] guns;
    public int arrayIndex;
    public float materialChangeSpeed = 10f;
    public float destroyTime = 2f;
    public Image gunImageUI;
    public Text bulletNumUI;

    private MeshRenderer[] meshRenderer;
    private PlayerHealth pHealth;
    private Material newM;
    private float destroyTimer;
    private Sprite currentWeaponUI;
    private WeaponSetting weaponSetting;

    private void Awake()
    {
        
        SetWeapon(0);
        pHealth = GetComponent<PlayerHealth>();
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetWeapon(3);
        }
        if (pHealth.health <= 0f)
        {
            if (guns[arrayIndex].GetComponent<MeshRenderer>() != null)
            {
                //meshRenderer = guns[arrayIndex].GetComponent<MeshRenderer>();
                meshRenderer = guns[arrayIndex].GetComponents<MeshRenderer>();
            }
            else
            {

                //meshRenderer = guns[arrayIndex].GetComponentInChildren<MeshRenderer>();
                meshRenderer = guns[arrayIndex].GetComponentsInChildren<MeshRenderer>();
                
            }
            Color deathColor = new Color(0, 227, 233, 100);
            newM = new Material(Shader.Find("Transparent/Diffuse"));
            newM.color = deathColor;
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material.Lerp(meshRenderer[i].material, newM, materialChangeSpeed * Time.deltaTime);
            }
            
            destroyTimer += Time.deltaTime;

            if (destroyTimer > destroyTime)
            {
                guns[arrayIndex].SetActive(false);
            }
        }



    }

    private void SetWeapon(int index)
    {
        arrayIndex = index;
        DisableAllWeapons();
        guns[index].SetActive(true);
        currentWeaponUI = guns[index].GetComponent<Image>().sprite;
        gunImageUI.sprite = currentWeaponUI;
        weaponSetting = guns[index].GetComponent<WeaponSetting>();
        bulletNumUI.text = weaponSetting.bulletNum.ToString();
    }

    private void DisableAllWeapons()
    {
        for (int i = 0; i < guns.Length; i ++)
        {
            guns[i].SetActive(false);
        }
    }

    private void EableAllWeapons()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(true);
        }
    }
}
