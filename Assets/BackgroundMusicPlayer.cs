using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    private static BackgroundMusicPlayer _instance;

    // Use this for initialization
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Kep music playing
        }
    }
}
