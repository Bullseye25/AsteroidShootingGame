using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    private Text m_scoreText, m_gameState, m_finalScore;
    private int m_score;
    internal GameObject m_countDownPanal, m_gameEndPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        m_finalScore = GameObject.Find("YourScore").GetComponent<Text>();

        m_scoreText = GameObject.Find("Score").GetComponent<Text>();

        m_gameState = GameObject.Find("GameState").GetComponent<Text>();

        m_countDownPanal = GameObject.Find("CountDown");

        m_gameEndPanel = GameObject.Find("Game");

        m_gameEndPanel.SetActive(false);
    }

    public void AddScore()
    {
        m_score++;

        if (m_score == GameManager.instance.m_amountOfAsteroides)
        {
            GameEndStats("Congratulations You Won!!");
        }
        else
        {
            m_scoreText.text = "Score: " + m_score;
        }
    }

    public void GameEndStats(string _state)
    {
        m_gameState.text = _state;

        m_finalScore.text = "Your Final Score: " + m_score;

        m_gameEndPanel.SetActive(true);
    }

    public void OnPressRetry()
    {
        SceneManager.LoadScene("GameScene");
    }
}
