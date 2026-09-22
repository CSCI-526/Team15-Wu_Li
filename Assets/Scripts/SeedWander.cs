using UnityEngine;

public class SeedWander : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float moveRadius = 2f;
    [SerializeField] private float waitTime = 0.5f;

    private Vector2 startingPosition;
    private Vector2 targetPosition;
    private float waitTimer;

    void Start()
    {
        startingPosition = transform.position;
        ChooseNewTarget();
    }

    void Update()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPosition) < 0.05f)
        {
            waitTimer = waitTime;
            ChooseNewTarget();
        }
    }

    void ChooseNewTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * moveRadius;
        targetPosition = startingPosition + randomOffset;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 center = Application.isPlaying
            ? startingPosition
            : transform.position;

        Gizmos.DrawWireSphere(center, moveRadius);
    }
}