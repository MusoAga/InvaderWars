using UnityEngine;
using System.Collections;

public class tets : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {


        // Steuerung links/rechts
        //if (Input.GetButton("d")) 
        //   moveInDirection(new Vector2(Input.GetAxis("Horizontal"), 0));
        //if (Input.GetButton(KeyCode("d"))) moveInDirection(Vector2.right);
        //	if (Input.GetButton("w")) moveInDirection(Vector2.up);
        //	if (Input.GetButton("s")) moveInDirection(Vector2.down);
        print(Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Horizontal") < 0)
        {
            print("ti");
            //moveInDirection(Vector2.left);
        }
        // print("jo");
        //	base.Update();
    }
}
