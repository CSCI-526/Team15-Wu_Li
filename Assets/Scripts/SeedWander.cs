using UnityEngine;

public class SeedWander : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float moveRadius = 2f;
    [SerializeField] private float waitTime = 0.5f;

    [Header("Wall Detection")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float seedRadius = 0.25f;

    private Vector2 startingPosition;
    private Vector2 targetPosition;
    private float waitTimer;
    private MoonSeed moonSeed;

    void Start()
    {
        moonSeed = GetComponent<MoonSeed>();

        if (moonSeed == null)
        {
            Debug.LogError(
                gameObject.name + " needs a MoonSeed component."
            );

            enabled = false;
            return;
        }

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

        Vector2 currentPosition = transform.position;

        // Stop this movement if a different-colored wall is ahead.
        if (!PathIsAllowed(currentPosition, targetPosition))
        {
            ChooseNewTarget();
            return;
        }

        transform.position = Vector2.MoveTowards(
            currentPosition,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(
                transform.position,
                targetPosition
            ) < 0.05f)
        {
            waitTimer = waitTime;
            ChooseNewTarget();
        }
    }

    void ChooseNewTarget()
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            Vector2 randomOffset =
                Random.insideUnitCircle * moveRadius;

            Vector2 possibleTarget =
                startingPosition + randomOffset;

            if (PathIsAllowed(transform.position, possibleTarget))
            {
                targetPosition = possibleTarget;
                return;
            }
        }

        targetPosition = transform.position;
        waitTimer = waitTime;
    }

    bool PathIsAllowed(Vector2 start, Vector2 end)
    {
        Vector2 direction = end - start;
        float distance = direction.magnitude;

        if (distance <= 0.01f)
        {
            return true;
        }

        RaycastHit2D[] wallHits = Physics2D.CircleCastAll(
            start,
            seedRadius,
            direction.normalized,
            distance,
            wallLayer
        );

        foreach (RaycastHit2D hit in wallHits)
        {
            ColoredWall coloredWall =
                hit.collider.GetComponentInParent<ColoredWall>();

            // A normal wall has no color, so every seed is blocked.
            if (coloredWall == null)
            {
                return false;
            }

            // A different-colored wall blocks this seed.
            if (coloredWall.WallColor != moonSeed.SeedColor)
            {
                return false;
            }

            // A matching-colored wall is allowed.
        }

        return true;
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