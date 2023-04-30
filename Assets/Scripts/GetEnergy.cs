using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetEnergy : MonoBehaviour
{
    private PigeonMovement pigeonMovement;
    private Image energyBar;

    // Start is called before the first frame update
    void Start()
    {
        pigeonMovement = GameObject.FindWithTag("Player").GetComponent<PigeonMovement>();
        energyBar = GetComponent<Image>();
        energyBar.fillAmount = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        energyBar.fillAmount = pigeonMovement.energyMultiplier;
    }
}
