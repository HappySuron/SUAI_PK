using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ChoiceClickHandler : MonoBehaviour, IPointerClickHandler
{
    private Action onClick;

    public void Init(Action onClick)
    {
        this.onClick = onClick;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}
