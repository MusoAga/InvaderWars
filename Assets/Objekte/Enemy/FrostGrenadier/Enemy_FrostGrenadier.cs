using UnityEngine;
using System.Collections;

public class Enemy_FrostGrenadier : EnemyController_Base {

	// Use this for initialization
	void Start () {
        lifepoints = 3;
	}

    void dropGrenade()
    {
        charge = 0;
        for (int i = 0; i < 6; i++)
        {
            GameObject grenade = Instantiate(shot);
            grenade.transform.position = transform.position;
            grenade.GetComponent<ShotBehaviour>().shoot(gameObject);
            grenade.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(i)*50, Mathf.Cos(i) * 50));
            GetComponent<AudioSource>().PlayOneShot(shootSound);
        }
    }

    

    public override void enemyBehaviour()
    {
        base.enemyBehaviour();
        charge++;
        if (charge >= 150)
            dropGrenade();
    }

}
