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

        private void Start()
        {
            StartCoroutine(Shoot_Routine());
        }

        private IEnumerator Shoot_Routine()
        {
            while (true)
            {
                var enemy = _enemyManager.GetNearestEnemy(transform);
                if (!enemy)
                {
                    yield return 0;
                    continue;
                }

                if (Physics2D.Raycast(transform.position, enemy.position - transform.position, Vector2.Distance(transform.position, enemy.position)))
                    Debug.DrawRay(transform.position, enemy.position - transform.position, Color.cyan);

                var bullet = Instantiate(_bullet);
                bullet.transform.position = transform.position;
                bullet.TryGetComponent<BulletController>(out var bulletController);
                bulletController.Move(enemy.transform);
                
                yield return new WaitForSeconds(_shootDelay);
            }
        }
    }
}