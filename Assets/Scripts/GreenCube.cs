using UnityEngine;
using System.Collections.Generic;

public class GreenCube : Interactable
{
    // Структура для варианта выбора


    // Текущий текст


    public override void Interact()
    {
        base.Interact();
        DialogueManager.Instance.ShowDiologWindow();
        ShowNodeInitial();
    }

    private void ShowNodeInitial()
    {
        
        this.AddTextBlock($"Куб <b>стоит</b>, а ты <i>смотришь</i>. Куб <color={DialogueColors.Green}>зеленый</color>\n\n");
        this.currentChoices = new List<Choice>
        {
            new Choice($"1. <color={DialogueColors.SUAI_cyan}>Привет куб</color>", ShowNodeHello),
            new Choice("2. Куб ты отстой", ShowNodeBad)
        };

        DialogueManager.Instance.ShowTextWithChoices(GetFullText(), currentChoices);
    }

    private void ShowNodeHello()
    {
        Debug.Log("Said Hello to the cube");
        AddTextBlock("Привет куб\n\n");
        AddTextBlock("Куб стоит\n\n");
        currentChoices = new List<Choice>
        {
            new Choice("1. Привет куб", ShowNodeHello),
            new Choice("2. Куб ты отстой", ShowNodeBad)
        };

        DialogueManager.Instance.ShowTextWithChoices(GetFullText(), currentChoices);
    }

    private void ShowNodeBad()
    {
        Debug.Log("Said FU to cube");
        AddTextBlock("Куб явно взгруснул\n\n");
        currentChoices = new List<Choice>
        {
            new Choice("1. Оставить куб", () =>
            {
                // при выборе сразу выходим
                ResetCurrentText();
                DialogueManager.Instance.HideDiologWindow();
            })
        };

        DialogueManager.Instance.ShowTextWithChoices(GetFullText(), currentChoices);
    }
}
