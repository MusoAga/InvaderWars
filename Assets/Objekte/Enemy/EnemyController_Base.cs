﻿using UnityEngine;
using System.Collections;

public class EnemyController_Base : MonoBehaviour, Hitable {

    protected float speed = 2; // Flug-Geschwindigkeit 
    protected float lifepoints = 1; // Lebenspunkte
    protected int resources; // Anzahl der Ressourcen, die beim Tod abgeworfen werden
    protected float charge; // Aufladung des Schusses
    GameObject spawnOrigin = null;
    public GameObject shot;
    public AudioClip shootSound;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(!FindObjectOfType<GameController>().isPaused())
            enemyBehaviour();
        // Sobald ein Gegner das Ende des Bildschirms erreicht hat, wird er entfernt
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            spawnOrigin.GetComponent<SpawnController>().enemyDied();
        }
    }

    public void setSpawnOrigin(GameObject spawn) 
    {
        this.spawnOrigin = spawn;
    }

    public void setup(float _speed, float _lifePoints, int _resources)
    {
        this.speed = _speed;
        this.lifepoints = _lifePoints;
        this.resources = _resources;
    }

    public virtual void enemyBehaviour()
    {
        moveInDirection(transform.up);
    }

    // Feuere Schüsse ab
    public virtual void shoot()
    {
        // Sound abspielen
        charge = 0;
        GetComponent<AudioSource>().PlayOneShot(shootSound);
        GameObject fire = (GameObject)Instantiate(shot, transform.localPosition + transform.up *0.6f, transform.localRotation);
        fire.GetComponent<Rigidbody2D>().AddForce(transform.up * 300);
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

    void initializeStats()
    {

    }

    void onDestruction()
    {
        Explosion.explode(this.gameObject);
        Destroy(this.gameObject);
        spawnOrigin.GetComponent<SpawnController>().enemyDied();
    }

    // Der Gegner wird getroffen
    public virtual void onHit()
    {
        lifepoints--;
        if (lifepoints <= 0)
            onDestruction();
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
    }
}
