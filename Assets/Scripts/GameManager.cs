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
    [SerializeField] private int _levelTime = 60;

    public float TimeTillGameOver = 1.5f;

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

        CurrentTime = _levelTime;
        int minutes = CurrentTime / 60;
        int seconds = CurrentTime % 60;
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        StartCoroutine(UpdateTime());
    }

    //public void IncreaseScore(int amount)
    //{
    //    CurrentScore += amount;
    //    _scoreText.text = CurrentScore.ToString("0");
    //}

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
        StartCoroutine(ResetGame());
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
