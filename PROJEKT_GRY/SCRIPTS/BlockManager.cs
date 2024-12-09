using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BlockManager : MonoBehaviour
{
    public List<GameObject> birdPrefabs;
    public GameObject eggPrefab;
    public GameObject powerUpPrefab;
    public GameObject player;
    public GameObject heartPrefab;
    public Transform healthPanel;
    public GameObject restartButton;
    public int rows = 3;
    public int columns = 5;
    public float spacingX = 1.5f;
    public float spacingY = 2.0f;
    public Vector2 startOffset = new Vector2(-4.0f, 4.0f);
    public float dropInterval = 5f;
    public int eggsPerDrop = 1;

    private int totalBlocks;
    private float blockHeight;
    private int roundNumber = 1;
    private List<GameObject> blocks = new List<GameObject>();
    private int score = 0;

    [SerializeField] private TextMeshProUGUI scoreText;
    private PlayerHealth playerHealth;
    private AudioSource gameOverAudio;

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        if (birdPrefabs.Count > 0)
        {
            blockHeight = birdPrefabs[0].GetComponent<SpriteRenderer>().bounds.size.y;
        }

        gameOverAudio = GetComponent<AudioSource>();

        GenerateBlocks();
        StartCoroutine(DropEggRoutine());
        UpdateScoreUI();
        playerHealth.InitializeHealthUI(heartPrefab, healthPanel);

        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }
    }

    void GenerateBlocks()
    {
        foreach (Transform child in transform)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }

        totalBlocks = 0;
        blocks.Clear();
        Debug.Log("Round " + roundNumber);
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                Vector2 position = new Vector2(
                    startOffset.x + column * spacingX,
                    startOffset.y - row * (blockHeight * 0.3f)
                );
                GameObject randomBirdPrefab = birdPrefabs[Random.Range(0, birdPrefabs.Count)];
                GameObject newBlock = Instantiate(randomBirdPrefab, position, Quaternion.identity);
                newBlock.transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);
                newBlock.transform.parent = transform;
                blocks.Add(newBlock);
                totalBlocks++;
            }
        }
    }

    public void BlockDestroyed(Vector2 blockPosition)
    {
        totalBlocks--;
        int pointsEarned = CalculatePoints();
        score += pointsEarned;
        UpdateScoreUI();

        float dropChance = Random.value;
        if (dropChance <= 0.2f)
        {
            PowerUp.PowerUpType powerUpType = (PowerUp.PowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUp.PowerUpType)).Length);
            GameObject powerUp = Instantiate(powerUpPrefab, blockPosition, Quaternion.identity);
            powerUp.GetComponent<PowerUp>().powerUpType = powerUpType;
        }

        if (totalBlocks <= 0)
        {
            roundNumber++;
            StartCoroutine(StartNextRound());
        }
    }

    int CalculatePoints()
    {
        if (roundNumber >= 1 && roundNumber <= 5)
        {
            return 100;
        }
        else if (roundNumber > 5 && roundNumber <= 10)
        {
            return 200;
        }
        else
        {
            return 500;
        }
    }

    IEnumerator StartNextRound()
    {
        yield return new WaitForSeconds(3f);
        GenerateBlocks();
        dropInterval = Mathf.Max(1f, dropInterval - 0.5f);
        eggsPerDrop = Mathf.Min(8, eggsPerDrop + 1);
    }

    IEnumerator DropEggRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(dropInterval);

            if (blocks.Count > 0)
            {
                for (int i = 0; i < eggsPerDrop; i++)
                {
                    int randomIndex = Random.Range(0, blocks.Count);
                    GameObject randomBlock = blocks[randomIndex];

                    if (randomBlock != null)
                    {
                        GameObject egg = Instantiate(eggPrefab, randomBlock.transform.position, Quaternion.identity);
                        egg.GetComponent<Egg>().SetBlockManager(this);
                    }
                }
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("Koniec gry! Twój wynik: " + score);

        if (gameOverAudio != null)
        {
            gameOverAudio.Play();
            Debug.Log("Muzyka przegranej została odtworzona.");
        }
        else
        {
            Debug.LogWarning("Brak przypisanego Audio.");
        }

        if (restartButton != null)
        {
            restartButton.SetActive(true);
            Debug.Log("Przycisk Restart został aktywowany.");
        }
        else
        {
            Debug.LogWarning("RestartButton nie jest przypisany.");
        }

        StartCoroutine(PauseGameAfterUI());
    }

    IEnumerator PauseGameAfterUI()
    {
        yield return new WaitForEndOfFrame();
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
