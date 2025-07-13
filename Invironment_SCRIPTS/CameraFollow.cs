using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Сюда перетащим персонажа (Elliott)
    public float smoothSpeed = 0.125f;
public Vector2 minBounds = new Vector2(-1495f, 0f);
public Vector2 maxBounds = new Vector2(1495f, 0f);


    public float camHalfWidth;
    public float camHalfHeight;

    private void Start()
    {
        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Ограничиваем координаты камеры в пределах границ
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}

