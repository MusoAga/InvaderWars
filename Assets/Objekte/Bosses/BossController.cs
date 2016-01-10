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
        enemyBehaviour();
	}

    public override void onHit()
    {
        base.onHit();
        bossHealthBar.value = lifepoints;
    }
}
