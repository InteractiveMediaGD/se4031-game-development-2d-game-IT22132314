using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int HealingAmount = 25; // How much health to give?

    [Header("Animation Settings")]
    public float FloatAmplitude = 0.25f; // How far up/down?
    public float FloatSpeed = 1.5f;      // How fast?
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        // Simple sine wave for floating effect
        float newY = startY + (Mathf.Sin(Time.time * FloatSpeed) * FloatAmplitude);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                // Only pick up if we actually need health!
                if (player.CurrentLifePoints < player.LifePoints)
                {
                    player.RecoverHealth(HealingAmount);
                    
                    // Add a tiny bit of score for being healthy!
                    if (ScoreManager.Instance != null) {
                        ScoreManager.Instance.AddScore(50);
                    }
                    
                    // Destroy the pack so it can't be reused
                    Destroy(gameObject);
                }
            }
        }
    }
}
