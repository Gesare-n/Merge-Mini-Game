using UnityEngine;
using UnityEngine.UI;

public class FruitSelectorUI : MonoBehaviour
{
    public Image fruitImage;          // UI Image in the middle
    public Sprite[] fruitSprites;     // assign fruit sprites in inspector
    private int currentIndex = 0;

    void OnEnable()
    {
        // reset to first fruit each time panel opens
        currentIndex = 0;
        UpdateFruitDisplay();
    }

    public void NextFruit()
    {
        currentIndex = (currentIndex + 1) % fruitSprites.Length;
        UpdateFruitDisplay();
    }

    public void PreviousFruit()
    {
        currentIndex = (currentIndex - 1 + fruitSprites.Length) % fruitSprites.Length;
        UpdateFruitDisplay();
    }

    public void SelectFruit()
    {
        // set the next fruit in the game manager
        GameManager.instance.SetNextFruit(currentIndex);
        gameObject.SetActive(false); // close panel
    }

    private void UpdateFruitDisplay()
    {
        fruitImage.sprite = fruitSprites[currentIndex];
    }
}
