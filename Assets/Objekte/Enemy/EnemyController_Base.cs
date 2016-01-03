using UnityEngine;
using System.Collections;

public class EnemyController_Base : MonoBehaviour, Hitable {

    protected float speed = 2; // Flug-Geschwindigkeit 
    protected float lifepoints = 1; // Lebenspunkte
    protected int ressources; // Anzahl der Ressourcen, die beim Tod abgeworfen werden


    public GameObject shot;
    public AudioClip shootSound;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        enemyBehaviour();
    }

    public virtual void enemyBehaviour()
    {
        moveInDirection(transform.up);
    }

    // Feuere Schüsse ab
    void shoot()
    {
        // Sound abspielen
        GetComponent<AudioSource>().PlayOneShot(shootSound);
        GameObject fire = (GameObject)Instantiate(shot, transform.localPosition + transform.up / 2, transform.localRotation);
        fire.GetComponent<Rigidbody2D>().AddForce(transform.up * 300);
    }

    protected void moveInDirection(Vector3 direction)
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody != null)
            rigidBody.velocity = direction;
    }


    void initializeStats()
    {

    }

    void onDestruction()
    {
        Explosion.explode(this.gameObject);
        Destroy(this.gameObject);
    }

    // Der Gegner wird getroffen
    public void onHit()
    {
        lifepoints--;
        if (lifepoints <= 0)
            onDestruction();
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
    }
}
