using UnityEngine;

public class PickNextButton : MonoBehaviour
{
    public GameObject selectorPanel; // drag  FruitSelectPanel here

    public void OnClick()
    {
        //if (!FruitSelectorPowerUp.selectorActive) return; // require power-up
        //selectorPanel.SetActive(true);
        //FruitSelectorPowerUp.selectorActive = false; // consume power-up
    }
}
