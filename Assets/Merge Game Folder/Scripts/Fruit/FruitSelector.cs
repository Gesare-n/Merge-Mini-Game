using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitSelector : MonoBehaviour
{
    public static FruitSelector instance;

    public GameObject[] Fruits;
    public GameObject[] NoPhysicsFruits;
    public int HighestStartingIndex = 3;

    [SerializeField] private Image _nextFruitImage;
    [SerializeField] private Sprite[] _fruitSprites;

    public GameObject NextFruit { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PickNextFruit();
    }

    public GameObject PickRandomFruitForThrow()
    {
        // Ensure Fruit selected is one among these allowed in the level.
        // Additionally, highest allowed fruit can't be spawned - player needs at least one merge to reach it.
        if (HighestStartingIndex > GameManager.instance.highestFruitIndex)
        {
            HighestStartingIndex = GameManager.instance.highestFruitIndex - 1;
        }

        int randomIndex = Random.Range(0, HighestStartingIndex + 1);

        if (randomIndex < NoPhysicsFruits.Length)
        {
            GameObject randomFruit = NoPhysicsFruits[randomIndex];
            return randomFruit;
        }

        return null;
    }

    public void PickNextFruit()
    {
        if (HighestStartingIndex > GameManager.instance.highestFruitIndex)
        {
            HighestStartingIndex = GameManager.instance.highestFruitIndex - 1;
        }

        int randomIndex = Random.Range(0, HighestStartingIndex + 1);

        if (randomIndex < Fruits.Length)
        {
            GameObject nextFruit = NoPhysicsFruits[randomIndex];
            NextFruit = nextFruit;

            _nextFruitImage.sprite = _fruitSprites[randomIndex];
        }
    }
}
