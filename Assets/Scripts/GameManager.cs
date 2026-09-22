using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int totalSeeds = 3;

    private int collectedSeeds = 0;

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
        UpdateScoreText();
    }

    public void CollectSeed()
    {
        collectedSeeds++;
        UpdateScoreText();

        if (collectedSeeds >= totalSeeds)
        {
            Debug.Log("All Moon Seeds collected!");
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "Moon Seeds: " + collectedSeeds + " / " + totalSeeds;
        }
    }
}