using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coins = 0;
    public TextMeshProUGUI coinText;

    void Start()
    {
        UpdateCoinText();
    }

    public void AddCoin()
    {
        coins++;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        coinText.text = "Monedas: " + coins;
    }
}
