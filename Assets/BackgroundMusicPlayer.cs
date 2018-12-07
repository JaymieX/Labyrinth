using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Kep music playing
    }
}
