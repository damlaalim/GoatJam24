using _GoatJam24.Scripts.MyDialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _GoatJam24.Scripts.Canvases
{
    public class InGameCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogue;
        [SerializeField] private TextMeshProUGUI _characterText;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private TextMeshProUGUI _choose1Text;
        [SerializeField] private TextMeshProUGUI _choose2Text;
        [SerializeField] private Button _choose1Button;
        [SerializeField] private Button _choose2Button;

        // [Inject] private DialogueManager _dialogueManager;
        
        // public void ShowDialogue(bool show) => _dialogue.SetActive(show);
        // public void ShowChoose1Button(bool show) => _choose1Button.gameObject.SetActive(show);
        // public void ShowChoose2Button(bool show) => _choose2Button.gameObject.SetActive(show);
        // public void ChangeCharacter(string character) => _characterText.text = character;
        // public void ChangeDialogue(string dialogue) => _dialogueText.text = dialogue;
        //
        // public void ChangeChoosingButton1(string text)
        // {
        //     _choose1Text.text = text;
        //     ShowChoose1Button(true);
        // }
        //
        // public void ChangeChoosingButton2(string text)
        // {
        //     _choose2Text.text = text;
        //     ShowChoose2Button(true);
        // }

        // public void OnClick_DialogButton1()
        // {
        //     _dialogueManager.ChooseButton1();
        // }
        //
        // public void OnClick_DialogButton2()
        // {
        //     _dialogueManager.ChooseButton2();
        // }
    }
}