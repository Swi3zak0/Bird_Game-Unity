using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { TripleShot, SpeedBoost, ExtraLife }
    public PowerUpType powerUpType;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("Brak SpriteRenderer.");
        }
        else
        {
            SetPowerUpColor();
        }
    }

    void SetPowerUpColor()
    {
        switch (powerUpType)
        {
            case PowerUpType.TripleShot:
                spriteRenderer.color = Color.red;
                break;
            case PowerUpType.SpeedBoost:
                spriteRenderer.color = Color.blue;
                break;
            case PowerUpType.ExtraLife:
                spriteRenderer.color = Color.green;
                break;
            default:
                spriteRenderer.color = Color.white;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                ApplyPowerUp(playerHealth);
                Destroy(gameObject);
            }
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    void ApplyPowerUp(PlayerHealth player)
    {
        switch (powerUpType)
        {
            case PowerUpType.TripleShot:
                player.ActivateTripleShot();
                break;
            case PowerUpType.SpeedBoost:
                player.ActivateSpeedBoost();
                break;
            case PowerUpType.ExtraLife:
                player.AddLife();
                break;
        }
    }
}
