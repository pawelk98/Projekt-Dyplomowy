﻿using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public UIController uIController;
    protected Weapons weaponsScript;
    public float health = 100;
    public int weapon = 1;
    public Transform bulletOffset;
    public AudioSource gun;

    protected float lastShot;
    protected float currentHealth;
    private Color newColor;
    private bool isDead = false;

    protected virtual void Start()
    {
        uIController = GameObject.Find("GameController").GetComponent<UIController>();
        weaponsScript = GameObject.Find("GameController").GetComponent<Weapons>();

        currentHealth = health;
        lastShot = Time.time - lastShot;
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate() 
    {
        
    }

    public void dealDamage(float damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                isDead = true;
                kill();
            }
        }
    }

    protected virtual void kill()
    {
        Destroy(gameObject);
    }

    protected virtual void shoot(Vector3 direction)
    {
        if(gun != null)
        {
            gun.Play();
        }

        float recoil = weaponsScript.getRecoil(weapon);

        switch (weapon)
        {
            case 4:
                singleShot(Quaternion.AngleAxis(Random.Range(-recoil, recoil), Vector3.up) * direction);
                singleShot(Quaternion.AngleAxis(-15f + Random.Range(-recoil, recoil), Vector3.up) * direction);
                singleShot(Quaternion.AngleAxis(-7.5f + Random.Range(-recoil, recoil), Vector3.up) * direction);
                singleShot(Quaternion.AngleAxis(7.5f + Random.Range(-recoil, recoil), Vector3.up) * direction);
                singleShot(Quaternion.AngleAxis(15f + Random.Range(-recoil, recoil), Vector3.up) * direction);
                break;

            default:
                singleShot(Quaternion.AngleAxis(Random.Range(-recoil, recoil), Vector3.up) * direction);
                break;
        }

        lastShot = Time.time;
    }

    private void singleShot(Vector3 direction)
    {
        GameObject bullet = Instantiate(weaponsScript.bulletPrefabs[weapon]);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        BulletScript bulletSc = bullet.GetComponent<BulletScript>();

        bulletSc.setValues(weaponsScript.getDamage(weapon), weaponsScript.getLifeLength(weapon), gameObject.tag);

        bullet.transform.position = bulletOffset.position;
        float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
        
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        bulletRb.velocity = direction * weaponsScript.getBulletSpeed(weapon);
    }
}
