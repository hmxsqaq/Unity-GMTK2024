using Hmxs_GMTK.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPage : MonoBehaviour
{
    [SerializeField] private Rolling rolling;

    public void StartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void QuitGame()
    {
      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
      #else
        Application.Quit();
      #endif
    }

    public void OpenRolling()
    {
        rolling.gameObject.SetActive(true);
    }
}
