using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float pigeonSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        rb.MovePosition(transform.position + input * Time.deltaTime *pigeonSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Package")
        {
            StartCoroutine("Indicator");
            Destroy(collision.gameObject);
            int balance = PlayerPrefs.GetInt("money", 0);
            PlayerPrefs.SetInt("money", balance + 5);
        }
    }

    IEnumerator Indicator()
    {
        float timer = 0f;
        float duration = 1f;
        float randX = Random.Range(-3.0f, 3.0f);
        float randY = Random.Range(-2.0f, 2.0f);
        GameObject indicator = Instantiate((GameObject)Resources.Load("Prefabs/Indicator - Minigame"), new Vector3(transform.position.x + randX, transform.position.y + randY, transform.position.z), Quaternion.identity, FindObjectOfType<Canvas>().transform);
        while (indicator.GetComponent<Image>().color.a > 0)
        {
            indicator.GetComponent<Image>().color = new Color(1, 1, 1, 1 - (timer / duration));
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(indicator);
    }
}
