using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform interactionPoint; // точка, куда бежит персонаж
    public bool shouldRotateTowards = true;

    // Метод, который вызывается при взаимодействии — переопределяй в дочерних классах
    public virtual void Interact()
    {
        Debug.Log($"Interacted with {gameObject.name}");
    }
}
