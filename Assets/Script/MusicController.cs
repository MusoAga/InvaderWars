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
        playMenuMusic();
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 4)
        {
            playIngameMusic();
        }
    }

    // Update is called once per frame
    void Update () {
        if(GameObject.FindObjectOfType<GameController>() != null && GameObject.FindObjectOfType<GameController>().bossIsSpawned())
        {
            playBossMusic();
            GameObject.FindObjectOfType<GameController>().setBossSpwaned(false);
        }
    }

    private void playBossMusic()
    {
        audioSource.clip = Resources.Load<AudioClip>("Music/Boss1");
        audioSource.Play();
    }

    private void playIngameMusic()
    {
        audioSource.clip = Resources.Load<AudioClip>("Music/Normal1");
        audioSource.Play();
    }

    private void playMenuMusic()
    {
        audioSource.clip = Resources.Load<AudioClip>("Music/Normal2");
        audioSource.Play();
    }

    public void onValueChanged(float volume)
    {
        audioSource.volume = volume;
    }
}
