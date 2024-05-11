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
        [SerializeField] private Transform _spriteTransform, modelTransform;
        
        private EnemyManager _enemyManager;
        private float _currentHealth;
        private Coroutine _followRoutine;
        private Rigidbody2D _rb;
        private Transform _playerTransform;
        private Collider2D _collider;
        
        public void Instantiate(EnemyManager enemyManager, Transform playerTransform)
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _enemyManager = enemyManager;
            _playerTransform = playerTransform;
            
            _collider.enabled = false;
            _currentHealth = _health;
            
            _healthSlider.gameObject.SetActive(false);
            modelTransform.gameObject.SetActive(false);

            CreateEnemy();
        }

        private void CreateEnemy()
        {
            StartCoroutine(CreateRoutine());
            
            IEnumerator CreateRoutine()
            {
                _spriteTransform.gameObject.SetActive(true);
                _spriteTransform.DOScale(Vector3.one * 4f, 1.5f).SetLoops(2, LoopType.Yoyo);
                
                yield return new WaitForSeconds(3f);
                
                _spriteTransform.DOScale(Vector3.zero, .3f);

                yield return new WaitForSeconds(.3f);
                
                _spriteTransform.gameObject.SetActive(false);
                _healthSlider.gameObject.SetActive(true);
                modelTransform.gameObject.SetActive(true);
                _collider.enabled = true;
                
                var targetScale = transform.localScale;
                transform.localScale = Vector3.zero;
                transform.DOScale(targetScale, .2f);
            
                _enemyManager.createdEnemyList.Add(this);
                _enemyManager._orderEnemyList.Remove(this);
            
                FollowToPlayer();
            }
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

        public void FollowToPlayer()
        {
            _followRoutine = StartCoroutine(FollowToPlayer_Routine());
        }

        private IEnumerator FollowToPlayer_Routine()
        {
            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, Time.deltaTime * _speed);

                var scale = transform.localScale;
                if (transform.position.x < _playerTransform.position.x && scale.x < 0)
                    scale.x *= -1;
                else if (transform.position.x > _playerTransform.position.x && scale.x > 0)
                    scale.x *= -1;

                transform.localScale = scale;
                yield return 0;
            }
        }
    }
}