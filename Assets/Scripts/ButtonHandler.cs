using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
