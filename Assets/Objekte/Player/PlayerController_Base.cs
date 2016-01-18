using UnityEngine;
using System.Collections;
using System;

public class PlayerController_Base : MonoBehaviour, Hitable
{	
    
	public float speed = 1; // Bewegungsgeschwindigkeit des Spielers
    public float fireRate = 20; // Schussrate
	protected float currentFireCharge;
    public int lifePoints = 10; // Lebenspunkte
    public float fireRatestep;
    public float speedStep;
    public int frostResistence = 0;
    
    public GameObject shot; // GameObjects, um Schüsse und Explosionen zu initialisieren. Enthält den aktuellen Schuss des Spielers
    public GameObject defaultShot;
    public GameObject laserShot;

    public AudioClip shootSound; // Abgespielter Sound beim Schießen
    public AudioClip shootSound_Bullet;
    public AudioClip shootSound_Laser;

    public virtual void Start()
    {
        currentFireCharge = fireRate;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Spieler drückt "Schuss"-Taste
        if (Input.GetButton("Fire"))
        {
            currentFireCharge--;
            if (currentFireCharge <= 0) shoot();
        }
        else currentFireCharge = fireRate;
    }

    public void resetPlayerStats()
    {
        this.shot = defaultShot;
        this.shootSound = shootSound_Bullet;
        this.lifePoints = 3;
        this.speed = 1;
        this.fireRate = 20;

    }

    public int getFrostResistence()
    {
        return this.frostResistence;
    }

    public virtual void shoot()
    {
        // Feueraufladung wieder zurücksetzen
        currentFireCharge = fireRate;
        // Schuss erstellen
       GameObject firedShot = (GameObject) Instantiate(shot, transform.position+this.transform.up/2, transform.rotation);
        if (firedShot.GetComponent<Rigidbody2D>() != null)
            firedShot.GetComponent<Rigidbody2D>().AddForce(this.transform.up*300);
        GetComponent<AudioSource>().PlayOneShot(shootSound);
        // Den Besitzer des Schusses zuweisen
        firedShot.GetComponent<ShotBehaviour>().shoot(gameObject);
    }

    // Spieler stirbt
    void onDestruction()
    {
        Explosion.explode(this.gameObject);
		Destroy(gameObject);
        GameController gc = FindObjectOfType<GameController>();
        if (gc != null)
            gc.onPlayerDestruction();
    }

    public virtual void moveInDirection(Vector2 direction)
    {
        if (GetComponent<Rigidbody2D>() != null)
            GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }

    public void onHit()
    {
    }

    public void dealDamage(int damage)
    {
        if (GetComponent<Effect_Damaged>() != null) return;
        gameObject.AddComponent<Effect_Damaged>();
        lifePoints -= damage;
        // Keine lifepoints mehr vorhanden ? Spieler stirbt
        if (lifePoints <= 0) onDestruction();
    }

    public void increaseFireRate()
    {
        fireRate -= fireRatestep;
    }

    public void increaseLifePoints()
    {
        lifePoints++;
    }

    public int getLifePoints()
    {
        return lifePoints;
    }

    public void changeShot(string shotType)
    {

        if (shotType.Contains("Laser"))
        {
            this.shot = laserShot;
            this.shootSound = shootSound_Laser;
        }
        else
        {
            this.shot = defaultShot;
            this.shootSound = shootSound_Bullet;
        }
    }

    public void increaseSpeed()
    {
        this.speed += speedStep;
    }

    public void increaseFrostResistence()
    {
        this.frostResistence++;
    }


}
