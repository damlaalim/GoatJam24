using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using _GoatJam24.Scripts.Game;

namespace _GoatJam24.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public bool canTakeDamage;
        
        [SerializeField] private float _health;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Transform _modelTransform;
        
        private float _currentHealth;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;

        [Inject] private GameManager _gameManager;
        [Inject] private PlayerMovement _playerMovement;

        private void Start()
        {
            canTakeDamage = true;
            _currentHealth = _health;
            _spriteRenderer = _modelTransform.GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void StartGame()
        {
            _modelTransform.gameObject.SetActive(true);
            _healthBar.gameObject.SetActive(true);
        }

        public void TakeDamage(float damage, Vector3 enemyPosition)
        {
            canTakeDamage = false;
            _currentHealth -= damage;
            _healthBar.value = _currentHealth / _health;

            var currentColor = _spriteRenderer.color;
            var targetColor = currentColor;
            targetColor.a = 0;

            _rb.velocity = new Vector2(-_rb.velocity.x, -_rb.velocity.y);
            
            _spriteRenderer.DOColor(targetColor, .3f).SetLoops(3).OnComplete(() =>
            {
                canTakeDamage = true;
                currentColor.a = 1;
                _spriteRenderer.DOColor(currentColor, .2f);
            });

            if (_currentHealth <= 0)
                _gameManager.MiniGameOver(false);

        }

        public void Destroy()
        {
            _modelTransform.DOScale(Vector3.zero, .5f).OnComplete(() =>
            {
                _modelTransform.gameObject.SetActive(false);
                _modelTransform.localScale = Vector3.one;
            });
            _healthBar.gameObject.SetActive(false);
        }

        public void ResetHealth()
        {
            _currentHealth = _health;
            _healthBar.value = 1;
        }
    }
}