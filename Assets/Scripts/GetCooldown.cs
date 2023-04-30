using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetCooldown : MonoBehaviour
{
    private TextMeshProUGUI cooldownText;
    private Image cooldownImage;
    private Image abilityImage;
    public float cooldownTimer;
    public float cooldownTime;
    private bool abilityUnlocked;
    public string ability;

    // Start is called before the first frame update
    void Start()
    {
        abilityUnlocked = PlayerPrefs.HasKey(ability);
        cooldownText = GetComponentInChildren<TextMeshProUGUI>();
        abilityImage = GetComponent<Image>();
        cooldownImage = GetComponentsInChildren<Image>()[1];
        cooldownText.gameObject.SetActive(false);
        cooldownImage.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!abilityUnlocked)
        {
            abilityImage.color = new Color32(128,128,128,100);
            return;
        }
        cooldownText.text = Mathf.RoundToInt(cooldownTimer).ToString();
        cooldownImage.fillAmount = cooldownTimer/cooldownTime;
        if(cooldownTimer > 0)
            cooldownText.gameObject.SetActive(true);
        else
            cooldownText.gameObject.SetActive(false);
        cooldownTimer -= Time.deltaTime;
    }
}
