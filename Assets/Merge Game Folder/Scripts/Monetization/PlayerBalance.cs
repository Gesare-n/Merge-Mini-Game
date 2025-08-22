using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerBalance : MonoBehaviour
{
    public static PlayerBalance instance;

    //public UserInterface UI;

    [SerializeField] private int currentBalance = 0;

    private string gems = "g_01";           // Obfuscated key name for gems
    public TextMeshProUGUI balanceText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentBalance = LoadBalance();
        balanceText.text = currentBalance.ToString();
        //UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterface>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToBalance (int amount)
    {
        currentBalance += amount;
        SaveBalance();
        balanceText.text = currentBalance.ToString();
    }

    public void RemoveFromBalance (int amount)
    {
        currentBalance -= amount;
        SaveBalance();
        balanceText.text = currentBalance.ToString();
    }

    public void ResetBalance()
    {
        currentBalance = 0;
        SaveBalance ();
        balanceText.text = currentBalance.ToString();
    }

    void SaveBalance()
    {
        PlayerPrefs.SetInt (gems, currentBalance);
    }

    int LoadBalance()
    {
        int balance = PlayerPrefs.GetInt(gems);
        return (balance);
    }

    public void IncreaseBalanceCheat()
    {
        AddToBalance(50);
    }

    public void DecreaseBalanceCheat()
    {
        RemoveFromBalance(50);
    }
}
