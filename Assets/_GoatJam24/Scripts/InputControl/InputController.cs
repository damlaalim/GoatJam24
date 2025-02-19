﻿using System;
using _GoatJam24.Scripts.NPCManagement;
using _GoatJam24.Scripts.Player;
using _GoatJam24.Scripts.TaskSystem;
using UnityEngine;
using Zenject;

namespace _GoatJam24.Scripts.InputControl
{
    public class InputController : MonoBehaviour
    {
        private Camera _mainCam;

        [Inject] private PlayerWorldController _playerWorldController;
        [Inject] private TaskManager _taskManager;
        
        private void Start()
        {
            _mainCam = Camera.main;    
        }

        private void Update() 
        {
            if (Input.GetMouseButtonDown(0) 
                && Physics.Raycast(_mainCam.ScreenPointToRay(Input.mousePosition), out var hit, 1000)
                )
            {
                if (hit.transform.TryGetComponent<NPCController>(out var npcController))
                {
                    _playerWorldController.Teleport(npcController.playerTransform.position);
                    npcController.InteractPlayer();
                }
                if (hit.transform.CompareTag("Nasa"))
                    _taskManager.NextTask(3);
            }
        }
    }
}