using UnityEngine;
using System.Collections.Generic;


public static class DialogueColors
{
    public const string Red = "#FF0000";
    public const string Green = "#00FF00";
    public const string Blue = "#0000FF";
    public const string SUAI_red = "#E4003A";
    public const string SUAI_purple = "#ab3a8d";
    public const string SUAI_blue = "#005aaa";
    public const string SUAI_cyan = "#00b8ee";
    }

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

    protected void ResetCurrentText()
    {
        currentTexts.Clear();
    }

    // Метод, который вызывается при взаимодействии — переопределяй в дочерних классах
    public virtual void Interact()
    {
        Debug.Log($"Interacted with {gameObject.name}");
    }
}
