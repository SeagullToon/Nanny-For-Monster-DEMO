using UnityEngine;

public class MomoDragController : MonoBehaviour
{
    public MomoAnimator momoAnimator;
    public static MomoDragController Instance { get; private set; }

    public float floorY = -3.5f;
    public float dragBorderLeft = -13.67f;
    public float dragBorderRight = 12.48f;
    public float dragBorderTop = 2.5f;
    public float dragBorderBottom = -7.19f;

    private bool isDragging = false;
    private Camera mainCamera;
    private Vector3 offset;

    void Awake()
    {
        enabled = false;
        mainCamera = Camera.main;
    }

    public void EnableDrag()
    {
        enabled = true;
    }

    void OnMouseEnter()
    {
        Debug.Log("grab momo?");
    }

    void OnMouseDown()
    {
        Debug.Log("MouseDown detected");
        isDragging = true;
        momoAnimator.SetHanging();
        offset = transform.parent.position - GetMouseWorldPos(); // ← смещение родителя
    }

    void OnMouseUp()
    {
        Debug.Log("MouseUp detected");
        isDragging = false;
        momoAnimator.SetJumping();
        StartCoroutine(JumpToFloor());
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;
            targetPos.x = Mathf.Clamp(targetPos.x, dragBorderLeft, dragBorderRight);
            targetPos.y = Mathf.Clamp(targetPos.y, dragBorderBottom, dragBorderTop);
            targetPos.z = transform.parent.position.z;
            transform.parent.position = targetPos; // ← двигаем родителя
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = 10f;
        return mainCamera.ScreenToWorldPoint(screenPos);
    }

    System.Collections.IEnumerator JumpToFloor()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        Vector3 start = transform.parent.position;
        Vector3 end = new Vector3(start.x, floorY, start.z);

        while (elapsed < duration)
        {
            transform.parent.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.parent.position = end;
        momoAnimator.SetIdle();
    }
}
