using UnityEngine;
using System.Collections.Generic;

public class GreenCube : Interactable
{
    // Структура для варианта выбора
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

    // Текущий текст
    private string currentText;
    // Текущие варианты выбора
    private List<Choice> currentChoices;

    public override void Interact()
    {
        base.Interact();
        ShowNodeInitial();
    }

    private void ShowNodeInitial()
    {
        currentText = "Куб стоит";
        currentChoices = new List<Choice>
        {
            new Choice("Привет куб", ShowNodeHello),
            new Choice("Куб ты отстой", ShowNodeBad)
        };

        DialogueManager.Instance.ShowTextWithChoices(currentText, currentChoices);
    }

    private void ShowNodeHello()
    {
        currentText = "Куб стоит";
        currentChoices = new List<Choice>
        {
            new Choice("Привет куб", ShowNodeHello),
            new Choice("Куб ты отстой", ShowNodeBad)
        };

        DialogueManager.Instance.ShowTextWithChoices(currentText, currentChoices);
    }

    private void ShowNodeBad()
    {
        currentText = "Куб отстой";
        currentChoices = new List<Choice>
        {
            new Choice("Оставить куб", ShowNodeInitial)
        };

        DialogueManager.Instance.ShowTextWithChoices(currentText, currentChoices);
    }
}
