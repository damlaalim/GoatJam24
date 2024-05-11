using System;
using UnityEngine;
using System.Collections.Generic;
using Zenject;
using _GoatJam24.Scripts.Player;

namespace _GoatJam24.Scripts.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<EnemyController> _enemyList;

        [Inject] private PlayerMovement _playerMovement;
        private void Start()
        {
            foreach (var enemy in _enemyList)
            {
                enemy.FollowToPlayer(_playerMovement.transform);
            }
        }

        public Transform GetNearestEnemy(Transform player)
        {
            if (_enemyList.Count <= 0)
                return null;
            
            var nearestEnemy = _enemyList[0];
            foreach (var enemy in _enemyList)
            {
                var currentEnemyDis = Vector3.Distance(enemy.transform.position, player.position); 
                var nearestEnemyDis = Vector3.Distance(nearestEnemy.transform.position, player.position);

                if (currentEnemyDis < nearestEnemyDis)
                    nearestEnemy = enemy;
            }

            return nearestEnemy.transform;
        }
    }
}