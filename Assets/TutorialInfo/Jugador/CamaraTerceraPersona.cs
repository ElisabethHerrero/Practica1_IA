using UnityEngine;

public class CamaraTerceraPersona : MonoBehaviour
{
    public Transform target;

    [Header("Posición cámara")]
    public float distance = 4f;
    public float height = 1.5f;
    public float lookHeight = 1.2f;

    [Header("Suavizado")]
    public float followSpeed = 10f;
    public float rotationSpeed = 150f;

    [Header("Colisiones cámara")]
    public LayerMask collisionLayers;
    public float cameraRadius = 0.3f;
    public float minDistance = 1f;

    private float yaw;

    void LateUpdate()
    {
        if (target == null) return;

        yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);

        Vector3 lookPoint = target.position + Vector3.up * lookHeight;

        Vector3 desiredPosition =
            target.position
            - rotation * Vector3.forward * distance
            + Vector3.up * height;

        Vector3 direction = desiredPosition - lookPoint;
        float desiredDistance = direction.magnitude;
        direction.Normalize();

        Vector3 finalPosition = desiredPosition;

        if (Physics.SphereCast(
            lookPoint,
            cameraRadius,
            direction,
            out RaycastHit hit,
            desiredDistance,
            collisionLayers
        ))
        {
            float correctedDistance = Mathf.Max(hit.distance - cameraRadius, minDistance);
            finalPosition = lookPoint + direction * correctedDistance;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            finalPosition,
            followSpeed * Time.deltaTime
        );

        transform.LookAt(lookPoint);
    }
}