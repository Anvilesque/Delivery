using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private Button settingsButton;
    // Start is called before the first frame update
    void Start()
    {
        settingsButton = GetComponent<Button>();
        settingsButton.onClick.AddListener(GoToSettings);
    }
    private void GoToSettings()
    {
        // TODO
    }
}
