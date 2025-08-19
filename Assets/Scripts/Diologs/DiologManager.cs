using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


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
    public GameObject panel;


    [Header("Audio")]
    public AudioSource audioSource;
    //public AudioClip defaultDialogueClip; // звук по умолчанию
    public AudioClip[] dialogueClips; 
    public bool randomizePitch = true;    // чтобы звучало "живее"
    public Vector2 pitchRange = new Vector2(0.9f, 1.1f);

    // Метод для показа текста и вариантов выбора
    public void ShowTextWithChoices(string text, List<GreenCube.Choice> choices)
    {
        ClearContent();
        // Debug.Log("SETTED");
        // Показываем текст NPC
        TMP_Text npcLine = Instantiate(npcTextPrefab, contentArea);
        npcLine.text = text;

        PlayDialogueSound();

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
        Canvas.ForceUpdateCanvases();
        panel.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    public void ShowDiologWindow()
    {
        panel.SetActive(true);
        PlayerController.CanMove = false;
    }

    public void HideDiologWindow()
    {
        ClearContent();
        panel.SetActive(false);
        PlayerController.CanMove = true;
    }


    private void ClearContent()
    {
        foreach (Transform child in contentArea)
            Destroy(child.gameObject);
        //if (child.gameObject.GetComponent<ChoiceClickHandler>() != null)

    }
    
    private void PlayDialogueSound()
    {
        if (audioSource == null || dialogueClips == null || dialogueClips.Length == 0) return;

        if (randomizePitch)
            audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        else
            audioSource.pitch = 1f;

        // Берём случайный клип из списка
        AudioClip clip = dialogueClips[Random.Range(0, dialogueClips.Length)];
        audioSource.PlayOneShot(clip);
    }
}
