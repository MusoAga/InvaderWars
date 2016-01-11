using UnityEngine;
using System.Collections;

// Oberklasse für allgemeine Funktionen
abstract class InvaderWars : MonoBehaviour {

    public static float getAngleBetweenTwoPoints(Vector3 point1, Vector2 point2)
    {
        return Mathf.Atan2(point1.y - point2.y, point1.x - point2.x) * 180 / Mathf.PI + 90;
    }
	
}
