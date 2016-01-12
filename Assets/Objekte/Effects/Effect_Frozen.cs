using UnityEngine;
using System.Collections;

public class Effect_Stun : MonoBehaviour {

    float lifetime = 80;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        lifetime--;
        if (lifetime == 0) Destroy(this);
	}
}
