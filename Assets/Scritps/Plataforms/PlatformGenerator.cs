using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int initialPlatforms = 10;
    [SerializeField] private float minX = -3f, maxX = 3f;
    [SerializeField] private float minY = 2f, maxY = 4f;
    [SerializeField] private float minDistance = 2.5f;

    [Header("Moving Platforms")]
    [SerializeField] private float movingPlatformChance = 0.3f;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float moveSpeed = 1f;

    private Vector3 lastPlatformPosition;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        lastPlatformPosition = transform.position;

        GeneratePlatform(transform.position.x, transform.position.y - 2f);

        for (int i = 0; i < initialPlatforms; i++)
        {
            GeneratePlatform();
        }
    }

    private void Update()
    {
        if (mainCamera.WorldToViewportPoint(lastPlatformPosition).y < 0.8f)
        {
            GeneratePlatform();
        }
    }

    private void GeneratePlatform(float specificX = float.NaN, float specificY = float.NaN)
    {
        Vector3 newPosition;

        if (!float.IsNaN(specificX) && !float.IsNaN(specificY))
        {
            newPosition = new Vector3(specificX, specificY, 0);
        }
        else
        {
            float newX = Random.Range(minX, maxX);
            float newY = lastPlatformPosition.y + Random.Range(minY, maxY);

            int attempts = 0;
            do
            {
                newX = Random.Range(minX, maxX);
                attempts++;
            } while (attempts < 100 && Mathf.Abs(newX - lastPlatformPosition.x) < minDistance);

            newPosition = new Vector3(newX, newY, 0);
        }

        GameObject platform = Instantiate(platformPrefab, newPosition, Quaternion.identity, transform);
        lastPlatformPosition = newPosition;

        platform.tag = "Plataform";

        platform.layer = LayerMask.NameToLayer("Plataform");

        if (Random.value < movingPlatformChance)
        {
            var movingPlatform = platform.AddComponent<MovingPlatform>();
            movingPlatform.Initialize(moveDistance, moveSpeed, Random.value > 0.5f);
        }
    }
}


public class MovingPlatform : MonoBehaviour
{
    private float moveDistance;
    private float moveSpeed;
    private bool moveHorizontal;
    private Vector3 startPosition;

    public void Initialize(float distance, float speed, bool horizontal)
    {
        moveDistance = distance;
        moveSpeed = speed;
        moveHorizontal = horizontal;
        startPosition = transform.position;
    }

    private void Update()
    {
        if (moveHorizontal)
        {
            transform.position = new Vector3(
                startPosition.x + Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance,
                transform.position.y,
                transform.position.z
            );
        }
        else
        {
            transform.position = new Vector3(
                transform.position.x,
                startPosition.y + Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance,
                transform.position.z
            );
        }
    }
}