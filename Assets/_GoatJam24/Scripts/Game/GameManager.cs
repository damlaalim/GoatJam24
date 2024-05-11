using UnityEngine.UI;
using UnityEngine;
using Zenject;
using _GoatJam24.Scripts.Player;
using _GoatJam24.Scripts.Enemy;
using _GoatJam24.Scripts.MyExtensions;
using TMPro;

namespace _GoatJam24.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform _miniGameTransform;
        [SerializeField] private TextMeshProUGUI _winText, _loseText;
        [SerializeField] private Button _closeButton;
        
        [Inject] private PlayerController _playerController;
        [Inject] private PlayerMovement _playerMovement;
        [Inject] private EnemyManager _enemyManager;
        [Inject] private PlayerShootController _playerShootController;

        public void StartMiniGame()
        {
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
            _miniGameTransform.gameObject.SetActive(false);
        }
    }
}