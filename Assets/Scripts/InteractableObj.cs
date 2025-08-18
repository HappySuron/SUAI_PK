using UnityEngine;
using System.Collections.Generic;


public class Interactable : MonoBehaviour
{
    public Transform interactionPoint; // точка, куда бежит персонаж
    public bool shouldRotateTowards = true;



    protected List<string> currentTexts = new List<string>();
    // Текущие варианты выбора
    protected List<Choice> currentChoices;
    
    public class Choice
    {
        public string text;
        public System.Action action;

        public Choice(string t, System.Action a)
        {
            text = t;
            action = a;
        }
    }

    protected void AddTextBlock(string text)
    {
        currentTexts.Add(text);
    }

    // Получаем полный текст для отображения
    protected string GetFullText()
    {
        return string.Join("\n", currentTexts);
    }

    // Метод, который вызывается при взаимодействии — переопределяй в дочерних классах
    public virtual void Interact()
    {
        Debug.Log($"Interacted with {gameObject.name}");
    }
}
