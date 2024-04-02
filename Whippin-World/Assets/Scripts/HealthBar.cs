using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        slider.maxValue = gameManager.PlayerHealth;
        slider.value = gameManager.PlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameOver)
        {
            slider.value = gameManager.PlayerHealth;
        }
    }
}
