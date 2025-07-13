using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public Transform cameraTransform;         // Камера
    public float parallaxFactor = 0.5f;       // Чем меньше — тем медленнее движется

    private Vector3 previousCameraPos;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        previousCameraPos = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - previousCameraPos;
        transform.position += new Vector3(delta.x * parallaxFactor, 0f, 0f);
        previousCameraPos = cameraTransform.position;
    }
}
