using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Seed Count")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int totalSeeds = 6;

    private int collectedSeeds = 0;

    [Header("Player HP")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private int maxHP = 3;

    private int currentHP;

    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float startingTime = 60f;

    private float timeRemaining;

    [Header("Game Over")]
    [SerializeField] private GameObject failText;

    private bool gameOver = false;

    public bool GameIsOver
    {
        get { return gameOver; }
    }

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
        timeRemaining = startingTime;

        UpdateScoreText();
        UpdateHPText();
        UpdateTimerText();

        if (failText != null)
        {
            failText.SetActive(false);
        }
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            UpdateTimerText();
            LoseGame();
            return;
        }

        UpdateTimerText();
    }

    public void CollectSeed()
    {
        if (gameOver)
        {
            return;
        }

        collectedSeeds++;
        UpdateScoreText();

        if (collectedSeeds >= totalSeeds)
        {
            Debug.Log("All Moon Seeds collected! Go to the exit.");
        }
    }

    public void HealPlayer()
    {
        if (gameOver)
        {
            return;
        }

        if (currentHP < maxHP)
        {
            currentHP++;
        }

        UpdateHPText();
    }

    public void DamagePlayer()
    {
        if (gameOver)
        {
            return;
        }

        currentHP--;

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        UpdateHPText();

        if (currentHP <= 0)
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        if (gameOver)
        {
            return;
        }

        gameOver = true;
        Time.timeScale = 0f;

        Debug.Log("You win!");
    }

    void LoseGame()
    {
        if (gameOver)
        {
            return;
        }

        gameOver = true;

        if (failText != null)
        {
            failText.SetActive(true);
        }

        Time.timeScale = 0f;

        Debug.Log("You lose!");
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

    void UpdateTimerText()
    {
        if (timerText == null)
        {
            return;
        }

        int displayedTime = Mathf.CeilToInt(timeRemaining);
        int minutes = displayedTime / 60;
        int seconds = displayedTime % 60;

        timerText.text =
            "Time: "
            + minutes.ToString("0")
            + ":"
            + seconds.ToString("00");
    }
}