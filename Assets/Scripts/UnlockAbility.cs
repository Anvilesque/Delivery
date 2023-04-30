using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockAbility: MonoBehaviour
{
    private Button unlockButton;
    public string stat;
    private Dictionary<string, int> upgradeCosts = new Dictionary<string, int>(){
        {"dash", 10}, {"boost", 100}, {"robo-pigeon", 500}
        };
    private TextMeshProUGUI description;
    private Dictionary<string, string> descriptions = new Dictionary<string, string>(){
        {"dash", "Your Pigeon gains the ability to quickly dash a short distance at the cost of energy."}, {"boost", "Your Pigeon gains the ability to fly with 50% extra speed, but tires more quickly"}, 
        {"robo-pigeon", "What does this ability do??"},
    };
    // Start is called before the first frame update
    void Start()
    {
        unlockButton = GetComponent<Button>();
        unlockButton.onClick.AddListener(UnlockAb);
        description = GameObject.FindWithTag("Description").GetComponent<TextMeshProUGUI>();
    }
    private void UnlockAb()
    {
        int money = PlayerPrefs.GetInt("money", 0);
        bool hasAbility = PlayerPrefs.HasKey(stat);
        int cost = upgradeCosts[stat];
        if (hasAbility)
        {
            description.text = $"You have already unlocked the {stat} ability";
        }
        else if (money >= cost)
        {
            PlayerPrefs.SetInt("money", money-cost);
            PlayerPrefs.SetInt(stat, 1);
        }
        else
        {
            description.text = "Insufficient Funds";
        }
    }
    public void ShowDesc()
    {
        int cost = upgradeCosts[stat];
        string desc = descriptions[stat];
        description.text = $"${cost}- {desc}";
    }
    public void ResetDesc()
    {
        description.text = "";
    }
}
