using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public int CurrentScore { get; set; }
    public int CurrentTime { get; set; }

    //[SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _gameOverPanel;
    [SerializeField] private float _fadeTime = 2f;

    public bool isGameAcitve = false;

    public float TimeTillGameOver = 1.5f;

    [Header("Level Settings")]
    public int timeLimit = 60;

    [Tooltip("Choose a number that is less than the avialable number of fruits in the game.")]
    public int highestFruitIndex = 10;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += FadeGame;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= FadeGame;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //_scoreText.text = CurrentScore.ToString("0");

        CurrentTime = timeLimit;
        int minutes = CurrentTime / 60;
        int seconds = CurrentTime % 60;
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //public void IncreaseScore(int amount)
    //{
    //    CurrentScore += amount;
    //    _scoreText.text = CurrentScore.ToString("0");
    //}


    public void StartGame()
    {
        isGameAcitve=true;
        StartCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        while (CurrentTime > 0)
        {
            yield return new WaitForSeconds(1);
            CurrentTime--;
            int minutes = CurrentTime / 60;
            int seconds = CurrentTime % 60;
            _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            if (CurrentTime <= 0)
            {
                GameOver();
                yield break;
            }
        }
    }

    public void GameOver()
    {
        isGameAcitve = false;   
        StartCoroutine(ResetGame());
    }

    public void ToDailyRewards()
    {
        SceneManager.LoadScene("DailyRewards");
    }

    private IEnumerator ResetGame()
    {
        _gameOverPanel.gameObject.SetActive(true);

        Color startColor = _gameOverPanel.color;
        startColor.a = 0f;
        _gameOverPanel.color = startColor;

        float elapsedTime = 0f;
        while(elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(0f, 1f, (elapsedTime / _fadeTime));
            startColor.a = newAlpha;
            _gameOverPanel.color = startColor;

            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FadeGame(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeGameIn());
    }

    private IEnumerator FadeGameIn()
    {
        _gameOverPanel.gameObject.SetActive(true);
        Color startColor = _gameOverPanel.color;
        startColor.a = 1f;
        _gameOverPanel.color = startColor;

        float elapsedTime = 0f;
        while(elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(1f, 0f, (elapsedTime / _fadeTime));
            startColor.a = newAlpha;
            _gameOverPanel.color = startColor;

            yield return null;
        }

        _gameOverPanel.gameObject.SetActive(false);
    }
}
