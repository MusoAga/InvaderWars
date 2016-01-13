using UnityEngine;
using System.Collections;

public class Enemy_FrostGrenadier : EnemyController_Base {

	// Use this for initialization
	void Start () {
        lifepoints = 3;
        resources = 45;
	}

    void dropGrenade()
    {
        charge = 0;
        GetComponent<Animator>().Play("FrostGrenadier_Shoot");
        for (int i = 0; i < 3; i++)
        {
            GameObject grenade = Instantiate(shot);
            grenade.transform.position = transform.position;
            grenade.GetComponent<ShotBehaviour>().shoot(gameObject);
            grenade.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(i*2)*50, Mathf.Cos(i*2) * 50));
            GetComponent<AudioSource>().PlayOneShot(shootSound);
        }
    }

    

    public override void enemyBehaviour()
    {
        base.enemyBehaviour();
        charge++;
        if (charge >= 250)
            dropGrenade();
    }

}
