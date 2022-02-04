using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    [SerializeField] private Button instructionButton;
    [SerializeField] private Text endScoreText, scoreText, bestScoreText;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        MakeInstance();
        Time.timeScale = 0;
    }

    public void InstructionButton()
    {
        Time.timeScale = 1;
        instructionButton.gameObject.SetActive(false);
    }
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void BirdDiedShowPanel(int score)
    {
        endScoreText.text = score.ToString();
        if (score > GameManager.instance.GetHighScore())
        {
            GameManager.instance.SetHighScore(score);
        }
        bestScoreText.text = GameManager.instance.GetHighScore().ToString();
        gameOverPanel.SetActive(true);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
