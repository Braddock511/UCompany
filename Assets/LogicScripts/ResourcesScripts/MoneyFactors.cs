using UnityEngine;
using TMPro;

public class MoneyFactors : MonoBehaviour
{
    // Unity
    public static MoneyFactors Instance;
    public TextMeshProUGUI moneyText;
    // Variables
    private int money;
    public float testFactor = 1.0f;

    void Start()
    {
        Instance = this;
        moneyText = GetComponent<TextMeshProUGUI>();
        money = GameManager.Instance.money;
    }

    public void Upgrade()
    {
        if (money > 1000)
        {            
            money -= 1000;
            testFactor += 0.1f;
            moneyText.text = money.ToString();
        }
        else
        {
            print(1);
        }
    }
}
