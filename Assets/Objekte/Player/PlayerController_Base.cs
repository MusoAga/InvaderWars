using UnityEngine;
using System.Collections;



public class PlayerController_Base : MonoBehaviour
{	
    
	protected float speed = 10; // Bewegungsgeschwindigkeit des Spielers
    protected float fireRate = 20; // Schussrate
	protected float currentFireCharge;
    protected int lifePoints = 1; // Lebenspunkte
    
    public GameObject shot; // GameObjects, um Schüsse und Explosionen zu initialisieren
    public AudioClip shootSound; // Abgespielter Sound beim Schießen

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

    public virtual void shoot()
    {
        currentFireCharge = fireRate;
       GameObject firedShot = (GameObject) Instantiate(shot, transform.position+this.transform.up/2, transform.rotation);
        if (firedShot.GetComponent<Rigidbody2D>() != null)
            firedShot.GetComponent<Rigidbody2D>().AddForce(this.transform.up*300);
        GetComponent<AudioSource>().PlayOneShot(shootSound);
    }

    // Wird aufgerufen, sobald der Spieler von Schüssen oder Raumschiffen getroffen wurde
    public virtual void hit(int damage)
    {
		lifePoints = -damage;
		// Keine lifepoints mehr vorhanden ? Spieler stirbt
		if (lifePoints <= 0) onDestruction();
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
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<EnemyController_Defense>() != null)
        { 
            coll.gameObject.GetComponent<EnemyController_Defense>().hit();
            hit(1);
        }
    }

    //void onPickup(GameObject powerup) { }

    /* TODO FileReader
	void initialiseStats(ArrayList fileInput) {   }*/
}
