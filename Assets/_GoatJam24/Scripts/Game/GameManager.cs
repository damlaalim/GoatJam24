using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using _GoatJam24.Scripts.Player;
using _GoatJam24.Scripts.Enemy;
using _GoatJam24.Scripts.MyExtensions;
using _GoatJam24.Scripts.NPCManagement;
using Cinemachine;
using TMPro;

namespace _GoatJam24.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        public bool PlanetCanMovement => !_startCanvas.enabled && !_miniGameTransform.gameObject.activeSelf;
        
        [SerializeField] private Transform _miniGameTransform;
        [SerializeField] private TextMeshProUGUI _winText, _loseText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private CinemachineVirtualCamera _cinemachine;
        [SerializeField] private GameObject _planet, _rocket;
        [SerializeField] private Canvas _startCanvas;
        
        [Inject] private PlayerController _playerController;
        [Inject] private PlayerMovement _playerMovement;
        [Inject] private EnemyManager _enemyManager;
        [Inject] private PlayerShootController _playerShootController;
        [Inject] private PlayerWorldController _playerWorldController;
        [Inject] private NPCManager _npcManager;

        private void Start()
        {
            _playerWorldController.canTeleport = false;
        }

        public void StartMiniGame()
        {
            _startCanvas.enabled = false;
            _miniGameTransform.gameObject.SetActive(true);
            _playerController.transform.position = Vector3.zero.With(z: _playerController.transform.position.z);
            _playerMovement.StartGame();
            _playerController.StartGame();
            _enemyManager.StartEnemyCreate();
            _playerShootController.StartGame();
            
            _winText.enabled = _loseText.enabled = false;
            _closeButton.gameObject.SetActive(false);
        }
        
        public void MiniGameOver(bool success)
        {
            _winText.enabled = success;
            _loseText.enabled = !success;
            _closeButton.gameObject.SetActive(true);

            _playerController.Destroy();
            _playerController.ResetHealth();
            _playerShootController.OverGame();
            _enemyManager.Reset();
            _playerMovement.OverGame();
        }
        
        public void OnClick_CloseMiniGame()
        {
            _startCanvas.enabled = true;
            _miniGameTransform.gameObject.SetActive(false);
        }
        
        public void OnClick_StartGame()
        {
            _startCanvas.enabled = false;
            _playerWorldController.canTeleport = true;
            _npcManager.StartGame();
            
            StartCoroutine(StartRoutine());
            
            IEnumerator StartRoutine()
            {
                _cinemachine.LookAt = _rocket.transform;
                _planet.GetComponent<Animator>().SetTrigger("rocket");
                yield return new WaitForSeconds(5.3f);
                _cinemachine.LookAt = _planet.transform;

                yield return new WaitForSeconds(.3f);
                
                _playerWorldController.StartGame();
            }
        }
    }
}