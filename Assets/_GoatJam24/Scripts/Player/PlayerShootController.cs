using System;
using System.Collections;
using _GoatJam24.Scripts.Enemy;
using UnityEngine;
using Zenject;
using _GoatJam24.Scripts.Bullet;
using _GoatJam24.Scripts.MyExtensions;

namespace _GoatJam24.Scripts.Player
{
    public class PlayerShootController : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _shootDelay;
        
        [Inject] private EnemyManager _enemyManager;
        [Inject] private BulletManager _bulletManager;

        private Coroutine _shootRoutine;
        private Transform _targetEnemy;
        
        public void StartGame()
        {
            _shootRoutine = StartCoroutine(Shoot_Routine());
        }

        public void OverGame()
        {
            if (_shootRoutine is not null)
                StopCoroutine(_shootRoutine);
        }

        private void Update()
        {
            if (!_enemyManager)
                return;
            
            _targetEnemy = _enemyManager.GetNearestEnemy(transform);
            
            if (!_targetEnemy)
                return;
            
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _targetEnemy.position - transform.position);
        }

        private IEnumerator Shoot_Routine()
        {
            while (true)
            {
                if (!_targetEnemy)
                {
                    yield return 0;
                    continue;
                }

                if (Physics2D.Raycast(transform.position, _targetEnemy.position - transform.position, Vector2.Distance(transform.position, _targetEnemy.position)))
                    Debug.DrawRay(transform.position, _targetEnemy.position - transform.position, Color.cyan);

                var bullet = _bulletManager.GetBullet();
                if (bullet is null)
                {
                    yield return 0;
                    continue;
                }
                bullet.transform.position = transform.position;
                bullet.Move(_targetEnemy.transform);
                
                yield return new WaitForSeconds(_shootDelay);
            }
        }
    }
}