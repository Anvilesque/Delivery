using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade: MonoBehaviour
{
    private Button upgradeButton;
    public string stat;
    private Dictionary<string, List<int>> upgradeCosts = new Dictionary<string, List<int>>(){
        {"speed", new List<int> {1, 3, 5, 10, 15, 25, 35, 50, 75, 100, 250, 500, 1000}}, {"energy", new List<int> {1, 3, 5, 10, 15, 25, 35, 50, 75, 100, 250, 500, 1000}}
        };
    private TextMeshProUGUI description;
    private Dictionary<string, string> descriptions = new Dictionary<string, string>(){
        {"speed", "Your Pigeon trains to fly faster than ever before, but also tires more quickly."}, {"energy", "Your Pigeon trains to fly farther than ever before."}
    };
    // Start is called before the first frame update
    void Start()
    {
        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(UpgradeStat);
        description = GameObject.FindWithTag("Description").GetComponent<TextMeshProUGUI>();
    }
    private void UpgradeStat()
    {
        int money = PlayerPrefs.GetInt("money", 0);
        int statLevel = PlayerPrefs.GetInt(stat, 1);
        int cost = upgradeCosts[stat][statLevel-1];
        if (statLevel == upgradeCosts[stat].Count)
        {
            description.text = $"You have already reached the maximum level in {stat}";
        }
        else if (money >= cost)
        {
            PlayerPrefs.SetInt("money", money-cost);
            PlayerPrefs.SetInt(stat, statLevel + 1);
        }
        else
        {
            description.text = "Insufficient Funds";
        }
    }
    public void ShowDesc()
    {
        int statLevel = PlayerPrefs.GetInt(stat, 1);
        int cost = upgradeCosts[stat][statLevel-1];
        string desc = descriptions[stat];
        description.text = $"${cost}- {desc}";
    }
    public void ResetDesc()
    {
        description.text = "";
    }
}
