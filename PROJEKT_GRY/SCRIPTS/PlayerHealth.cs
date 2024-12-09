using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public bool tripleShotActive = false;
    private float tripleShotDuration = 5.0f;
    private float speedBoostMultiplier = 3.0f;
    private float normalSpeed = 5f;
    private float currentSpeed;
    private Transform healthPanel;
    public GameObject heartPrefab;

    private BlockManager blockManager;

    void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = normalSpeed;

        blockManager = Object.FindFirstObjectByType<BlockManager>();
    }

    public void PlayerHit()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            Debug.Log("Gracz został trafiony! Pozostałe zdrowie: " + currentHealth);
            UpdateHealthUI();

            if (currentHealth <= 0)
            {
                UpdateHealthUI();
                if (blockManager != null)
                {
                    blockManager.GameOver();
                }
            }
        }
    }

    public void InitializeHealthUI(GameObject heartPrefab, Transform panel)
    {
        healthPanel = panel;
        this.heartPrefab = heartPrefab;

        for (int i = 0; i < maxHealth; i++)
        {
            Instantiate(heartPrefab, healthPanel);
        }
    }

    void UpdateHealthUI()
    {
        int currentHeartsInPanel = healthPanel.childCount;
        if (currentHeartsInPanel < currentHealth)
        {
            Instantiate(heartPrefab, healthPanel);
        }
        else if (currentHeartsInPanel > currentHealth)
        {
            Destroy(healthPanel.GetChild(healthPanel.childCount - 1).gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        if (!tripleShotActive)
        {
            tripleShotActive = true;
            Debug.Log("Potrójny strzał aktywowany!");
            StartCoroutine(TripleShotDuration());
        }
    }

    IEnumerator TripleShotDuration()
    {
        yield return new WaitForSeconds(tripleShotDuration);
        tripleShotActive = false;
        Debug.Log("Potrójny strzał zakończony.");
    }

    public void ActivateSpeedBoost()
    {
        currentSpeed = normalSpeed * speedBoostMultiplier;
        Debug.Log("Szybsze poruszanie aktywowane!");
    }

    public void AddLife()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            Debug.Log("Dodatkowe życie! Aktualne zdrowie: " + currentHealth);
            UpdateHealthUI();
        }
        else
        {
            Debug.Log("Zdrowie jest już na maksymalnym poziomie.");
        }
    }
}
