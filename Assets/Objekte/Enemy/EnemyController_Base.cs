﻿using UnityEngine;
using System.Collections;
using System;

public class EnemyController_Base : MonoBehaviour, Hitable {

    public float speed = 2; // Flug-Geschwindigkeit 
    public float lifepoints = 1; // Lebenspunkte
    public int resources = 10; // Anzahl der Ressourcen, die beim Tod abgeworfen werden
    protected float charge; // Aufladung des Schusses
    GameObject spawnOrigin = null;
    public GameObject shot;
    public AudioClip shootSound;
    private Vector3 spawnPos;


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if(!FindObjectOfType<GameController>().isPaused())
            enemyBehaviour();
        // Sobald ein Gegner das Ende des Bildschirms erreicht hat, wird er entfernt

        if (transform.position.y < InvaderWars.boundYmin(gameObject))
        {
            Destroy(gameObject);
            spawnOrigin.GetComponent<SpawnController>().enemyDied();
        }
    }

    public void setSpawnOrigin(GameObject spawn) 
    {
        this.spawnOrigin = spawn;
        spawnPos = gameObject.transform.position;
       // print(spawnPos.ToString());
    }

    public void setup(float _speed, float _lifePoints, int _resources)
    {
        //this.speed = _speed;
        this.lifepoints = _lifePoints;
        this.resources = _resources;
        print("Resources: " + resources.ToString() + "\n_Resources: " + _resources.ToString());
    }
    bool moveRight = false;
    
    public virtual void enemyBehaviour()
    {
            moveInDirection(new Vector2((Mathf.Sin(transform.position.y * 2) + (UnityEngine.Random.value >= 0-5f ? UnityEngine.Random.value / 3 : -(UnityEngine.Random.value / 2))), -0.1f - speed ));
    }

    // Feuere Schüsse ab
    public virtual void shoot()
    {
        // Sound abspielen
        charge = 0;
        GetComponent<AudioSource>().PlayOneShot(shootSound);
        GameObject fire = (GameObject)Instantiate(shot, transform.localPosition + transform.up/2, transform.localRotation);
        fire.GetComponent<Rigidbody2D>().AddForce(transform.up * 300);
        fire.GetComponent<ShotBehaviour>().shoot(gameObject);
    }

    protected void moveInDirection(Vector3 direction)
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody != null)
            rigidBody.velocity = direction;
    }

    // Bewegt das Raumschiff auf den angegebenen Punkt mit einer Geschwindigkeit von "speed" zu
    protected void moveTowardsPoint(Vector3 targetPoint, float speed)
    {
      moveInDirection(Vector3.Lerp(transform.position, targetPoint, speed) - transform.position);
    }

    public virtual void onDestruction()
    {
        FindObjectOfType<GameController>().addResources(this.resources);
        Explosion.explode(this.gameObject);
        Destroy(gameObject);
        if(!(this is BossController))
        spawnOrigin.GetComponent<SpawnController>().enemyDied();
    }

    // Der Gegner wird getroffen
    public virtual void onHit()
    {
        dealDamage((int) lifepoints+1);
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<PlayerController_Base>() != null || coll.gameObject.GetComponent<PlayerBaseController>() != null)
        {
            coll.gameObject.GetComponent<Hitable>().dealDamage(1);
            onHit();
        }
    }

    // Füge Schaden zu
    public virtual void dealDamage(int damage)
    {
        lifepoints--;
        if (lifepoints <= 0)
            onDestruction();
    }
}
