using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SimpleCharacterRequest : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] private string characterName = "Mama Gold";
    [SerializeField] private Image characterPortrait;
    [SerializeField] private Text requestText;
    
    [Header("Current Request")]
    [SerializeField] private Image fruitIcon;
    [SerializeField] private Text fruitCountText;
    [SerializeField] private Text progressText;
    [SerializeField] private Button completeButton;
    [SerializeField] private GameObject completedIndicator;
    
    [Header("Request Settings")]
    [SerializeField] private List<FruitRequest> possibleRequests = new List<FruitRequest>();
    
    // Current request data
    private FruitRequest currentRequest;
    private int currentProgress = 0;
    private bool isCompleted = false;
    
    [System.Serializable]
    public class FruitRequest
    {
        public string fruitName;        // "strawberry", "orange", etc.
        public Sprite fruitSprite;      // Icon to display
        public int requiredAmount;      // How many needed
        public string requestMessage;   // "I want 2 strawberries."
    }
    
    private void Start()
    {
        SetupEventManager();
        GenerateNewRequest();
        SetupUI();
    }
    
    private void SetupEventManager()
    {
        // Create EventManager if it doesn't exist
        if (EventManager.Instance == null)
        {
            GameObject eventManagerObj = new GameObject("EventManager");
            eventManagerObj.AddComponent<EventManager>();
        }
    }
    
    private void GenerateNewRequest()
    {
        if (possibleRequests.Count > 0)
        {
            currentRequest = possibleRequests[Random.Range(0, possibleRequests.Count)];
            currentProgress = 0;
            isCompleted = false;
            
            // Subscribe to fruit collection events
            EventManager.Instance.AddListener<FruitCollectedEvent>(OnFruitCollected);
            
            Debug.Log($"New request: {currentRequest.requestMessage}");
        }
    }
    
    private void SetupUI()
    {
        if (currentRequest == null) return;
        
        // Set request text
        if (requestText != null)
        {
            requestText.text = currentRequest.requestMessage;
        }
        
        // Set fruit icon
        if (fruitIcon != null && currentRequest.fruitSprite != null)
        {
            fruitIcon.sprite = currentRequest.fruitSprite;
        }
        
        // Set fruit count
        if (fruitCountText != null)
        {
            fruitCountText.text = "×" + currentRequest.requiredAmount.ToString();
        }
        
        // Setup complete button
        if (completeButton != null)
        {
            completeButton.onClick.RemoveAllListeners();
            completeButton.onClick.AddListener(CompleteRequest);
        }
        
        UpdateUI();
    }
    
    private void OnFruitCollected(FruitCollectedEvent eventData)
    {
        if (currentRequest == null || isCompleted) return;
        
        // Check if collected fruit matches current request
        if (eventData.FruitType.ToLower() == currentRequest.fruitName.ToLower())
        {
            currentProgress += eventData.Amount;
            
            // Check if request is completed
            if (currentProgress >= currentRequest.requiredAmount)
            {
                currentProgress = currentRequest.requiredAmount; // Cap at required amount
                isCompleted = true;
                Debug.Log($"Request completed! Collected {currentProgress}/{currentRequest.requiredAmount} {currentRequest.fruitName}s");
            }
            
            UpdateUI();
            Debug.Log($"Progress: {currentProgress}/{currentRequest.requiredAmount} {currentRequest.fruitName}s");
        }
    }
    
    private void UpdateUI()
    {
        // Update progress text
        if (progressText != null)
        {
            progressText.text = $"{currentProgress}/{currentRequest.requiredAmount}";
        }
        
        // Update complete button
        if (completeButton != null)
        {
            completeButton.interactable = isCompleted;
            Text buttonText = completeButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = isCompleted ? "Complete!" : "Collecting...";
            }
        }
        
        // Show/hide completed indicator
        if (completedIndicator != null)
        {
            completedIndicator.SetActive(isCompleted);
        }
        
        // Change colors based on completion
        if (isCompleted)
        {
            // Make everything look completed (green tint, etc.)
            if (progressText != null)
                progressText.color = Color.green;
        }
        else
        {
            if (progressText != null)
                progressText.color = Color.white;
        }
    }
    
    private void CompleteRequest()
    {
        if (!isCompleted) return;
        
        Debug.Log($"Request completed by {characterName}! Generated new request.");
        
        // Unsubscribe from current events
        EventManager.Instance.RemoveListener<FruitCollectedEvent>(OnFruitCollected);
        
        // Generate new request
        GenerateNewRequest();
        SetupUI();
    }
    
    // Call this from your fruit collection logic
    public void CollectFruit(string fruitType, int amount = 1)
    {
        Debug.Log($"Fruit collected: {amount} {fruitType}(s)");
        EventManager.Instance.QueueEvent(new FruitCollectedEvent(fruitType, amount));
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from events
        if (EventManager.Instance != null)
        {
            EventManager.Instance.RemoveListener<FruitCollectedEvent>(OnFruitCollected);
        }
    }
}