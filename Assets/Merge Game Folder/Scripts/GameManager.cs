using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance; // keep only ONE singleton, not two
    #endregion

    #region Properties
    public int CurrentTime { get; set; }
    public bool isGameAcitve = false;
    #endregion

    #region Serialized Fields
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _gameOverPanel;
    [SerializeField] private float _fadeTime = 2f;

    [Header("Game Settings")]
    public float TimeTillGameOver = 1.5f;

    [Header("Level Settings")]
    public int timeLimit = 60;

    [Tooltip("Choose a number that is less than the avialable number of fruits in the game.")]
    public int highestFruitIndex = 10;

    [Header("Merged Fruit Tracking")]
    public Dictionary<string, int> mergedFruitCounts = new Dictionary<string, int>();

    [Header("Fruit Spawning")]
    public GameObject[] fruitPrefabs; // set these in the Inspector
    public GameObject nextFruitPrefab;
    #endregion

    #region Unity Lifecycle
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
        InitializeSingleton();
        InitializeTimer();
    }
    #endregion

    #region Initialization
    private void InitializeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void InitializeTimer()
    {
        CurrentTime = timeLimit;
        UpdateTimeDisplay();
    }

    private void UpdateTimeDisplay()
    {
        int minutes = CurrentTime / 60;
        int seconds = CurrentTime % 60;
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion

    #region Fruit Management
    public void SetNextFruit(int index)
    {
        if (index >= 0 && index < fruitPrefabs.Length)
        {
            nextFruitPrefab = fruitPrefabs[index];
            Debug.Log("Next fruit set to: " + nextFruitPrefab.name);
        }
        else
        {
            Debug.LogWarning("Invalid fruit index passed to SetNextFruit!");
        }
    }

    public GameObject GetNextFruit()
    {
        return nextFruitPrefab;
    }
    #endregion

    #region Game Control Methods
    public void StartGame()
    {
        isGameAcitve=true;
        StartCoroutine(UpdateTime());
    }

    public void GameOver()
    {
        isGameAcitve = false;
        DisplayMergedFruitStats();  
        StartCoroutine(ResetGame());
    }
    #endregion

    #region Timer System
    private IEnumerator UpdateTime()
    {
        while (CurrentTime > 0 && isGameAcitve)
        {
            yield return new WaitForSeconds(1f);
            CurrentTime--;
            UpdateTimeDisplay();

            if (CurrentTime <= 0)
            {
                GameOver();
                yield break;
            }
        }
    }
    #endregion

    #region Merged Fruit Tracking
    public void AddMergedFruit(string fruitName)
    {
        if (string.IsNullOrEmpty(fruitName)) return;

        if (mergedFruitCounts.ContainsKey(fruitName))
        {
            mergedFruitCounts[fruitName]++;
        }
        else
        {
            mergedFruitCounts.Add(fruitName, 1);
        }
        
        Debug.Log($"Merged {fruitName}: {mergedFruitCounts[fruitName]} times");
    }

    public int GetMergedFruitCount(string fruitName)
    {
        if (string.IsNullOrEmpty(fruitName)) return 0;
        return mergedFruitCounts.ContainsKey(fruitName) ? mergedFruitCounts[fruitName] : 0;
    }

    public int GetTotalMergedFruits()
    {
        int total = 0;
        foreach (var count in mergedFruitCounts.Values)
        {
            total += count;
        }
        return total;
    }

    public Dictionary<string, int> GetAllMergedFruitCounts()
    {
        return new Dictionary<string, int>(mergedFruitCounts);
    }

    public void ClearMergedFruitCounts()
    {
        mergedFruitCounts.Clear();
        Debug.Log("Merged fruit counts cleared");
    }

    private void DisplayMergedFruitStats()
    {
        Debug.Log("=== GAME OVER - Merged Fruit Statistics ===");
        
        if (mergedFruitCounts.Count == 0)
        {
            Debug.Log("No fruits were merged this game.");
        }
        else
        {
            foreach (var kvp in mergedFruitCounts)
            {
                Debug.Log($"{kvp.Key}: {kvp.Value} merges");
            }
            Debug.Log($"Total fruits merged: {GetTotalMergedFruits()}");
        }
        
        Debug.Log("==========================================");
    }
    #endregion

    #region Scene Management & Fading
    private IEnumerator ResetGame()
    {
        yield return StartCoroutine(FadeGameOut());
        ClearMergedFruitCounts();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FadeGame(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeGameIn());
    }

    private IEnumerator FadeGameOut()
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

        startColor.a = 1f;
        _gameOverPanel.color = startColor;
    }

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

        startColor.a = 0f;
        _gameOverPanel.color = startColor;
        _gameOverPanel.gameObject.SetActive(false);
    }
    #endregion

    #region Public Utility Methods
    public bool IsGameActive()
    {
        return isGameAcitve;
    }

    public int GetRemainingTime()
    {
        return CurrentTime;
    }

    public void AddTime(int seconds)
    {
        CurrentTime += seconds;
        UpdateTimeDisplay();
    }

    public void RemoveTime(int seconds)
    {
        CurrentTime = Mathf.Max(0, CurrentTime - seconds);
        UpdateTimeDisplay();
        
        if (CurrentTime <= 0)
        {
            GameOver();
        }
    }
    #endregion

        public void ToDailyRewards()
    {
        SceneManager.LoadScene("DailyRewards");
    }
}
