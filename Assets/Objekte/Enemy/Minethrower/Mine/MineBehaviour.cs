﻿using UnityEngine;
using System.Collections;
using System;

public class MineBehaviour : MonoBehaviour, Hitable {

    float timer;

    public void OnTriggerEnter2D(Collider2D coll)
    {
        onDestruction();
        if (coll.gameObject.GetComponent<PlayerController_Base>() != null)
            coll.gameObject.GetComponent<Hitable>().dealDamage(2);
    }

    public void onHit()
    {
        onDestruction();
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerController_Base player = FindObjectOfType<PlayerController_Base>();
        if (player != null)
        {
            Vector2 force = (transform.position - Vector3.Lerp(transform.position, player.transform.position, 1))*0.05f;
            player.gameObject.GetComponent<Rigidbody2D>().velocity += force;
            //player.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.position - Vector3.Lerp(transform.position, player.transform.position, 1000));
        }
        transform.Rotate(0, 0, 2);
        timer++;
        if (timer % 50 == 0)
            GetComponent<AudioSource>().Play();
        if (timer > 150)
            onDestruction();
    }

    void onDestruction()
    {
        Explosion.explode(gameObject);
        Destroy(gameObject);
    }

    public void dealDamage(int damage)
    {
        onDestruction();
    }
}
