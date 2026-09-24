using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject winText;

    private void Start()
    {
        winText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        GameObject[] seeds = GameObject.FindGameObjectsWithTag("Seeds");

        if (seeds.Length == 0)
        {
            winText.SetActive(true);
        }
    }
}