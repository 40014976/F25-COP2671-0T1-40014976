using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector2 camLimits;
    public float offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position;
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, -camLimits.x, camLimits.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, -camLimits.y, camLimits.y);
        desiredPosition.z = offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = RoundToPixel(transform.position, 16);
    }

    Vector3 RoundToPixel(Vector3 position, float pixelsPerUnit)
    {
        float unitsPerPixel = 1f / pixelsPerUnit;
        return new Vector3(Mathf.Round(position.x / unitsPerPixel) * unitsPerPixel, Mathf.Round(position.y / unitsPerPixel) * unitsPerPixel, position.z);
    }
}
