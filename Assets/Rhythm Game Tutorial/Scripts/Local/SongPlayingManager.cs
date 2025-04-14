using UnityEngine;
using UnityEngine.SceneManagement;

public class SongPlayingManager : MonoBehaviour
{
    public static SongPlayingManager Instance;

    public AudioClip SelectedAudioClip;
    public TrackTimerLists_Dic SelectedTrackData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void EnsureExists()
    {
        if (Instance == null)
        {
            GameObject go = new GameObject("SongPlayingManager");
            go.AddComponent<SongPlayingManager>();
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
