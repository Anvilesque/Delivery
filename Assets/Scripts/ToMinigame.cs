using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToMinigame : MonoBehaviour
{
    private Button toMinigame;
    // Start is called before the first frame update
    void Start()
    {
        toMinigame = GetComponent<Button>();
        toMinigame.onClick.AddListener(ToGame);
    }
    private void ToGame()
    {
        SceneManager.LoadScene("Packaging Minigame");
        FindObjectOfType<AudioManager>().PlayMusic("Music_Game");
    }
}