using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainLevel()
    {
        // Load the MainLevel scene
        SceneManager.LoadScene("MainLevel");
    }
}
