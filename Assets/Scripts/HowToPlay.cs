using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    private Button howToPlayButton;
    private GameObject howToPlayMenu;
    // Start is called before the first frame update
    void Start()
    {
        howToPlayButton = GetComponent<Button>();
        howToPlayMenu = transform.Find("How to Play Menu").gameObject;
        howToPlayButton.onClick.AddListener(EnableSettings);
        howToPlayMenu.GetComponentInChildren<Button>().onClick.AddListener(DisableSettings);
    }

    private void EnableSettings()
    {
        howToPlayMenu.SetActive(true);
    }

    private void DisableSettings()
    {
        howToPlayMenu.SetActive(false);
    }
}
