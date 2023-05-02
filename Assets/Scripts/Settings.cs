using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private Button settingsButton;
    private GameObject settingsMenu;
    // Start is called before the first frame update
    void Start()
    {
        settingsButton = GetComponent<Button>();
        settingsMenu = transform.Find("Settings Menu").gameObject;
        settingsButton.onClick.AddListener(EnableSettings);
        settingsMenu.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("Volume", 0.2f);
        settingsMenu.GetComponentInChildren<Slider>().onValueChanged.AddListener(UpdateVolume);
        settingsMenu.transform.Find("X Button").GetComponent<Button>().onClick.AddListener(DisableSettings);
    }
    private void EnableSettings()
    {
        settingsMenu.SetActive(true);
    }

    private void DisableSettings()
    {
        settingsMenu.SetActive(false);
    }

    private void UpdateVolume(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        Music currentSong = FindObjectOfType<AudioManager>().currentSong;
        if (currentSong != null) currentSong.source.volume = value;
    }
}
