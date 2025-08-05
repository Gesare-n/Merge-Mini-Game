using UnityEngine;
using TMPro;

public class VisualFeedback : MonoBehaviour
{
    public static VisualFeedback Instance {get; set;}
    public GameObject visualScoreTextPrefab;

    void Awake()
    {
        Instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTextVisuals(int amount)
    {
        Instantiate(visualScoreTextPrefab, FruitCombiner.Instance.collisionPosition, Quaternion.identity, transform);
        visualScoreTextPrefab.GetComponent<TMP_Text>().text = amount.ToString();
    }
}
