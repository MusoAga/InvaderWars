using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour {

    // Static singleton instance
    private static Singleton instance;

    // Static singleton property
    public static Singleton Instance
    {
        // Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
        // otherwise we assign instance to a new component and return that
        get { return instance ?? (instance = new GameObject("Singleton").AddComponent<Singleton>()); }
    }
}
