using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PigeonMovement : MonoBehaviour
{
    private List<GuardDetect> guardList;
    private Vector2 pigeonVelocity;
    private float pigeonSpeed;
    private float baseSpeed;
    private float energy;
    private Rigidbody2D rb;
    public float durationTimer = 0f;
    private bool dashUnlocked;
    private bool boostUnlocked;
    private GetCooldown dashCooldown;
    private GetCooldown speedCooldown;
    private int packagesDelivered = 0;
    private List<int> packagesToDeliver = new List<int>{1,2,3};
    public float energyPercent = 1;
    private float maxEnergy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashCooldown = GameObject.Find("DashCooldown").GetComponent<GetCooldown>();
        speedCooldown = GameObject.Find("SpeedCooldown").GetComponent<GetCooldown>();
        guardList = new List<GuardDetect>(FindObjectsOfType<GuardDetect>());
        pigeonSpeed = (float)PlayerPrefs.GetInt("speed", 1)*0.4f + 3;
        energy = (float)PlayerPrefs.GetInt("energy", 1)*30f + 70;
        maxEnergy = energy;
        dashUnlocked = PlayerPrefs.HasKey("dash");
        boostUnlocked = PlayerPrefs.HasKey("boost");
        baseSpeed = pigeonSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GuardDetect guard in guardList)
            guard.detectDistance = 2f;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if(Input.GetButton("Dash") && dashUnlocked && dashCooldown.cooldownTimer <= 0 && durationTimer <= 0)
        {
            pigeonSpeed *= 4f;
            dashCooldown.cooldownTimer = dashCooldown.cooldownTime;
            durationTimer = 3*Time.deltaTime;
        }
        else if(Input.GetButton("SpeedBoost") && boostUnlocked && speedCooldown.cooldownTimer <= 0 && durationTimer <= 0)
        {
            pigeonSpeed *= 1.5f;
            durationTimer = 4f;
            speedCooldown.cooldownTimer = speedCooldown.cooldownTime;
        }
        else if(Input.GetButton("Sneak") && durationTimer <= 0) 
        {
            pigeonSpeed /= 2f;
            foreach (GuardDetect guard in guardList)
                guard.detectDistance = 1f;
        }
        energyPercent = energy/maxEnergy;
        rb.MovePosition(transform.position + input * Time.deltaTime *pigeonSpeed);
        if(durationTimer <= 0)
            pigeonSpeed = baseSpeed;
        if(energy > 0)
            energy -= Time.deltaTime *pigeonSpeed*1.5f;
        else
        {
            energy = 0;
            pigeonSpeed = 0;
        }
        durationTimer -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Receiver")
        {
            Destroy(collision.gameObject);
            packagesDelivered++;
            CheckLevelCompletion();
        }
    }
    
    void CheckLevelCompletion()
    {
        int level = PlayerPrefs.GetInt("level", 1);
        if (packagesDelivered == packagesToDeliver[level-1])
        {
            PlayerPrefs.SetInt("level", level + 1);
            int balance = PlayerPrefs.GetInt("money", 0);
            PlayerPrefs.SetInt("money", (int)(balance + level*100 + energy));
            SceneManager.LoadScene("Shop");
        }
    }

}
