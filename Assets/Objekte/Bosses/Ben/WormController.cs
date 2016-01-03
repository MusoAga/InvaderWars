using UnityEngine;
using System.Collections;

public class WormController : EnemyController_Defense {

    public GameObject wormEngine;
    SortedList engines = new SortedList();
    float movementSpeed = 0.5f;
    bool isRushing;
    float timer;

	// Use this for initialization
	void Start () {
        engines.Add(engines.Count, gameObject);
        for (int i = 0; i < 0; i++)
            addEngine();
    }
	
    void addEngine()
    {
        GameObject engine = Instantiate(wormEngine);
        engine.transform.position = new Vector2(0, engines.Count * -1);
        engines.Add(engines.Count, engine);
        HingeJoint2D hj = engine.GetComponent<HingeJoint2D>();
        if (hj != null)
        {
            GameObject prevEngine = (GameObject) engines[engines.Count-2];
            hj.connectedBody = prevEngine.GetComponent<Rigidbody2D>();
            if (engines.Count == 2) ;
                hj.connectedAnchor = new Vector2(0, -0.6f);
        }
    }

	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = transform.up * movementSpeed;
        timer = Mathf.Repeat(timer + 0.001f, 2)-1;
        if (!isRushing)
            transform.rotation.Angle(new Vector2(), Mathf.Sin(timer));
	}
}
