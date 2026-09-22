using UnityEngine;

public enum MoonColor
{
    Blue,
    Red,
    Yellow
}

public class PlayerColorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private MoonColor currentColor = MoonColor.Blue;

    public MoonColor CurrentColor
    {
        get { return currentColor; }
    }

    void Start()
    {
        UpdatePlayerColor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        switch (currentColor)
        {
            case MoonColor.Blue:
                currentColor = MoonColor.Red;
                break;

            case MoonColor.Red:
                currentColor = MoonColor.Yellow;
                break;

            case MoonColor.Yellow:
                currentColor = MoonColor.Blue;
                break;
        }

        UpdatePlayerColor();
    }

    void UpdatePlayerColor()
    {
        switch (currentColor)
        {
            case MoonColor.Blue:
                playerSprite.color = Color.blue;
                break;

            case MoonColor.Red:
                playerSprite.color = Color.red;
                break;

            case MoonColor.Yellow:
                playerSprite.color = Color.yellow;
                break;
        }
    }
}