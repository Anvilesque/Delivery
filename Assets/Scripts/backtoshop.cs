using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToShop : MonoBehaviour
{
    private Button backToShop;
    // Start is called before the first frame update
    void Start()
    {
        backToShop = GetComponent<Button>();
        backToShop.onClick.AddListener(ToShop);
    }
    private void ToShop()
    {
        SceneManager.LoadScene("Shop");
        FindObjectOfType<AudioManager>().PlayMusic("Music_Shop");
    }
}
