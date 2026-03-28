using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int Damage = 1; // Lower default so it doesn't kill in one hit

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ReceiveDamage(Damage, transform.position.x);
            }
        }
    }
}
