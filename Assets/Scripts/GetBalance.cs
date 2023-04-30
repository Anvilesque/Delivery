using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetBalance : MonoBehaviour
{
    private TextMeshProUGUI balanceText;
    private int balance;
    // Start is called before the first frame update
    void Start()
    {
        balanceText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        balance = PlayerPrefs.GetInt("money", 0);
        balanceText.text = $"Balance: {balance}";
    }
}
