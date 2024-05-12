using _GoatJam24.Scripts.MyDialogue;
using UnityEngine;
using Zenject;

namespace _GoatJam24.Scripts.NPCManagement
{
    public class NPCController : MonoBehaviour
    {
        public Transform playerTransform;
        [SerializeField] private DialogueData _dialogueData;

        [Inject] private DialogueManager _dialogueManager;
        
        public void InteractPlayer()
        {
            _dialogueManager.StartNewDialogue(_dialogueData);    
        }
    }
}