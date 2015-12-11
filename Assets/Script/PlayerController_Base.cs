using UnityEngine;
using System.Collections;



public class PlayerController_Base : MonoBehaviour
{	

   
	public float speed; // Bewegungsgeschwindigkeit des Spielers
    public float fireRate; // Schussrate
	private float nextFire;


    // GameObjects, um Schüsse und Explosionen zu initialisieren
    public GameObject shot;
	public Transform  shotSpawn;
	public GameObject playerExplosion;

	//Audio
    //TODO public AudioClip shootSound;

    // Lebenspunkte
    public int lifePoints;

    // Geschwindigkeit, die der Spieler weiter"rutscht", nachdem Tasten losgelassen wurden
    //private float drag = 10;
    // Schaden eines Schusses (optional)
    //private float damage = 1; 

    // Use this for initialization
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {


        // Spieler drückt "Schuss"-Taste
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			//TODO GetComponent<AudioSource>().Play ();
		}

    }

    /* TODO FileReader
	void initialiseStats(ArrayList fileInput)
    {

    }*/
	
    

    // Wird aufgerufen, sobald der Spieler von Schüssen oder Raumschiffen getroffen wurde
    void hit(int damage)
    {
		lifePoints = -damage;
			
		// Keine lifepoints mehr vorhanden ? Spieler stirbt
		if (lifePoints <= 0) {
			onDestruction ();
		}
	}

    // Spieler stirbt
    void onDestruction()
    {
		if (playerExplosion != null)
		{
			Instantiate(playerExplosion, transform.position, transform.rotation);
		}
		
		//TODO Audio
		//TODO send Message zu GameManager
		Destroy (gameObject);
    }

    void onPickup(GameObject powerup)
    {
        //powerup.activate(this);
    }

    /* TODO EnemyHit Sinnvoll?
	void onEnemyHit(GameObject enemy)
    {
        hit();
        //enemy.sendMessage("EnemyHit");
    }*/
}
