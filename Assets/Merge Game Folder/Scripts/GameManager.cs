using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Settings")]
    public bool isGameActive = false;
    public int timeLimit = 60;
    public float TimeTillGameOver = 1.5f;

    [Tooltip("Choose a number that is less than the available number of fruits in the game.")]
    public int highestFruitIndex = 10;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _gameOverPanel;
    [SerializeField] private float _fadeTime = 2f;

    [Header("Powerup Settings")]
    [Tooltip("Number of times player can use NextFruit powerup")]
    public int nextFruitPowerupUses = 2;
    [SerializeField] private TextMeshProUGUI _powerupText; // 👈 Add this in Inspector to show remaining powerups

    // Internal state
    public int CurrentTime { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // Initialize time and UI
        CurrentTime = timeLimit;
        UpdateTimeUI();
        UpdatePowerupUI();
    }

    private void OnEnable() => SceneManager.sceneLoaded += FadeGame;
    private void OnDisable() => SceneManager.sceneLoaded -= FadeGame;

    #region GameFlow
    public void StartGame()
    {
        isGameActive = true;
        StartCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        while (CurrentTime > 0)
        {
            yield return new WaitForSeconds(1);
            CurrentTime--;
            UpdateTimeUI();

            if (CurrentTime <= 0)
            {
                GameOver();
                yield break;
            }
        }
    }

    private void UpdateTimeUI()
    {
        int minutes = CurrentTime / 60;
        int seconds = CurrentTime % 60;
        _timeText.text = $"{minutes:00}:{seconds:00}";
    }

    public void GameOver()
    {
        isGameActive = false;   
        StartCoroutine(ResetGame());
    }

    private IEnumerator ResetGame()
    {
        _gameOverPanel.gameObject.SetActive(true);

        Color startColor = _gameOverPanel.color;
        startColor.a = 0f;
        _gameOverPanel.color = startColor;

        float elapsedTime = 0f;
        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / _fadeTime);
            startColor.a = newAlpha;
            _gameOverPanel.color = startColor;
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FadeGame(Scene scene, LoadSceneMode mode) => StartCoroutine(FadeGameIn());

    private IEnumerator FadeGameIn()
    {
        _gameOverPanel.gameObject.SetActive(true);
        Color startColor = _gameOverPanel.color;
        startColor.a = 1f;
        _gameOverPanel.color = startColor;

        float elapsedTime = 0f;
        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / _fadeTime);
            startColor.a = newAlpha;
            _gameOverPanel.color = startColor;
            yield return null;
        }

        _gameOverPanel.gameObject.SetActive(false);
    }
    #endregion

    #region Powerups
    /// <summary>
    /// Check if a powerup can still be used.
    /// </summary>
    public bool CanUsePowerup() => nextFruitPowerupUses > 0;

    /// <summary>
    /// Consume one powerup use and update UI.
    /// </summary>
    public void UsePowerup()
    {
        if (nextFruitPowerupUses > 0)
        {
            nextFruitPowerupUses--;
            Debug.Log("Powerup used! Remaining: " + nextFruitPowerupUses);
            UpdatePowerupUI();
        }
        else
        {
            Debug.Log("No powerups left!");
        }
    }

    /// <summary>
    /// Called by UI button to trigger NextFruit powerup.
    /// </summary>
    public void TryUseNextFruitPowerup()
    {
        if (CanUsePowerup())
        {
            UsePowerup();
            ThrowFruitController.instance?.UpgradeCurrentFruit();
        }
        else
        {
            Debug.Log("No powerups left to use.");
        }
    }

    /// <summary>
    /// Update the UI to show remaining powerups.
    /// </summary>
    private void UpdatePowerupUI()
    {
        if (_powerupText != null)
            _powerupText.text = $"Powerups: {nextFruitPowerupUses}";
    }
    #endregion
}
