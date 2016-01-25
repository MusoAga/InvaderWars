using UnityEngine;
using System.Collections;

public class PlayerBaseController : MonoBehaviour,Hitable {

    private Explosion explosion;
    private int lifePoints = 3;
    private GameObject gameController;

    public void onHit()
    {
        dealDamage(1);
    }

    public void dealDamage(int damage)
    {
        lifePoints -= damage;
        if(lifePoints <= 0)
        {
            onBaseDestruction();
        }
    }



    private void onBaseDestruction()
    {
        Explosion.explode(this.gameObject);
        gameController.GetComponent<GameController>().removePlayerBase(gameObject);
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
