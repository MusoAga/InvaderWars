using UnityEngine;
using System.Collections;

// Oberklasse für allgemeine Funktionen
abstract class InvaderWars : MonoBehaviour {

    public static float getAngleBetweenTwoPoints(Vector3 point1, Vector2 point2)
    {
        return Mathf.Atan2(point1.y - point2.y, point1.x - point2.x) * 180 / Mathf.PI + 90;
    }
	
    public static float boundXmin(GameObject obj)
    {
        // Berechne die Boundary Dynamisch zu jeder Auflösung
        float camDistance = Vector3.Distance(obj.transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        float offset = 0;
        if (obj.GetComponent<BoxCollider2D>() != null)
            offset = obj.GetComponent<BoxCollider2D>().size.x / 2;
        return bottomCorner.x + offset;
    }

    public static float boundXmax(GameObject obj)
    {
        // Berechne die Boundary Dynamisch zu jeder Auflösung
        float camDistance = Vector3.Distance(obj.transform.position, Camera.main.transform.position);
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
        float offset = 0;
        if (obj.GetComponent<BoxCollider2D>() != null)
            offset = obj.GetComponent<BoxCollider2D>().size.x / 2;
        return topCorner.x - offset;
    }


    public static float boundYmin(GameObject obj)
    {
        // Berechne die Boundary Dynamisch zu jeder Auflösung
        float camDistance = Vector3.Distance(obj.transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        float offset = 0;
        if (obj.GetComponent<BoxCollider2D>() != null)
            offset = obj.GetComponent<BoxCollider2D>().size.y / 2;
        return bottomCorner.y + offset;
    }

    public static float boundYmax(GameObject obj)
    {
        // Berechne die Boundary Dynamisch zu jeder Auflösung
        float camDistance = Vector3.Distance(obj.transform.position, Camera.main.transform.position);
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
        float offset = 0;
        if (obj.GetComponent<BoxCollider2D>() != null)
            offset = obj.GetComponent<BoxCollider2D>().size.y / 2;
        return topCorner.y - offset;
    }

}
