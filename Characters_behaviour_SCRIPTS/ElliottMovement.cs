using UnityEngine;

public class ElliottMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float scaleMultiplier = 0.05f;

    // Для замены спрайтов
    public Sprite idleRight;
    public Sprite idleLeft;
    public Sprite walkRight;
    public Sprite walkLeft;

    private Vector3 originalScale;
    private SpriteRenderer sr;
    
    // private float lastMoveX = 1f; // 1 = вправо, -1 = влево
    private float lastHorizontalDir = 1f; // по умолчанию вправо

    // Для отслеживания направления
    private Vector2 lastDirection;
    private Rigidbody2D rb;
    public bool canMove = true; // Управление можно отключать извне



    private void Start()
    {

        originalScale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();


    }

    private void HandleDepthLayer()
    {
        // float bedDepthSwitchY = -4.2f; // Порог Y, можно подстроить
        // float frontZ = -1f;  // Перед кроватью (ближе к камере)
        // float backZ = 0f;    // За кроватью (дальше)

        // Vector3 pos = transform.position;
        // pos.z = (pos.y < bedDepthSwitchY) ? frontZ : backZ;
        // transform.position = pos;

        int sortingBase = 1000;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = sortingBase - Mathf.RoundToInt(transform.position.y * 100);

    }


    void FixedUpdate()
    {

        // Этот блок работает всегда (масштаб + слой по глубине)
        HandleDepthLayer();
        // === Масштабирование (можно оставить здесь или в Update, зависит от эффекта)
        float centerY = -3.61f;
        float distanceFromCenter = rb.position.y - centerY;
        float scaleFactor = 0.49f - (distanceFromCenter * scaleMultiplier);
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);


        if (canMove)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            Vector2 moveDir = new Vector2(moveX, moveY).normalized;
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }




    }


    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.magnitude > 0.01f)
        {
            lastDirection = moveDir;

            if (moveX > 0)
            {
                sr.sprite = walkRight;
                lastHorizontalDir = 1f;
            }
            else if (moveX < 0)
            {
                sr.sprite = walkLeft;
                lastHorizontalDir = -1f;
            }
            else
            {
                if (lastHorizontalDir > 0)
                    sr.sprite = walkRight;
                else
                    sr.sprite = walkLeft;
            }
        }
        else
        {
            if (lastDirection.x > 0 || lastHorizontalDir > 0)
                sr.sprite = idleRight;
            else
                sr.sprite = idleLeft;
        }
    }


    
}
