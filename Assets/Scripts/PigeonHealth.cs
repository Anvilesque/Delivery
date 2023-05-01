using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PigeonHealth : MonoBehaviour
{
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GuardAttack.killPigeon)
        {
            GuardAttack.killPigeon = false;
            SceneManager.LoadScene("Shop");
            audioManager.PlayMusic("Music_Shop");
        }
    }
}
