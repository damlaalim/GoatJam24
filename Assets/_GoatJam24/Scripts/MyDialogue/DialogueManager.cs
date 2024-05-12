using System.Collections.Generic;
using _GoatJam24.Scripts.Canvases;
using UnityEngine;
using Zenject;

namespace _GoatJam24.Scripts.MyDialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [Inject] private InGameCanvas _inGame;

        // private bool _dialogueIsContinue = false, _dialogIsOver = false;
        // private List<DialogueResponse> lastNode;
        // private int dialogCount;

        public void StartNewDialogue(DialogueData data)
        {
            // _dialogueIsContinue = true;
            // _inGame.ShowDialogue(true);
            // _inGame.ShowChoose1Button(false);
            // _inGame.ShowChoose2Button(false);
            //
            // _inGame.ChangeCharacter(data.rootNode.character);
            // _inGame.ChangeDialogue(data.rootNode.dialogue);
            //
            // dialogCount = 0;
            // lastNode = data.rootNode.nextNode;
        }

        private void Update()
        {
            // if (_dialogIsOver && Input.GetKeyDown(KeyCode.Space))
            // {
            //     _dialogueIsContinue = false;
            //     _inGame.ShowDialogue(false);
            // }
            // else if (_dialogueIsContinue && Input.GetKeyDown(KeyCode.Space))
            //     NextDialogue();
        }

        public void NextDialogue()
        {
            // if (lastNode.Count <= dialogCount)
            // {
            //     _dialogIsOver = true;
            //     _dialogueIsContinue = false;
            // }
            //
            // if (lastNode[dialogCount] is not null && lastNode[dialogCount].character.ToLower() == "choose")
            // {
            //     // burada seçim yapılacak demek
            //     
            //     _dialogueIsContinue = false;
            //     
            //     _inGame.ChangeCharacter(lastNode[dialogCount].responses.character);
            //     _inGame.ChangeDialogue(lastNode[dialogCount].responses.dialogue);
            //
            //     var choose1 = lastNode[dialogCount].responses.nextNode[0];
            //     var choose2 = lastNode[dialogCount].responses.nextNode[1];
            //     
            //     _inGame.ChangeChoosingButton1(choose1.responseText);
            //     _inGame.ChangeChoosingButton2(choose2.responseText);
            //
            //     _inGame.ShowChoose1Button(true);
            //     _inGame.ShowChoose2Button(true);
            // }
            // else if (lastNode[dialogCount] is not null && lastNode[dialogCount]?.responses.character == "")
            // {
            //     // burada cevaplanacak öğe yok, sonraki dialoga geç
            //
            //     _inGame.ChangeCharacter(lastNode[dialogCount].character);
            //     _inGame.ChangeDialogue(lastNode[dialogCount].responseText);
            //   
            //     dialogCount++;
            // }
        }

        public void ChooseButton1()
        {
            // _inGame.ChangeDialogue(lastNode[dialogCount].responses.nextNode[0].responses.dialogue);
            //
            // lastNode = lastNode[dialogCount].responses.nextNode[0].responses.nextNode;
            //
            // _inGame.ShowChoose1Button(false);
            // _inGame.ShowChoose2Button(false);
            //
            // _dialogueIsContinue = true;
            // dialogCount = 0;
            // NextDialogue();
        }

        public void ChooseButton2()
        {
            // _inGame.ChangeDialogue(lastNode[dialogCount].responses.nextNode[1].responses.dialogue);
            //
            // lastNode = lastNode[dialogCount].responses.nextNode[1].responses.nextNode;
            //
            // dialogCount = 0;
            // NextDialogue();
            // _dialogueIsContinue = true;
            //
            // _inGame.ShowChoose1Button(false);
            // _inGame.ShowChoose2Button(false);
        }
    }
}