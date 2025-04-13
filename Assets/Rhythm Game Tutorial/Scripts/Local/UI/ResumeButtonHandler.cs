using UnityEngine;

public class ResumeButtonHandler : MonoBehaviour
{
    public void Resume()
    {
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.ResumeGame();
        }
    }
}
