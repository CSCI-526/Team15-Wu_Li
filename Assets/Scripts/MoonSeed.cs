using UnityEngine;

public class MoonSeed : MonoBehaviour
{
    [SerializeField] private MoonColor seedColor = MoonColor.Blue;

    private SpriteRenderer seedSprite;
    private bool collected = false;

    void Start()
    {
        seedSprite = GetComponent<SpriteRenderer>();
        UpdateSeedColor();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Seed touched by: " + other.gameObject.name);

        PlayerColorController player =
            other.GetComponentInParent<PlayerColorController>();

        if (player == null)
        {
            Debug.Log("The touching object is not the player.");
            return;
        }

        if (collected)
        {
            return;
        }

        if (player.CurrentColor == seedColor)
        {
            collected = true;
            Debug.Log("Correct color! Seed collected.");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.CollectSeed();
            }
            else
            {
                Debug.LogWarning("GameManager was not found.");
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log(
                "Wrong color. Player: " + player.CurrentColor
                + ", Seed: " + seedColor
            );
        }
    }

    void UpdateSeedColor()
    {
        if (seedSprite == null)
        {
            return;
        }

        switch (seedColor)
        {
            case MoonColor.Blue:
                seedSprite.color = Color.blue;
                break;

            case MoonColor.Red:
                seedSprite.color = Color.red;
                break;

            case MoonColor.Yellow:
                seedSprite.color = Color.yellow;
                break;
        }
    }
}