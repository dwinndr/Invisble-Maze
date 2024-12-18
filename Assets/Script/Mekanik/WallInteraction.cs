using UnityEngine;
using UnityEngine.EventSystems;

public class WallInteraction : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerMovement.Instance.currentPlayer == 1)
            {

                Debug.Log("Tertabrak 1");
                HealthScoring.instance.TakeDamage1(1);
            }
            else
            {
                Debug.Log("Tertabrak 2");
                HealthScoring.instance.TakeDamage2(1);
            }
        }
    }
}
