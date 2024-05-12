using _GoatJam24.Scripts.Game;
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
        [Inject] private GameManager _gameManager;
        
        public void InteractPlayer()
        {
            if (_taskManager.TaskNumber == 0)
                _taskManager.NextTask(1);
            if (gameObject.CompareTag("kaya"))
            {
                _taskManager.NextTask(2);
                _gameManager.nasa.SetActive(true);
            }
            if (gameObject.CompareTag("Nasa"))
                _taskManager.NextTask(3);
            // _dialogueManager.StartNewDialogue(_dialogueData);    
        }
    }
}