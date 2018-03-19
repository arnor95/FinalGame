using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public bool isFiring;

    public Bullet bullet;
    public float bulletSpeed;

    public float cooldown;
    public float timer;

    public Transform firePoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            if (timer <= 0)
            {
                timer = cooldown;
                Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
                newBullet.speed = bulletSpeed;
            }
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
