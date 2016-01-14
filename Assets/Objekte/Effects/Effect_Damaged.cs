using UnityEngine;
using System.Collections;

public class Effect_Damaged : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(End());
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.frameCount % 10 == 0)
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        if (Time.frameCount % 20 == 0)
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(this);
    }
}
