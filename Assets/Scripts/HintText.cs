using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintText : MonoBehaviour
{
    public string hint;
    private TextMeshProUGUI hintText;
    // Start is called before the first frame update
    void Start()
    {
        hintText = GetComponent<TextMeshProUGUI>();
        hintText.text = hint;
    }
}
