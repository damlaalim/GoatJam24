using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using _GoatJam24.Scripts.Bullet;
using _GoatJam24.Scripts.MyExtensions;
using DG.Tweening;

namespace _GoatJam24.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public float damage;
        
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private Slider _healthSlider;
        
        private EnemyManager _enemyManager;
        private float _currentHealth;
        private Coroutine _followRoutine;
        private Rigidbody2D _rb;
        
        public void Instantiate(EnemyManager enemyManager)
        {
            _rb = GetComponent<Rigidbody2D>();
            _enemyManager = enemyManager;
            _currentHealth = _health;
            var targetScale = transform.localScale;
            transform.localScale = Vector3.zero;
            transform.DOScale(targetScale, .2f).SetEase(Ease.OutBounce);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent<BulletController>(out var bulletController))
            {
                TakeDamage(bulletController.damage);
                bulletController.Destroy();
            }
        }

        private void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            var value = _currentHealth / _health;
            _healthSlider.value = Mathf.Lerp(0, 1, value);

            transform.DOPunchPosition(Vector3.one * .15f, .2f, 1);
            transform.DOPunchScale(Vector3.one * .008f, .15f);
            if (_currentHealth <= 0)
                Destroy();
        }

        private void Destroy()
        {
            _enemyManager.createdEnemyList.Remove(this);
            Death();
            transform.DOScale(Vector3.zero, .2f).SetEase(Ease.OutBounce);
        }

        public void Death()
        {
            if (_followRoutine is not null)
                StopCoroutine(_followRoutine);
        }

        public void FollowToPlayer(Transform player)
        {
            _followRoutine = StartCoroutine(FollowToPlayer_Routine(player));
        }

        private IEnumerator FollowToPlayer_Routine(Transform player)
        {
            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * _speed);

                var scale = transform.localScale;
                if (transform.position.x < player.position.x && scale.x < 0)
                    scale.x *= -1;
                else if (transform.position.x > player.position.x && scale.x > 0)
                    scale.x *= -1;

                transform.localScale = scale;
                yield return 0;
            }
        }
    }
}