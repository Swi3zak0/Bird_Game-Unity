using UnityEngine;

public class Egg : MonoBehaviour
{
    private BlockManager blockManager;
    private PlayerHealth playerHealth;

    public void SetBlockManager(BlockManager manager)
    {
        blockManager = manager;
    }

    private void Start()
    {
        playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.PlayerHit();
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
