using UnityEngine;
using System.Collections;

public class PumpkinAnimator : MonoBehaviour
{
    public static PumpkinAnimator Instance { get; private set; }
    public Sprite idleSprite;
    public Sprite walkSprite;

    public bool CustomTransform;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    public Trigger_Pumpkin trigger_Pumpkin;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetIdle() => sr.sprite = idleSprite;
    public void SetWalk() => sr.sprite = walkSprite;
    public void MoveToPosition(Vector3 targetPosition, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(MoveRoutine(targetPosition, duration));
    }

    public IEnumerator MoveRoutine(Vector3 target, float duration)
    {
        CustomTransform = false;
        SetWalk();

        Vector3 start = transform.position;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, time / duration);
            yield return null;
        }

        SetIdle();
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
        float scaleMultiplier = 0.05f;
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

    // // shoo shoo from bed
    public IEnumerator PumpkinMovesAwayRoutine()
    {
        Debug.Log("ask pumpkin to move away from bed");

        // После этого тыковка уходит с места и освобождает триггер у кровати
        sr.flipX = false;
        SetIdle();
        Debug.Log("Тыковка тянется");

        yield return new WaitForSeconds(2f);

        MoveToPosition(new Vector3(0.2264671f, -3.286376f, -0.6f), 2f);

        yield return new WaitForSeconds(2f);
        trigger_Pumpkin.move_once = true;

    }

    // sitting on a bed start point for DemoEnd

    public void Pumpkin_DemoEnd()
    {
        CustomTransform = true;
        sr.transform.position = new Vector2(6.586467f, -1.626376f);
        sr.sortingOrder = 1346;
    }

 
}
