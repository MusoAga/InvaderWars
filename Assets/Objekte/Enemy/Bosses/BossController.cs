using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossController : EnemyController_Base {

    public Slider bossHealthBar;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(!FindObjectOfType<GameController>().isPaused())
            enemyBehaviour();
	}

    public override void onHit()
    {
        base.onHit();
        bossHealthBar.value = lifepoints;
    }

    public override void onDestruction()
    {
        base.onDestruction();
        FindObjectOfType<GameController>().addResources(200);
        FindObjectOfType<GameController>().onVictory();
        FindObjectOfType<GameController>().setLevelComplete(true);
    }
}
