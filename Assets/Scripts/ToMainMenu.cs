using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
    private Button mainMenuButton;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        mainMenuButton = GetComponent<Button>();
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // Update is called once per frame
    private void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        audioManager.StopMusic();
        // FindObjectOfType<AudioManager>().PlayMusic("Music_Main");
    }
}
