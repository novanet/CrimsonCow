using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class OnQuitKey : MonoBehaviour
    {
        void Update()
        {
            Quit();
        }

        private void Quit()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
