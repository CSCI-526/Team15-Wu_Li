using System.Collections.Generic;
using UnityEngine;

public class ColoredWall : MonoBehaviour
{
    [SerializeField] private MoonColor wallColor = MoonColor.Blue;
    [SerializeField] private SpriteRenderer wallSprite;

    [Header("Colliders")]
    [SerializeField] private Collider2D solidCollider;
    [SerializeField] private Collider2D detectionTrigger;

    private HashSet<Collider2D> damagedPlayers =
        new HashSet<Collider2D>();

    public MoonColor WallColor
    {
        get { return wallColor; }
    }

    void Start()
    {
        if (wallSprite == null)
        {
            wallSprite = GetComponent<SpriteRenderer>();
        }

        UpdateWallColor();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerColorController player =
            other.GetComponentInParent<PlayerColorController>();

        if (player == null)
        {
            return;
        }

        bool colorsMatch = player.CurrentColor == wallColor;

        Physics2D.IgnoreCollision(
            solidCollider,
            other,
            colorsMatch
        );

        if (colorsMatch && !damagedPlayers.Contains(other))
        {
            damagedPlayers.Add(other);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.DamagePlayer();
            }

            Debug.Log("Passed through matching wall. HP -1.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerColorController player =
            other.GetComponentInParent<PlayerColorController>();

        if (player == null)
        {
            return;
        }

        Physics2D.IgnoreCollision(
            solidCollider,
            other,
            false
        );

        damagedPlayers.Remove(other);
    }

    public bool CanSeedPass(MoonColor seedColor)
    {
        return seedColor == wallColor;
    }

    void UpdateWallColor()
    {
        if (wallSprite == null)
        {
            return;
        }

        switch (wallColor)
        {
            case MoonColor.Blue:
                wallSprite.color = Color.blue;
                break;

            case MoonColor.Red:
                wallSprite.color = Color.red;
                break;

            case MoonColor.Yellow:
                wallSprite.color = Color.yellow;
                break;
        }
    }
}