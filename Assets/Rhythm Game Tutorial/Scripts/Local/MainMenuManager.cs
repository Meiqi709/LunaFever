using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("SongSelectScene");
    }

    public void LoadPlayOnlineScene()
    {
        SceneManager.LoadScene("PlayOnlineScene");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void LoadChartEditor()
    {
        SceneManager.LoadScene("PartitionsEditeur");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
