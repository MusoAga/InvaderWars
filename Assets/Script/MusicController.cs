using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

    public static MusicController instance = null;

    private AudioSource audioSource;

	// Use this for initialization
	void Awake () {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Music/Normal3_Loop");
        playMusic();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playMusic()
    {
        audioSource.Play();
    }
}
