using System.Collections;
using UnityEngine;
using Zenject;
using _GoatJam24.Scripts.Player;
using _GoatJam24.Scripts.Enemy;
using _GoatJam24.Scripts.MyExtensions;

namespace _GoatJam24.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform _miniGameTransform;
        
        [Inject] private PlayerController _playerController;
        [Inject] private PlayerMovement _playerMovement;
        [Inject] private EnemyManager _enemyManager;

        public void StartMiniGame()
        {
            _miniGameTransform.gameObject.SetActive(true);
            _playerController.transform.position = Vector3.zero.With(z: _playerController.transform.position.z);
            _playerMovement.StartGame();
            _enemyManager.StartEnemyCreate();
        }
        
        public void MiniGameOver()
        {
            _miniGameTransform.gameObject.SetActive(false);    
            _playerController.ResetHealth();
            _enemyManager.Reset();
            _playerMovement.OverGame();
        }
    }
}