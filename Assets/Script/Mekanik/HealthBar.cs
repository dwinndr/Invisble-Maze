using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthScoring playerHealth;
    public UnityEngine.UI.Image totalHealthBar;
    public UnityEngine.UI.Image currentHealth;

    void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth1 / 10;
    }

    void Update()
    {
        currentHealth.fillAmount = playerHealth.currentHealth1 / 10;
    }
}
