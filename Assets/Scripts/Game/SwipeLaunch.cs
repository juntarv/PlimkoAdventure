using UnityEngine;

public class SwipeLaunch : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool canLaunch = false;
    private bool isBallLaunched = false;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private Vector2 swipeDirection;

    public float maxLaunchForce = 20f;
    public LineRenderer trajectoryLineRenderer;
    public float startWidth = 5f;
    public float endWidth = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        trajectoryLineRenderer.positionCount = 2;
        trajectoryLineRenderer.enabled = false;
    }

    void Update()
    {
        if (!isBallLaunched)
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                touchStartPos = Input.mousePosition;
                canLaunch = true;
            }

            if ((Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) && canLaunch)
            {
                touchEndPos = Input.mousePosition;
                swipeDirection = touchEndPos - touchStartPos;
                DrawTrajectoryLine();
            }

            if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && canLaunch)
            {
                touchEndPos = Input.mousePosition;
                swipeDirection = touchEndPos - touchStartPos;
                rb.bodyType = RigidbodyType2D.Dynamic;
                float launchForce = Mathf.Clamp(swipeDirection.magnitude, 0f, maxLaunchForce);
                rb.AddForce(swipeDirection.normalized * launchForce, ForceMode2D.Impulse);
                canLaunch = false;
                isBallLaunched = true;
                trajectoryLineRenderer.enabled = false;
            }
        }
    }

    void DrawTrajectoryLine()
    {
        trajectoryLineRenderer.enabled = true;
        trajectoryLineRenderer.startWidth = startWidth;
        trajectoryLineRenderer.endWidth = endWidth;

        Vector3 startPos = transform.position;
        Vector3 endPos = (Vector3)transform.position + (Vector3)swipeDirection.normalized * Mathf.Clamp(swipeDirection.magnitude * 0.1f, 0f, maxLaunchForce);

        // Уменьшаем максимальную длину луча, например, до 50
        float maxLength = 3f;
        float distance = Vector3.Distance(transform.position, endPos);
        if (distance > maxLength)
        {
            endPos = transform.position + (endPos - transform.position).normalized * maxLength;
        }

        trajectoryLineRenderer.SetPosition(0, startPos);
        trajectoryLineRenderer.SetPosition(1, endPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Сбрасываем переменную isBallLaunched при столкновении с препятствием
        isBallLaunched = false;
    }
}







