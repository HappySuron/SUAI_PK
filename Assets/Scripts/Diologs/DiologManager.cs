using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    [Header("UI")]
    public TMP_Text npcTextPrefab;
    public TMP_Text choiceTextPrefab;
    public RectTransform contentArea;

    // Метод для показа текста и вариантов выбора
    public void ShowTextWithChoices(string text, List<GreenCube.Choice> choices)
    {
        ClearContent();
        // Debug.Log("SETTED");
        // Показываем текст NPC
        TMP_Text npcLine = Instantiate(npcTextPrefab, contentArea);
        npcLine.text = text;

        // Показываем кнопки выбора
        foreach (var choice in choices)
        {
            TMP_Text choiceLine = Instantiate(choiceTextPrefab, contentArea);
            choiceLine.text = choice.text;

            var clickHandler = choiceLine.gameObject.AddComponent<ChoiceClickHandler>();
            clickHandler.Init(() =>
            {
                choice.action.Invoke(); // выполняем действие выбора
            });
        }
    }

    private void ClearContent()
    {
        foreach (Transform child in contentArea)
            Destroy(child.gameObject);
    }
}
