using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [Header("Pause Menu UI")]
    public GameObject pauseMenuUI;  

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);  
            pauseMenuUI.transform.localScale = Vector3.one;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        Debug.Log("pause game");
        Time.timeScale = 0f;
        AudioListener.pause = true;
        isPaused = true;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        isPaused = false;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);        
            Debug.Log("resume game");

    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SongSelectScene")
            SceneManager.LoadScene("MainMenu");
        else if (scene.name == "PlayScene")
            SceneManager.LoadScene("SongSelectScene");
        else
            SceneManager.LoadScene("MainMenu");
    }
}
