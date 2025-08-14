using System;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    public string nodeId;                  // Уникальный ID узла
    public string speaker;                 
    [TextArea(3, 10)] public string text;
    public DialogueChoice[] choices;
}

[Serializable]
public class DialogueChoice
{
    [TextArea(1, 5)] public string text;
    public DialogueNode nextNode;
}
