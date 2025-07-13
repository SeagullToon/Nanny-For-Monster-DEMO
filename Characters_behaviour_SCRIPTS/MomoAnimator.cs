using UnityEngine;
using System.Collections;

public class MomoAnimator : MonoBehaviour
{
    public static MomoAnimator Instance { get; private set; }
    public Sprite idleSprite;
    public Sprite walkSprite;
    public Sprite hangingSprite;
    public Sprite jumpingSprite;

    public Sprite layingOnBedSprite;
    public bool CustomTransform;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetIdle() => sr.sprite = idleSprite;
    public void SetWalk() => sr.sprite = walkSprite;
    public void SetHanging() => sr.sprite = hangingSprite;
    public void SetJumping() => sr.sprite = jumpingSprite;

    public void SetLayingOnBed(bool flip = true)
    {
        sr.sprite = layingOnBedSprite;
        sr.flipX = flip;
    }
    public void MoveToPosition(Vector3 targetPosition, float duration)
    {
        StopAllCoroutines(); // остановить прошлое движение, если было
        StartCoroutine(MoveRoutine(targetPosition, duration));
    }

    private IEnumerator MoveRoutine(Vector3 target, float duration)
    {
        CustomTransform = false;
        SetWalk(); // переключаем на анимацию ходьбы
        Vector3 start = transform.position;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, time / duration);
            yield return null;
        }
        SetIdle(); // вернуться к idle
    }



    void Update()
    {
        if (!CustomTransform)
        {
            UpdateScaleByY();
            HandleDepthLayer();
        }
    }

    void UpdateScaleByY()
    {
        float centerY = -3.61f;
        float scaleMultiplier = 0.05f; // Подстрой по вкусу
        float distanceFromCenter = rb.position.y - centerY;
        float scaleFactor = 0.49f - (distanceFromCenter * scaleMultiplier);
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
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



    //---------------------------------All custom func-s-------------------------------

    public void MoveCustomSprite(Vector3 targetPosition, float duration)
    {
        StopAllCoroutines(); // остановить прошлое движение, если было
        StartCoroutine(MoveCustomSpriteRoutine(targetPosition, duration));
    }
    private IEnumerator MoveCustomSpriteRoutine(Vector3 target, float duration)
    {
        CustomTransform = true;
        sr.sortingOrder = 1346;
        SetLayingOnBed(false);
        Vector3 start = transform.position;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, time / duration);
            yield return null;
        }

        float scaleFactor = 0.49f;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

    }

    
    // for DemoEnd

    public void Momo_DemoEnd()
    {
        sr.transform.position=new Vector3(-2.163533f, -3.426376f, 0f);
    }


}
