using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class GetEnergy : MonoBehaviour
{
    private PigeonMovement pigeonMovement;
    private Image energyBar;
    private GameObject outOfEnergy;
    private bool loading = false;

    // Start is called before the first frame update
    void Start()
    {
        pigeonMovement = GameObject.FindWithTag("Player").GetComponent<PigeonMovement>();
        energyBar = GetComponent<Image>();
        energyBar.fillAmount = 1.0f;
        outOfEnergy = GameObject.Find("Out of Energy");
        outOfEnergy.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        energyBar.fillAmount = pigeonMovement.energyPercent;
        if(pigeonMovement.energyPercent == 0 && !loading)
        {
            outOfEnergy.SetActive(true);
            loading = true;
            StartCoroutine("LoadShop");
        }
    }

    IEnumerator LoadShop()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Shop");
    }
}
