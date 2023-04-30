using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetCooldown : MonoBehaviour
{
    private PigeonMovement pigeonMovement;
    private TextMeshProUGUI cooldownText;
    private Image imageCooldown;
    public float cooldownTimer;
    public float cooldownTime;

    // Start is called before the first frame update
    void Start()
    {
        pigeonMovement = GameObject.FindWithTag("Player").GetComponent<PigeonMovement>();
        cooldownText = GetComponentInChildren<TextMeshProUGUI>();
        imageCooldown = GetComponentsInChildren<Image>()[1];
        cooldownText.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cooldownText.text = Mathf.RoundToInt(cooldownTimer).ToString();
        imageCooldown.fillAmount = cooldownTimer/cooldownTime;
        if(cooldownTimer > 0)
            cooldownText.gameObject.SetActive(true);
        else
            cooldownText.gameObject.SetActive(false);
        cooldownTimer -= Time.deltaTime;
    }
}
