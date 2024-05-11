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
        
        private float _currentHealth;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;

        [Inject] private GameManager _gameManager;
        [Inject] private PlayerMovement _playerMovement;

        private void Start()
        {
            canTakeDamage = true;
            _currentHealth = _health;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void TakeDamage(float damage, Vector3 enemyPosition)
        {
            canTakeDamage = false;
            _currentHealth -= damage;
            _healthBar.value = _currentHealth / _health;

            var currentColor = _spriteRenderer.color;
            var targetColor = currentColor;
            targetColor.a = 0;

            // var targetPos = enemyPosition * -1;
            // targetPos.z = transform.position.z;
            // transform.DOMoveX(targetPos.x / 5, .3f);

            _rb.velocity = new Vector2(-_rb.velocity.x, -_rb.velocity.y);
            
            _spriteRenderer.DOColor(targetColor, .3f).SetLoops(3).OnComplete(() =>
            {
                canTakeDamage = true;
                currentColor.a = 1;
                _spriteRenderer.DOColor(currentColor, .2f);
            });

            if (_currentHealth <= 0)
                _gameManager.MiniGameOver();
        }

        public void ResetHealth()
        {
            _currentHealth = _health;
            _healthBar.value = 1;
        }
    }
}