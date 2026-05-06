using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class MovimientoPlayerTerceraPersona : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 6f;
    public float rotationSpeed = 12f;
    public float gravity = -9.81f;

    [Header("Cámara")]
    public Transform cameraTransform;

    [Header("Modelo y animación")]
    public Transform modelTransform;
    public Animator animator;

    [Header("NavMesh")]
    public float navMeshOffsetY = 0.5f;
    public float navMeshSearchRadius = 2f;

    private CharacterController controller;
    private Vector3 verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Mover();
        AjustarAlturaAlNavMesh();
    }

    void Mover()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(x, 0f, z).normalized;

        bool isMoving = inputDirection.magnitude > 0.1f;

        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }

        if (isMoving)
        {
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;
            moveDirection.Normalize();

            controller.Move(moveDirection * speed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            if (modelTransform != null)
            {
                modelTransform.rotation = Quaternion.Slerp(
                    modelTransform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
            else
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    void AjustarAlturaAlNavMesh()
    {
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, navMeshSearchRadius, NavMesh.AllAreas))
        {
            Vector3 targetPosition = transform.position;
            targetPosition.y = hit.position.y + navMeshOffsetY;

            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                15f * Time.deltaTime
            );

            verticalVelocity.y = 0f;
        }
    }
}