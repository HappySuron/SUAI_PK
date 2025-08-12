using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;    // Ссылка на игрока
    [SerializeField] private Vector3 offset;      // Смещение камеры относительно игрока
    [SerializeField] private float smoothTime = 0.15f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
