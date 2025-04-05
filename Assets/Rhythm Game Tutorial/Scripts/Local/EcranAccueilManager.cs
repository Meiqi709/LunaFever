using UnityEngine;
using UnityEngine.SceneManagement;

public class EcranAccueilManager : MonoBehaviour
{
    private bool isLoading = false;
    void Update()
    {
        if (!isLoading && InputDetected())
        {
            isLoading = true;
            LoadMainMenu();
        }
    }

    private bool InputDetected()
    {
        return Input.anyKeyDown;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
