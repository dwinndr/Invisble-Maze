using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar2 : MonoBehaviour
{
    [SerializeField] private HealthScoring playerHealth2;
    public UnityEngine.UI.Image totalHealthBar2;
    public UnityEngine.UI.Image currentHealth2;

    void Start()
    {
        totalHealthBar2.fillAmount = playerHealth2.currentHealth2 / 10;
    }

    void Update()
    {
        currentHealth2.fillAmount = playerHealth2.currentHealth2 / 10;
    }
}
