using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject objectivePanel;
    public GameObject winPanel;

    [SerializeField] private Image _ObjectivehighestFruitImage;
    [SerializeField] private Image _WinhighestFruitImage;
    [SerializeField] private Sprite[] _fruitSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ObjectivehighestFruitImage.sprite = _fruitSprites[GameManager.instance.highestFruitIndex];
        _WinhighestFruitImage.sprite = _fruitSprites[GameManager.instance.highestFruitIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameAcitve)
        {
            objectivePanel.SetActive(false);
        }

        if (UserInput.hasTouch)
        {
            instructionPanel.SetActive(false);
        }

        if(!GameManager.instance.isGameAcitve && !instructionPanel.activeSelf)
        {
            winPanel.SetActive(true);
        }
    }
}
