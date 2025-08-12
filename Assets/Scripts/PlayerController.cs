using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private int[] interactLayers;
    [SerializeField] private Animator animator;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float wasdMoveDistance = 1f;

    private NavMeshAgent agent;
    private Interactable currentTarget;

    private bool isRotatingToTarget = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Мы сами крутим персонажа
    }

    private void Update()
    {
        HandleInput();
        HandleRotation();
        HandleInteraction();
        UpdateAnimator();
    }

    private void HandleInput()
    {
        Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (inputDir.sqrMagnitude >= 0.01f)
        {
            // При управлении с клавиатуры сбрасываем цель
            currentTarget = null;
            isRotatingToTarget = false;

            Vector3 camForward = cam.transform.forward;
            camForward.y = 0;
            camForward.Normalize();

            Vector3 camRight = cam.transform.right;
            camRight.y = 0;
            camRight.Normalize();

            Vector3 moveDir = (camForward * inputDir.z + camRight * inputDir.x).normalized;
            Vector3 targetPos = transform.position + moveDir * wasdMoveDistance;

            agent.isStopped = false;
            agent.SetDestination(targetPos);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (IsInLayers(hit.collider.gameObject))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        Debug.Log($"Кликнули по объекту: {interactable.gameObject.name}");
                        currentTarget = interactable;
                        isRotatingToTarget = false;
                        agent.isStopped = false;
                        agent.SetDestination(interactable.interactionPoint.position);
                    }
                    else
                    {
                        currentTarget = null;
                        isRotatingToTarget = false;
                        agent.isStopped = false;
                        agent.SetDestination(hit.point);
                    }
                }
            }
        }
    }

private void HandleInteraction()
{
    if (currentTarget == null)
        return;

    float dist = Vector3.Distance(transform.position, currentTarget.interactionPoint.position);

    // Если почти подошли к объекту
    if (dist <= 0.5f && !isRotatingToTarget)
    {
        agent.isStopped = true; // стопаем движение
        agent.velocity = Vector3.zero;
        if (currentTarget.shouldRotateTowards)
        {
            Debug.Log("Начали поворот");
            isRotatingToTarget = true; // включаем режим поворота
        }
        else
        {
            currentTarget.Interact();
            currentTarget = null;
        }
    }
}

private void HandleRotation()
{
    // Если мы должны повернуться к цели
    if (isRotatingToTarget && currentTarget != null)
    {
        Vector3 lookDir = currentTarget.transform.position - transform.position;
        lookDir.y = 0;

        if (lookDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime * 100f
            );

            // Когда почти повернулись — взаимодействуем
            if (Quaternion.Angle(transform.rotation, targetRotation) < 2f)
            {
                isRotatingToTarget = false;
                currentTarget.Interact();
                currentTarget = null;
            }
        }
    }
    // Обычный поворот при движении
    else if (agent.velocity.sqrMagnitude > 0.1f)
    {
        Vector3 direction = agent.velocity.normalized;
        direction.y = 0;
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime * 100f
            );
        }
    }
}

    private void UpdateAnimator()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("Speed", speedPercent);
    }

    private bool IsInLayers(GameObject obj)
    {
        int objLayer = obj.layer;
        foreach (var layer in interactLayers)
        {
            if (objLayer == layer)
                return true;
        }
        return false;
    }
}
