using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Seed count
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int totalSeeds = 6;

    private int collectedSeeds = 0;

    // HP
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private int maxHP = 3;

    private int currentHP = 3;

    // Fail UI
    [SerializeField] private GameObject failText;

    private bool gameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;

        currentHP = maxHP;

        UpdateScoreText();
        UpdateHPText();

        if (failText != null)
        {
            failText.SetActive(false);
        }
    }

    public void CollectSeed()
    {
        if (gameOver)
            return;

        collectedSeeds++;
        UpdateScoreText();

        if (collectedSeeds >= totalSeeds)
        {
            Debug.Log("All Moon Seeds collected!");
        }
    }

    // Touch the seed which has the same color 
    public void HealPlayer()
    {
        if (gameOver)
            return;

        if (currentHP < maxHP)
        {
            currentHP++;
        }

        UpdateHPText();
    }

    // Touch the seed which has the different color 
    public void DamagePlayer()
    {
        if (gameOver)
            return;

        currentHP--;

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        UpdateHPText();

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;

        if (failText != null)
        {
            failText.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "Moon Seeds: " + collectedSeeds + " / " + totalSeeds;
        }
    }

    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text =
                "HP: " + currentHP + " / " + maxHP;
        }
    }
}