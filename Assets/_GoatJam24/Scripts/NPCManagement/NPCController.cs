using _GoatJam24.Scripts.MyDialogue;
using _GoatJam24.Scripts.TaskSystem;
using UnityEngine;
using Zenject;

namespace _GoatJam24.Scripts.NPCManagement
{
    public class NPCController : MonoBehaviour
    {
        public Transform playerTransform;
        // [SerializeField] private DialogueDatca _dialogueData;

        [Inject] private TaskManager _taskManager;
        
        public void InteractPlayer()
        {
            if (gameObject.CompareTag("Nasa"))
                _taskManager.NextTask(3);
            // _dialogueManager.StartNewDialogue(_dialogueData);    
        }
    }
}