using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject winText;

    void Start()
    {
        if (winText != null)
        {
            winText.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerColorController player =
            other.GetComponentInParent<PlayerColorController>();

        if (player == null)
        {
            return;
        }

        GameObject[] seeds =
            GameObject.FindGameObjectsWithTag("Seeds");

        if (seeds.Length == 0)
        {
            if (winText != null)
            {
                winText.SetActive(true);
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.WinGame();
            }
        }
        else
        {
            Debug.Log(
                "Collect all Moon Seeds before using the exit!"
            );
        }
    }
}