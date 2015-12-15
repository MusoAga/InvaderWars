using UnityEngine;
using System.Collections;

public class EnemyController_Defense : MonoBehaviour {

    protected float speed = 2; // Flug-Geschwindigkeit 
    protected float lifepoints = 1; // Lebenspunkte
    protected int ressources; // Anzahl der Ressourcen, die beim Tod abgeworfen werden
    

    public GameObject shot;
    public AudioClip shootSound;


	// Use this for initialization
	void Start () {
        // transform.Rotate(0, 0, Vector2.Angle(new Vector2(0, 0), new Vector2(transform.position.y, transform.position.x)), 0);
        transform.Rotate(new Vector3(0, 0, 180));
        //transform.Rotate(0, 0, Mathf.Atan2(-transform.position.y, -transform.position.x) * 180 / Mathf.PI - 90);
    }
	
	// Update is called once per frame
	void Update () {
        moveInDirection(transform.up);
        //if (Random.Range(0, 100) == 1)
        // shoot();
    }

    void moveInDirection(Vector3 direction)
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody != null)
            rigidBody.AddForce(direction * speed);
    }

    void initializeStats()
    {

    }

    // Feuere Schüsse ab
    void shoot()
    {
        // Sound abspielen
        GetComponent<AudioSource>().PlayOneShot(shootSound);
            GameObject fire = (GameObject)Instantiate(shot, transform.localPosition + transform.up/2, transform.localRotation);
            fire.GetComponent<Rigidbody2D>().AddForce(transform.up * 300);
    }

    public virtual void hit()
    {
        lifepoints--;
        if (lifepoints <= 0)
            onDestruction();
    }

    void onDestruction()
    {
        Explosion.explode(this.gameObject);
        Destroy(this.gameObject);
    }
}
