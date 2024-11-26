using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit button clicked! Exiting game...");

        // Exit the application
        Application.Quit();

        // Note: In the Unity editor, Application.Quit() won't work.
        // Use Debug.Log to verify functionality while testing in the editor.
    }
}
