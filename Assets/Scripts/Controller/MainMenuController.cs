using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void _PlayButton()
    {
        SceneManager.LoadScene("GamePlay");
        print("Go to Game Play");
    }
}
