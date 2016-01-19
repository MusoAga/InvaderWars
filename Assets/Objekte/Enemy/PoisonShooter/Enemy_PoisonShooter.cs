using UnityEngine;
using System.Collections;

public class Enemy_PoisonShooter : EnemyController_Base {

    void Start()
    {
        charge = 180;
    }

    public override void shoot()
    {
        // Sound abspielen
        charge = 0;
        GetComponent<AudioSource>().PlayOneShot(shootSound);
        GameObject fire = (GameObject)Instantiate(shot, transform.localPosition + transform.up / 2, transform.localRotation);
        fire.GetComponent<Rigidbody2D>().AddForce(transform.up * 400);
        fire.GetComponent<ShotBehaviour>().shoot(gameObject);
    }

    public override void enemyBehaviour()
    {
        base.enemyBehaviour();
        charge++;
        if (charge >= 200)
            shoot();
    }
}
