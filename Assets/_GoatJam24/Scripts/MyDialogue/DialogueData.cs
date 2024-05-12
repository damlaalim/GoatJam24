using System;
using System.Collections.Generic;
using UnityEngine;

namespace _GoatJam24.Scripts.MyDialogue
{
    [CreateAssetMenu(menuName = "Create DialogueData", fileName = "DialogueData", order = 0)]
    public class DialogueData : ScriptableObject
    {
        public DialogueNode rootNode;
    }

    [Serializable]
    public class DialogueResponse
    {
        // public string character;
        // public string responseText;
        // public DialogueNode responses; 
    }

    [Serializable]
    public class DialogueNode
    {   
        // public string character;
        // public string dialogue;
        // public List<DialogueResponse> nextNode;
    }
}