using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    float lifetime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Explosion lebt zu lange -> Zerstören
        lifetime++;
        if (lifetime >= 30) Destroy(this.gameObject);
	}

    // Grund-Funktion zum erstellen von Explosionen
    public static void explode(GameObject target, float explosionSize = 1)
    {
        GameObject exp = Instantiate(Resources.Load("Explosion") as GameObject);
        exp.transform.position = target.transform.position;
        exp.transform.localScale *= explosionSize;
    }

}
