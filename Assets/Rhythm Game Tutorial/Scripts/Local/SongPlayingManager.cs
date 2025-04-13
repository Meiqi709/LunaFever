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
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
